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

namespace GCMSClassLib.Content
{
	/// <summary>
	/// Log 的摘要说明。
	/// </summary>
	public class Log
	{
		private int m_Content_Id;
		public int Content_Id
		{
			get { return m_Content_Id;}
			set { m_Content_Id=value;}
		}
		private int m_Log_ID;
		public int Log_ID
		{
			get { return m_Log_ID;}
			set { m_Log_ID=value;}
		}
		private string m_Log_Txt;
		public string Log_Txt
		{
			get { return m_Log_Txt;}
			set { m_Log_Txt=value;}
		}
		private string m_Log_Action;
		public string Log_Action
		{
			get { return m_Log_Action;}
			set { m_Log_Action=value;}
		}
		private System.DateTime m_Log_Date;
		public System.DateTime Log_Date
		{
			get { return m_Log_Date;}
			set { m_Log_Date=value;}
		}
		private string m_Master_Name;
		public string Master_Name
		{
			get { return m_Master_Name;}
			set { m_Master_Name=value;}
		}
		private int m_Master_ID;
		public int Master_ID
		{
			get { return m_Master_ID;}
			set { m_Master_ID=value;}
		}


		public bool Create( )
		{
			
			int max_id=Tools.QueryMaxID("Log_ID");
			string sql="insert into Content_Log  (" +
				"Log_ID,Content_Id,Log_Txt,Log_Action,Log_Date,Master_ID,Master_Name )" + 
				" values "+
				" ("  + max_id + "," + this.Content_Id + ",'" + Tools.WebToDB(this.Log_Txt) + "','" +  Tools.WebToDB(this.Log_Action) + "',getdate()," + this.Master_ID + ",'"+ this.Master_Name + "')";


            return Tools.DoSql(sql);
		}


	}
}
