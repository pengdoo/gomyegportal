//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-7-9
//
// 功能描述: 扩展表的操作
//
// 已修改问题:
// 未修改问题:
//      1 似乎和Type_TypeTree.cs扩展字段功能重叠。
// 修改记录
//       1   2008-7-9 添加注释
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using GCMSClassLib.Public_Cls;


namespace GCMSClassLib.Content
{
	/// <summary>
	/// ContentTable 的摘要说明。
	/// </summary>
	public class ContentTable
	{
		//private DataConn data;
        //private string sXml = "";
        #region 实体定义
        private String m_TableName;
		public String TableName
		{
			get { return m_TableName;}
			set { m_TableName=value;}
		}
		private String m_ColumnName;
		public String ColumnName
		{
			get { return m_ColumnName;}
			set { m_ColumnName=value;}
		}
		private String m_ColumnNewName;
		public String ColumnNewName
		{
			get { return m_ColumnNewName;}
			set { m_ColumnNewName=value;}
		}
		private String m_ColumnType;
		public String ColumnType
		{
			get { return m_ColumnType;}
			set { m_ColumnType=value;}
		}
		private String m_CreateColumn;
		public String CreateColumn
		{
			get { return m_CreateColumn;}
			set { m_CreateColumn=value;}
		}

		private String m_TableNewName;
		public String TableNewName
		{
			get { return m_TableNewName;}
			set { m_TableNewName=value;}
        }
        #endregion 实体定义

        #region 表数据库操作定义
        private string sSQL;
        /// <summary>
		/// 创建表SQL
		///传入参数例子：CreateColumn = message varchar(5000)
		/// </summary>
		public string cCreateTable()//#此处有特殊含义数字,重构时注意#
		{
			sSQL = "create table "+this.TableName+" (Content_ID int,TypeTree_id int,Content_PId int,Author  varchar(50),Status int,Clicks int,OrderNum int,lockedby varchar(50),User_ID int,AtTop int,PublishDate datetime,SubmitDate datetime,Url varchar(200))";	
			return sSQL;
		}
			
		/// <summary>
		/// 修改表名
		/// </summary>
		public string cAlterTableName()
		{
		sSQL = "exec sp_rename '"+this.TableName+"','"+this.TableNewName+"','Object'";	
		return sSQL;
		}

		public string cDelTableName()
		{
			sSQL = "drop table "+this.TableName;	
			return sSQL;
		}


		public string cAlterTable(int iStatus)
		{
			
            switch (iStatus)//#此处有特殊含义数字,重构时注意#
			{
				case 0: //增加字段
                    sSQL = "alter table " + this.TableName + " add [" + this.ColumnName + "] " + this.ColumnType + "";
					break;
				case 1: //删除字段
                    sSQL = "alter table " + this.TableName + " drop COLUMN [" + this.ColumnName + "]";
					break;
				case 2: //修改字段
                    sSQL = "exec  sp_rename   '" + this.TableName + "." + this.ColumnName + "','" + this.ColumnNewName + "', N'COLUMN'";
					break;
				case 3://修改字段类型
                    sSQL = "ALTER TABLE " + this.TableName + " ALTER COLUMN [" + this.ColumnName + "] " + this.ColumnType;
					break;

			}
			return sSQL;
        }
        #endregion 表数据库操作定义
    }
}
