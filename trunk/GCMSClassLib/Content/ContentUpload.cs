//------------------------------------------------------------------------------
// ������ʶ: Copyright (C) 2008 Gomye.com.cn ��Ȩ����
// ��������: Galen Mu ������ 2008-7-9
//
// ��������: �ϴ��ļ�����
//
// ���޸�����:
// δ�޸�����:
// �޸ļ�¼
//       1   2008-7-9 ���ע��
//       2   2008-8-26 �ı����ݷ��ʲ����÷�����д��
//       3   2008-9-4 �޸�Type_TypeTree ��MarkID ������ΪTools.QueryMaxID��Type_TypeTree.UpdateIDΪ
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient ;
using GCMSClassLib.Public_Cls;

namespace GCMSClassLib.Content
{
	/// <summary>
	/// ContentUpload ��ժҪ˵����
	/// </summary>
	public class ContentUpload
    {
        #region ʵ�嶨��
        private int m_File_ID;
		public int File_ID
		{
			get { return m_File_ID;}
			set { m_File_ID=value;}
		}
		private int m_User_ID;
		public int User_ID
		{
			get { return m_User_ID;}
			set { m_User_ID=value;}
		}
		private string m_Url;
		public string Url
		{
			get { return m_Url;}
			set { m_Url=value;}
		}
		private string m_Type;
		public string Type
		{
			get { return m_Type;}
			set { m_Type=value;}
		}
		private System.DateTime m_AddDate;
		public System.DateTime AddDate
		{
			get { return m_AddDate;}
			set { m_AddDate=value;}
        }
        #endregion ʵ�嶨��

        public string  Create(int TypeTree_ID)
		{
			Type_TypeTree _Type_TypeTree = new Type_TypeTree ();
			_Type_TypeTree.Init (TypeTree_ID);

				string fileExtension = this.Url.Substring(this.Url.LastIndexOf("."));
				int maxid =Tools.QueryMaxID("Upload_ID");
                Tools.UpdateMaxID("Upload_ID");
				string TmpFile = _Type_TypeTree.TypeTreePictureURL + maxid + fileExtension; //����
				fileExtension = fileExtension.Substring (1);



            string sql="insert into Content_Upload  (" +
                        " File_ID,User_ID,Url,Type,AddDate) " +
				        " values "+
                        " (" + maxid + "," + this.User_ID + ",'" + TmpFile + "','" + fileExtension + "',getdate())";

            int reval = Tools.DoSqlRowsAffected(sql);


			return TmpFile;
		}

	}

}


