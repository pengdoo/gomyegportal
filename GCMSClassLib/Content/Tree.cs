//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-7-9
//
// 功能描述:树形菜单的XML管理
//
// 已修改问题:
// 未修改问题:
// 修改记录
//       1   2008-7-10 添加注释
//       2   2008-8-26 改变数据访问层引用方法的写法
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
	/// Tree 的摘要说明。
	/// </summary>
	public class Tree
	{
		private string sSQL;
		private string sXml = "<?xml version='1.0' encoding='utf-8' ?> ";
//		private string sXml = "";
		public Tree()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 得到任意树结点的子树结构
		/// 传入一个树结点
		/// </summary>
		/// <param name="iTypeTree_ID"></param>
		/// <returns></returns>
		public string ListColumn(int iTypeTree_ID)
		{
			sSQL = "select TypeTree_ID,TypeTree_CName,isnull(TypeTree_URL,'') from Content_Type_TypeTree where TypeTree_ParentID="+iTypeTree_ID;
			SqlDataReader myRead = Tools.DoSqlReader(sSQL);
			while (myRead.Read())
			{
				sXml = sXml + "<Table Item='Parent'>";
				sXml = sXml + "<TypeTree_ID>" +myRead.GetInt32(0).ToString()+ "</TypeTree_ID>";
				sXml = sXml + "<TypeTree_CName>" +myRead.GetString(1).ToString()+ "</TypeTree_CName>";
				sXml = sXml + "<TypeTree_URL>" +myRead.GetString(2).ToString()+ "</TypeTree_URL>";
				sXml = sXml + "</Table>";
				SubColumn(int.Parse(myRead.GetInt32(0).ToString()));
			}
			myRead.Close();
			return sXml;
		}

		public void SubColumn(int iTypeTree_ParentID)
		{
			sSQL = "select TypeTree_ID,TypeTree_CName,isnull(TypeTree_URL,'') from Content_Type_TypeTree where TypeTree_ParentID="+iTypeTree_ParentID;
            SqlDataReader subRead = Tools.DoSqlReader(sSQL);
			while (subRead.Read())
			{
				sXml = sXml + "<Table>";
				sXml = sXml + "<TypeTree_ID>" +subRead.GetInt32(0).ToString()+ "</TypeTree_ID>";
				sXml = sXml + "<TypeTree_CName>" +subRead.GetString(1).ToString()+ "</TypeTree_CName>";
				sXml = sXml + "<TypeTree_URL>" +subRead.GetString(2).ToString()+ "</TypeTree_URL>";
				sXml = sXml + "</Table>";
				SubColumn(int.Parse(subRead.GetInt32(0).ToString()));
			}
			subRead.Close();
		}
	}
}
