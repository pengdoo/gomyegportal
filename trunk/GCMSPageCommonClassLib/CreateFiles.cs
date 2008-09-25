//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-8-31
//
// 功能描述:输出生成文件的操作类
//
// 已修改问题:
// 未修改问题:
// 修改记录
//     1 2008-9-4 将 FileIn，FileOut，ListFilesOut函数移到Tools.cs中。更新了其引用
//     2 2008-9-25 修改CreateContentFiles方法，加入生成相关的级联文章
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.IO;
using System.Text;
using GCMSClassLib.Public_Cls;
using GCMSClassLib.Content;

namespace GCMS.PageCommonClassLib
{
	/// <summary>
	/// CreateFiles 的摘要说明。
	/// </summary>
	public class CreateFiles
	{
		GCMSContentCreate.TemplateSystem ContentCreate = new GCMSContentCreate.TemplateSystem();
		StringBuilder htmltext = new StringBuilder();
		string ContentText = string.Empty;
        /// <summary>
        ///  生成最终页面 
        /// </summary>
        /// <param name="TypeTree_ID"></param>
        /// <param name="Content_ID"></param>
        /// <param name="ifIncluedFather"></param>
        /// <returns></returns>
		public bool CreateContentFiles(int TypeTree_ID,int Content_ID,bool ifIncluedFather)
		{
			// 最终页面 start
			Type_TypeTree typeTree = new Type_TypeTree();
			typeTree.Init(TypeTree_ID);
            string TypeTree_Template = typeTree.TypeTreeTemplate;//获取模板路径
            string Url = string.Empty;
            string Pid = string.Empty;
            
            ContentCls content = new ContentCls();
            if (typeTree.IsFullExtenFields)
            {
                string sql=string.Format("Select * from {0} Where Content_ID={1}",typeTree.ExtentFieldTableName,Content_ID);
                SqlDataReader reader = Tools.DoSqlReader(sql);
                if(reader.Read())
                {
                    Url = reader["Url"].ToString();
                    Pid = reader["Content_PID"].ToString();
                }
                
                if (string.IsNullOrEmpty(Url))
                {
                    Url = typeTree.TypeTreeURL.Replace("{@UID}", Content_ID.ToString()); //获得URL
                    Tools.DoSql("update " + typeTree.ExtentFieldTableName + " set Url = '" + Url + "' where Content_ID = " + Content_ID);
                }
               
            }
            else if (typeTree.IsCommonPublish)
            {
                content.Init(Content_ID);
                Url = content.Url;
                if (string.IsNullOrEmpty(Url))
                {
                    Url = typeTree.TypeTreeURL.Replace("{@UID}", Content_ID.ToString()); //获得URL
                    Tools.DoSql("update " + typeTree.MainFieldTableName + " set Url = '" + Url + "' where Content_ID = " + Content_ID);
                }
                int CountPages = 1;
                if (!string.IsNullOrEmpty(TypeTree_Template))
                {
                    if (string.IsNullOrEmpty(FilesIn(TypeTree_Template, typeTree.TypeTree_Language).ToString()))
                    {
                        return false;
                    }
                    string _Url;
                    string fileExtension = Url.Substring(Url.LastIndexOf("."));
                    ContentCreate.GCMS.PgUp = "";
                    ContentCreate.GCMS.PgDn = "";
                    int txtCountPages = 1; // Request.Form["CountPages"])

                    XmlDataDocument xmlDoc = new XmlDataDocument();
                    if (!string.IsNullOrEmpty(content.Description))
                    {
                        xmlDoc.LoadXml(Tools.DBToWeb(content.Description));
                        txtCountPages = xmlDoc.DocumentElement.ChildNodes.Count;
                        ContentCreate.GCMS.PgCount = txtCountPages.ToString(); //多少页
                    }
                    //分页生成
                    do
                    {
                        ContentCreate.GCMS.PageID = CountPages - 1;
                        ContentCreate.GCMS.PgThis = CountPages;

                        if (txtCountPages > 1 && CountPages == 1)
                        {
                            ContentCreate.GCMS.PgDn = content.Url.Substring(0, content.Url.LastIndexOf(".")) + "_" + (CountPages + 1) + fileExtension;
                            ContentCreate.GCMS.PgUp = "";
                        }

                        if (txtCountPages > 1 && CountPages > 1)
                        {
                            ContentCreate.GCMS.PgDn = content.Url.Substring(0, content.Url.LastIndexOf(".")) + "_" + (CountPages + 1) + fileExtension;
                            ContentCreate.GCMS.PgUp = content.Url.Substring(0, content.Url.LastIndexOf(".")) + "_" + (CountPages - 1) + fileExtension;

                            if (CountPages == 2)
                            { ContentCreate.GCMS.PgUp = content.Url; }
                            if (CountPages == txtCountPages)
                            { ContentCreate.GCMS.PgDn = ""; }
                        }
                        ContentText = ContentCreate.Execute(TypeTree_ID, Content_ID, FilesIn(TypeTree_Template, typeTree.TypeTree_Language).ToString());

                        if (CountPages == 1)
                            _Url = Url;
                        else
                            _Url = Url.Substring(0, Url.LastIndexOf(".")) + "_" + CountPages.ToString() + fileExtension;

                        if (!String.IsNullOrEmpty(_Url))
                        {
                            FilesOut(_Url, ContentText, typeTree.TypeTree_Language);

                        }
                        CountPages = CountPages + 1;
                    }
                    while (CountPages <= txtCountPages);
                    htmltext = null; //清空缓存
                    ContentText = "";
                }
            }

            if (!string.IsNullOrEmpty(Pid) && ifIncluedFather)
            {
                ContentCls Pcontent = new ContentCls();
                Pcontent.Init(int.Parse(Pid));
                CreateContentFiles(Pcontent.TypeTree_ID, int.Parse(Pid), true);//及联生成相关的父文章,仅支持普通文文章的关联
            }
            CreateContentOnlyFiles(TypeTree_ID, Content_ID, TypeTree_Template, Url);
            return true;
           

			

		}

		// 最终页面 over
		// 最终页面only start


		public bool CreateContentOnlyFiles(int TypeTree_ID,int Content_ID,string TypeTree_Template,string Url)
		{

			// 最终页面 start
            ContentText = ContentCreate.Execute(TypeTree_ID, Content_ID, FilesIn(TypeTree_Template, "GB2312").ToString());
            FilesOut(Url, ContentText, "GB2312");
			return true;

		}






		// 最终页面only over
		// 列表页面 start

		public bool CreateChannelFiles(int TypeTree_ID)
		{
			StringBuilder htmltextChannel =new StringBuilder();
			ContentCls content = new ContentCls();
			Type_TypeTree _TypeTree = new Type_TypeTree();
			_TypeTree.Init(TypeTree_ID);
			string TypeTree_ListTemplate = _TypeTree.TypeTreeListTemplate;
			string TypeTreeListURL = _TypeTree.TypeTreeListURL;
			int List_amount = _TypeTree.Listamount ;
			string LinkUrl = _TypeTree.TypeTreeListURL;


			if( TypeTree_ListTemplate !="")
			{

				int CountID = content.CountID(TypeTree_ID);

                if (FilesIn(TypeTree_ListTemplate, _TypeTree.TypeTree_Language).ToString() == "" || TypeTreeListURL == "")
				{
					//Response.Write("<Script>alert('列表读取文件错误')</Script>");
					return false;
				}

				//------------------------------------------------------------------------------------
				ContentCreate.GCMS.PgUp = "";
				ContentCreate.GCMS.PgDn = "";
				ContentCreate.GCMS.AllCount = CountID.ToString();
				ContentCreate.GCMS.ListLastID = "";

				if (List_amount.ToString () == ""){List_amount=20;} //默认20
//				if( txtCountPages >1 && CountID == 1)
//				{
//					ContentCreate.GCMS.PgDn			= content.Url.Substring(0,content.Url.LastIndexOf("."))+"_"+(CountPages+1)+fileExtension;
//					ContentCreate.GCMS.PgUp = "";
//				}
				int CountIDs = CountID;


				if (CountIDs >= List_amount)
				{
					int j = 1;
					do 
					{
						CountIDs = CountIDs - List_amount;
						j = j + 1;
					}
					while( CountIDs > List_amount );
					ContentCreate.GCMS.PgCount = j.ToString();

					string fileExtension = TypeTreeListURL.Substring(TypeTreeListURL.LastIndexOf("."));
					
					string _TypeTreeListURL;
					ContentCreate.GCMS.ListTop = List_amount;

					_TypeTreeListURL = TypeTreeListURL;
					//ContentCreate.GCMS.PgDn = TypeTreeListURL.Substring(0,TypeTreeListURL.LastIndexOf("."))+"_"+i.ToString()+fileExtension;
					//ContentCreate.GCMS.PgDn = LinkUrl.Substring(0,LinkUrl.LastIndexOf("."))+"_"+k.ToString()+fileExtension;
					//ContentText = ContentCreate.Execute (TypeTree_ID,Content_ID,FilesIn(TypeTree_ListTemplate).ToString());
					//ContentText = ContentCreate.ExecuteList (TypeTree_ID,FilesIn(TypeTree_ListTemplate,_TypeTree.TypeTree_Language).ToString());
					//FilesOut(TypeTreeListURL,ContentText,_TypeTree.TypeTree_Language);
					int k = 0;
					do
					{
//						CountID = CountID - List_amount;
						//ContentCreate.GCMS.PgUp = _TypeTreeListURL;
//						ContentCreate.GCMS.PgUp =LinkUrl;
						int i = k;

						if(j > k && k > 1)
						{
							//ContentCreate.GCMS.PgDn = TypeTreeListURL.Substring(0,TypeTreeListURL.LastIndexOf("."))+"_"+(i+1).ToString()+fileExtension;
							ContentCreate.GCMS.PgUp	= LinkUrl.Substring(0,LinkUrl.LastIndexOf("."))+"_"+(k-1).ToString()+fileExtension;
							ContentCreate.GCMS.PgDn = LinkUrl.Substring(0,LinkUrl.LastIndexOf("."))+"_"+(k+1).ToString()+fileExtension;
						}
//						else
//						{
//							ContentCreate.GCMS.PgDn = "";
//						}


						if(k == 0)
						{
							ContentCreate.GCMS.PgDn	= LinkUrl.Substring(0,LinkUrl.LastIndexOf("."))+"_1"+fileExtension;
							ContentCreate.GCMS.PgUp = "";
						}

						
						if(k == 1)
						{
							ContentCreate.GCMS.PgUp	= LinkUrl.Substring(0,LinkUrl.LastIndexOf("."))+fileExtension;
							ContentCreate.GCMS.PgDn = LinkUrl.Substring(0,LinkUrl.LastIndexOf("."))+"_"+(k+1).ToString()+fileExtension;
						}

						
						if(j == k+1)
						{
							ContentCreate.GCMS.PgDn	= "";
							if (j==2)
							{
								ContentCreate.GCMS.PgUp = LinkUrl.Substring(0,LinkUrl.LastIndexOf("."))+fileExtension;
							}
							else
							{
								ContentCreate.GCMS.PgUp = LinkUrl.Substring(0,LinkUrl.LastIndexOf("."))+"_"+(k-1).ToString()+fileExtension;
							}
						}

//						if(j > 1 && i == 1)
//						{
//							ContentCreate.GCMS.PgUp = LinkUrl.Substring(0,LinkUrl.LastIndexOf("."))+fileExtension;
//						}
						if (k == 0)
						{
							_TypeTreeListURL = TypeTreeListURL.Substring(0,TypeTreeListURL.LastIndexOf("."))+fileExtension;
						}
						else
						{
							_TypeTreeListURL = TypeTreeListURL.Substring(0,TypeTreeListURL.LastIndexOf("."))+"_"+k.ToString()+fileExtension;
						}
						

						//ContentText = ContentCreate.Execute (TypeTree_ID,Content_ID,FilesIn(TypeTree_ListTemplate).ToString());
						ContentCreate.GCMS.PgThis = k + 1;
                        ContentText = ContentCreate.ExecuteList(TypeTree_ID, FilesIn(TypeTree_ListTemplate, _TypeTree.TypeTree_Language).ToString());
                       FilesOut(_TypeTreeListURL, ContentText, _TypeTree.TypeTree_Language);

						k = k + 1;
					}
					while( k < j );

					htmltext=null; //清空缓存
					ContentText = "";


				}
				else
				{
					ContentCreate.GCMS.ListTop = List_amount;
					ContentCreate.GCMS.PgCount = "1";
					ContentCreate.GCMS.PgThis = 1;
					//ContentText = ContentCreate.Execute (TypeTree_ID,Content_ID,FilesIn(TypeTree_ListTemplate).ToString());
                    ContentText = ContentCreate.ExecuteList(TypeTree_ID, FilesIn(TypeTree_ListTemplate, _TypeTree.TypeTree_Language).ToString());



					if(!String.IsNullOrEmpty(TypeTreeListURL ))
					{
                        FilesOut(TypeTreeListURL, ContentText, _TypeTree.TypeTree_Language);
					}
					htmltext=null; //清空缓存
					ContentText = "";
				}
			}
			return true;

		}

			// 列表页面 over
			// 附带发布 start
		public bool CreateLinkPushFiles(int TypeTree_ID)
		{

			Type_TypeTree _TypeTree = new Type_TypeTree();
			_TypeTree.Init(TypeTree_ID);

			SqlDataReader myReader;
			String sql = "SELECT TypeTree_Template ,TypeTree_URL FROM Content_Type_LinkPush WHERE TypeTree_ID =" + TypeTree_ID + " and LinkType = 1 ";
			myReader= Tools.DoSqlReader(sql);

			while (myReader.Read()) 
			{
                if (FilesIn(myReader.GetString(0), _TypeTree.TypeTree_Language).ToString() == "")
				{
					//Response.Write("<Script>alert('附带发布读取文件错误')</Script>");
					return false;
				}

				//ContentText = ContentCreate.Execute (TypeTree_ID,Content_ID,FilesIn(LinkPushTemplate,_TypeTree.TypeTree_Language).ToString());
                ContentText = ContentCreate.ExecuteList(TypeTree_ID, FilesIn(myReader.GetString(0), _TypeTree.TypeTree_Language).ToString());

				if(!String.IsNullOrEmpty(myReader.GetString(1).ToString()))
				{
                    FilesOut(myReader.GetString(1).ToString(), ContentText, _TypeTree.TypeTree_Language);
				}
				
				htmltext=null; //清空缓存
				ContentText = "";
			}

			myReader.Close();
			return true;
		}

        public bool CreateLinkPushSingleFile(int Link_ID,int TypeTree_ID)
        {
            Type_TypeTree _TypeTree = new Type_TypeTree();
            _TypeTree.Init(TypeTree_ID);
            Type_LinkPush _LinkPush = new Type_LinkPush();
            _LinkPush.Init(Link_ID);
            if (FilesIn(_LinkPush.TypeTreeTemplate, _TypeTree.TypeTree_Language).ToString() == "")
            {
                //Response.Write("<Script>alert('附带发布读取文件错误')</Script>");
                return false;
            }

            //ContentText = ContentCreate.Execute (TypeTree_ID,Content_ID,FilesIn(LinkPushTemplate,_TypeTree.TypeTree_Language).ToString());
            string ContentText = ContentCreate.ExecuteList(TypeTree_ID, FilesIn(_LinkPush.TypeTreeTemplate, _TypeTree.TypeTree_Language).ToString());

            if (!String.IsNullOrEmpty(_LinkPush.TypeTreeURL))
            {
                FilesOut(_LinkPush.TypeTreeURL, ContentText, _TypeTree.TypeTree_Language);
            }

            htmltext = null; //清空缓存
            ContentText = "";
            return true;
        }
			// 附带发布 over

        public  void PushList(int TypeTree_ID)
        {
            CreateChannelFiles(TypeTree_ID);
            CreateLinkPushFiles(TypeTree_ID);
        }
        public  void PushSingle(int Content_ID)
        {
            ContentCls _ContentCls = new ContentCls();
            Type_TypeTree _Type_TypeTree = new Type_TypeTree();
            _ContentCls.Init(Content_ID);
            _Type_TypeTree.Init(_ContentCls.TypeTree_ID);
            PushList(_ContentCls.TypeTree_ID);

        }

        // 文件读取
        public  string FilesIn(string TemplatesUrl, string TypeTree_Language)
        {
            StringBuilder htmltext = new StringBuilder();
            try
            {
                Encoding encoding = System.Text.Encoding.Default;

                if (!string.IsNullOrEmpty(TypeTree_Language))
                    encoding = System.Text.Encoding.GetEncoding(TypeTree_Language);

                using (StreamReader sr = new StreamReader(Tools.FilesUrl(TemplatesUrl), encoding))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        htmltext.AppendLine(line);
                    }
                    sr.Close();
                }
                return htmltext.ToString();
            }
            catch
            {
                return "";
            }
        }

        // 文件写入
        public  bool FilesOut(string FilesUrl, string ContentText, string TypeTree_Language)
        {
            try
            {
                Encoding encoding = System.Text.Encoding.Default;

                if (!string.IsNullOrEmpty(TypeTree_Language)) encoding = System.Text.Encoding.GetEncoding(TypeTree_Language);
                File.WriteAllText(Tools.FilesUrl(FilesUrl), ContentText, encoding);
                return true;
            }
            catch
            {
                return false;
            }

        }
       

	}

}
