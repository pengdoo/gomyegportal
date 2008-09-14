//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-7-8
//
// 功能描述: 栏目类型定义
//
// 已修改问题:
//       1   CreateTypeTreeXml函数有问题，一定会返回Flase
//       处理办法：删除,已废弃该函数
//------------------------------------------------------------------------------
//       2   UpSchema函数有并发隐患
//       处理办法：删除,已废弃该函数
//------------------------------------------------------------------------------
//       3   扩展字段TypeAddFields类，应该单独文件列出    
//       处理办法：新建 TypeAddFields.cs
//------------------------------------------------------------------------------
//       4   权限相关的封装应该独立 
//       处理办法：将RoleConnect等操作RoleConnect表的方法移到RoleConnect.cs中
//------------------------------------------------------------------------------
//       4    MakeID函数应该公用，单独封装
//       处理办法：Tools.QueryMaxId
//------------------------------------------------------------------------------
// 未修改问题:
//       1   Channels函数和TypeTree_Xml的意义？  
//       3   SelectAllSonTree等函数不能级联递归获取子节点
//       4   IDSonTypeTree有性能问题，由数据库完成递归过程会较快
//       5    IsExistDoc 方法未完成
// 修改记录
//       1   2008-7-9 添加注释
//       2   2008-7-11 删除CreateTypeTreeXml，UpSchema函数，删除TypeAddFields类
//       3   2008-8-27 修改数据访问层方法写法
//       4   2008-9-4 将RoleConnect,Create等操作RoleConnect表的方法移到RoleConnect.cs中。
//       5   2009-9-13 添加HasExtentFields,IsFullExtenFields两个属性，用来判断节点类型
//       6   2009-9-15 添加 MainFieldTableName,ExtentFieldTableName 属性，标示当前栏目扩展表名称,主表名称
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
	/// Type_TypeTree 的摘要说明。
	/// </summary>
	public class Type_TypeTree
	{
		public Type_TypeTree()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
        }
        #region 实体定义
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

        #endregion 实体定义

        #region 常用数据库操作
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <returns>成功返回true，不成功返回false</returns>
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

			//同时更新Content_ID表中的TypeTree_ID的值
			this.TypeTree_ID = max_id;

            Tools.UpdateMaxID("TypeTree_ID");
		    // #此处含有可优化的内容, 重构时注意#
            return res;
		}
		
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <returns>成功返回true，不成功返回false</returns>
		public bool Delete(int TypeTree_ID)
		{
			Type_TypeTree typeTree = new Type_TypeTree();
			typeTree.Init(TypeTree_ID);			
			int TypeTree_OrderNum = 0;
			int TypeTree_ParentID = 0;
			
			//得到TypeTree_OrderNum和TypeTree_ParentID
			TypeTree_OrderNum = int.Parse(typeTree.TypeTreeOrderNum.ToString());
			TypeTree_ParentID = int.Parse(typeTree.TypeTreeParentID.ToString());
			
			//得到相关的TypeTree_ID值
			string strSql = "select TypeTree_ID from Content_Type_TypeTree where TypeTree_OrderNum >" + TypeTree_OrderNum + " AND TypeTree_ParentID=" + TypeTree_ParentID;
			SqlDataReader reader  = Tools.DoSqlReader(strSql);
			while(reader.Read())
			{	
				//更新相关的TypeTree_OrderNum值
				typeTree.UpdateTypeTreeOrderNum(int.Parse(reader["TypeTree_ID"].ToString()));
			}			
			reader.Close();

			//执行删除目录
			string sql = "delete from Content_Type_TypeTree where TypeTree_ID = " + TypeTree_ID;
            return Tools.DoSql(sql);
		}

		/// <summary>
        /// 更新数据
		/// </summary>
		/// <param name="TypeTree_ID"></param>
        /// <returns>成功返回true，不成功返回fals</returns>
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
        /// 根据传入的TypeTree_ID初始化类TypeTree
	    /// </summary>
	    /// <param name="TypeTree_ID"></param>
        /// <returns>成功返回true，不成功返回false	</returns>
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
            //-----------------------------------载入相关栏目扩展字段属性--------------------------------
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
        /// 得到所有的Type_TypeTree对象数组
        /// </summary>
        /// <returns>返回Type_TypeTree对象数组	</returns>
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
        #endregion 常用数据库操作

        #region 树形栏目相关函数
        /// <summary>
        /// 得到所有的根目录的Type_TypeTree对象数组
        /// </summary>
        /// <returns>返回根目录Type_TypeTree对象数组</returns>
		public System.Collections.ArrayList SelectAllParentTree()		
		{
			System.Collections.ArrayList list = new System.Collections.ArrayList();

			string sql = "select TypeTree_ID,TypeTree_ParentID,TypeTree_CName,TypeTree_EName,TypeTree_Explain," +
				"TypeTree_OrderNum,TypeTree_Issuance,TypeTree_URL,TypeTree_Template,TypeTree_PictureURL,"+
				"TypeTree_ListTemplate,TypeTree_ListURL,List_amount,TypeTree_Images " +
				"from Content_Type_TypeTree " +
                "where TypeTree_ParentID = 0";//#此处有特殊含义数字,重构时注意#

			SqlDataReader reader  = Tools.DoSqlReader(sql);
			while(reader.Read())
			{
                list.Add(InitTypeTreeFromReader(reader));
			}

			reader.Close();
			return list;
		}

        /// <summary>
        /// 根据传入的父目录的ID得到子目录的Type_TypeTree对象数组
        /// </summary>
        /// <param name="typeTreeParentID">父目录ID typeTreeParentID</param>
        /// <returns>子目录的Type_TypeTree对象数组	</returns>
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
				foreach(XmlNode xn in nodeList)//遍历所有子节点 
				{ 
					XmlElement xe=(XmlElement)xn;//将子节点类型转换为XmlElement类型 
					if(xe.GetAttribute("GName")==Words)//如果genre属性值为“张三” 
					{ 
						XmlNodeList nls=xe.ChildNodes;//继续获取xe子节点的所有子节点 
						foreach(XmlNode xn1 in nls)//遍历 
						{ 
							XmlElement xe2=(XmlElement)xn1;//转换类型 
							if(xe2.Name=="Value")//如果找到 
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
        /// 得到Content_Type_TypeTree表中根据TypeTree_ID得到最大TypeTree_OrderNum值
        /// </summary>
        /// <param name="typeTree_ID"></param>
        /// <returns>返回最大TypeTree_OrderNum值</returns>
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
        /// 更新Content_Type_TypeTree表中的TypeTree_OrderNum的值
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <returns>更新成功，返回true，否则，返回false</returns>
		public bool UpdateTypeTreeOrderNum(int TypeTree_ID)
		{
			string sql = "update Content_Type_TypeTree set TypeTree_OrderNum = TypeTree_OrderNum-1 where TypeTree_ID  = " + TypeTree_ID;
            return Tools.DoSql(sql);
		}

        /// <summary>
        /// 判断目录中是否存在子目录
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <returns>存在子目录，返回true，否则，返回false</returns>
		public bool IsExistSonType(int TypeTree_ID)
		{
			string sql = "select TypeTree_ID from Content_Type_TypeTree where TypeTree_ParentID = " + TypeTree_ID;
            SqlDataReader reader = reader = Tools.DoSqlReader(sql);
            bool res = reader.Read();
            reader.Close();
            return res;
		}

        /// <summary>
        /// 判断目录中是否存在文章
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <returns>存在文章，返回true，否则，返回false</returns>
		public bool IsExistDoc(int TypeTree_ID)
		{
            string sql = " ";//#未完成代码#
            SqlDataReader reader  = Tools.DoSqlReader(sql);

			bool res = reader.Read();
            reader.Close();
            return res;
		}

		public string txtstrSonTypeTree = "";			
        /// <summary>
        /// 得到当前目录的子目录名称
        /// </summary>
        /// <param name="TypeTree_ID">TypeTree_ID</param>
        /// <returns>当前目录的子目录名称</returns>
        public string strSonTypeTree(int TypeTree_ID)//#此处含有可优化的内容, 重构时注意#
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
                txtstrSonTypeTree = "本节点没有子节点";////#此处有文本,多语言化时注意#
			}
			return txtstrSonTypeTree;
		}

		public string txtIDSonTypeTree = "";	
        /// <summary>
        /// 得到当前目录的子目录名称，级联递归获取
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <returns>当前目录的子目录ID，逗号分隔</returns>
		public string IDSonTypeTree(int TypeTree_ID)
		{			
			SqlDataReader reader = null;
			string sql = "select TypeTree_ID from Content_Type_TypeTree where TypeTree_ParentID = " + TypeTree_ID;

			reader = Tools.DoSqlReader(sql);

			while(reader.Read())//递归过程
			{
				txtIDSonTypeTree = txtIDSonTypeTree + reader["TypeTree_ID"].ToString() + ",";
				IDSonTypeTree(int.Parse(reader["TypeTree_ID"].ToString()));

			}

			reader.Close();
			return txtIDSonTypeTree + TypeTree_ID.ToString();
		}


        /// <summary>
        /// 得到当前目录的父目录名称
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <returns当前目录的父目录></returns>
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
        /// 得到当前目录的子目录名称
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <returns>当前目录的子目录,逗号分隔的ID</returns>
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
        /// 得到当前目录的子目录名称文章点击数
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <returns>当前目录的子目录</returns>
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
        /// 某用户的发文章数
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>当前目录的子目录ID</returns>
		public int UserCount(string UserName , string startDate ,string endDate)
		{
			string sqls ="";
            if (startDate != "0" && endDate != "0")//#此处有特殊含义数字,重构时注意#
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
        #endregion 树形栏目相关函数

        #region 权限相关函数
        // --------判断权限----------------------------------------------------------------------------------------------------------------------------

		// 功能：是否具备子节点
		// 输入：
		// 输出：成功返回true，不成功返回false
		public bool HaveSon(int TypeTree_ID)
		{
			string sql = "select * from Content_Type_TypeTree where TypeTree_ParentID = "+ TypeTree_ID;
			SqlDataReader reader  = Tools.DoSqlReader(sql);
            //#此处含有可优化的内容, 重构时注意#
            bool res = reader.Read();
            reader.Close();
            return res;
		}

		// 功能：某权限下是否具备子节点
		// 输入：
		// 输出：成功返回true，不成功返回false
		public bool SbHaveSon(int TypeTree_ID,int SessionID)
		{
			string sql = "SELECT Content_Type_TypeTree.* FROM Content_Type_TypeTree , Content_RolesConnect WHERE Content_RolesConnect.Roles_ID = "+ SessionID +" and Content_RolesConnect.TypeTree_ID=Content_Type_TypeTree.TypeTree_ID and Content_Type_TypeTree.TypeTree_ParentID= "+TypeTree_ID+" ORDER BY Content_Type_TypeTree.TypeTree_OrderNum";

			SqlDataReader reader = Tools.DoSqlReader(sql);
            //#此处含有可优化的内容, 重构时注意#
            bool res = reader.Read();
            reader.Close();
            return res;
		}
        #endregion 权限相关函数

        #region 常用逻辑属性判断

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
        /// 显示当前状态的字符串含义
        /// </summary>
        /// <param name="typeTreeIssuance"></param>
        /// <returns>返回其对应的字符含义</returns>
        public string strTypeTreeIssuance(int typeTreeIssuance)
        {
            string strTypeTreeIssuance;

            switch (typeTreeIssuance)//#此处有文本,多语言化时注意#
            {
                case 0:
                    strTypeTreeIssuance = "关闭( 前台不可见)";
                    break;

                case 1:
                    strTypeTreeIssuance = "发布 (可编辑)";
                    break;

                case 2:
                    strTypeTreeIssuance = "锁定 (不可编辑)";
                    break;

                default:
                    strTypeTreeIssuance = "发布 (可编辑)";
                    break;
            }

            return strTypeTreeIssuance;
        }

        /// <summary>
        /// 显示当前状态的字符串含义
        /// </summary>
        /// <param name="typeTreeType">typeTreeIssuance</param>
        /// <returns>返回其对应的字符含义</returns>
        public string strTypeTreeType(int typeTreeType)
        {
            string strTypeTreeType;

            switch (typeTreeType)//#此处有文本,多语言化时注意#
            {

                case 0:
                    strTypeTreeType = "文章 (常规文章发布)";
                    break;

                case 1:
                    strTypeTreeType = "映射 (只能映射其他栏目的文章)";
                    break;

                case 2:
                    strTypeTreeType = "扩展 (不使用基础字段)";
                    break;

                //				case 2:
                //					strTypeTreeType =  "图片 (GPhoto系统支持)";
                //					break;

                //				case 3:
                //					strTypeTreeType =  "网络硬盘 (下载栏目和频道)";
                //					break;
                //
                //				case 4:
                //					strTypeTreeType =  "商城 (GShop系统支持)";
                //					break;
                //
                //				case 5:
                //					strTypeTreeType =  "博客 (GBlog系统支持)";
                //					break;
                //
                //				case 6:
                //					strTypeTreeType =  "论坛 (GForums系统支持)";
                //					break;

                default:
                    strTypeTreeType = "文章 (常规文章发布)";
                    break;
            }

            return strTypeTreeType;
        }

        #endregion 常用逻辑属性判断

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
