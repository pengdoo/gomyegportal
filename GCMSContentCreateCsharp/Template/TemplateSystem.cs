using System.Diagnostics;
using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Web;
using System.Text;
using System.Xml;
using System.IO;
using MSScriptControl;
using GCMSClassLib.Content;
using GCMSClassLib.Public_Cls;
using Microsoft.VisualBasic.CompilerServices;
using System.Runtime.CompilerServices;




namespace GCMSContentCreate
{
	public class TemplateSystem
	{
		
		
		public TemplateResponse Response = new TemplateResponse();
		public GCMS GCMS=new GCMS() ;
		private bool inTemplate;
		
		/// <summary>
        //  运行内容页模板,返回结果
        //  执行脚本，核心算法
        //  本函数将执行参数scriptUri所指定的模板脚本，并将输出作为返回
        //  在模板系统第一次执行脚本时将执行这个函数，同时，在脚本内部也可以使用
        //  这个函数来加载子模板
        //  如果脚本出错，则错误信息被输出到输出中。
		/// </summary>
		/// <param name="TypeTree_ID"></param>
		/// <param name="Content_ID"></param>
		/// <param name="Template_String"></param>
		/// <returns></returns>
		public string Execute(int TypeTree_ID, int Content_ID, string Template_String)
		{
			string returnValue= "";
            //------------------------判断是否空文件------------------------
			if (Template_String.Trim() == "")
			{
				return returnValue;
			}
            //------------------------执行内容页模板时，首先初始化当前内容编号和节点编号------------------------
            GCMS = new GCMS(Content_ID, TypeTree_ID);
            //------------------------需处理模板，生成执行脚本------------------------
            Template_String = PreDeal(Template_String);
			Template_String = "function Main(d)" +"\r\n"+ Template_String;
            Template_String = Template_String + "\r\n" + "Main = Response.OutputBuffer";
            Template_String = Template_String + "\r\n" + "end function";
            //------------------------执行脚本------------------------
            ScriptControlClass objScript = new ScriptControlClass();
            objScript.Language = "VBSCRIPT";
            objScript.Timeout = -1;
			objScript.AllowUI = false;
			objScript.AddObject("Response", this.Response, false); //输出内容
            objScript.AddObject("GCMS", this.GCMS, false); //输出内容		
			objScript.AddCode(Template_String);
            object[] parameters = new object[] {"" };
			string mainFunctionName = "Main";
			try
			{
                returnValue = objScript.Run(mainFunctionName, ref parameters).ToString();
			}
			catch (Exception ex)
			{
				returnValue = ex.Message;
			}
            //------------------------清除内存，防止内存泄露------------------------
			objScript = null;
			Response.Clear();		
			return returnValue;
		}
		
        /// <summary>
        /// 运行列表页模板,返回结果
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <param name="Template_String"></param>
        /// <returns></returns>
		public string ExecuteList(int TypeTree_ID, string Template_String)
		{
			string returnValue = "";
            //------------------------判断是否空文件------------------------
			if (Template_String.Trim() == "")
			{
				return returnValue;
			}
            GCMS.ChannelID = TypeTree_ID;
            GCMS._ChannelID = TypeTree_ID; 
           
            //------------------------需处理模板，生成执行脚本-----------------------
            Template_String = PreDeal(Template_String);
			Template_String = "function Main(d)" + "\r\n" + Template_String;
			Template_String = Template_String + "\r\n" + "Main = Response.OutputBuffer";
			Template_String = Template_String + "\r\n" + "end function";
            //------------------------执行脚本-----------------------
            ScriptControlClass objScript = new ScriptControlClass();
            objScript.Language = "VBSCRIPT";
            objScript.Timeout = -1;
            objScript.AllowUI = false;
            objScript.AddObject("Response", this.Response, false); //输出内容
            objScript.AddObject("GCMS", this.GCMS, false); //输出内容
            objScript.AddCode(Template_String);
            object[] parameters = new object[] { "" };
            string mainFunctionName = "Main";
            try
            {
                returnValue = objScript.Run(mainFunctionName, ref parameters).ToString();
            }
            catch (Exception ex)
            {
                returnValue = ex.Message;
            }
            //------------------------清除内存，防止内存泄露------------------------
            objScript = null;
            Response.Clear();
            
			return returnValue;
		}
		
		//---------------------------------
        //预处理模板文件
        //模板文件中，脚本以<!--% 和 %-->括起来，非脚本部分要全部换成Response.Output 语句输出
		//---------------------------------
		private string PreDeal(string oldstr)
		{
			string returnValue;
			string newStr;
			int p;
			int p1;
			
			if (oldstr == "")
			{
				returnValue = oldstr;
				return returnValue;
			}
			
			inTemplate = false;
			newStr = "";

            //找第一个开始标记
			p1 = 1;
			// p = oldstr.IndexOf("<!--%", p1)
			// p = oldstr.IndexOf(
			p = Strings.InStr(p1, oldstr, "<!--%", 0);
			while (p != 0)
			{
                //输出p1到p间的内容
				newStr = newStr + SplitLine(PreDeal2(oldstr.Substring(p1 - 1, p - p1)).Replace("\u0022", "\"\""));
				p1 = p + 5;
				p = Strings.InStr(p1, oldstr, "%-->", 0);

                if (p == 0) //如果找不到反括号了
				{
					Information.Err().Raise(555, "", "Can not find %-->", null, null);
				}

                //将代码原样返回
				newStr = newStr + oldstr.Substring(p1 - 1, p - p1) + "\r\n";

                //找下一个开始标记
				p1 = p + 4;
				p = Strings.InStr(p1, oldstr, "<!--%", 0);
			}

            //将剩下部分输出
			newStr = newStr + SplitLine(PreDeal2(oldstr.Substring(p1 - 1)).Replace("\u0022", "\"\""));
			
			returnValue = newStr;
			
			return returnValue;
		}
		
		
		//-------------------------------------------------------------------
        //预处理之二，将<!--TemplateBegin-->与<!--TemplateEnd-->间的内容忽略掉。
        //多个调用间会有连贯性
		//-------------------------------------------------------------------
		private string PreDeal2(string oldstr)
		{
			string returnValue;
			string newStr=string.Empty;
			int p;
            int p1;
			
			if (oldstr == "")
			{
				returnValue = "";
				return returnValue;
			}

            //找第一个开始标记
			p1 = 1;
			p = Strings.InStr(p1, oldstr, "<!--Template", 0);
			while (p != 0)
			{
                //输出p1到p 的内容
				if (! inTemplate)
				{
					newStr = newStr + oldstr.Substring(p1 - 1, p - p1);
				}
				p1 = p + 12;
				p = Strings.InStr(p1, oldstr, "-->", 0);

                if (p == 0) //如果找不到反括号了
				{
					Information.Err().Raise(555, "", "Can not find -->", null, null);
				}

                //查看是进入标记还是结束标记
				if (oldstr.Substring(p1 - 1, 5) == "Begin")
				{
					inTemplate = true;
				}
				else
				{
					inTemplate = false;
				}
				p1 = p + 3;

                //找下一个开始标记
				p = Strings.InStr(p1, oldstr, "<!--Template", 0);
			}

            //剩下的内容
			if (! inTemplate)
			{
				newStr = newStr + oldstr.Substring(p1 - 1);
			}
			
			returnValue = newStr;
			return returnValue;
		}
		
		//-----------------------------------------------------------
        //将输出内容按行分隔，并输出适当的换行以保持模板文件的原样。
		//-----------------------------------------------------------
		private string SplitLine(string block)
		{
			string returnValue=string.Empty;
            block=block.Replace("\r", "");//Change By Calen
			string[] lines = block.Split('\n');
			
			long i;
			for (i = 0; i <= (lines.Length - 1); i++)
			{
				if (!String.IsNullOrEmpty(Strings.Trim(lines[i])))
				{
					returnValue = returnValue + "Response.Output \"" + lines[i] + "\"";
					if (i < (lines.Length - 1))
					{
						returnValue = returnValue + '\u0026' + " vbCrlf" + "\r\n";
					}
					else
					{
						returnValue = returnValue + "\r\n";
					}
				}
			}
			return returnValue;
		}
		
	}
	
	
	
}
