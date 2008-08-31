//------------------------------------------------------------------------------
// ������ʶ: Copyright (C) 2008 Gomye.com.cn ��Ȩ����
// ��������: Galen Mu ������ 2008-7-9
//
// ��������: ��չ��Ĳ���
//
// ���޸�����:
// δ�޸�����:
//      1 �ƺ���Type_TypeTree.cs��չ�ֶι����ص���
// �޸ļ�¼
//       1   2008-7-9 ���ע��
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
	/// ContentTable ��ժҪ˵����
	/// </summary>
	public class ContentTable
	{
		//private DataConn data;
        //private string sXml = "";
        #region ʵ�嶨��
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
        #endregion ʵ�嶨��

        #region �����ݿ��������
        private string sSQL;
        /// <summary>
		/// ������SQL
		///����������ӣ�CreateColumn = message varchar(5000)
		/// </summary>
		public string cCreateTable()//#�˴������⺬������,�ع�ʱע��#
		{
			sSQL = "create table "+this.TableName+" (Content_ID int,TypeTree_id int,Content_PId int,Author  varchar(50),Status int,Clicks int,OrderNum int,lockedby varchar(50),User_ID int,AtTop int,PublishDate datetime,SubmitDate datetime,Url varchar(200))";	
			return sSQL;
		}
			
		/// <summary>
		/// �޸ı���
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
			
            switch (iStatus)//#�˴������⺬������,�ع�ʱע��#
			{
				case 0: //�����ֶ�
                    sSQL = "alter table " + this.TableName + " add [" + this.ColumnName + "] " + this.ColumnType + "";
					break;
				case 1: //ɾ���ֶ�
                    sSQL = "alter table " + this.TableName + " drop COLUMN [" + this.ColumnName + "]";
					break;
				case 2: //�޸��ֶ�
                    sSQL = "exec  sp_rename   '" + this.TableName + "." + this.ColumnName + "','" + this.ColumnNewName + "', N'COLUMN'";
					break;
				case 3://�޸��ֶ�����
                    sSQL = "ALTER TABLE " + this.TableName + " ALTER COLUMN [" + this.ColumnName + "] " + this.ColumnType;
					break;

			}
			return sSQL;
        }
        #endregion �����ݿ��������
    }
}
