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
		public GCMS GCMS = new GCMS();
		private bool inTemplate;
		
        //-------------------------------------------------
        //ִ�нű��������㷨
        //��������ִ�в���scriptUri��ָ����ģ��ű������������Ϊ����
        //��ģ��ϵͳ��һ��ִ�нű�ʱ��ִ�����������ͬʱ���ڽű��ڲ�Ҳ����ʹ��
        //���������������ģ��
        //����ű������������Ϣ�����������С�
		//-------------------------------------------------
		
		public string Execute(int TypeTree_ID, int Content_ID, string Template_String)
		{
			string returnValue;
			//Public Function Execute(ByVal Content_ID As Int32, ByVal Template_String As String) As String
			//On Error Resume Next
			
			returnValue = "";
            //�ж��Ƿ���ļ�
			if (Template_String.Trim() == "")
			{
				return returnValue;
			}
			GCMS.ContentID = Content_ID;
			GCMS._ContentID = Content_ID;
			GCMS.ChannelID = TypeTree_ID;
			GCMS._ChannelID = TypeTree_ID;
			//string Item;
            GCMS.UserWhere = PreDealUserWhere( Template_String);//Change By Galen 2008.9.1
            Template_String = PreDeal(Template_String);
            //Template_String = Template_String.Replace("\r\n", "");
			Template_String = "function Main(d)" +"\r\n"+ Template_String;
            Template_String = Template_String + "\r\n" + "Main = Response.OutputBuffer";
            Template_String = Template_String + "\r\n" + "end function";


            ScriptControlClass objScript = new ScriptControlClass();
            objScript.Language = "VBSCRIPT";
            objScript.Timeout = -1;

			objScript.AllowUI = false;
			objScript.AddObject("Response", this.Response, false); //������ݮ�
            objScript.AddObject("GCMS", this.GCMS, false); //������ݮ�
			
			objScript.AddCode(Template_String);
			
			//int sParams;
            object[] parameters = new object[] {"" };
			string mainFunctionName = "Main";
			
			try
			{
                //object[] tep = new object[] { RuntimeHelpers.GetObjectValue(parameters) };
                //returnValue = StringType.FromObject(objScript.Run(StringType.FromObject(mainFunctionName), ref tep));
                returnValue = objScript.Run(mainFunctionName, ref parameters).ToString();
			}
			catch (Exception ex)
			{
				returnValue = ex.Message;
			}
			
			//Execute = Template_String
			objScript = null;
			Response.Clear();
			
			return returnValue;
		}

        /// <summary>
        /// �����û�ɸѡ����
        /// </summary>
        /// <param name="Template_String"></param>
        /// <returns></returns>
        private string PreDealUserWhere( string Template_String)
        {
            string returnValue=string .Empty;

            int pbg, ped;
            if (String.IsNullOrEmpty(Template_String))
            {
                returnValue = ""; 
                return returnValue;
            }
            string bgStr="<!--%SelectSetBegin";
            string edStr="SelectSetend%-->";
            //�ҵ�һ����ʼ���
            pbg = Strings.InStr(1, Template_String, bgStr, 0);
            ped = Strings.InStr(1, Template_String, edStr, 0);
            if (ped == 0) //����Ҳ�����������
            {
                Information.Err().Raise(555, "", "Can not find SelectSetend%-->", null, null);
                return "";
            }
            //ɾ��ģ���ж����
            Template_String.Replace(Template_String.Substring(pbg - 1, ped - pbg),"");
            //��ȡ���û����õ��ַ���
            try
            {
                string orgSetString = Template_String.Substring(pbg - 1, ped - pbg).Replace(bgStr, "").Replace(edStr, "");
                string[]sets=orgSetString.Split(',');
                string sql=string.Empty;
                foreach (string set in sets)
                {
                    string setStr = set.TrimStart('[').TrimEnd(']');
                    string[] paramStrs = setStr.Split(':');
                    string colsName = string.Format("[{0}]", paramStrs[0].Trim());

                    string opStr = paramStrs[1].Trim().Substring(0, 1);
                    switch (opStr)
                    {
                        case ">":
                            opStr = ">";
                            break;
                        case "<":
                            opStr = "<";
                            break;
                        case "=":
                            opStr = "=";
                            break;
                        case "!":
                            opStr = "<>";
                            break;
                        default:
                            opStr = "=";
                            break;
                    }

                    string selectValue = paramStrs[1].Trim().Substring(1);
                    //��ע��
                    string forbidenStrs = "|and|exec|insert|select|delete|update|count|*|%|chr|mid|master|truncate|char|declare| ";
                    foreach (string fs in forbidenStrs.Split('|'))
                    {
                        selectValue = selectValue.Replace(fs, "");
                    }
                    foreach(string sv in selectValue.Split('$'))
                    {
                        sql += string.Format("{0} {1} {2} or  ", colsName, opStr, selectValue);
                    }
                    sql+="'1'='2'";//���ı��ʽ��ƴ�� or�� 
                    returnValue+=string.Format("({0}) and ",sql);
                }
            }
            catch(Exception exps)
            {   
                Information.Err().Raise(555, "", "Select Setting Format Error:"+exps.Message+"!", null, null);
                return "";
            }
            returnValue+="'1'='1'";//����ı��ʽ��ƴ�� and �� 

            returnValue = string.Format(" {0} ", returnValue);
            return returnValue;
        }
		
		public string ExecuteList(int TypeTree_ID, string Template_String)
		{
			string returnValue;
			
			returnValue = "";
			//�ж��Ƿ���ļ�
			if (Template_String.Trim() == "")
			{
				return returnValue;
			}
			GCMS.ChannelID = TypeTree_ID;
			GCMS._ChannelID = TypeTree_ID;
			//string Item;
            GCMS.UserWhere = PreDealUserWhere( Template_String);//Change By Galen 2008.9.1
			Template_String = PreDeal(Template_String);
			
			Template_String = "function Main(d)" + "\r\n" + Template_String;
			Template_String = Template_String + "\r\n" + "Main = Response.OutputBuffer";
			Template_String = Template_String + "\r\n" + "end function";


            ScriptControlClass objScript = new ScriptControlClass();
            
			objScript.Language = "VBSCRIPT";
			objScript.Timeout = -1;
			objScript.AllowUI = false;
			objScript.AddObject("Response", this.Response, false); //������ݮ�
            objScript.AddObject("GCMS", this.GCMS, false); //������ݮ�
			
			objScript.AddCode(Template_String);
			
			//int sParams;
            object[] parameters = new object[] {""};
			string mainFunctionName = "Main";
			
			
			try
			{
                //object[] tep = new object[] { RuntimeHelpers.GetObjectValue(parameters) };
                //returnValue = StringType.FromObject(objScript.Run(StringType.FromObject(mainFunctionName), ref tep));
				returnValue = objScript.Run(mainFunctionName, ref parameters).ToString();
			}
			catch (Exception ex)
			{
				returnValue = ex.Message;
			}
			
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
