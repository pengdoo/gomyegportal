//------------------------------------------------------------------------------
// ������ʶ: Copyright (C) 2008 Gomye.com.cn ��Ȩ����
// ��������: Galen Mu ������ 2008-7-9
//
// ��������:�������������
//
// ���޸�����:
// δ�޸�����:
//       1   ��Type_LinkPush �ܶඨ���ظ�
// �޸ļ�¼
//       1   2008-7-10 ���ע��
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
	/// Type ��ժҪ˵����
	/// </summary>
	public class Type_Parenthesis
    {
        #region ʵ�嶨��
        private int m_Link_ID;
		public int Link_ID
		{
			get { return m_Link_ID;}
			set { m_Link_ID=value;}
		}
		private String m_LinkName;
		public String LinkName
		{
			get { return m_LinkName;}
			set { m_LinkName=value;}
		}
		private int m_TypeTree_ID;
		public int TypeTree_ID
		{
			get { return m_TypeTree_ID;}
			set { m_TypeTree_ID=value;}
		}
		private String m_TypeTree_URL;
		public String TypeTree_URL
		{
			get { return m_TypeTree_URL;}
			set { m_TypeTree_URL=value;}
		}
		private String m_TypeTree_Template;
		public String TypeTree_Template
		{
			get { return m_TypeTree_Template;}
			set { m_TypeTree_Template=value;}
		}
		
		private int m_List_Amount;
		public int List_Amount
		{
			get { return m_List_Amount;}
			set { m_List_Amount=value;}
		}
		private int m_LinkType;
		public int LinkType
		{
			get { return m_LinkType;}
			set { m_LinkType=value;}
        }

        #endregion ʵ�嶨��

        #region �������ݿ����
        // ���ܣ��õ�Content_Type_TypeTree�������TypeTree_ID
		// ���룺
		// ������������TypeTree_IDֵ
		public int  QueryLink_ID( )
		{
			int Max_Id = 0;
			SqlDataReader reader = null;			
 
			string sql = "select ID_Number from Content_ID where ID_Name = 'Link_ID'";
		 
			reader=Tools.DoSqlReader(sql);

			if(reader.Read())
				//if (reader.Equals(null))
			{
				Max_Id = Int32.Parse(reader["ID_Number"].ToString());
			}
			else
			{					
				//this.Response.Write ("��ʼ�����ݴ���");
			}

			reader.Close();
			return Max_Id;
		}

		// ���ܣ�����Content_ID���е�TypeTree_ID��ֵ
		// ���룺TypeTree_ID��ֵ
		// ��������³ɹ�������true�����򣬷���false
		public bool UpdateLink_ID(int Link_ID)
		{
			String sql = "update Content_ID set ID_Number = " + Link_ID + " where (ID_Name = 'Link_ID')";
            return Tools.DoSql(sql);
		}



		// ���ܣ���������
		// ���룺
		// ������ɹ�����true�����ɹ�����false
		public bool Create()
		{
			int max_id = QueryLink_ID() + 1;
			String sql = "insert into Content_Type_LinkPush " +
				"(Link_ID,LinkName,TypeTree_ID,TypeTree_URL,TypeTree_Template,List_Amount,LinkType) " +
				"values( " +
				max_id + ",'" +
				this.LinkName + "'," + this.TypeTree_ID +
				",'" + this.TypeTree_URL + "','" +
				this.TypeTree_Template + "'," +
				this.List_Amount + "," +
				this.LinkType +
				")";
			int reval = Tools.DoSqlRowsAffected(sql);

			//ͬʱ����Content_ID���е�TypeTree_ID��ֵ
			UpdateLink_ID(max_id+1);
            //#�˴����п��Ż�������, �ع�ʱע��#
			if(reval==1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool Delete(int Link_ID)
		{
			//ִ��ɾ��Ŀ¼
			String sql = "delete from Content_Type_LinkPush where Link_ID = " + Link_ID;
            return Tools.DoSql(sql);
		}


		// ���ܣ��޸�����
		// ���룺TypeTree_ID
		// ������ɹ�����true�����ɹ�����false
		public bool Update(int Link_ID)
		{
			String sql = "update Content_Type_LinkPush set " + 						 
				"LinkName = '" + this.LinkName + "', " +
				"TypeTree_ID = " + this.TypeTree_ID + ", " +
				"TypeTree_Template = '" + this.TypeTree_Template + "', " +	
				"TypeTree_URL = '" + this.TypeTree_URL + "', " +		
				"List_Amount = " + this.List_Amount + ", " +		
				"LinkType = " + this.LinkType +  
				" where Link_ID = " + Link_ID ;
            return Tools.DoSql(sql);
		}
		
		public bool Init(int Link_ID)
		{
			SqlDataReader reader = null;
            string sql=" select * from Content_Type_LinkPush  where Link_ID=" + Link_ID;
			reader= Tools.DoSqlReader(sql);
            //#�˴����п��Ż�������, �ع�ʱע��#
			if(reader.Read())
			{
				this.Link_ID=int.Parse(reader["Link_ID"].ToString());
				this.LinkName=reader["LinkName"].ToString();
				this.TypeTree_ID=int.Parse(reader["TypeTree_ID"].ToString());
				this.TypeTree_Template=reader["TypeTree_Template"].ToString();
				this.List_Amount=int.Parse(reader["List_Amount"].ToString());
				this.LinkType=int.Parse(reader["LinkType"].ToString());
				this.TypeTree_URL = reader["TypeTree_URL"].ToString();
				reader.Close();
				return true;
			}
			else
			{
				reader.Close();
				return false;
			}
        }
        #endregion �������ݿ����
        
		// ��չ�ֶδ��� -----------------------------------------------

		public DataTable QueryTypeFieldsList(int TypeTree_ID)
		{
			string sql = "SELECT *  FROM  Content_Content  WHERE TypeTree_ID =" + TypeTree_ID;
			return Tools.DoSqlTable(sql).Copy();
			 	
		}

		// ��չ�ֶδ������ -----------------------------------------------

	}
}
