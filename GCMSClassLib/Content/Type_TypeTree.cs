//------------------------------------------------------------------------------
// ������ʶ: Copyright (C) 2008 Gomye.com.cn ��Ȩ����
// ��������: Galen Mu ������ 2008-7-8
//
// ��������: ��Ŀ���Ͷ���
//
// ���޸�����:
//       1   CreateTypeTreeXml���������⣬һ���᷵��Flase
//       ����취��ɾ��,�ѷ����ú���
//------------------------------------------------------------------------------
//       2   UpSchema�����в�������
//       ����취��ɾ��,�ѷ����ú���
//------------------------------------------------------------------------------
//       3   ��չ�ֶ�TypeAddFields�࣬Ӧ�õ����ļ��г�    
//       ����취���½� TypeAddFields.cs
//------------------------------------------------------------------------------
//       4   Ȩ����صķ�װӦ�ö��� 
//       ����취����RoleConnect�Ȳ���RoleConnect��ķ����Ƶ�RoleConnect.cs��
//------------------------------------------------------------------------------
//       4    MakeID����Ӧ�ù��ã�������װ
//       ����취��Tools.QueryMaxId
//------------------------------------------------------------------------------
// δ�޸�����:
//       1   Channels������TypeTree_Xml�����壿  
//       3   SelectAllSonTree�Ⱥ������ܼ����ݹ��ȡ�ӽڵ�
//       4   IDSonTypeTree���������⣬�����ݿ���ɵݹ���̻�Ͽ�
//       5    IsExistDoc ����δ���
// �޸ļ�¼
//       1   2008-7-9 ���ע��
//       2   2008-7-11 ɾ��CreateTypeTreeXml��UpSchema������ɾ��TypeAddFields��
//       3   2008-8-27 �޸����ݷ��ʲ㷽��д��
//       4   2008-9-4 ��RoleConnect,Create�Ȳ���RoleConnect��ķ����Ƶ�RoleConnect.cs�С�
//       5   2009-9-13 ���HasExtentFields,IsFullExtenFields�������ԣ������жϽڵ�����
//       6   2009-9-15 ��� MainFieldTableName,ExtentFieldTableName ���ԣ���ʾ��ǰ��Ŀ��չ������,��������
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Collections;
using System.Data.Common;
using System.Data.SqlClient;
using GCMSClassLib.Public_Cls;
using System.Xml;

namespace GCMSClassLib.Content
{
	/// <summary>
	/// Type_TypeTree ��ժҪ˵����
	/// </summary>
	public class Type_TypeTree
	{
		public Type_TypeTree()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
        }
        #region ʵ�嶨��
        private int m_TypeTreeID;
		public int TypeTree_ID
		{			
			get	{ return m_TypeTreeID; }
			set { m_TypeTreeID = value; }
		}		

		private int m_TypeTreeParentID;
		public int TypeTreeParentID
		{
			get { return m_TypeTreeParentID; }
			set { m_TypeTreeParentID = value; }
		}

		private string m_TypeTreeCName;
		public string TypeTreeCName
		{
			get { return m_TypeTreeCName; }
			set { m_TypeTreeCName = value; }
		}

		private string m_TypeTreeEName;
		public string TypeTreeEName
		{
			get { return m_TypeTreeEName; }
			set { m_TypeTreeEName = value; }
		}

		private string m_TypeTreeExplain;
		public string TypeTreeExplain
		{
			get { return m_TypeTreeExplain; }
			set { m_TypeTreeExplain = value; }
		}

		private int m_TypeTreeOrderNum;
		public int TypeTreeOrderNum
		{
			get { return m_TypeTreeOrderNum; }
			set { m_TypeTreeOrderNum = value; }
		}

		private int m_TypeTreeIssuance;
		public int TypeTreeIssuance
		{
			get { return m_TypeTreeIssuance; }
			set { m_TypeTreeIssuance = value; }
		}

		private string m_TypeTreeURL;
		public string TypeTreeURL
		{
			get { return m_TypeTreeURL; }
			set { m_TypeTreeURL = value; }
		}

		private string m_TypeTreeTemplate;
		public string TypeTreeTemplate
		{
			get { return m_TypeTreeTemplate; }
			set { m_TypeTreeTemplate = value; }
		}

		private string m_TypeTreePictureURL;
		public string TypeTreePictureURL
		{
			get { return m_TypeTreePictureURL; }
			set { m_TypeTreePictureURL = value; }
		}

		private string m_TypeTreeListTemplate;
		public string TypeTreeListTemplate
		{
			get { return m_TypeTreeListTemplate; }
			set { m_TypeTreeListTemplate = value; }
		}

		private string m_TypeTreeListURL;
		public string TypeTreeListURL
		{
			get { return m_TypeTreeListURL; }
			set { m_TypeTreeListURL = value; }
		}

		private int m_Listamount;
		public int Listamount
		{
			get { return m_Listamount; }
			set { m_Listamount = value; }
		}

		private string m_TypeTreeImages;
		public string TypeTreeImages
		{
			get { return m_TypeTreeImages; }
			set { m_TypeTreeImages = value; }
		}

		private string m_CountContent;
		public string CountContent
		{
			get { return m_CountContent; }
			set { m_CountContent = value; }
		}

		private string m_SumContent;
		public string SumContent
		{
			get { return m_SumContent; }
			set { m_SumContent = value; }
		}

		private string m_TypeTree_Language;
		public string TypeTree_Language
		{
			get { return m_TypeTree_Language; }
			set { m_TypeTree_Language = value; }
		}
		
		private int m_TypeTree_Type;
		public int TypeTree_Type
		{
			get { return m_TypeTree_Type; }
			set { m_TypeTree_Type = value; }
		}

		private string m_TypeTree_Xml;
		public string TypeTree_Xml
		{
			get { return m_TypeTree_Xml; }
			set { m_TypeTree_Xml = value; }
		}		
		
		private int m_TTypeTree_TypeFields;
		public int TypeTree_TypeFields
		{
			get { return m_TTypeTree_TypeFields; }
			set { m_TTypeTree_TypeFields = value; }
		}
		
		private int m_TypeTree_ContentFields;
		public int TypeTree_ContentFields
		{
			get { return m_TypeTree_ContentFields; }
			set { m_TypeTree_ContentFields = value; }
		}

		private string m_MailMsg;
		public string MailMsg
		{
			get { return m_MailMsg; }
			set { m_MailMsg = value; }
		}	
	
		private string m_TypeTree_Show;
		public string TypeTree_Show
		{
			get { return m_TypeTree_Show; }
			set { m_TypeTree_Show = value; }
		}


		private string m_TypeTree_XMLContent;
		public string TypeTree_XMLContent
		{
			get { return m_TypeTree_XMLContent; }
			set { m_TypeTree_XMLContent = value; }
        }

        #endregion ʵ�嶨��

        #region �������ݿ����
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns>�ɹ�����true�����ɹ�����false</returns>
		public bool Create()
		{
			int max_id =Tools.QueryMaxID("TypeTree_ID");
			string sql = "insert into Content_Type_TypeTree " +
				"(TypeTree_ID,TypeTree_ParentID,TypeTree_CName,TypeTree_EName,TypeTree_Explain,"+
				"TypeTree_OrderNum,TypeTree_Issuance,TypeTree_URL,TypeTree_Template,TypeTree_PictureURL,"+
				"TypeTree_ListTemplate,TypeTree_ListURL,List_amount,TypeTree_Images,TypeTree_Language,TypeTree_Type,TypeTree_TypeFields,TypeTree_ContentFields,MailMsg) " +
				"values( " +
				max_id + "," +
				this.TypeTreeParentID + "," +
				"'" + this.TypeTreeCName + "'," +
				"'" + this.TypeTreeEName + "'," +
				"'" + this.TypeTreeExplain + "'," +
				max_id + "," +
				this.TypeTreeIssuance + "," +
				"'" + this.TypeTreeURL + "'," +
				"'" + this.TypeTreeTemplate + "'," +
				"'" + this.TypeTreePictureURL + "'," +
				"'" + this.TypeTreeListTemplate + "'," +
				"'" + this.TypeTreeListURL + "'," +
				this.Listamount + "," +
				"'" + this.TypeTreeImages + "'," +
				"'" + this.TypeTree_Language + "'," +
				this.TypeTree_Type +"," + this.TypeTree_TypeFields +"," + this.TypeTree_ContentFields + ",'"+ this.MailMsg + "'" +
				")";
			 bool res = Tools.DoSql(sql);

			//ͬʱ����Content_ID���е�TypeTree_ID��ֵ
			this.TypeTree_ID = max_id;

            Tools.UpdateMaxID("TypeTree_ID");
		    // #�˴����п��Ż�������, �ع�ʱע��#
            return res;
		}
		
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <returns>�ɹ�����true�����ɹ�����false</returns>
		public bool Delete(int TypeTree_ID)
		{
			Type_TypeTree typeTree = new Type_TypeTree();
			typeTree.Init(TypeTree_ID);			
			int TypeTree_OrderNum = 0;
			int TypeTree_ParentID = 0;
			
			//�õ�TypeTree_OrderNum��TypeTree_ParentID
			TypeTree_OrderNum = int.Parse(typeTree.TypeTreeOrderNum.ToString());
			TypeTree_ParentID = int.Parse(typeTree.TypeTreeParentID.ToString());
			
			//�õ���ص�TypeTree_IDֵ
			string strSql = "select TypeTree_ID from Content_Type_TypeTree where TypeTree_OrderNum >" + TypeTree_OrderNum + " AND TypeTree_ParentID=" + TypeTree_ParentID;
			SqlDataReader reader  = Tools.DoSqlReader(strSql);
			while(reader.Read())
			{	
				//������ص�TypeTree_OrderNumֵ
				typeTree.UpdateTypeTreeOrderNum(int.Parse(reader["TypeTree_ID"].ToString()));
			}			
			reader.Close();

			//ִ��ɾ��Ŀ¼
			string sql = "delete from Content_Type_TypeTree where TypeTree_ID = " + TypeTree_ID;
            return Tools.DoSql(sql);
		}

		/// <summary>
        /// ��������
		/// </summary>
		/// <param name="TypeTree_ID"></param>
        /// <returns>�ɹ�����true�����ɹ�����fals</returns>
		public bool Update(int TypeTree_ID)
		{
			string sql = "update Content_Type_TypeTree set " + 						 
				"TypeTree_CName = '" + this.TypeTreeCName + "', " +
				"TypeTree_EName = '" + this.TypeTreeEName + "', " +
				"TypeTree_Explain = '" + this.TypeTreeExplain + "', " +						 
				"TypeTree_Issuance = " + this.TypeTreeIssuance + ", " +	
				"TypeTree_URL = '" + this.TypeTreeURL + "', " +		
				"TypeTree_Template = '" + this.TypeTreeTemplate + "', " +		
				"TypeTree_ListURL = '" + this.TypeTreeListURL + "', " +		
				"TypeTree_PictureURL = '" + this.TypeTreePictureURL + "', " +
				"TypeTree_ListTemplate = '" + this.TypeTreeListTemplate + "', " +
				"TypeTree_Language = '" + this.TypeTree_Language + "', " +
				"List_amount = " + this.Listamount + ", " +
				"TypeTree_Type = " + this.TypeTree_Type + ", " +
				"TypeTree_Xml = '" + this.TypeTree_Xml+ "', " + 
				"MailMsg = '" + this.MailMsg+ "', " + 
				"TypeTree_Images = '" + this.TypeTreeImages + "' " + 
				"where TypeTree_ID = " + TypeTree_ID ;

            return Tools.DoSql(sql);
		}

	    /// <summary>
        /// ���ݴ����TypeTree_ID��ʼ����TypeTree
	    /// </summary>
	    /// <param name="TypeTree_ID"></param>
        /// <returns>�ɹ�����true�����ɹ�����false	</returns>
		public bool Init(int TypeTree_ID)
		{
			string sql = "select TypeTree_ID,TypeTree_ParentID,TypeTree_CName,TypeTree_EName,TypeTree_Explain," +
				"TypeTree_OrderNum,TypeTree_Issuance,TypeTree_URL,TypeTree_Template,TypeTree_PictureURL,"+
				"TypeTree_ListTemplate,TypeTree_ListURL,List_amount,TypeTree_Images,isnull(TypeTree_Type,'0') TypeTree_Type,isnull(TypeTree_Language,'GB2312') TypeTree_Language,isnull(TypeTree_Xml,'') TypeTree_Xml, " +
				"isnull(TypeTree_TypeFields,'0') TypeTree_TypeFields,isnull(TypeTree_ContentFields,'0') TypeTree_ContentFields,MailMsg ,TypeTree_Show ,TypeTree_XMLContent "+
				"from Content_Type_TypeTree "+
				"where TypeTree_ID = " + TypeTree_ID;
			SqlDataReader reader = Tools.DoSqlReader(sql);
            bool success = false;
			if(reader.Read())
			{
				this.TypeTree_ID = int.Parse(reader["TypeTree_ID"].ToString());
				this.TypeTreeParentID = int.Parse(reader["TypeTree_ParentID"].ToString());
				this.TypeTreeCName = reader["TypeTree_CName"].ToString();
				this.TypeTreeEName = reader["TypeTree_EName"].ToString();
				this.TypeTreeExplain = reader["TypeTree_Explain"].ToString();
				this.TypeTreeOrderNum = int.Parse(reader["TypeTree_OrderNum"].ToString());
				this.TypeTreeIssuance = int.Parse(reader["TypeTree_Issuance"].ToString());
				this.TypeTreeURL = reader["TypeTree_URL"].ToString();
				this.TypeTreeTemplate = reader["TypeTree_Template"].ToString();
				this.TypeTreePictureURL = reader["TypeTree_PictureURL"].ToString();
				this.TypeTreeListTemplate = reader["TypeTree_ListTemplate"].ToString();
				this.TypeTreeListURL = reader["TypeTree_ListURL"].ToString();
				this.Listamount = int.Parse(reader["List_amount"].ToString());
				this.TypeTree_Type = int.Parse(reader["TypeTree_Type"].ToString());
				this.TypeTreeImages = reader["TypeTree_Images"].ToString();
				this.TypeTree_Language = reader["TypeTree_Language"].ToString();
				this.TypeTree_Xml = reader["TypeTree_Xml"].ToString();
				this.TypeTree_TypeFields =  int.Parse(reader["TypeTree_TypeFields"].ToString());
				this.TypeTree_ContentFields =  int.Parse(reader["TypeTree_ContentFields"].ToString());
				this.MailMsg = reader["MailMsg"].ToString();
				this.TypeTree_Show = reader["TypeTree_Show"].ToString();
				this.TypeTree_XMLContent = reader["TypeTree_XMLContent"].ToString();
				reader.Close();
                success = true;
			}
			else
			{
				reader.Close();
                success = false;
			}
            //-----------------------------------���������Ŀ��չ�ֶ�����--------------------------------
            if (HasExtentFields)
            {
                Content_FieldsName contentfieldsname = new Content_FieldsName();
                contentfieldsname.Init(TypeTree_ContentFields == 0 ? TypeTree_TypeFields : TypeTree_ContentFields);
                m_extentfieldstablename = "ContentUser_" + contentfieldsname.FieldsBase_Name;
                
            }
            return success;

		}
        private Type_TypeTree InitTypeTreeFromReader(SqlDataReader reader)
        {
            Type_TypeTree _typetree = new Type_TypeTree();
            _typetree.TypeTree_ID = int.Parse(reader["TypeTree_ID"].ToString());
            _typetree.TypeTreeParentID = int.Parse(reader["TypeTree_ParentID"].ToString());
            _typetree.TypeTreeCName = reader["TypeTree_CName"].ToString();
            _typetree.TypeTreeEName = reader["TypeTree_EName"].ToString();
            _typetree.TypeTreeExplain = reader["TypeTree_Explain"].ToString();
            _typetree.TypeTreeOrderNum = int.Parse(reader["TypeTree_OrderNum"].ToString());
            _typetree.TypeTreeIssuance = int.Parse(reader["TypeTree_Issuance"].ToString());
            _typetree.TypeTreeURL = reader["TypeTree_URL"].ToString();
            _typetree.TypeTreeTemplate = reader["TypeTree_Template"].ToString();
            _typetree.TypeTreePictureURL = reader["TypeTree_PictureURL"].ToString();
            _typetree.TypeTreeListTemplate = reader["TypeTree_ListTemplate"].ToString();
            _typetree.TypeTreeListURL = reader["TypeTree_ListURL"].ToString();
            _typetree.Listamount = int.Parse(reader["List_amount"].ToString());
            _typetree.TypeTreeImages = reader["TypeTree_Images"].ToString();
            return _typetree;
        }
        /// <summary>
        /// �õ����е�Type_TypeTree��������
        /// </summary>
        /// <returns>����Type_TypeTree��������	</returns>
		public System.Collections.ArrayList SelectAll()		
		{
			System.Collections.ArrayList list = new System.Collections.ArrayList();

			string sql = "select TypeTree_ID,TypeTree_ParentID,TypeTree_CName,TypeTree_EName,TypeTree_Explain," +
				"TypeTree_OrderNum,TypeTree_Issuance,TypeTree_URL,TypeTree_Template,TypeTree_PictureURL,"+
				"TypeTree_ListTemplate,TypeTree_ListURL,List_amount,TypeTree_Images " +
				"from Content_Type_TypeTree ";

            SqlDataReader reader = Tools.DoSqlReader(sql);
			while(reader.Read())
			{
				list.Add(InitTypeTreeFromReader(reader));
			}

			reader.Close();
			return list;
        }
        #endregion �������ݿ����

        #region ������Ŀ��غ���
        /// <summary>
        /// �õ����еĸ�Ŀ¼��Type_TypeTree��������
        /// </summary>
        /// <returns>���ظ�Ŀ¼Type_TypeTree��������</returns>
		public System.Collections.ArrayList SelectAllParentTree()		
		{
			System.Collections.ArrayList list = new System.Collections.ArrayList();

			string sql = "select TypeTree_ID,TypeTree_ParentID,TypeTree_CName,TypeTree_EName,TypeTree_Explain," +
				"TypeTree_OrderNum,TypeTree_Issuance,TypeTree_URL,TypeTree_Template,TypeTree_PictureURL,"+
				"TypeTree_ListTemplate,TypeTree_ListURL,List_amount,TypeTree_Images " +
				"from Content_Type_TypeTree " +
                "where TypeTree_ParentID = 0";//#�˴������⺬������,�ع�ʱע��#

			SqlDataReader reader  = Tools.DoSqlReader(sql);
			while(reader.Read())
			{
                list.Add(InitTypeTreeFromReader(reader));
			}

			reader.Close();
			return list;
		}

        /// <summary>
        /// ���ݴ���ĸ�Ŀ¼��ID�õ���Ŀ¼��Type_TypeTree��������
        /// </summary>
        /// <param name="typeTreeParentID">��Ŀ¼ID typeTreeParentID</param>
        /// <returns>��Ŀ¼��Type_TypeTree��������	</returns>
		public System.Collections.ArrayList SelectAllSonTree(int typeTreeParentID)		
		{
			System.Collections.ArrayList list = new System.Collections.ArrayList();

			string sql = "select TypeTree_ID,TypeTree_ParentID,TypeTree_CName,TypeTree_EName,TypeTree_Explain," +
				"TypeTree_OrderNum,TypeTree_Issuance,TypeTree_URL,TypeTree_Template,TypeTree_PictureURL,"+
				"TypeTree_ListTemplate,TypeTree_ListURL,List_amount,TypeTree_Images " +
				"from Content_Type_TypeTree " +
				"where TypeTree_ParentID = " + typeTreeParentID ;

			SqlDataReader reader  = Tools.DoSqlReader(sql);
			while(reader.Read())
			{
                list.Add(InitTypeTreeFromReader(reader));
			}

			reader.Close();
			return list;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <param name="Words"></param>
        /// <returns></returns>
		public string Channels(int TypeTree_ID,string Words)
		{

			Type_TypeTree _Type_TypeTree = new Type_TypeTree() ;
			_Type_TypeTree.Init (TypeTree_ID);
			string txtInitXML="";
			XmlDocument xmlDoc=new XmlDocument(); 

			if (_Type_TypeTree.TypeTree_Xml !="")
			{
				xmlDoc.LoadXml(_Type_TypeTree.TypeTree_Xml);
				XmlNodeList nodeList=xmlDoc.SelectSingleNode("Employees").ChildNodes; 
				foreach(XmlNode xn in nodeList)//���������ӽڵ� 
				{ 
					XmlElement xe=(XmlElement)xn;//���ӽڵ�����ת��ΪXmlElement���� 
					if(xe.GetAttribute("GName")==Words)//���genre����ֵΪ�������� 
					{ 
						XmlNodeList nls=xe.ChildNodes;//������ȡxe�ӽڵ�������ӽڵ� 
						foreach(XmlNode xn1 in nls)//���� 
						{ 
							XmlElement xe2=(XmlElement)xn1;//ת������ 
							if(xe2.Name=="Value")//����ҵ� 
							{ 
								txtInitXML = xe2.InnerText;
							}
						}
				
					} 
				}
			}

			return txtInitXML;
		}

        /// <summary>
        /// �õ�Content_Type_TypeTree���и���TypeTree_ID�õ����TypeTree_OrderNumֵ
        /// </summary>
        /// <param name="typeTree_ID"></param>
        /// <returns>�������TypeTree_OrderNumֵ</returns>
		public int  QueryMaxTypeTreeOrderNum(int typeTree_ID)
		{
			int Max_Id = 0;
			SqlDataReader reader = null; 
			string sql = "select  max(TypeTree_OrderNum) Max_Id from Content_Type_TypeTree where TypeTree_ParentID = " + typeTree_ID;
			
			reader=Tools.DoSqlReader(sql);
		    
			if(reader.Read())
			{		
				if(reader["Max_Id"]==DBNull.Value)
				{
					Max_Id = 1;
				}
				else
				{					
					Max_Id = int.Parse(reader["Max_Id"].ToString())+1;
				}
			}
			reader.Close();
			return Max_Id;
		}

        /// <summary>
        /// ����Content_Type_TypeTree���е�TypeTree_OrderNum��ֵ
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <returns>���³ɹ�������true�����򣬷���false</returns>
		public bool UpdateTypeTreeOrderNum(int TypeTree_ID)
		{
			string sql = "update Content_Type_TypeTree set TypeTree_OrderNum = TypeTree_OrderNum-1 where TypeTree_ID  = " + TypeTree_ID;
            return Tools.DoSql(sql);
		}

        /// <summary>
        /// �ж�Ŀ¼���Ƿ������Ŀ¼
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <returns>������Ŀ¼������true�����򣬷���false</returns>
		public bool IsExistSonType(int TypeTree_ID)
		{
			string sql = "select TypeTree_ID from Content_Type_TypeTree where TypeTree_ParentID = " + TypeTree_ID;
            SqlDataReader reader = reader = Tools.DoSqlReader(sql);
            bool res = reader.Read();
            reader.Close();
            return res;
		}

        /// <summary>
        /// �ж�Ŀ¼���Ƿ��������
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <returns>�������£�����true�����򣬷���false</returns>
		public bool IsExistDoc(int TypeTree_ID)
		{
            string sql = " ";//#δ��ɴ���#
            SqlDataReader reader  = Tools.DoSqlReader(sql);

			bool res = reader.Read();
            reader.Close();
            return res;
		}

		public string txtstrSonTypeTree = "";			
        /// <summary>
        /// �õ���ǰĿ¼����Ŀ¼����
        /// </summary>
        /// <param name="TypeTree_ID">TypeTree_ID</param>
        /// <returns>��ǰĿ¼����Ŀ¼����</returns>
        public string strSonTypeTree(int TypeTree_ID)//#�˴����п��Ż�������, �ع�ʱע��#
		{			
			SqlDataReader reader = null;
			string sql = "select TypeTree_CName,TypeTree_ID from Content_Type_TypeTree where TypeTree_ParentID = " + TypeTree_ID;

			reader = Tools.DoSqlReader(sql);

			while(reader.Read())
			{
				txtstrSonTypeTree = txtstrSonTypeTree + reader["TypeTree_CName"].ToString() + "," ;
				strSonTypeTree(int.Parse(reader["TypeTree_ID"].ToString()));

			}

			reader.Close();
			
			if(!txtstrSonTypeTree.Equals(""))
			{
				txtstrSonTypeTree = txtstrSonTypeTree.ToString().Trim();
			}
			else
			{
                txtstrSonTypeTree = "���ڵ�û���ӽڵ�";////#�˴����ı�,�����Ի�ʱע��#
			}
			return txtstrSonTypeTree;
		}

		public string txtIDSonTypeTree = "";	
        /// <summary>
        /// �õ���ǰĿ¼����Ŀ¼���ƣ������ݹ��ȡ
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <returns>��ǰĿ¼����Ŀ¼ID�����ŷָ�</returns>
		public string IDSonTypeTree(int TypeTree_ID)
		{			
			SqlDataReader reader = null;
			string sql = "select TypeTree_ID from Content_Type_TypeTree where TypeTree_ParentID = " + TypeTree_ID;

			reader = Tools.DoSqlReader(sql);

			while(reader.Read())//�ݹ����
			{
				txtIDSonTypeTree = txtIDSonTypeTree + reader["TypeTree_ID"].ToString() + ",";
				IDSonTypeTree(int.Parse(reader["TypeTree_ID"].ToString()));

			}

			reader.Close();
			return txtIDSonTypeTree + TypeTree_ID.ToString();
		}


        /// <summary>
        /// �õ���ǰĿ¼�ĸ�Ŀ¼����
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <returns��ǰĿ¼�ĸ�Ŀ¼></returns>
		public string IDParentTypeTree(int TypeTree_ID)
		{			
			SqlDataReader reader = null;
			string sql = "select TypeTree_ParentID from Content_Type_TypeTree where TypeTree_ID = " + TypeTree_ID;

			reader = Tools.DoSqlReader(sql);

			while(reader.Read())
			{
				txtIDSonTypeTree = txtIDSonTypeTree + reader["TypeTree_ParentID"].ToString() + ",";
				IDParentTypeTree(int.Parse(reader["TypeTree_ParentID"].ToString()));

			}

			reader.Close();

			return txtIDSonTypeTree + TypeTree_ID.ToString();
		}


        /// <summary>
        /// �õ���ǰĿ¼����Ŀ¼����
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <returns>��ǰĿ¼����Ŀ¼,���ŷָ���ID</returns>
		public string SonTypeTree(int TypeTree_ID)
		{
			SqlDataReader reader = null;
			string intId ="";
			string sql = "select TypeTree_ID from Content_Type_TypeTree where TypeTree_ParentID = " + TypeTree_ID;
			reader = Tools.DoSqlReader(sql);
			while(reader.Read())
			{
				intId = intId + "," + reader["TypeTree_ID"].ToString();
			}
			reader.Close();
			return intId;

		}

        /// <summary>
        /// �õ���ǰĿ¼����Ŀ¼�������µ����
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <returns>��ǰĿ¼����Ŀ¼</returns>
		public int ThisSumTypeTree(int TypeTree_ID)
		{
			SqlDataReader reader = null;
			string sql = "select Count(Clicks) as CountContent , isnull(Sum(Clicks),'0') as SumContent from Content_Content where Status in (1,2,3,4,5) and TypeTree_ID in ("+ IDSonTypeTree(TypeTree_ID) +")";
			reader = Tools.DoSqlReader(sql);
			while(reader.Read())
			{
				this.CountContent	= reader["CountContent"].ToString();

				if (! reader["SumContent"].Equals(null))

				{this.SumContent	= reader["SumContent"].ToString();}
		}
			reader.Close();
			return int.Parse(this.SumContent);


		}

        /// <summary>
        /// ĳ�û��ķ�������
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>��ǰĿ¼����Ŀ¼ID</returns>
		public int UserCount(string UserName , string startDate ,string endDate)
		{
			string sqls ="";
            if (startDate != "0" && endDate != "0")//#�˴������⺬������,�ع�ʱע��#
			{
				sqls = " and (PublishDate between '"+startDate+"'and '"+endDate+"')";
			}
			string sql = "select Count(Clicks) as CountContent from Content_Content where Status in (1,2,3,4,5) and Author = '"+UserName+"'"+sqls;
			SqlDataReader reader = Tools.DoSqlReader(sql);
			while(reader.Read())
			{
				this.CountContent	= reader["CountContent"].ToString();
			}
			reader.Close();
			return int.Parse(CountContent);
        }
        #endregion ������Ŀ��غ���

        #region Ȩ����غ���
        // --------�ж�Ȩ��----------------------------------------------------------------------------------------------------------------------------

		// ���ܣ��Ƿ�߱��ӽڵ�
		// ���룺
		// ������ɹ�����true�����ɹ�����false
		public bool HaveSon(int TypeTree_ID)
		{
			string sql = "select * from Content_Type_TypeTree where TypeTree_ParentID = "+ TypeTree_ID;
			SqlDataReader reader  = Tools.DoSqlReader(sql);
            //#�˴����п��Ż�������, �ع�ʱע��#
            bool res = reader.Read();
            reader.Close();
            return res;
		}

		// ���ܣ�ĳȨ�����Ƿ�߱��ӽڵ�
		// ���룺
		// ������ɹ�����true�����ɹ�����false
		public bool SbHaveSon(int TypeTree_ID,int SessionID)
		{
			string sql = "SELECT Content_Type_TypeTree.* FROM Content_Type_TypeTree , Content_RolesConnect WHERE Content_RolesConnect.Roles_ID = "+ SessionID +" and Content_RolesConnect.TypeTree_ID=Content_Type_TypeTree.TypeTree_ID and Content_Type_TypeTree.TypeTree_ParentID= "+TypeTree_ID+" ORDER BY Content_Type_TypeTree.TypeTree_OrderNum";

			SqlDataReader reader = Tools.DoSqlReader(sql);
            //#�˴����п��Ż�������, �ع�ʱע��#
            bool res = reader.Read();
            reader.Close();
            return res;
		}
        #endregion Ȩ����غ���

        #region �����߼������ж�

        public bool HasExtentFields
        {
            get { return m_TTypeTree_TypeFields != 0 || m_TypeTree_ContentFields != 0; }
        }

        public bool IsFullExtenFields
        {
            get { return m_TypeTree_Type == 2; }
        }

        public bool IsCommonPublish
        {
            get { return m_TypeTree_Type == 0; }
        }



        /// <summary>
        /// ��ʾ��ǰ״̬���ַ�������
        /// </summary>
        /// <param name="typeTreeIssuance"></param>
        /// <returns>�������Ӧ���ַ�����</returns>
        public string strTypeTreeIssuance(int typeTreeIssuance)
        {
            string strTypeTreeIssuance;

            switch (typeTreeIssuance)//#�˴����ı�,�����Ի�ʱע��#
            {
                case 0:
                    strTypeTreeIssuance = "�ر�( ǰ̨���ɼ�)";
                    break;

                case 1:
                    strTypeTreeIssuance = "���� (�ɱ༭)";
                    break;

                case 2:
                    strTypeTreeIssuance = "���� (���ɱ༭)";
                    break;

                default:
                    strTypeTreeIssuance = "���� (�ɱ༭)";
                    break;
            }

            return strTypeTreeIssuance;
        }

        /// <summary>
        /// ��ʾ��ǰ״̬���ַ�������
        /// </summary>
        /// <param name="typeTreeType">typeTreeIssuance</param>
        /// <returns>�������Ӧ���ַ�����</returns>
        public string strTypeTreeType(int typeTreeType)
        {
            string strTypeTreeType;

            switch (typeTreeType)//#�˴����ı�,�����Ի�ʱע��#
            {

                case 0:
                    strTypeTreeType = "���� (�������·���)";
                    break;

                case 1:
                    strTypeTreeType = "ӳ�� (ֻ��ӳ��������Ŀ������)";
                    break;

                case 2:
                    strTypeTreeType = "��չ (��ʹ�û����ֶ�)";
                    break;

                //				case 2:
                //					strTypeTreeType =  "ͼƬ (GPhotoϵͳ֧��)";
                //					break;

                //				case 3:
                //					strTypeTreeType =  "����Ӳ�� (������Ŀ��Ƶ��)";
                //					break;
                //
                //				case 4:
                //					strTypeTreeType =  "�̳� (GShopϵͳ֧��)";
                //					break;
                //
                //				case 5:
                //					strTypeTreeType =  "���� (GBlogϵͳ֧��)";
                //					break;
                //
                //				case 6:
                //					strTypeTreeType =  "��̳ (GForumsϵͳ֧��)";
                //					break;

                default:
                    strTypeTreeType = "���� (�������·���)";
                    break;
            }

            return strTypeTreeType;
        }

        #endregion �����߼������ж�

        private string m_extentfieldstablename;
        public string MainFieldTableName
        {
            get
            {
                string f = "Content_Content";
                if (IsCommonPublish)
                    f = "Content_Content";
                else if (IsFullExtenFields&&!string.IsNullOrEmpty(m_extentfieldstablename))
                    f = m_extentfieldstablename;

                return f;

            }
        }
        public string ExtentFieldTableName
        {
            get { return m_extentfieldstablename; }
        }
    }

}
