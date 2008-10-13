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
        //  ��������ҳģ��,���ؽ��
        //  ִ�нű��������㷨
        //  ��������ִ�в���scriptUri��ָ����ģ��ű������������Ϊ����
        //  ��ģ��ϵͳ��һ��ִ�нű�ʱ��ִ�����������ͬʱ���ڽű��ڲ�Ҳ����ʹ��
        //  ���������������ģ��
        //  ����ű������������Ϣ�����������С�
		/// </summary>
		/// <param name="TypeTree_ID"></param>
		/// <param name="Content_ID"></param>
		/// <param name="Template_String"></param>
		/// <returns></returns>
		public string Execute(int TypeTree_ID, int Content_ID, string Template_String)
		{
			string returnValue= "";
            //------------------------�ж��Ƿ���ļ�------------------------
			if (Template_String.Trim() == "")
			{
				return returnValue;
			}
            //------------------------ִ������ҳģ��ʱ�����ȳ�ʼ����ǰ���ݱ�źͽڵ���------------------------
            GCMS = new GCMS(Content_ID, TypeTree_ID);
            //------------------------�账��ģ�壬����ִ�нű�------------------------
            Template_String = PreDeal(Template_String);
			Template_String = "function Main(d)" +"\r\n"+ Template_String;
            Template_String = Template_String + "\r\n" + "Main = Response.OutputBuffer";
            Template_String = Template_String + "\r\n" + "end function";
            //------------------------ִ�нű�------------------------
            ScriptControlClass objScript = new ScriptControlClass();
            objScript.Language = "VBSCRIPT";
            objScript.Timeout = -1;
			objScript.AllowUI = false;
			objScript.AddObject("Response", this.Response, false); //������ݮ�
            objScript.AddObject("GCMS", this.GCMS, false); //�������		
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
            //------------------------����ڴ棬��ֹ�ڴ�й¶------------------------
			objScript = null;
			Response.Clear();		
			return returnValue;
		}
		
        /// <summary>
        /// �����б�ҳģ��,���ؽ��
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <param name="Template_String"></param>
        /// <returns></returns>
		public string ExecuteList(int TypeTree_ID, string Template_String)
		{
			string returnValue = "";
            //------------------------�ж��Ƿ���ļ�------------------------
			if (Template_String.Trim() == "")
			{
				return returnValue;
			}
            GCMS.ChannelID = TypeTree_ID;
            GCMS._ChannelID = TypeTree_ID; 
           
            //------------------------�账��ģ�壬����ִ�нű�-----------------------
            Template_String = PreDeal(Template_String);
			Template_String = "function Main(d)" + "\r\n" + Template_String;
			Template_String = Template_String + "\r\n" + "Main = Response.OutputBuffer";
			Template_String = Template_String + "\r\n" + "end function";
            //------------------------ִ�нű�-----------------------
            ScriptControlClass objScript = new ScriptControlClass();
            objScript.Language = "VBSCRIPT";
            objScript.Timeout = -1;
            objScript.AllowUI = false;
            objScript.AddObject("Response", this.Response, false); //������ݮ�
            objScript.AddObject("GCMS", this.GCMS, false); //������ݮ�
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
            //------------------------����ڴ棬��ֹ�ڴ�й¶------------------------
            objScript = null;
            Response.Clear();
            
			return returnValue;
		}
		
		//---------------------------------
        //Ԥ����ģ���ļ�
        //ģ���ļ��У��ű���<!--% �� %-->���������ǽű�����Ҫȫ������Response.Output ������
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

            //�ҵ�һ����ʼ���
			p1 = 1;
			// p = oldstr.IndexOf("<!--%", p1)
			// p = oldstr.IndexOf(
			p = Strings.InStr(p1, oldstr, "<!--%", 0);
			while (p != 0)
			{
                //���p1��p�������
				newStr = newStr + SplitLine(PreDeal2(oldstr.Substring(p1 - 1, p - p1)).Replace("\u0022", "\"\""));
				p1 = p + 5;
				p = Strings.InStr(p1, oldstr, "%-->", 0);

                if (p == 0) //����Ҳ�����������
				{
					Information.Err().Raise(555, "", "Can not find %-->", null, null);
				}

                //������ԭ������
				newStr = newStr + oldstr.Substring(p1 - 1, p - p1) + "\r\n";

                //����һ����ʼ���
				p1 = p + 4;
				p = Strings.InStr(p1, oldstr, "<!--%", 0);
			}

            //��ʣ�²������
			newStr = newStr + SplitLine(PreDeal2(oldstr.Substring(p1 - 1)).Replace("\u0022", "\"\""));
			
			returnValue = newStr;
			
			return returnValue;
		}
		
		
		//-------------------------------------------------------------------
        //Ԥ����֮������<!--TemplateBegin-->��<!--TemplateEnd-->������ݺ��Ե���
        //������ü����������
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

            //�ҵ�һ����ʼ���
			p1 = 1;
			p = Strings.InStr(p1, oldstr, "<!--Template", 0);
			while (p != 0)
			{
                //���p1��p ������
				if (! inTemplate)
				{
					newStr = newStr + oldstr.Substring(p1 - 1, p - p1);
				}
				p1 = p + 12;
				p = Strings.InStr(p1, oldstr, "-->", 0);

                if (p == 0) //����Ҳ�����������
				{
					Information.Err().Raise(555, "", "Can not find -->", null, null);
				}

                //�鿴�ǽ����ǻ��ǽ������
				if (oldstr.Substring(p1 - 1, 5) == "Begin")
				{
					inTemplate = true;
				}
				else
				{
					inTemplate = false;
				}
				p1 = p + 3;

                //����һ����ʼ���
				p = Strings.InStr(p1, oldstr, "<!--Template", 0);
			}

            //ʣ�µ�����
			if (! inTemplate)
			{
				newStr = newStr + oldstr.Substring(p1 - 1);
			}
			
			returnValue = newStr;
			return returnValue;
		}
		
		//-----------------------------------------------------------
        //��������ݰ��зָ���������ʵ��Ļ����Ա���ģ���ļ���ԭ����
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
