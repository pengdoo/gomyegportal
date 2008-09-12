//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-7-8
//
// 功能描述: 发布内容
//
// 已修改问题:
// 未修改问题:
//      1 SelectAll中的参数无意义
//      2 DoSelect和Content无关，不应该封装在该类里
//      3 很多方法，可以封装在存储过程中
//      4 ID自增，应该用事务来完成。否则会有并发的隐患
//      5 读取扩展字段的Contents中，存在效率问题
//      6 Status = 4 未注释，也未封装
// 修改记录
//       1   2008-7-8 添加注释
//       2   2008-8-25 封装Content_Content的数据库逻辑,
//           更新QueryMaxID引用,
//           移除DoSelect到Tools.cs
//       3   2008-8-26 改变数据访问层引用方法的写法
//
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient ;
using System.Collections;
using System.Data.Common;
using GCMSClassLib.Public_Cls;
using System.Text;
using System.IO;
using System.Xml;
using Gomye.CommonClassLib.Data;

namespace GCMSClassLib.Content
{
	/// <summary>
	/// Content 的摘要说明。
	/// </summary>
	/// <summary>
	///
	/// </summary>
	public class ContentCls
    {
        #region 实体定义
        private int m_ContentId;
		public int ContentId
		{
			get { return m_ContentId;}
			set { m_ContentId=value;}
		}
		private int m_Content_PID;
		public int Content_PID
		{
			get { return m_Content_PID;}
			set { m_Content_PID=value;}
		}		
		private int m_TypeTreeID;
		public int TypeTree_ID
		{
			get { return m_TypeTreeID;}
			set { m_TypeTreeID=value;}
		}
		private string m_Name;
		public string Name
		{
			get { return m_Name;}
			set { m_Name=value;}
		}
		private string m_Author;
		public string Author
		{
			get { return m_Author;}
			set { m_Author=value;}
		}
		private System.DateTime m_SubmitDate;
		public System.DateTime SubmitDate
		{
			get { return m_SubmitDate;}
			set { m_SubmitDate=value;}
		}
		private string m_Approver;
		public string Approver
		{
			get { return m_Approver;}
			set { m_Approver=value;}
		}
		private System.DateTime m_ApproveDate;
		public System.DateTime ApproveDate
		{
			get { return m_ApproveDate;}
			set { m_ApproveDate=value;}
		}
		private string m_Status;
		public string Status
		{
			get { return m_Status;}
			set { m_Status=value;}
		}
		private string m_Description;
		public string Description
		{
			get { return m_Description;}
			set { m_Description=value;}
		}
		private int m_Clicks;
		public int Clicks
		{
			get { return m_Clicks;}
			set { m_Clicks=value;}
		}
		private int m_OrderNum;
		public int OrderNum
		{
			get { return m_OrderNum;}
			set { m_OrderNum=value;}
		}
		private System.DateTime m_PublishDate;
		public System.DateTime PublishDate
		{
			get { return m_PublishDate;}
			set { m_PublishDate=value;}
		}
		private string m_Derivation;
		public string Derivation
		{
			get { return m_Derivation;}
			set { m_Derivation=value;}
		}
		private string m_DerivationLink;
		public string DerivationLink
		{
			get { return m_DerivationLink;}
			set { m_DerivationLink=value;}
		}
		private string m_HeadNews;
		public string HeadNews
		{
			get { return m_HeadNews;}
			set { m_HeadNews=value;}
		}
		private string m_PictureNews;
		public string PictureNews
		{
			get { return m_PictureNews;}
			set { m_PictureNews=value;}
		}
		private string m_PictureNotes;
		public string PictureNotes
		{
			get { return m_PictureNotes;}
			set { m_PictureNotes=value;}
		}
		private string m_PictureName;
		public string PictureName
		{
			get { return m_PictureName;}
			set { m_PictureName=value;}
		}
		private string m_PictureNameD;
		public string PictureNameD
		{
			get { return m_PictureNameD;}
			set { m_PictureNameD=value;}
		}

		private string m_Url;
		public string Url
		{
			get { return m_Url;}
			set { m_Url=value;}
		}
		private string m_lockedby;
		public string lockedby
		{
			get { return m_lockedby;}
			set { m_lockedby=value;}
		}


		private string m_KeyWord;
		public string KeyWord
		{
			get { return m_KeyWord;}
			set { m_KeyWord=value;}
		}
		private string m_Original;
		public string Original
		{
			get { return m_Original;}
			set { m_Original=value;}
		}

		private int m_ReCount;
		public int ReCount
		{
			get { return m_ReCount;}
			set { m_ReCount=value;}
		}
	
	
		private string m_ContentType;
		public string ContentType
		{
			get { return m_ContentType;}
			set { m_ContentType=value;}
		}

		private int m_User_ID;
		public int User_ID
		{
			get { return m_User_ID;}
			set { m_User_ID=value;}
		}

		private int m_Distillate;
		public int Distillate
		{
			get { return m_Distillate;}
			set { m_Distillate=value;}
		}

		private int m_Commend;
		public int Commend
		{
			get { return m_Commend;}
			set { m_Commend=value;}
		}

		private int m_User_id;
		public int User_id
		{
			get { return m_User_id;}
			set { m_User_id=value;}
		}

		private int m_AtTop;
		public int AtTop
		{
			get { return m_AtTop;}
			set { m_AtTop=value;}
		}

		private int m_IsBallot;
		public int IsBallot
		{
			get { return m_IsBallot;}
			set { m_IsBallot=value;}
		}

		private string m_Album;
		public string Album
		{
			get { return m_Album;}
			set { m_Album=value;}
		}		

		private string m_Content_Xml;
		public string Content_Xml
		{
			get { return m_Content_Xml;}
			set { m_Content_Xml=value;}
        }
        #endregion 实体定义

        #region 常用数据库操作
        public bool Create( )
		{
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@TypeTree_ID", SqlDbType.Int),
                    new SqlParameter("@Name", SqlDbType.VarChar,255),
                    new SqlParameter("@Author", SqlDbType.VarChar,50),
                    new SqlParameter("@KeyWord", SqlDbType.VarChar,500),
                    new SqlParameter("@Original", SqlDbType.VarChar,50),
                    new SqlParameter("@SubmitDate", SqlDbType.DateTime),
                    new SqlParameter("@Approver", SqlDbType.VarChar,50),
                    new SqlParameter("@Status", SqlDbType.VarChar,50),
                    new SqlParameter("@Description", SqlDbType.Text),
                    new SqlParameter("@Clicks", SqlDbType.Int),
                    new SqlParameter("@OrderNum", SqlDbType.Int),
                    new SqlParameter("@Derivation", SqlDbType.VarChar,50),
                    new SqlParameter("@DerivationLink", SqlDbType.VarChar,50),
                    new SqlParameter("@Head_news", SqlDbType.Char,1),
                    new SqlParameter("@Picture_news", SqlDbType.Char,1),
                    new SqlParameter("@Picture_Notes", SqlDbType.VarChar,2000),
                    new SqlParameter("@Picture_Name", SqlDbType.VarChar,50),
                    new SqlParameter("@Picture_DName", SqlDbType.VarChar,50),
                    new SqlParameter("@URL", SqlDbType.VarChar,200),
                    new SqlParameter("@ContentType", SqlDbType.Char,10),
                    new SqlParameter("@User_id", SqlDbType.Int),
                    new SqlParameter("@IsBallot", SqlDbType.Int),
                    new SqlParameter("@Album", SqlDbType.VarChar,500),
                    new SqlParameter("@content_xml", SqlDbType.Text),
                    new SqlParameter("@Content_ID", SqlDbType.Int),

            };
            parameters[0].Value = this.TypeTree_ID;

            if (this.Name != null)
                parameters[1].Value =Tools.WebToDB( this.Name);
            else
                parameters[1].Value = DBNull.Value;


            if (this.Author != null)
                parameters[2].Value = this.Author;
            else
                parameters[2].Value = DBNull.Value;


            if (this.KeyWord != null)
                parameters[3].Value = this.KeyWord;
            else
                parameters[3].Value = DBNull.Value;


            if (this.Original != null)
                parameters[4].Value = this.Original;
            else
                parameters[4].Value = DBNull.Value;


            if (this.SubmitDate != DateTime.MinValue)
                parameters[5].Value = this.SubmitDate;
            else
                parameters[5].Value = DBNull.Value;


            if (this.Approver != null)
                parameters[6].Value = this.Approver;
            else
                parameters[6].Value = DBNull.Value;



            if (this.Status != null)
                parameters[7].Value = this.Status;
            else
                parameters[7].Value = DBNull.Value;


            if (this.Description != null)
                parameters[8].Value =Tools.WebToDB( this.Description);
            else
                parameters[8].Value = DBNull.Value;

            parameters[9].Value = this.Clicks;
            parameters[10].Value = this.OrderNum;

           
            if (this.Derivation != null)
                parameters[11].Value = this.Derivation;
            else
                parameters[11].Value = DBNull.Value;


            if (this.DerivationLink != null)
                parameters[12].Value = this.DerivationLink;
            else
                parameters[12].Value = DBNull.Value;


            if (this.HeadNews != null)
                parameters[13].Value = this.HeadNews;
            else
                parameters[13].Value = DBNull.Value;


            if (this.PictureNews != null)
                parameters[14].Value = this.PictureNews;
            else
                parameters[14].Value = DBNull.Value;


            if (this.PictureNotes != null)
                parameters[15].Value = this.PictureNotes;
            else
                parameters[15].Value = DBNull.Value;


            if (this.PictureName != null)
                parameters[16].Value = this.PictureName;
            else
                parameters[16].Value = DBNull.Value;


            if (this.PictureNameD != null)
                parameters[17].Value = this.PictureNameD;
            else
                parameters[17].Value = DBNull.Value;


            if (this.Url != null)
                parameters[18].Value = this.Url;
            else
                parameters[18].Value = DBNull.Value;

            if (this.ContentType != null)
                parameters[19].Value = this.ContentType;
            else
                parameters[19].Value = DBNull.Value;

            parameters[20].Value = this.User_id;

            parameters[20].Value = this.IsBallot;

            if (this.Album != null)
                parameters[21].Value = this.Album;
            else
                parameters[21].Value = DBNull.Value;


            if (this.Content_Xml != null)
                parameters[22].Value = this.Content_Xml;
            else
                parameters[22].Value = DBNull.Value;

            parameters[23].Value = this.Content_PID;
            parameters[24].Direction = ParameterDirection.Output;

            SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "p_Content_Content_ADD", parameters, out rowsAffected);

            if (rowsAffected > 0)
            {
                this.ContentId = (int)parameters[24].Value;
                return true;
            }
            else
            {
                return false;
            }
		}

		public bool Delete(int contentId)
		{
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Content_Id", SqlDbType.Int)};
            parameters[0].Value = contentId;

            SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "p_Content_Content_Delete", parameters, out rowsAffected);
            return rowsAffected > 0;
			
		}

		public bool Update(int ContentId)
		{

            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Content_Id", SqlDbType.Int),
                    new SqlParameter("@Name", SqlDbType.VarChar,255),
                    new SqlParameter("@KeyWord", SqlDbType.VarChar,500),
                    new SqlParameter("@Original", SqlDbType.VarChar,50),
                    new SqlParameter("@SubmitDate", SqlDbType.DateTime),
                    new SqlParameter("@Approver", SqlDbType.VarChar,50),
                    new SqlParameter("@Status", SqlDbType.VarChar,50),
                    new SqlParameter("@Description", SqlDbType.Text),
                    new SqlParameter("@Derivation", SqlDbType.VarChar,50),
                    new SqlParameter("@DerivationLink", SqlDbType.VarChar,50),
                    new SqlParameter("@Head_news", SqlDbType.Char,1),
                    new SqlParameter("@Picture_news", SqlDbType.Char,1),
                    new SqlParameter("@Picture_Notes", SqlDbType.VarChar,2000),
                    new SqlParameter("@Picture_Name", SqlDbType.VarChar,50),
                    new SqlParameter("@Picture_DName", SqlDbType.VarChar,50),
                    new SqlParameter("@URL", SqlDbType.VarChar,200),
                    new SqlParameter("@Lockedby", SqlDbType.VarChar,50),
                    new SqlParameter("@ContentType", SqlDbType.Char,10),
                    new SqlParameter("@IsBallot", SqlDbType.Int),
                    new SqlParameter("@Album", SqlDbType.Char,500),
                    new SqlParameter("@content_xml", SqlDbType.Text),
            };
            parameters[0].Value = ContentId;

            if (this.Name != null)
                parameters[1].Value = Tools.WebToDB(this.Name);
            else
                parameters[1].Value = DBNull.Value;

            if (this.KeyWord != null)
                parameters[2].Value = this.KeyWord;
            else
                parameters[2].Value = DBNull.Value;


            if (this.Original != null)
                parameters[3].Value = this.Original;
            else
                parameters[3].Value = DBNull.Value;


            if (this.SubmitDate != DateTime.MinValue)
                parameters[4].Value = this.SubmitDate;
            else
                parameters[4].Value = DBNull.Value;


            if (this.Approver != null)
                parameters[5].Value = this.Approver;
            else
                parameters[5].Value = DBNull.Value;

            if (this.Status != null)
                parameters[6].Value = this.Status;
            else
                parameters[6].Value = DBNull.Value;


            if (this.Description != null)
                parameters[7].Value = Tools.WebToDB(this.Description);
            else
                parameters[7].Value = DBNull.Value;

      

            if (this.Derivation != null)
                parameters[8].Value = this.Derivation;
            else
                parameters[8].Value = DBNull.Value;


            if (this.DerivationLink != null)
                parameters[9].Value = this.DerivationLink;
            else
                parameters[9].Value = DBNull.Value;


            if (this.HeadNews != null)
                parameters[10].Value = this.HeadNews;
            else
                parameters[10].Value = DBNull.Value;


            if (this.PictureNews != null)
                parameters[11].Value = this.PictureNews;
            else
                parameters[11].Value = DBNull.Value;


            if (this.PictureNotes != null)
                parameters[12].Value = this.PictureNotes;
            else
                parameters[12].Value = DBNull.Value;


            if (this.PictureName != null)
                parameters[13].Value = this.PictureName;
            else
                parameters[13].Value = DBNull.Value;


            if (this.PictureNameD != null)
                parameters[14].Value = this.PictureNameD;
            else
                parameters[14].Value = DBNull.Value;


            if (this.Url != null)
                parameters[15].Value = this.Url;
            else
                parameters[15].Value = DBNull.Value;


            if (this.lockedby != null)
                parameters[16].Value = this.lockedby;
            else
                parameters[16].Value = DBNull.Value;

          
            if (this.ContentType != null)
                parameters[17].Value = this.ContentType;
            else
                parameters[17].Value = DBNull.Value;

     
            parameters[18].Value = this.IsBallot;

            if (this.Album != null)
                parameters[19].Value = this.Album;
            else
                parameters[19].Value = DBNull.Value;

            if (this.Content_Xml != null)
                parameters[20].Value = this.Content_Xml;
            else
                parameters[20].Value = DBNull.Value;
           
            SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "p_Content_Content_S_Update", parameters, out rowsAffected);
            return rowsAffected > 0;
		}
	
		public bool Init(int contentId)
		{
            SqlParameter[] parameters = {
                    new SqlParameter("@Content_Id", SqlDbType.Int)};
            parameters[0].Value = contentId;
            DataSet ds = SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "p_Content_Content_S_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow r = ds.Tables[0].Rows[0];
                GetModel(r);
                return true;
            }
            else
            {
                return false;
            }
		}

		///<summary>
		///按条件检查数据库中是否已经存在
		///</summary>
		public bool IsExist(int contentId)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Content_Id", SqlDbType.Int)};
            parameters[0].Value = contentId;
            return SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "p_Content_Content_Exists", parameters, out rowsAffected) == 1;
        }

        public System.Collections.ArrayList SelectAll(int contentId)
        {
            SqlParameter[] parameters ={ };
            DataSet ds = SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "p_Content_Content_GetList", parameters, "ds");
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    ContentCls model = new ContentCls();
                    model.ContentId = SqlHelper.GetInt(r["Content_Id"]);
                    model.TypeTree_ID = SqlHelper.GetInt(r["TypeTree_ID"]);
                    model.Name = SqlHelper.GetString(r["Name"]);
                    model.Author = SqlHelper.GetString(r["Author"]);
                    model.KeyWord = SqlHelper.GetString(r["KeyWord"]);
                    model.Original = SqlHelper.GetString(r["Original"]);
                    model.SubmitDate = SqlHelper.GetDateTime(r["SubmitDate"]);
                    model.Approver = SqlHelper.GetString(r["Approver"]);
                    model.ApproveDate = SqlHelper.GetDateTime(r["ApproveDate"]);
                    model.Status = SqlHelper.GetString(r["Status"]);
                    model.Description = SqlHelper.GetString(r["Description"]);
                    model.Clicks = SqlHelper.GetInt(r["Clicks"]);
                    model.OrderNum = SqlHelper.GetInt(r["OrderNum"]);
                    model.PublishDate = SqlHelper.GetDateTime(r["PublishDate"]);
                    model.Derivation = SqlHelper.GetString(r["Derivation"]);
                    model.DerivationLink = SqlHelper.GetString(r["DerivationLink"]);
                    model.HeadNews = SqlHelper.GetString(r["Head_news"]);
                    model.PictureNews = SqlHelper.GetString(r["Picture_news"]);
                    model.PictureNotes = SqlHelper.GetString(r["Picture_Notes"]);
                    model.PictureName = SqlHelper.GetString(r["Picture_Name"]);
                    model.PictureNameD = SqlHelper.GetString(r["Picture_DName"]);
                    model.Url = SqlHelper.GetString(r["URL"]);
                    model.lockedby = SqlHelper.GetString(r["Lockedby"]);
                    model.ReCount = SqlHelper.GetInt(r["ReCount"]);
                    model.Distillate = SqlHelper.GetInt(r["Distillate"]);
                    model.Commend = SqlHelper.GetInt(r["Commend"]);
                    model.ContentType = SqlHelper.GetString(r["ContentType"]);
                    model.User_id = SqlHelper.GetInt(r["User_id"]);
                    model.AtTop = SqlHelper.GetInt(r["AtTop"]);
                    model.IsBallot = SqlHelper.GetInt(r["IsBallot"]);
                    model.Album = SqlHelper.GetString(r["Album"]);
                    model.Content_Xml = SqlHelper.GetString(r["content_xml"]);
                    model.Content_PID = SqlHelper.GetInt(r["Content_PID"]);
                    list.Add(model);
                }
            }
            return list;
        }
        #endregion 常用数据库操作

        private void GetModel(DataRow r)
        {
            this.ContentId = SqlHelper.GetInt(r["Content_Id"]);
            this.Content_PID =SqlHelper.GetInt(r["Content_PID"]);
            this.TypeTree_ID = SqlHelper.GetInt(r["TypeTree_ID"]);
            this.Name = Tools.DBToWeb( SqlHelper.GetString(r["Name"]));
            this.Author = SqlHelper.GetString(r["Author"]);
            this.SubmitDate =  SqlHelper.GetDateTime(r["SubmitDate"]);
            this.Approver = SqlHelper.GetString(r["Approver"]);

            this.Status =SqlHelper.GetString(r["Status"]);
            this.Description = Tools.DBToWeb(SqlHelper.GetString(r["Description"]));
            this.Clicks =SqlHelper.GetInt(r["Clicks"]);
            this.OrderNum = SqlHelper.GetInt(r["OrderNum"]);
            this.PublishDate = SqlHelper.GetDateTime(r["PublishDate"]);
            this.Derivation = SqlHelper.GetString(r["Derivation"]);
            this.DerivationLink =SqlHelper.GetString(r["DerivationLink"]);
            this.HeadNews =  SqlHelper.GetString(r["Head_news"]);
            this.PictureNews =  SqlHelper.GetString(r["Picture_news"]);
            this.PictureNotes = SqlHelper.GetString(r["Picture_Notes"]);
            this.PictureName = SqlHelper.GetString(r["Picture_Name"]);
            this.PictureNameD =  SqlHelper.GetString(r["Picture_DName"]);
            this.Url =  SqlHelper.GetString(r["URL"]);
            //this.lockedby=SqlHelper.GetString(r["Lockedby"]);
            this.ReCount =  SqlHelper.GetInt(r["ReCount"]);
            this.Distillate = SqlHelper.GetInt(r["Distillate"]);
            this.KeyWord = SqlHelper.GetString(r["KeyWord"]);
            this.Original = SqlHelper.GetString(r["Original"]);
            this.Commend =  SqlHelper.GetInt(r["Commend"]);
            this.ContentType =  SqlHelper.GetString(r["ContentType"]);        
            this.User_ID =  SqlHelper.GetInt(r["User_id"]);
            this.IsBallot =SqlHelper.GetInt(r["IsBallot"]);
            this.AtTop =SqlHelper.GetInt(r["AtTop"]);
            this.Content_Xml = SqlHelper.GetString(r["content_xml"]);
           //this.Album = SqlHelper.GetString(r["Album"]);

        }
        #region 内容相关工具函数
        /// <summary>
        /// 按栏目选出内容
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <returns></returns>
		public DataTable QueryContentList(int TypeTree_ID)
		{
			 
			string sql = "SELECT *  FROM  Content_Content  WHERE TypeTree_ID =" + TypeTree_ID;
            return Tools.DoSqlTable(sql).Copy();
			 	
		}

        /// <summary>
        /// 获取当前的最大ID
        /// </summary>
        /// <returns></returns>
		public int QueryMaxContentID( )
		{
            return Tools.QueryMaxID("Content_Id"); 	 
		}

        /// <summary>
        /// 控制ID自增#过时方法#
        /// </summary>
        /// <returns></returns>
		public bool UpdateMaxContentID( )
		{
            return Tools.UpdateMaxID("Content_ID");
		}

        /// <summary>
        /// 获取当前OrderNum
        /// </summary>
        /// <param name="contentId"></param>
        /// <returns></returns>
		public int OrderNumInit(int contentId)
		{
            SqlParameter[] parameters = {
                    new SqlParameter("@Content_Id", SqlDbType.Int)};
            parameters[0].Value = contentId;
            DataSet ds = SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "p_Content_Content_S_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow r = ds.Tables[0].Rows[0];
                this.OrderNum = SqlHelper.GetInt(r["OrderNum"]);
                return this.OrderNum;
            }
            else
            {
                return 0;
            }
        }
        #endregion 内容相关工具函数

        #region 扩展字段读取
        public string Contents(int Content_ID, string Words, int TypeTree_ID)//#此处含有可优化的内容, 重构时注意#
		{

			Type_TypeTree _Type_TypeTree = new Type_TypeTree();
			_Type_TypeTree.Init(TypeTree_ID);
		    string Property="";
			string sqls="select FieldsBase_Name from Content_FieldsName where FieldsName_ID = "+_Type_TypeTree.TypeTree_ContentFields;
			
			SqlDataReader readers = null;
			readers=Tools.DoSqlReader(sqls);
			if(readers.Read())
			{

			string sql="select "+ Words +" from ContentUser_"+readers["FieldsBase_Name"].ToString()+" where content_Id = "+Content_ID;
			SqlDataReader reader = null;
			reader=Tools.DoSqlReader(sql);

			if(reader.Read())
				{
					Property=reader[Words].ToString();
					reader.Close();
				}
			else
				{
					reader.Close();
					Property = "";
				}

			}
			readers.Close();
			return Property;

		}


        //字段读取 20080627 测试最优化方式读取数据
		public  DataTable ContentsAll (int Content_ID ,int TypeTree_ID)
		{	
			Type_TypeTree _Type_TypeTree = new Type_TypeTree();
			Content_FieldsName _Content_FieldsName = new Content_FieldsName ();
			_Type_TypeTree.Init(TypeTree_ID);
			string sql = "";
			_Content_FieldsName.Init(_Type_TypeTree.TypeTree_ContentFields);
            string baseTableName = _Content_FieldsName.FieldsBase_Name;
			if (_Type_TypeTree.TypeTree_Type == 2)
			{
                baseTableName = _Content_FieldsName.FieldsBase_Name;
			}
			else
			{
                baseTableName = "Content_Content";
				//sql = "select  * from ContentUser_letter join Content_Content on ContentUser_letter.Content_ID = Content_Content.Content_ID where Content_Content.Status = 4 and Content_Content.Content_ID =  "+Content_ID;
			}

            sql = "select * from ContentUser_" + baseTableName + " where Status = 4 and content_Id = " + Content_ID;	
			DataTable dt=Tools.DoSqlTable(sql);
			Tools.ContentList = dt ;
			return dt;
        }

        #endregion 扩展字段读取

        //==========静态生成=======================================================================================

        public int CountID(int TypeTree_ID)//#此处含有可优化的内容, 重构时注意#
		{
			SqlDataReader reader = null;
			string sql=" SELECT COUNT(Content_Id) AS CountID FROM Content_Content where Status = 4 and TypeTree_ID = " +TypeTree_ID +" or Content_ID in (select Content_ID from Content_Commend WHERE  TypeTree_ID = '" + TypeTree_ID + "')";
//        sql = "SELECT Top " & ListTop & " Content_ID,Name,Url,OrderNum FROM Content_Content WHERE TypeTree_ID =" & ChannelID.ToString() & strListLastID & " and status = 4 or Content_ID in (select Content_ID from Content_Commend WHERE  TypeTree_ID = " & ChannelID.ToString() & ") order by AtTop desc ,OrderNum desc"
            //#此处有特殊含义数字,重构时注意#
			reader= Tools.DoSqlReader(sql);
			if(reader.Read())
			{		
				int CountID = int.Parse(reader["CountID"].ToString());
				reader.Close();
				return CountID;
			}
			else
			{
				reader.Close();
				return 0;
			}
		}
	}
}
