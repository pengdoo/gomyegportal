//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-7-9
//
// 功能描述: 扩展字段访问
//
// 已修改问题:
// 未修改问题:
// 修改记录
//       1   2008-7-9 添加注释
//       2   2008-8-27 改变数据访问层引用方法的写法
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
	/// Master 的摘要说明。
	/// </summary>
	public class Content_FieldsName
	{

		public Content_FieldsName()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		private int m_FieldsName_ID;
		public int FieldsName_ID
		{
			get { return m_FieldsName_ID;}
			set { m_FieldsName_ID=value;}
		}
		private String m_FieldsName_Name;
		public String FieldsName_Name
		{
			get { return m_FieldsName_Name;}
			set { m_FieldsName_Name=value;}
		}
		private int m_FieldsName_State;
		public int FieldsName_State
		{
			get { return m_FieldsName_State;}
			set { m_FieldsName_State=value;}
		}
		private String m_FieldsBase_Name;
		public String FieldsBase_Name
		{
			get { return m_FieldsBase_Name;}
			set { m_FieldsBase_Name=value;}
		}
		


		public bool Create()
		{
			string sName = "FieldsName_ID";
			int max_id=Tools.QueryMaxID(sName)+1;
			
			// int max_id=this.QueryMaxContentID() + 1;
			string sql="insert into Content_FieldsName(FieldsName_ID,FieldsName_Name,FieldsName_State,FieldsBase_Name) "+
				"values ("+max_id+",'"+this.FieldsName_Name+"',"+this.FieldsName_State+",'"+this.FieldsBase_Name+"')";
            Tools.UpdateMaxID(sName);
            return Tools.DoSql(sql);
		}
		public bool Delete(int FieldsName_ID)
		{
			string sql="delete from Content_FieldsName where FieldsName_ID =" + FieldsName_ID;
            return Tools.DoSql(sql);
		}
		
		public bool Init(int FieldsName_ID)
		{
			SqlDataReader reader = null; 
			string sql="select FieldsName_ID,FieldsName_Name,FieldsName_State,FieldsBase_Name from Content_FieldsName  where FieldsName_ID=" + FieldsName_ID;
			reader= Tools.DoSqlReader(sql);
			if(reader.Read())
			{
				this.FieldsName_ID=int.Parse(reader["FieldsName_ID"].ToString());
				this.FieldsName_Name=reader["FieldsName_Name"].ToString();
				this.FieldsName_State=int.Parse(reader["FieldsName_State"].ToString());
				this.FieldsBase_Name=reader["FieldsBase_Name"].ToString();
				
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
		public bool IsExist(int FieldsName_ID)
		{
			SqlDataReader reader = null;
			string sql=" select FieldsName_ID,FieldsName_Name,FieldsName_State from Content_FieldsName  where FieldsName_ID=" + FieldsName_ID;
			reader= Tools.DoSqlReader(sql);
            bool res = reader.Read();
            reader.Close();
            return res;
		}

		public System.Collections.ArrayList SelectAll()
		{
			SqlDataReader reader = null;
			System.Collections.ArrayList list = new System.Collections.ArrayList();
			string sql=" select FieldsName_ID,FieldsName_Name,FieldsName_State from  Content_FieldsName " ;
			reader=Tools.DoSqlReader(sql);
			while(reader.Read())
			{
				Content_FieldsName _Content_FieldsName= new Content_FieldsName();
				_Content_FieldsName.FieldsName_ID=Int32.Parse(reader["FieldsName_ID"].ToString());
				_Content_FieldsName.FieldsName_Name=reader["FieldsName_Name"].ToString();
				_Content_FieldsName.FieldsName_State=Int32.Parse(reader["FieldsName_State"].ToString());
				list.Add(_Content_FieldsName);
			}
			reader.Close();
			return list;
		}

        
	}
}
