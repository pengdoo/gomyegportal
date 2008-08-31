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
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using GCMSClassLib.Content;
using Gomye.CommonClassLib.Data;
using System.Diagnostics;

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
            Console.WriteLine("Sql-Do:", sSQL);
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
            string TmpFile = "";

            if (filename == "")			//���û�����ϴ��ļ����򷵻�
            {
                return "";
            }
            else
            {
                string fileExtension = filename.Substring(filename.LastIndexOf("."));
                int Names = _Type_TypeTree.MakeID("Upload_ID");
                _Type_TypeTree.UpdateID(Names + 1);
                TmpFile = TypeTreePictureURL + Names + fileExtension; //����
            }
            return TmpFile;
        }

        #endregion �ļ��ϴ���غ���

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
