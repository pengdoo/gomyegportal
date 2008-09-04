//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-7-9
//
// 功能描述:角色表管理
//
// 已修改问题:
//      1 Create方法未完成
// 未修改问题:
//      1 权限相关的封装和Type_TypeTree.cs一起,应该独立
//      2 Delect,Update方法未完成
// 修改记录
//       1   2008-7-10 添加注释
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
	/// Roles 的摘要说明。
	/// </summary>
	public class Roles
    {
        #region 实体定义
        private int m_RolesID;
		public int RolesID
		{
			get { return m_RolesID;}
			set { m_RolesID=value;}
		}
		private String m_RolesName;
		public String RolesName
		{
			get { return m_RolesName;}
			set { m_RolesName=value;}
		}
		private String m_RolesExplan;
		public String RolesExplan
		{
			get { return m_RolesExplan;}
			set { m_RolesExplan=value;}
        }

        #endregion 实体定义

        #region 常用数据库操作
        public bool Create(String rolesName, String rolesExplan, int addId)
		{
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Roles_ID", SqlDbType.Int),
                    new SqlParameter("@Roles_Name", SqlDbType.VarChar,50),
                    new SqlParameter("@Roles_Explan", SqlDbType.VarChar,500),
                    new SqlParameter("@Add_ID", SqlDbType.Int)
            };
            parameters[0].Direction = ParameterDirection.Output;

            if (rolesName != null)
                parameters[1].Value = rolesName;
            else
                parameters[1].Value = DBNull.Value;


            if (rolesExplan != null)
                parameters[2].Value = rolesExplan;
            else
                parameters[2].Value = DBNull.Value;

            parameters[3].Value = addId;
            SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "p_Content_Roles_ADD", parameters, out rowsAffected);

            if (rowsAffected > 0)
            {
                this.m_RolesID = (int)parameters[0].Value;
                return true;
            }
            else
            {
                return false;
            }

		}
		public bool Delete(int rolesID)
		{
			string sql= "";
            return Tools.DoSql(sql);
		}
		public bool Update()
		{
			string sql= "";
            return Tools.DoSql(sql);
		}
		 
		public bool Init(int rolesID)
		{
			SqlDataReader reader = null;
			string sql=" select *  from Content_Roles where Roles_ID=" + rolesID;
			reader= Tools.DoSqlReader(sql);
			if(reader.Read())
			{
				this.RolesID=int.Parse(reader["Roles_ID"].ToString());
				this.RolesName=reader["Roles_Name"].ToString();
				this.RolesExplan=reader["Roles_Explan"].ToString();
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
		public bool IsExist(int rolesID)
		{

			SqlDataReader reader = null;
			string sql="select  *  from Content_Roles where Roles_ID=" + rolesID;
			reader= Tools.DoSqlReader(sql);
            bool res = reader.Read();
            reader.Close();
            return res;
		}
		public System.Collections.ArrayList SelectAll()
		{
			SqlDataReader reader = null;
			System.Collections.ArrayList list = new System.Collections.ArrayList();
			string sql=" select *  from Content_Roles";
			reader= Tools.DoSqlReader(sql);
			while(reader.Read())
			{
				Roles _roles= new Roles();
				_roles.RolesID=int.Parse(reader["Roles_ID"].ToString());
				_roles.RolesName=reader["Roles_Name"].ToString();
				_roles.RolesExplan=reader["Roles_Explan"].ToString();
				list.Add(_roles);
			}
			reader.Close();
			return list;
		}

		/// <summary>
		/// 判断是否存在角色与目录的关联关系
		/// </summary>
		/// <param name="rolesID"></param>
		/// <param name="typeID"></param>
		/// <returns></returns>
		public bool IsExistType(int rolesID,int typeID)
		{
			SqlDataReader myRead = null;
			string sSQL = "select * from Content_RolesConnect where Roles_ID = "+rolesID+" and TypeTree_ID = "+typeID+"";
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
        #endregion 常用数据库操作


        #region 角色权限相关函数
        /// <summary>
		/// 为角色添加目录
		/// </summary>
		/// <param name="rolesID"></param>
		/// <param name="typeID"></param>
		/// <returns></returns>
		public bool AddType(int rolesID,int typeID)
		{
			string sSQL = "insert into Content_RolesConnect(Roles_ID,TypeTree_ID) values("+rolesID+","+typeID+")";
			int reval = Tools.DoSqlRowsAffected(sSQL);
			if (reval == 1)
			{ return true; }
			else
			{ return false; }			
		}


		/// <summary>
		/// 为角色删除所有目录
		/// </summary>
		/// <param name="rolesID"></param>
		/// <param name="sPopedom"></param>
		/// <returns></returns>
        public bool DeleteType(int rolesID)
        {
            string sSQL = "delete from Content_RolesConnect where Roles_ID = " + rolesID;
            return Tools.DoSql(sSQL);
        }
		
		/// <summary>
		/// 判断角色的权限类型是否存在
		/// </summary>
		/// <param name="rolesID"></param>
		/// <param name="popedom"></param>
		/// <returns></returns>
		public bool IsExistPopedom(int rolesID,string sPopedom)
		{
			SqlDataReader myRead = null;
			string sSQL = "select * from Content_RolesPopedom where Roles_ID="+rolesID+" and Popedom_EName='"+sPopedom+"'";
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
		/// 为角色添加权限
		/// </summary>
		/// <param name="rolesID"></param>
		/// <param name="sPopedom"></param>
		/// <returns></returns>
		public bool AddPopedom(int rolesID,string sPopedom)
		{
			string sSQL = "insert into Content_RolesPopedom(Roles_ID,Popedom_EName) values("+rolesID+",'"+sPopedom+"')";
            return Tools.DoSql(sSQL);
		}

		/// <summary>
		/// 为角色删除所有权限
		/// </summary>
		/// <param name="rolesID"></param>
		/// <param name="sPopedom"></param>
		/// <returns></returns>
		public bool DeletePopedom(int rolesID)
		{
			string sSQL = "delete from Content_RolesPopedom where Roles_ID = "+ rolesID;
            return Tools.DoSql(sSQL);
        }
        #endregion 角色权限相关函数
    }
}
