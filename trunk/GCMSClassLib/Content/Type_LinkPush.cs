//------------------------------------------------------------------------------
// ������ʶ: Copyright (C) 2008 Gomye.com.cn ��Ȩ����
// ��������: Galen Mu ������ 2008-7-9
//
// ��������:�������������
//
// ���޸�����:
// δ�޸�����:
// �޸ļ�¼
//       1   2008-7-10 ���ע��
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
	/// Type_LinkPush ��ժҪ˵����
	/// </summary>
	public class Type_LinkPush
	{
		public Type_LinkPush()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
        #region ʵ�嶨��
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
        #endregion ʵ�嶨��

        #region �������ݿ����
        // ���ܣ���������
		// ���룺
		// ������ɹ�����true�����ɹ�����false
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
        
        // ���ܣ���������
		// ���룺
		// ������ɹ�����true�����ɹ�����false
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

		// ���ܣ�ɾ������
		// ���룺linkID
		// ������ɹ�����true�����ɹ�����false
		public bool Delete(int linkID)
		{
			String sql = "delete from Content_Type_LinkPush where Link_ID = " + linkID ;
            return Tools.DoSql(sql);	
		}

		// ���ܣ��޸�����
		// ���룺
		// ������ɹ�����true�����ɹ�����false
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
		
		// ���ܣ����ݴ����linkID��ʼ����RolesConnect
		// ���룺linkID
		// ������ɹ�����true�����ɹ�����false		
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

		// ���ܣ��õ����е�Type_LinkPush��������
		// ���룺
		// ���������Type_LinkPush��������	
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

		// ���ܣ��õ�Content_Type_LinkPush�������Link_ID
		// ���룺
		// ������������Link_IDֵ
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
        #endregion �������ݿ����
    }
}
