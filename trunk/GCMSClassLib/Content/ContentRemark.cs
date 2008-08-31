//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-7-9
//
// 功能描述:
//
// 已修改问题:
// 未修改问题:
// 修改记录
//       1   2008-7-9 添加注释
//       2   2008-8-26 改变数据访问层引用方法的写法
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient ;
using System.Collections;
using System.Data.Common;
using GCMSClassLib.Public_Cls;
using System.Text;
using System.IO;

namespace GCMSClassLib.Content
{
	/// <summary>
	/// ContentRemark 的摘要说明。
	/// </summary>
	public class ContentRemark
	{

			private int m_Remark_ID;
			public int Remark_ID
			{
				get { return m_Remark_ID;}
				set { m_Remark_ID=value;}
			}
			private int m_Content_ID;
			public int Content_ID
			{
				get { return m_Content_ID;}
				set { m_Content_ID=value;}
			}
			private String m_Remark_Name;
			public String Remark_Name
			{
				get { return m_Remark_Name;}
				set { m_Remark_Name=value;}
			}
			private String m_Remark;
			public String Remark
			{
				get { return m_Remark;}
				set { m_Remark=value;}
			}
			private System.DateTime m_Remark_Date;
			public System.DateTime Remark_Date
			{
				get { return m_Remark_Date;}
				set { m_Remark_Date=value;}
			}

			private int m_Status;
			public int Status
			{
				get { return m_Status;}
				set { m_Status=value;}
			}
			private String m_Author;
			public String Author
			{
				get { return m_Author;}
				set { m_Author=value;}
			}
			private int m_User_ID;
			public int User_ID
			{
				get { return m_User_ID;}
				set { m_User_ID=value;}
			}

		private string m_Album;
		public string Album
		{
			get { return m_Album;}
			set { m_Album=value;}
		}		


		public bool Create( )
		{
			int max_id=this.QueryMaxContentID();
			string sql="insert into Content_ContentRemark  (" +
				" Remark_ID,Content_ID,Remark_Name,Remark,Remark_Date,Status,Author,User_ID,Album) " +
				" values (" + max_id + "," + this.Content_ID + ",'" + Tools.WebToDB(this.Remark_Name) + "','" + this.Remark + "','"+this.Remark_Date+"'," + this.Status + ",'"+this.Author+"',"+this.User_ID+",'"+this.Album+"')";

            return Tools.DoSql(sql);
		}

		public bool Update(int Remark_ID)
		{
			string sql= "update Content_ContentRemark set  " + 
				//"TypeTree_ID="  + this.TypeTree_ID + ", " + 
				"Remark_Name='"  + Tools.WebToDB(this.Remark_Name) + "', " + 
				"Remark='"  + Tools.WebToDB(this.Remark) + "'  " + 

				"where Remark_ID="  + Remark_ID + " ";

            return Tools.DoSql(sql);

		}

		public int QueryMaxContentID( )
		{
            return Tools.QueryMaxID("Remark_ID"); 
		}

		public bool UpdateMaxContentID( )
		{
            return Tools.UpdateMaxID("Remark_ID");
		}

		public bool Init(int Remark_ID)
		{
			SqlDataReader reader = null;
			string sql=" select Remark_ID,Content_ID,Remark_Name,Author,Remark,Remark_Date,Status,isnull(User_ID,'0') User_ID from Content_ContentRemark where Remark_ID=" +Remark_ID;
			reader= Tools.DoSqlReader(sql);
			if(reader.Read())
			{
				this.Remark_ID=int.Parse(reader["Remark_ID"].ToString());
				this.Content_ID=int.Parse(reader["Content_ID"].ToString());
				this.Remark_Name=Tools.DBToWeb(reader["Remark_Name"].ToString());
				this.Author=reader["Author"].ToString();
				this.Remark_Date=System.DateTime.Parse(reader["Remark_Date"].ToString());

				this.Status=int.Parse(reader["Status"].ToString());
				this.Remark=Tools.DBToWeb(reader["Remark"].ToString());
				this.User_ID=int.Parse(reader["User_ID"].ToString());

				reader.Close();
				return true;
			}
			else
			{
				reader.Close();
				return false;
			}
		}
	}
}
