//------------------------------------------------------------------------------
// ������ʶ: Copyright (C) 2008 Gomye.com.cn ��Ȩ����
// ��������: Galen Mu ������ 2008-7-9
//
// ��������: ��������Ĳ���
//
// ���޸�����:
// δ�޸�����:
// �޸ļ�¼
//       1   2008-7-9 ���ע��
//       2   2008-8-27 �ı����ݷ��ʲ�д��
//                     ɾ��Updata����
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient ;
using System.Collections;
using System.Data.Common;
using GCMSClassLib.Public_Cls;

namespace GCMSClassLib.SystemCls
{
	/// <summary>
	/// System ��ժҪ˵����
	/// </summary>
	public class SystemCls
	{
		public SystemCls()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
        }

        #region ʵ�嶨��
        private String m_System_Name;
		public String System_Name
		{
			get { return m_System_Name;}
			set { m_System_Name=value;}
		}
		private String m_System_Tools;
		public String System_Tools
		{
			get { return m_System_Tools;}
			set { m_System_Tools=value;}
		}
		private String m_JMail_MailServerUserName;
		public String JMail_MailServerUserName
		{
			get { return m_JMail_MailServerUserName;}
			set { m_JMail_MailServerUserName=value;}
		}
		private String m_JMail_MailServerPassWord;
		public String JMail_MailServerPassWord
		{
			get { return m_JMail_MailServerPassWord;}
			set { m_JMail_MailServerPassWord=value;}
		}
		private String m_JMail_From;
		public String JMail_From
		{
			get { return m_JMail_From;}
			set { m_JMail_From=value;}
		}
		private String m_JMail_Server;
		public String JMail_Server
		{
			get { return m_JMail_Server;}
			set { m_JMail_Server=value;}
        }
        #endregion ʵ�嶨��

        #region �������ݿ����
        public bool Create()
		{
			string sql="insert into Content_system(System_Name,System_Tools,JMail_MailServerUserName,JMail_MailServerPassWord,JMail_From,JMail_Server) "+
				"values('"+this.System_Name+"','"+this.System_Tools+"','"+this.JMail_MailServerUserName+"','"+this.JMail_MailServerPassWord+"','"+this.JMail_From+"','"+this.JMail_Server+"')";
            return Tools.DoSql(sql);
		}
		
		 
		public bool Init()
		{
			SqlDataReader reader = null; 
			string sql="select System_Name,System_Tools,JMail_MailServerUserName,JMail_MailServerPassWord,JMail_From,JMail_Server from Content_system";
			reader= Tools.DoSqlReader(sql);
			if(reader.Read())
			{
				this.System_Name=reader["System_Name"].ToString();
				this.System_Tools=reader["System_Tools"].ToString();
				this.JMail_MailServerUserName=reader["JMail_MailServerUserName"].ToString();
				this.JMail_MailServerPassWord=reader["JMail_MailServerPassWord"].ToString();
				this.JMail_From=reader["JMail_From"].ToString();
				this.JMail_Server=reader["JMail_Server"].ToString();
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
    }
}
