//------------------------------------------------------------------------------
// ������ʶ: Copyright (C) 2008 Gomye.com.cn ��Ȩ����
// ��������: Galen Mu ������ 2008-7-9
//
// ��������:�û������,��½��غ���
//
// ���޸�����:
// δ�޸�����:
//      1 Ȩ����صķ�װ��Type_TypeTree.csһ��,Ӧ�ö���
// �޸ļ�¼
//       1   2008-7-10 ���ע��
//       2   2008-8-26 �ı����ݷ��ʲ����÷�����д��
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
	/// Logon ��ժҪ˵����
	/// </summary>
	/// 

	public class SysLogon
    {
        #region ʵ�嶨��
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
        #endregion ʵ�嶨��

        #region �������ݿ����
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
        #endregion �������ݿ����


        // ���ҽ�ɫ
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

		// ��֤��ɫ
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

		// �鿴��ɫȨ��
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
