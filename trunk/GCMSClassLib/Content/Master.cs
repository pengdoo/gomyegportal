//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-7-9
//
// 功能描述:管理员表管理
//
// 已修改问题:
//      1 UpdatePWD函数实际上和其他DoSelect一样
//      2 Create方法未完成
// 未修改问题:
//     
// 修改记录
//       1   2008-7-10 添加注释
//       2   2008-8-27 改写数据访问层写法。
//                     改变Update函数引用到Tools.DoSql.删除Update函数
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient ;
using System.Collections;
using System.Data.Common;
using GCMSClassLib.Public_Cls;
using Gomye.CommonClassLib.Data;

namespace GCMSClassLib.Content
{
	/// <summary>
	/// Master 的摘要说明。
	/// </summary>
	public class Master
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
		private String m_MasterTel;
		public String MasterTel
		{
			get { return m_MasterTel;}
			set { m_MasterTel=value;}
		}
		private String m_MasterUsableness;
		public String MasterUsableness
		{
			get { return m_MasterUsableness;}
			set { m_MasterUsableness=value;}
		}
		private String m_MasterNote;
		public String MasterNote
		{
			get { return m_MasterNote;}
			set { m_MasterNote=value;}
		}
		private System.DateTime m_MasterAddDate;
		public System.DateTime MasterAddDate
		{
			get { return m_MasterAddDate;}
			set { m_MasterAddDate=value;}
        }
        private int _add_id;
        public int Add_ID
        {
            set { _add_id = value; }
            get { return _add_id; }
        }
        #endregion 实体定义

        #region 常用数据操作
        public bool Create()
		{
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Master_ID", SqlDbType.Int),
                    new SqlParameter("@Master_Name", SqlDbType.VarChar,50),
                    new SqlParameter("@Master_UserName", SqlDbType.VarChar,50),
                    new SqlParameter("@Master_Password", SqlDbType.VarChar,50),
                    new SqlParameter("@Master_Email", SqlDbType.VarChar,50),
                    new SqlParameter("@Master_Tel", SqlDbType.VarChar,50),
                    new SqlParameter("@Master_Usableness", SqlDbType.Char,1),
                    new SqlParameter("@Master_Note", SqlDbType.VarChar,500),
                    new SqlParameter("@Add_ID", SqlDbType.Int)
            };
            parameters[0].Direction = ParameterDirection.Output;

            if (this.MasterName != null)
                parameters[1].Value = this.MasterName;
            else
                parameters[1].Value = DBNull.Value;


            if (this.MasterUserName != null)
                parameters[2].Value = this.MasterUserName;
            else
                parameters[2].Value = DBNull.Value;


            if (this.MasterPassword != null)
                parameters[3].Value = this.MasterPassword;
            else
                parameters[3].Value = DBNull.Value;


            if (this.MasterEmail != null)
                parameters[4].Value = this.MasterEmail;
            else
                parameters[4].Value = DBNull.Value;


            if (this.MasterTel != null)
                parameters[5].Value = this.MasterTel;
            else
                parameters[5].Value = DBNull.Value;


            if (this.MasterUsableness != null)
                parameters[6].Value = this.MasterUsableness;
            else
                parameters[6].Value = DBNull.Value;


            if (this.MasterNote != null)
                parameters[7].Value = this.MasterNote;
            else
                parameters[7].Value = DBNull.Value;



            parameters[8].Value = this.Add_ID;
            SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "p_Content_Master_ADD", parameters, out rowsAffected);

            if (rowsAffected > 0)
            {
                this.MasterID = (int)parameters[9].Value;
                return true;
            }
            else
            {
                return false;
            }
        
		}
		public bool Delete(int masterID)
		{
			string sql="delete from Content_Master where Master_ID =" + masterID;
            return Tools.DoSql(sql);
		}
		
        public bool Init(int masterID)
        {
            SqlDataReader reader = null;
            string sql = "select Master_ID,Master_Name,Master_UserName,Master_Password,Master_Email,Master_Tel,Master_Usableness,Master_Note,isnull(Master_AddDate,'2000-01-01') Master_AddDate from Content_Master  where Master_ID=" + masterID;
            reader = Tools.DoSqlReader(sql);
            if (reader.Read())
            {
                this.MasterID = int.Parse(reader["Master_ID"].ToString());
                this.MasterName = reader["Master_Name"].ToString();
                this.MasterUserName = reader["Master_UserName"].ToString();
                this.MasterPassword = reader["Master_Password"].ToString();
                this.MasterEmail = reader["Master_Email"].ToString();
                this.MasterTel = reader["Master_Tel"].ToString();
                this.MasterUsableness = reader["Master_Usableness"].ToString();
                this.MasterNote = reader["Master_Note"].ToString();
                this.MasterAddDate = System.DateTime.Parse(reader["Master_AddDate"].ToString());
                reader.Close();
                return true;
            }
            else
            {
                reader.Close();
                return false;
            }
        }

        ///<summary>
        ///按条件检查数据库中是否已经存在
        ///</summary>
        public bool IsExist(int masterID)
        {
            SqlDataReader reader = null;
            string sql = " select  *  from     Content_Master  where Master_ID=" + masterID;
            reader = Tools.DoSqlReader(sql);
            if (reader.Read())
            {
                reader.Close();
                return true;
            }
            else
            {
                reader.Close();
                return false;
            }
        }
        public System.Collections.ArrayList SelectAll()
        {
            SqlDataReader reader = null;
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            string sql = " select  * from  Content_Master ";
            reader = Tools.DoSqlReader(sql);
            while (reader.Read())
            {
                Master _master = new Master();
                _master.MasterID = Int32.Parse(reader["Master_ID"].ToString());
                _master.MasterName = reader["Master_Name"].ToString();
                _master.MasterUserName = reader["Master_UserName"].ToString();
                _master.MasterPassword = reader["Master_Password"].ToString();
                _master.MasterEmail = reader["Master_Email"].ToString();
                _master.MasterTel = reader["Master_Tel"].ToString();
                _master.MasterUsableness = reader["Master_Usableness"].ToString();
                _master.MasterNote = reader["Master_Note"].ToString();
                _master.MasterAddDate = System.DateTime.Parse(reader["Master_AddDate"].ToString());
                list.Add(_master);


            }
            reader.Close();
            return list;
        }
        #endregion 常用数据操作

        #region 角色相关操作
		 
		
		/// <summary>
		/// 判断是否存在相同的用户名
		/// </summary>
		/// <param name="sMasterUserName"></param>
		/// <returns></returns>
		public bool IsExistUserName(string sMasterUserName)
		{
			SqlDataReader reader = null;
			string sql=" select  *  from Content_Master where Master_UserName='" + sMasterUserName + "'";
			reader= Tools.DoSqlReader(sql);
            bool res = reader.Read();
            reader.Close();
            return res;
		}

		

		/// <summary>
		/// 判断是否存在此用户和角色的关联关系
		/// </summary>
		/// <param name="masterID"></pram>
		/// <param name="rolesID"></param>
		/// <returns></returns>
		public bool IsExistRoles(int masterID,int rolesID)
		{
			SqlDataReader myRead = null;
			string sSQL = "select * from Content_RolesMaster where Master_ID="+masterID+" and Roles_ID="+rolesID;
			myRead = Tools.DoSqlReader(sSQL);
			if (myRead.Read())
			{
				myRead.Close();
				return true;
			}
			else
			{
				myRead.Close();
				return false;
			}
		
		}

		/// <summary>
		/// 为用户增加角色
		/// </summary>
		public bool AddRoles(int masterID,int rolesID)
		{
			SqlDataReader reader = null;
			string sSQL = "select (isnull(max(ID),0)+1) ID from Content_RolesMaster";
			reader = Tools.DoSqlReader(sSQL);
			int i_ID=0;
			while (reader.Read())
			{
				i_ID=int.Parse(reader["ID"].ToString());
			}
			reader.Close();

			sSQL = "insert into Content_RolesMaster(ID,Master_ID,Roles_ID) values("+i_ID+","+masterID+","+rolesID+")";
            return Tools.DoSql(sSQL);
        }
        #endregion 角色相关操作
    }
}
