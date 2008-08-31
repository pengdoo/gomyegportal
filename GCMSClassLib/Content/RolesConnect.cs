//------------------------------------------------------------------------------
// ������ʶ: Copyright (C) 2008 Gomye.com.cn ��Ȩ����
// ��������: Galen Mu ������ 2008-7-9
//
// ��������:��ɫ���������
//
// ���޸�����:
// δ�޸�����:
//      1 Ȩ����صķ�װ��Type_TypeTree.csһ��,Ӧ�ö���
// �޸ļ�¼
//       1   2008-7-10 ���ע��
//       2   2008-8-26 �ı����ݷ��ʲ����÷�����д��
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
	/// RolesConnect ��ժҪ˵����
	/// </summary>
	public class RolesConnect
	{
		public RolesConnect()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
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
				
		// ���ܣ���������
		// ���룺
		// ������ɹ�����true�����ɹ�����false
		public bool Create( )
		{
			string sql = "insert into Content_RolesConnect(Roles_ID,TypeTree_ID) values(" + 
						 this.RolesID + "," + 
						 this.TypeTree_ID + 
						 ")";
            return Tools.DoSql(sql);
		}
		
		// ���ܣ���������
		// ���룺rolesID, TypeTree_ID
		// ������ɹ�����true�����ɹ�����false
		public bool Create(int rolesID,int TypeTree_ID)
		{
			string sql = "insert into Content_RolesConnect(Roles_ID,TypeTree_ID) values(" + 
						 rolesID + "," + 
						 TypeTree_ID +
						 ")";
            return Tools.DoSql(sql);		
		}
		
		// ���ܣ���������
		// ���룺rolesID, TypeTree_ID����
		// ������ɹ�����true�����ɹ�����false
		public bool Create(int rolesID,int[] TypeTree_ID)
		{			
			RolesConnect rolesConnect = new RolesConnect();
			foreach (int i in TypeTree_ID)
			{
				rolesConnect.Create(rolesID,i);				
			}			
			return true;
		}
		
		// ���ܣ�ɾ������
		// ���룺rolesID ,TypeTree_ID
		// ������ɹ�����true�����ɹ�����false
		public bool Delete(int rolesID,int TypeTree_ID)
		{
			String sql = "delete Content_RolesConnect where Roles_ID = " + rolesID + " and TypeTree_ID = " + TypeTree_ID;

            return Tools.DoSql(sql);		
		}

		// ���ܣ�ɾ������
		// ���룺rolesID
		// ������ɹ�����true�����ɹ�����false
		public bool Delete(int rolesID)
		{
			String sql = "delete Content_RolesConnect where Roles_ID = " + rolesID ;
            return Tools.DoSql(sql);
		}
				
		// ���ܣ���������
		// ���룺rolesID , TypeTree_ID����
		// ������ɹ�����true�����ɹ�����false
		public bool Update(int rolesID,int[] TypeTree_ID)
		{
			RolesConnect rolesConnect = new RolesConnect();
			rolesConnect.Delete(rolesID);
			foreach (int i in TypeTree_ID)
			{
				rolesConnect.Create(rolesID,i);
			}			
			return true;
		}		
		
		// ���ܣ����ݴ����rolesID��ʼ����RolesConnect
		// ���룺rolesID 
		// ������ɹ�����true�����ɹ�����false
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
		
		// ���ܣ�������������ݿ����Ƿ��Ѿ�����
		// ���룺rolesID,TypeTree_ID 
		// ��������ڷ���true�������ڷ���false
		public bool IsExist(int rolesID, int TypeTree_ID)
		{
			SqlDataReader reader = null;
			string sql=" select  * from  Content_RolesConnect where Roles_ID = " + rolesID + " and TypeTree_ID = " + TypeTree_ID;
			reader= Tools.DoSqlReader(sql);
			if(reader.Read())
			{
				reader.Close();
				return true;
			}
			else
			{
				reader.Close();
				return false;
			}
		}
		
		// ���ܣ����ݴ����rolesID�õ�Ȩ����Ŀ¼��Ӧ��RolesConnect��������
		// ���룺rolesID 
		// ���������RolesConnect��������
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
