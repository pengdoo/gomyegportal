//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-7-9
//
// 功能描述:用户表管理,登陆相关函数
//
// 已修改问题:
// 未修改问题:
//      1 权限相关的封装和Type_TypeTree.cs一起,应该独立
// 修改记录
//       1   2008-7-10 添加注释
//       2   2008-8-26 改变数据访问层引用方法的写法
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient ;
using System.Collections;
using System.Data.Common;
using GCMSClassLib.Public_Cls;

namespace GCMSClassLib.Content
{
	/// <summary>
	/// Logon 的摘要说明。
	/// </summary>
	/// 

	public class SysLogon
    {
        #region 实体定义
        private int m_MasterID;
		public int MasterID
		{
			get { return m_MasterID;}
			set { m_MasterID=value;}
		}
		private String m_MasterName;
		public String MasterName
		{
			get { return m_MasterName;}
			set { m_MasterName=value;}
		}
		private String m_MasterUserName;
		public String MasterUserName
		{
			get { return m_MasterUserName;}
			set { m_MasterUserName=value;}
		}
		private String m_MasterPassword;
		public String MasterPassword
		{
			get { return m_MasterPassword;}
			set { m_MasterPassword=value;}
		}
		private String m_MasterEmail;
		public String MasterEmail
		{
			get { return m_MasterEmail;}
			set { m_MasterEmail=value;}
		}
		private String m_Roles_ID;
		public String Roles_ID
		{
			get { return m_Roles_ID;}
			set { m_Roles_ID=value;}
		}

		private String m_Roles_Name;
		public String Roles_Name
		{
			get { return m_Roles_Name;}
			set { m_Roles_Name=value;}
		}

		private String m_Popedom_EName;
		public String Popedom_EName
		{
			get { return m_Popedom_EName;}
			set { m_Popedom_EName=value;}
        }
        #endregion 实体定义

        #region 常用数据库操作
        public bool Init(String adminName ,String adminPwd)
		{
			SqlDataReader reader = null; 
			string sql="select Master_ID,Master_Name,Master_UserName,Master_Password,Master_Email,Master_Tel,Master_Usableness,Master_Note,isnull(Master_AddDate,'2000-01-01') Master_AddDate from Content_Master where Master_UserName='"+adminName+"'and Master_Password='"+adminPwd+"'";
			reader= Tools.DoSqlReader(sql);
			if(reader.Read())
			{
				this.MasterID=int.Parse(reader["Master_ID"].ToString());
				this.MasterUserName=reader["Master_UserName"].ToString();
				reader.Close();
				return true;
			}
			else
			{
				reader.Close();
				return false;
			}
        }
        #endregion 常用数据库操作


        // 查找角色
		public bool Roles(int Master_ID) 
		{
			SqlDataReader reader = null; 
			string sql="select Roles_ID from Content_RolesMaster where Master_ID="+Master_ID;
			reader= Tools.DoSqlReader(sql);
			if(reader.Read())
			{
				this.Roles_ID=reader["Roles_ID"].ToString();
				reader.Close();
				return true;
			}
			else
			{
				reader.Close();
				return false;
			}
		}

		// 验证角色
		public bool IsRoles(int Roles_Id) 
		{
			SqlDataReader reader = null; 
			string sql="select Roles_Name from Content_Roles where Roles_Id="+Roles_Id;
			reader= Tools.DoSqlReader(sql);
			if(reader.Read())
			{
				this.Roles_Name=reader["Roles_Name"].ToString();
				reader.Close();
				return true;
			}
			else
			{
				reader.Close();
				return false;
			}
		}

		// 查看角色权限
		public bool RolesPopedom(int Roles_id) 
		{
			SqlDataReader reader = null; 
			string sql="select Popedom_EName from Content_RolesPopedom where Roles_id="+Roles_id;
			reader= Tools.DoSqlReader(sql);
			//if(reader.Read())
			while(reader.Read())
			{
				//this.Popedom_EName=reader["Popedom_EName"].ToString();
				this.Popedom_EName = this.Popedom_EName + " " + reader.GetString(0);

			}
			//else
			//{
			//	reader.Close();
			//	data.Close();
//				data.Dispose();
			reader.Close();
			return true;
			///	return false;
//			}
		}
	}
}
