//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-7-9
//
// 功能描述:角色关联表管理
//
// 已修改问题:
// 未修改问题:
//      1 权限相关的封装和Type_TypeTree.cs一起,应该独立
// 修改记录
//       1   2008-7-10 添加注释
//       2   2008-8-26 改变数据访问层引用方法的写法
//       3   
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Data.Common;
using GCMSClassLib.Public_Cls;


namespace ContentClassLib
{
	/// <summary>
	/// RolesConnect 的摘要说明。
	/// </summary>
	public class RolesConnect
	{
        const string SQL_RolesConnectCreate = "insert into Content_RolesConnect(Roles_ID,TypeTree_ID) values({0},{1})";
		public RolesConnect()
		{
		}

		private int m_RolesID;
		public int RolesID
		{
			get { return m_RolesID;}
			set { m_RolesID=value;}
		}

		private int m_TypeTreeID;
		public int TypeTree_ID
		{
			get { return m_TypeTreeID;}
			set { m_TypeTreeID=value;}
		}
				
		// 功能：新增数据
		// 输入：
		// 输出：成功返回true，不成功返回false
		public bool Create( )
		{
			string sql =string.Format(SQL_RolesConnectCreate,  this.RolesID ,this.TypeTree_ID);
            return Tools.DoSql(sql);
		}

        // 功能：新增数据
        // 输入：
        // 输出：成功返回true，不成功返回false
        public static bool Create(int TypeTree_ID, int Roles_ID)
        {
            string sql = string.Format(SQL_RolesConnectCreate, Roles_ID, TypeTree_ID);
            return Tools.DoSql(sql);
        }
		
		
		
		// 功能：新增数据
		// 输入：rolesID, TypeTree_ID数组
		// 输出：成功返回true，不成功返回false
		public bool Create(int rolesID,int[] TypeTree_ID)
		{			
			RolesConnect rolesConnect = new RolesConnect();
			foreach (int i in TypeTree_ID)
			{
				Create(rolesID,i);				
			}			
			return true;
		}
		
		// 功能：删除数据
		// 输入：rolesID ,TypeTree_ID
		// 输出：成功返回true，不成功返回false
		public bool Delete(int rolesID,int TypeTree_ID)
		{
			String sql = "delete Content_RolesConnect where Roles_ID = " + rolesID + " and TypeTree_ID = " + TypeTree_ID;

            return Tools.DoSql(sql);		
		}

		// 功能：删除数据
		// 输入：rolesID
		// 输出：成功返回true，不成功返回false
		public bool Delete(int rolesID)
		{
			String sql = "delete Content_RolesConnect where Roles_ID = " + rolesID ;
            return Tools.DoSql(sql);
		}
				
		// 功能：更新数据
		// 输入：rolesID , TypeTree_ID数组
		// 输出：成功返回true，不成功返回false
		public bool Update(int rolesID,int[] TypeTree_ID)
		{
			RolesConnect rolesConnect = new RolesConnect();
			rolesConnect.Delete(rolesID);
			foreach (int i in TypeTree_ID)
			{
				Create(rolesID,i);
			}			
			return true;
		}		
		
		// 功能：根据传入的rolesID初始化类RolesConnect
		// 输入：rolesID 
		// 输出：成功返回true，不成功返回false
		public bool Init(int rolesID)
        {
			SqlDataReader reader = null;
			String sql = "select Roles_ID, TypeTree_ID from Content_RolesConnect where Roles_ID = " + rolesID;
			reader = Tools.DoSqlReader(sql);
			if(reader.Read())
			{
				this.RolesID = int.Parse(reader["Roles_ID"].ToString());
				this.TypeTree_ID = int.Parse(reader["TypeTree_ID"].ToString());
				reader.Close();
				return true;
			}
			else
			{	
				reader.Close();
				return false;
			}
		}
		
		// 功能：按条件检查数据库中是否已经存在
		// 输入：rolesID,TypeTree_ID 
		// 输出：存在返回true，不存在返回false
		public bool IsExist(int rolesID, int TypeTree_ID)
		{
			SqlDataReader reader = null;
			string sql=" select  * from  Content_RolesConnect where Roles_ID = " + rolesID + " and TypeTree_ID = " + TypeTree_ID;
			reader= Tools.DoSqlReader(sql);
            bool res = reader.Read();
            reader.Close();
            return res;
		}

        //功能：判断权限
        //输入：TypeTree_ID
        //输出：存在文章，返回true，否则，返回false
        public static bool IsRolesConnect(int TypeTree_ID, int Roles_ID)
        {
            SqlDataReader reader = null;
            string sql = "select * from Content_RolesConnect where TypeTree_ID = " + TypeTree_ID + " and Roles_ID = " + Roles_ID;
            reader = Tools.DoSqlReader(sql);
            
            bool res=reader.Read();
            reader.Close();
            return res;  
        }

		// 功能：根据传入的rolesID得到权限与目录对应的RolesConnect对象数组
		// 输入：rolesID 
		// 输出：返回RolesConnect对象数组
		public System.Collections.ArrayList SelectAll(int rolesID)
		{
			SqlDataReader reader = null;
			System.Collections.ArrayList list = new System.Collections.ArrayList();
			String sql = "select Roles_ID, TypeTree_ID from Content_RolesConnect where Roles_ID = " + rolesID;
			reader = Tools.DoSqlReader(sql);
			while(reader.Read())
			{
				RolesConnect _rolesConnect = new RolesConnect();
				_rolesConnect.RolesID = int.Parse(reader["Roles_ID"].ToString());
				_rolesConnect.TypeTree_ID = int.Parse(reader["TypeTree_ID"].ToString());

				list.Add(_rolesConnect);		
			}
			reader.Close();
			
			return list;
		}

	}
}
