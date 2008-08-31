
//------------------------------------------------------------------------------
// ������ʶ: Copyright (C) 2008 Gomye.com.cn ��Ȩ����
// ��������: Galen Mu ������ 2008-7-9
//
// ��������:�û��ͽ�ɫ�����
//
// ���޸�����:
// δ�޸�����:
//       1   ��User.cs��Roles�󲿷ֱ����ظ�
// �޸ļ�¼
//       1   2008-7-10 ���ע��
//       2   2008-8-26 �������������¡�ɾ������������Tools������װ
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient ;
using System.Collections;
using System.Data.Common;
using GCMSClassLib.Public_Cls;
using System.Text;
using System.IO;

namespace GCMSClassLib.Member
{
	/// <summary>
	/// Content ��ժҪ˵����
	/// </summary>
	/// <summary>
	///
	/// </summary>
	public class MemberCls
	{

        #region �û���ɫʵ�嶨��
        private int m_RoleID;
		public int RoleID
		{
			get { return m_RoleID;}
			set { m_RoleID=value;}
		}

		private String m_Member_Roles_Name;
		public String Member_Roles_Name
		{
			get { return m_Member_Roles_Name;}
			set { m_Member_Roles_Name=value;}
		}
		
		private String m_Description;
		public String Description
		{
			get { return m_Description;}
			set { m_Description=value;}
        }
        #endregion �û���ɫʵ�嶨��

        #region �û���ɫ�������ݿ����
        // ����
		public bool Create_Member_Roles( )
		{
			
			string sql="insert into Member_Roles (Name,Description) values ('"+ this.Member_Roles_Name+"','"+ this.Description +"')";
            return Tools.DoSql(sql);	
		}

        // ����
		public bool Update_Member_Roles(int ContentId)
		{
			string sql= "update Member_Roles set  " + 
				"Name='"  + this.Member_Roles_Name + "', " + 
				"Description='"  + this.Description + "' " + 
				"where RoleID="  + ContentId + " ";
            return Tools.DoSql(sql);
			
		}

        // ��ʼ��
		public bool Init_Member_Roles(int RoleID)
		{
			SqlDataReader reader = null;
			string	sql="select RoleID,Name,Description from Member_Roles where RoleID=" +RoleID;
			
			reader=Tools.DoSqlReader(sql);
			if(reader.Read())
			{
				this.RoleID				=int.Parse(reader["RoleID"].ToString());
				this.Member_Roles_Name	=reader["Name"].ToString();
				this.Description		=reader["Description"].ToString();
				reader.Close();
				return true;
			}
			else
			{
				reader.Close();
				return false;
			}
		}

        // ɾ��
		public bool Delete_Member_Roles(int RoleID)
		{
			return  Tools.DoSql("delete from Member_Roles where RoleID=" +RoleID );
        }
        #endregion �û���ɫ�������ݿ����

        #region �û���Ϣʵ�嶨��
        private int m_UserID;
		public int UserID
		{
			get { return m_UserID;}
			set { m_UserID=value;}
		}

		private String m_UserName;
		public String UserName
		{
			get { return m_UserName;}
			set { m_UserName=value;}
		}

		private String m_Password;
		public String Password
		{
			get { return m_Password;}
			set { m_Password=value;}
		}			

		private int m_PasswordFormat;
		public int PasswordFormat
		{
			get { return m_PasswordFormat;}
			set { m_PasswordFormat=value;}
		}
		private String m_Salt;
		public String Salt
		{
			get { return m_Salt;}
			set { m_Salt=value;}
		}
		private String m_PasswordQuestion;
		public String PasswordQuestion
		{
			get { return m_PasswordQuestion;}
			set { m_PasswordQuestion=value;}
		}	
				
		private String m_PasswordAnswer;
		public String PasswordAnswer
		{
			get { return m_PasswordAnswer;}
			set { m_PasswordAnswer=value;}
		}		
		private String m_Email;
		public String Email
		{
			get { return m_Email;}
			set { m_Email=value;}
		}	

		private System.DateTime m_Dispose_Gdate;
		public System.DateTime Dispose_Gdate
		{
			get { return m_Dispose_Gdate;}
			set { m_Dispose_Gdate=value;}
		}
		private System.DateTime m_DateCreated;
		public System.DateTime DateCreated
		{
			get { return m_DateCreated;}
			set { m_DateCreated=value;}
		}
		private System.DateTime m_LastLogin;
		public System.DateTime LastLogin
		{
			get { return m_LastLogin;}
			set { m_LastLogin=value;}
		}
		private System.DateTime m_LastActivity;
		public System.DateTime LastActivity
		{
			get { return m_LastActivity;}
			set { m_LastActivity=value;}
		}

		private String m_LastAction;
		public String LastAction
		{
			get { return m_LastAction;}
			set { m_LastAction=value;}
		}
		
		private int m_UserAccountStatus;
		public int UserAccountStatus
		{
			get { return m_UserAccountStatus;}
			set { m_UserAccountStatus=value;}
		}		

		private int m_IsAnonymous;
		public int IsAnonymous
		{
			get { return m_IsAnonymous;}
			set { m_IsAnonymous=value;}
		}	
		private int m_ForceLogin;
		public int ForceLogin
		{
			get { return m_ForceLogin;}
			set { m_ForceLogin=value;}
		}			
		private String m_AppUserToken;
		public String AppUserToken
		{
			get { return m_AppUserToken;}
			set { m_AppUserToken=value;}
		}	
		private String m_NickName;
		public String NickName
		{
			get { return m_NickName;}
			set { m_NickName=value;}
		}		
		private String m_IPCreated;
		public String IPCreated
		{
			get { return m_IPCreated;}
			set { m_IPCreated=value;}
		}			
		private String m_IPLastActivity;
		public String IPLastActivity
		{
			get { return m_IPLastActivity;}
			set { m_IPLastActivity=value;}
		}		
		private String m_IPLastLogin;
		public String IPLastLogin
		{
			get { return m_IPLastLogin;}
			set { m_IPLastLogin=value;}
		}	
		private String m_IPLocation;
		public String IPLocation
		{
			get { return m_IPLocation;}
			set { m_IPLocation=value;}
		}				
		private String m_Platform;
		public String Platform
		{
			get { return m_Platform;}
			set { m_Platform=value;}
		}				
		private String m_Browser;
		public String Browser
		{
			get { return m_Browser;}
			set { m_Browser=value;}
        }
        #endregion �û���Ϣʵ�嶨��

        #region �û���Ϣ�������ݿ��������
        // ����
		public bool Create( )
		{
			SqlDataReader reader = null;
			string sql="insert into Member_Users  (" +
				" UserName,Password,PasswordFormat,Email,NickName) " +
				" values "+
				" ('" + this.UserName + "','" + this.Password + "'," + this.PasswordFormat + ",'" + this.Email + "','" + this.NickName + "')";
            int reval = Tools.DoSqlRowsAffected(sql);

			sql = "select UserID from Member_Users where UserName='" + this.UserName + "'";
			reader=Tools.DoSqlReader(sql);
			if(reader.Read())
			{
				this.UserID	=int.Parse(reader["UserID"].ToString());
			}
			reader.Close();
			sql="insert into Member_UserProfile  (" +
				" UserID,TimeZone) " +
				" values "+
				" ('" + this.UserID + "','8')";
            int reval2 = Tools.DoSqlRowsAffected(sql);

			if(reval==1)
			{
				return true;
			}
			else
			{
				return false;
            }
            #endregion �û���Ϣ�������ݿ��������
        }
	}
}
