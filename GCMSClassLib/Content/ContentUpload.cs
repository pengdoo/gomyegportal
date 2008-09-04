//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-7-9
//
// 功能描述: 上传文件管理
//
// 已修改问题:
// 未修改问题:
// 修改记录
//       1   2008-7-9 添加注释
//       2   2008-8-26 改变数据访问层引用方法的写法
//       3   2008-9-4 修改Type_TypeTree 中MarkID 的引用为Tools.QueryMaxID，Type_TypeTree.UpdateID为
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient ;
using GCMSClassLib.Public_Cls;

namespace GCMSClassLib.Content
{
	/// <summary>
	/// ContentUpload 的摘要说明。
	/// </summary>
	public class ContentUpload
    {
        #region 实体定义
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
        #endregion 实体定义

        public string  Create(int TypeTree_ID)
		{
			Type_TypeTree _Type_TypeTree = new Type_TypeTree ();
			_Type_TypeTree.Init (TypeTree_ID);

				string fileExtension = this.Url.Substring(this.Url.LastIndexOf("."));
				int maxid =Tools.QueryMaxID("Upload_ID");
                Tools.UpdateMaxID("Upload_ID");
				string TmpFile = _Type_TypeTree.TypeTreePictureURL + maxid + fileExtension; //改名
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


