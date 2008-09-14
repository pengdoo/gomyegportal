//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-7-8
//
// 功能描述: 帮助类
//
// 已修改问题:
//        2  提示信息在程序内写死，多语言时需要修改
// 未修改问题:
//       1  m_txtStatus 定义写死，重构不方便
//      
//       3  QueryMaxID,UpdateMaxID,UpdateSQL,InitSQL等函数应该封装在数据操作的类中
// 修改记录
//       1   2008-7-8 添加注释
//       2   2008-8-25 修改UpdateMaxID,QueryMaxID为存储过程调用+
//           将UpdateSQL改名为DoSql
//       3   2008-8-29 删除静态消息提示Msg()方法，改用GCMS.PageCommonClassLib.Gsystem.GetSysteStateMsg()
//       4   2008-9-5 添加FileIn,FileOut函数，并修改其编码识别流程。添加默认编码
//       5   2008-9-13 添加WebtoLocalPic函数
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
    /// Tools 的摘要说明。
    /// </summary>
    public class Tools
    {
        public Tools()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region Web字符转换函数
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
        #endregion Web字符转换函数

        private static string m_txtStatus = "5";// #此处有特殊含义数字,重构时注意#
        public static string txtStatus
        {
            get { return m_txtStatus; }
            set { m_txtStatus = value; }
        }

      

        #region 数据库相关的函数

        /// <summary>
        /// 获取最大id号
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
        /// 更新指定的最大ID号
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
        /// 采用已知SQL更新用户信息
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
       
        #endregion 数据库相关的函数

        #region 文件上传相关函数
        /// 转换物理路径
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

            if (string.IsNullOrEmpty(filename))			//如果没输入上传文件，则返回
            {
                return string.Empty;
            }
            else
            {
                string fileExtension = filename.Substring(filename.LastIndexOf("."));
                int maxid = QueryMaxID("Upload_ID");
                UpdateMaxID("Upload_ID");
                TmpFile = TypeTreePictureURL + maxid + fileExtension; //改名
            }
            return TmpFile;
        }
        /// <summary>
        /// 将网站上的图片粘贴到本地
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
        #endregion 文件上传相关函数

        #region 页面生成相关函数
        
        #endregion 页面生成相关函数

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
            email.FromName = "GCMS系统邮件";
            email.ContentType = "text/html";
            email.Subject = subject;
            //email.AddAttachment("c:\\test.xml",true,"");

            email.Body = body;
            int count=0;
            foreach (KeyValuePair<string, string> adress in adresseslist)
            {
                if (!string.IsNullOrEmpty(adress.Value))//邮件不为空
                {
                    count++;
                    if (count == 1)
                        email.AddRecipient(adress.Value, adress.Key,null);
                    else
                        email.AddRecipient(adress.Value, adress.Key, null);
                }
            }
            

            email.Send(systemcls.JMail_Server, false);
            bool haserror = string.IsNullOrEmpty(email.ErrorMessage);//#待测试的修改#
            email.Close();
            return haserror;
        }


        //DataTable 数据集 20080627
        private static DataTable m_ContentList;
        public static DataTable ContentList
        {
            get { return m_ContentList; }
            set { m_ContentList = value; }
        }

        //DataTable 数据集 20080627 
        public static DataTable ContentsAll(string Sql)
        {
            
            return Tools.DoSqlTable(Sql);
        }

        
    }
}
