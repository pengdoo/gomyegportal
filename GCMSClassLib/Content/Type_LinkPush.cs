//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-7-9
//
// 功能描述:关联发布表管理
//
// 已修改问题:
// 未修改问题:
// 修改记录
//       1   2008-7-10 添加注释
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Data.Common;
using GCMSClassLib.Public_Cls;


namespace GCMSClassLib.Content
{
	/// <summary>
	/// Type_LinkPush 的摘要说明。
	/// </summary>
	public class Type_LinkPush
	{
		public Type_LinkPush()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        #region 实体定义
		private int m_LinkID;
		public int LinkID
		{
			get { return m_LinkID; }
			set { m_LinkID = value; }		
		}

		private String m_LinkName;
		public String LinkName
		{
			get { return m_LinkName; }
			set { m_LinkName = value; }
		}

		private int m_TypeTreeID;
		public int TypeTree_ID
		{
			get { return m_TypeTreeID; }
			set { m_TypeTreeID = value; }
		}

		private String m_TypeTreeURL;
		public String TypeTreeURL
		{
			get { return m_TypeTreeURL; }
			set { m_TypeTreeURL = value; }
		}

		private String m_TypeTreeTemplate;
		public String TypeTreeTemplate
		{
			get { return m_TypeTreeTemplate; }
			set { m_TypeTreeTemplate = value; }
		}

		private int m_ListAmount;
		public int ListAmount
		{
			get { return m_ListAmount; }
			set { m_ListAmount = value; }
		}

		private int m_LinkType;
		public int LinkType
		{
			get { return m_LinkType; }
			set { m_LinkType = value;}
        }
        #endregion 实体定义

        #region 常用数据库操作
        // 功能：新增数据
		// 输入：
		// 输出：成功返回true，不成功返回false
		public bool Create()
		{
			int max_id = QueryMaxLinkID() + 1;
			String sql = "insert into Content_Type_LinkPush " +
						 "(Link_ID, LinkName, TypeTree_ID, TypeTree_URL, TypeTree_Template, List_Amount, LinkType) " +
						 "values( " +
						 max_id + "," +
						 "'" + this.LinkName + "'," +
						 this.TypeTree_ID + "," +
						 "'" + this.TypeTreeURL + "'," +
						 "'" + this.TypeTreeTemplate + "'," +
						 this.ListAmount + "," +
						 this.LinkType +
						 ")" ;
            return Tools.DoSql(sql);
        }
        
        // 功能：新增数据
		// 输入：
		// 输出：成功返回true，不成功返回false
		public bool Create(String linkName, int typeTree_ID, String typeTree_URL, String typeTree_Template, int list_Amount, int linkType)
		{
			int max_id = QueryMaxLinkID() + 1;
			String sql = "insert into Content_Type_LinkPush " +
				"(LinkName, TypeTree_ID, TypeTree_URL, TypeTree_Template, List_Amount, LinkType) " +
				"values( " +
				max_id + "," +
				"'" + linkName + "'," +
				typeTree_ID + "," + 
				"'" + typeTree_URL + "'," +
				"'" + typeTree_Template + ",'" +
				list_Amount + "," +
				linkType +
				")" ;
            return Tools.DoSql(sql);		
		}

		// 功能：删除数据
		// 输入：linkID
		// 输出：成功返回true，不成功返回false
		public bool Delete(int linkID)
		{
			String sql = "delete from Content_Type_LinkPush where Link_ID = " + linkID ;
            return Tools.DoSql(sql);	
		}

		// 功能：修改数据
		// 输入：
		// 输出：成功返回true，不成功返回false
		public bool Update()
		{
			String sql = "update Content_Type_LinkPush set " +
						 "LinkName = '" + this.LinkName + "', " +
						 "TypeTree_ID = " + this.TypeTree_ID + ", " +
						 "TypeTree_URL = '" + this.TypeTreeURL + "', " +
						 "TypeTree_Template = '" + this.TypeTreeTemplate + "', " +
						 "List_Amount = " + this.ListAmount + ", " +
						 "LinkType = " + this.LinkType + " " +
						 "where Link_ID = " + this.LinkID ;
            return Tools.DoSql(sql);	
		}
		
		// 功能：根据传入的linkID初始化类RolesConnect
		// 输入：linkID
		// 输出：成功返回true，不成功返回false		
		public bool Init(int linkID)
		{
			SqlDataReader reader = null;
			String sql = "select Link_ID,LinkName,TypeTree_ID,TypeTree_URL,TypeTree_Template,List_Amount,LinkType from Content_Type_LinkPush where Link_ID = " +  linkID; 
			reader = Tools.DoSqlReader(sql);
			if(reader.Read())
			{
				this.LinkID = int.Parse(reader["Link_ID"].ToString());
				this.LinkName = reader["LinkName"].ToString();
				this.TypeTree_ID = int.Parse(reader["TypeTree_ID"].ToString());
				this.TypeTreeURL = reader["TypeTree_URL"].ToString();
				this.TypeTreeTemplate = reader["TypeTree_Template"].ToString();
				this.ListAmount = int.Parse(reader["List_Amount"].ToString());
				this.LinkType = int.Parse(reader["LinkType"].ToString());

				reader.Close();
				return true;
			}
			else
			{
				reader.Close();
				return false;
			}
		}

		// 功能：得到所有的Type_LinkPush对象数组
		// 输入：
		// 输出：返回Type_LinkPush对象数组	
		public System.Collections.ArrayList SelectAll()		
		{
			SqlDataReader reader = null;
			System.Collections.ArrayList list = new System.Collections.ArrayList();
			String sql = "select Link_ID,LinkName,TypeTree_ID,TypeTree_URL,TypeTree_Template,List_Amount,LinkType from Content_Type_LinkPush" ;
			reader = Tools.DoSqlReader(sql);
			
			while(reader.Read())
			{
				Type_LinkPush _typelinkpush = new Type_LinkPush();
				_typelinkpush.LinkID = int.Parse(reader["Link_ID"].ToString());
				_typelinkpush.LinkName = reader["LinkName"].ToString();
				_typelinkpush.TypeTree_ID = int.Parse(reader["TypeTree_ID"].ToString());
				_typelinkpush.TypeTreeURL = reader["TypeTree_URL"].ToString();
				_typelinkpush.TypeTreeTemplate = reader["TypeTree_Template"].ToString();
				_typelinkpush.ListAmount = int.Parse(reader["List_Amount"].ToString());
				_typelinkpush.LinkType = int.Parse(reader["LinkType"].ToString());

				list.Add(_typelinkpush);
			}
			reader.Close();
			return list;
		}

		// 功能：得到Content_Type_LinkPush表中最大Link_ID
		// 输入：
		// 输出：返回最大Link_ID值
		public int  QueryMaxLinkID( )
		{
			int Max_Id = 0;
			SqlDataReader reader = null;			
 
			string sql = "select  max(Link_ID)  Max_Id  from Content_Type_LinkPush  ";
		 
			reader=Tools.DoSqlReader(sql);
		    
			while(reader.Read())
			{				 
				Max_Id = Int32.Parse(reader["Max_Id"].ToString());			 
			}
			reader.Close();
			return Max_Id;

        }
        #endregion 常用数据库操作
    }
}
