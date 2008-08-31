//------------------------------------------------------------------------------
// ������ʶ: Copyright (C) 2008 Gomye.com.cn ��Ȩ����
// ��������: Galen Mu ������ 2008-7-9
//
// ��������:���β˵���XML����
//
// ���޸�����:
// δ�޸�����:
// �޸ļ�¼
//       1   2008-7-10 ���ע��
//       2   2008-8-26 �ı����ݷ��ʲ����÷�����д��
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
	/// Tree ��ժҪ˵����
	/// </summary>
	public class Tree
	{
		private string sSQL;
		private string sXml = "<?xml version='1.0' encoding='utf-8' ?> ";
//		private string sXml = "";
		public Tree()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// �õ����������������ṹ
		/// ����һ�������
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
