//------------------------------------------------------------------------------
// ������ʶ: Copyright (C) 2008 Gomye.com.cn ��Ȩ����
// ��������: Galen Mu ������ 2008-7-8
//
// ��������: ������
//
// ���޸�����:
//        2  ��ʾ��Ϣ�ڳ�����д����������ʱ��Ҫ�޸�
// δ�޸�����:
//       1  m_txtStatus ����д�����ع�������
//      
//       3  QueryMaxID,UpdateMaxID,UpdateSQL,InitSQL�Ⱥ���Ӧ�÷�װ�����ݲ���������
// �޸ļ�¼
//       1   2008-7-8 ���ע��
//       2   2008-8-25 �޸�UpdateMaxID,QueryMaxIDΪ�洢���̵���+
//           ��UpdateSQL����ΪDoSql
//       3   2008-8-29 ɾ����̬��Ϣ��ʾMsg()����������GCMS.PageCommonClassLib.Gsystem.GetSysteStateMsg()
//       4   2008-9-5 ���FileIn,FileOut���������޸������ʶ�����̡����Ĭ�ϱ���
//       5   2008-9-13 ���WebtoLocalPic����
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Net;
using jmail;
using GCMSClassLib.Content;
using Gomye.CommonClassLib.Data;
namespace GCMSClassLib.Public_Cls
{
    /// <summary>
    /// Tools ��ժҪ˵����
    /// </summary>
    public class Tools
    {
        public Tools()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region Web�ַ�ת������
        public static string WebToDB(string str)
        {
            string showStr = string.Empty;
            if (str != null)
            {
                showStr = str.Replace("&", "&amp;");
                showStr = showStr.Replace("\"", "&#34;");
                showStr = showStr.Replace("'", "&#39;");
            }
            return showStr;
        }

        public static string DBToWeb(string str)
        {
            string showStr = string.Empty;
            if (str != null)
            {
                showStr = str.Replace("&#34;", "\"");
                showStr = showStr.Replace("&#39;", "'");
                showStr = showStr.Replace("&amp;", "&");
            }
            return showStr;
        }
        #endregion Web�ַ�ת������

        private static string m_txtStatus = "5";// #�˴������⺬������,�ع�ʱע��#
        public static string txtStatus
        {
            get { return m_txtStatus; }
            set { m_txtStatus = value; }
        }

      

        #region ���ݿ���صĺ���

        /// <summary>
        /// ��ȡ���id��
        /// </summary>
        /// <returns></returns>
        public static int QueryMaxID(string sIDName)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Filedname", SqlDbType.NVarChar,100)
            };
            parameters[0].Value = sIDName;

            int maxid = 0;
            maxid = SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "p_Content_ID_GetMaxId", parameters, out rowsAffected);

            return maxid;
        }

        /// <summary>
        /// ����ָ�������ID��
        /// </summary>
        /// <param name="sIDName"></param>
        /// <returns></returns>
        public static bool UpdateMaxID(string sIDName)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Filedname", SqlDbType.NVarChar,100)

            };
            parameters[0].Value = sIDName;

            int res= SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "p_Content_ID_UpdateMaxId", parameters, out rowsAffected);

            if (rowsAffected == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ������֪SQL�����û���Ϣ
        /// </summary>
        /// <param name="sSQL"></param>
        /// <returns></returns>
        public static bool DoSql(string sSQL)
        {
            Debug.WriteLine("Sql-Do:", sSQL);
            int reval =SqlHelper.ExecuteSql(SqlHelper.LocalSqlServer,sSQL);
            if (reval == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static SqlDataReader DoSqlReader(string sSQL)
        {

            Debug.WriteLine("Sql-Reader:" + sSQL);
           return SqlHelper.ExecuteReader(SqlHelper.LocalSqlServer, sSQL);
         
        }
        public static int DoSqlRowsAffected(string sSQL)
        {
            Debug.WriteLine("Sql-RowsAffected:" + sSQL);
            return SqlHelper.ExecuteSql(SqlHelper.LocalSqlServer, sSQL);

        }
        public static DataTable DoSqlTable(string sSQL)
        {
            Debug.WriteLine("Sql-DataTable:"+ sSQL);
            return SqlHelper.ReaderToTable(SqlHelper.ExecuteReader(SqlHelper.LocalSqlServer, sSQL));

        }
        public static void DoSqlTrans(List<string> Sqls)
        {
            SqlHelper.ExecuteSqlTran(SqlHelper.LocalSqlServer, Sqls);
        }
       
        #endregion ���ݿ���صĺ���

        #region �ļ��ϴ���غ���
        /// ת������·��
        public static string FilesUrl(string _FilesUrl)
        {
            _FilesUrl = _FilesUrl.Replace("\\", "/");
            string folder = _FilesUrl.Substring(0, _FilesUrl.LastIndexOf('/') + 1);
            string filename = _FilesUrl.Substring( _FilesUrl.LastIndexOf('/') +1);
            _FilesUrl = System.Web.HttpContext.Current.Server.MapPath(folder)+filename;
            return _FilesUrl;
        }
        public static string UploadName(string filename, string TypeTreePictureURL)
        {
            Type_TypeTree _Type_TypeTree = new Type_TypeTree();
            string TmpFile = string.Empty;

            if (string.IsNullOrEmpty(filename))			//���û�����ϴ��ļ����򷵻�
            {
                return string.Empty;
            }
            else
            {
                string fileExtension = filename.Substring(filename.LastIndexOf("."));
                int maxid = QueryMaxID("Upload_ID");
                UpdateMaxID("Upload_ID");
                TmpFile = TypeTreePictureURL + maxid + fileExtension; //����
            }
            return TmpFile;
        }
        /// <summary>
        /// ����վ�ϵ�ͼƬճ��������
        /// </summary>
        /// <param name="Contents"></param>
        /// <returns></returns>
        public static string WebtoLocalPic(string Contents, string PictureURL)
        {
            string NewContents = Contents;
            int p1 = NewContents.IndexOf("<IMG", 0);

            if (p1 < 0) { return Contents; };
            do
            {
                p1 = NewContents.IndexOf("src", p1);
                int p2 = NewContents.IndexOf("\"", p1);
                p2 = p2 + 1;
                int p3 = NewContents.IndexOf("\"", p2);

                string ContentSub = NewContents.Substring(p2, p3 - p2);
                int httpint = ContentSub.IndexOf("ttp://");

                if (httpint > 0)
                {
                    string filename = ContentSub.Substring(ContentSub.LastIndexOf("/"));
                    string _file = Tools.UploadName(filename, PictureURL);

                    WebClient wc = new WebClient();
                    wc.DownloadFile(ContentSub, System.Web.HttpContext.Current.Server.MapPath(_file).Replace("\\", "\\\\"));
                    Contents = Contents.Replace(ContentSub, _file);
                }

                NewContents = NewContents.Substring(p3);
                p1 = NewContents.IndexOf("<IMG", 1);

            }
            while (p1 > 0);
            return Contents;
        }
        #endregion �ļ��ϴ���غ���

        #region ҳ��������غ���
        
        #endregion ҳ��������غ���

        public static bool SendEmail(string subject, string body,Dictionary<string,string> adresseslist )
        {
            SystemCls.SystemCls systemcls = new SystemCls.SystemCls();
            systemcls.Init();
            MessageClass email = new MessageClass();
            email.Logging = true;
            email.Silent = true;
            email.Charset = "GB2312";
            email.MailServerUserName = systemcls.JMail_MailServerUserName;
            email.MailServerPassWord = systemcls.JMail_MailServerPassWord;
            email.From = systemcls.JMail_From;
            email.FromName = "GCMSϵͳ�ʼ�";
            email.ContentType = "text/html";
            email.Subject = subject;
            //email.AddAttachment("c:\\test.xml",true,"");

            email.Body = body;
            int count=0;
            foreach (KeyValuePair<string, string> adress in adresseslist)
            {
                if (!string.IsNullOrEmpty(adress.Value))//�ʼ���Ϊ��
                {
                    count++;
                    if (count == 1)
                        email.AddRecipient(adress.Value, adress.Key,null);
                    else
                        email.AddRecipient(adress.Value, adress.Key, null);
                }
            }
            

            email.Send(systemcls.JMail_Server, false);
            bool haserror = string.IsNullOrEmpty(email.ErrorMessage);//#�����Ե��޸�#
            email.Close();
            return haserror;
        }


        //DataTable ���ݼ� 20080627
        private static DataTable m_ContentList;
        public static DataTable ContentList
        {
            get { return m_ContentList; }
            set { m_ContentList = value; }
        }

        //DataTable ���ݼ� 20080627 
        public static DataTable ContentsAll(string Sql)
        {
            
            return Tools.DoSqlTable(Sql);
        }

        
    }
}
