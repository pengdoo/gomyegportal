using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data.SqlClient;
using GCMSClassLib.Content;
using GCMS.PageCommonClassLib;
using GCMSClassLib.Public_Cls;
using System.IO;

public partial class Content_Type_Order : GCMS.PageCommonClassLib.PageBase
{
    //订阅页面的自定义事件
    protected override void OnPreInit(EventArgs e)
    {
        this.SessionAtuhFaiedEvent += new SessionAuthHandler(OnSessionAtuhFaiedEvent);//注册验证错误处理
        base.OnPreInit(e);
    }

    /// <summary>
    /// 验证失败事件响应
    /// </summary>
    void OnSessionAtuhFaiedEvent()
    {
        GSystem.SystemState = EnumTypes.SystemStates.Overtime;
        this.Response.Write("<script language=javascript>alert(\"超时或非法操作！！！\");parent.windowclose();</script>");
        return;
    }
    GCMSContentCreate.TemplateSystem ContentCreate = new GCMSContentCreate.TemplateSystem();
    StringBuilder htmltext = new StringBuilder();
    SqlDataReader myReader;
    ContentCls _ContentCls = new ContentCls();
    CreateFiles _CreateFiles = new CreateFiles();
    Type_TypeTree _Type_TypeTree = new Type_TypeTree();
    string sql;
    protected void Page_Load(object sender, EventArgs e)
    {
        string OrderType = Request.QueryString["OrderType"].ToString(); //命令
        int TypeTree_ParentID;
        switch (OrderType)
        {
            //生成表单  
            case "Feedback":
                int TypeTree_ID = int.Parse(Request.QueryString["TypeTree_ID"].ToString());
                int Content_ID = 0;

                Type_TypeTree typeTree = new Type_TypeTree();
                typeTree.Init(TypeTree_ID);
                string TypeTree_ListTemplate = typeTree.TypeTreeListTemplate;
                string TypeTreeListURL = typeTree.TypeTreeListURL;

                //					ContentCreate.GCMS.Feedback;

                string ContentText;
                if (!String.IsNullOrEmpty(TypeTree_ListTemplate))
                {

                    if (FilesIn(TypeTree_ListTemplate).ToString() == "")
                    {
                        Response.Write("<Script>alert('读取文件错误')</Script>");
                        return;
                    }

                    ContentText = ContentCreate.Execute(TypeTree_ID, Content_ID, FilesIn(TypeTree_ListTemplate).ToString());


                    if (!String.IsNullOrEmpty(TypeTreeListURL ))
                    {
                        FilesOut(TypeTreeListURL, ContentText);
                    }

                    htmltext = null; //清空缓存
                    ContentText = "";

                }

                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //整体发布
            case "AllPush":
                TypeTree_ID = int.Parse(Request.QueryString["TypeTree_ID"].ToString());
                string[] ops;
                char sSplit = ',';
                ops = _Type_TypeTree.IDSonTypeTree(TypeTree_ID).Split(sSplit);


                for (int j = 0; j < ops.Length; j++)
                {
                    if (ops[j].ToString() != "")
                    {
                        Content_ID = 0;
                        int n = 0;
                        int Countmax = 0;

                        string FieldsName = "Content_Content";

                        CreateFiles _CreateFiles = new CreateFiles();
                        //								int Countmax = _ContentCls.CountID(int.Parse(ops[j].ToString()));

                        _Type_TypeTree.Init(int.Parse(ops[j]));

                        if (_Type_TypeTree.TypeTree_Type == 2)
                        {
                            Content_FieldsName _Content_FieldsName = new Content_FieldsName();
                            if (_Type_TypeTree.TypeTree_ContentFields != 0)
                            {
                                _Content_FieldsName.Init(_Type_TypeTree.TypeTree_ContentFields);
                                FieldsName = "ContentUser_" + _Content_FieldsName.FieldsBase_Name;
                            }
                        }


                        sql = "select Content_ID,Url from " + FieldsName + " where Status = 4 and TypeTree_ID =" + ops[j].ToString() + " order by OrderNum desc";
                        DataTable dt = Tools.DoSqlTable(sql);
                        Countmax = dt.Rows.Count;

                        foreach (DataRow dr in dt.Rows)
                        {

                            _CreateFiles.CreateContentFiles(int.Parse(ops[j].ToString()), int.Parse(dr["Content_ID"].ToString()));
                            n = n + 1;
                            Response.Write("<script>this.parent.progress.style.width ='" + (n * 100 / Countmax) + "%' ;this.parent.progress.innerHTML='" + (n * 100 / Countmax) + "%';this.parent.pstr.innerHTML=' 当前栏目ID： " + dr["Content_ID"].ToString() + " <br/>当前文件： " + dr["Url"].ToString() + "';</script>");
                            Response.Flush();
                        }
                        if (Content_ID != 0) PushSystem(Content_ID);
                    }

                }
                PushSystemAll(TypeTree_ID);//附带发布
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //上移
            case "doMoveUp":
                TypeTree_ID = int.Parse(Request.QueryString["TypeTree_ID"].ToString());
                _Type_TypeTree.Init(TypeTree_ID);

                sql = "select top 1 TypeTree_ID,TypeTree_OrderNum from Content_Type_TypeTree where TypeTree_OrderNum < " + _Type_TypeTree.TypeTreeOrderNum + " and TypeTree_ParentID =" + _Type_TypeTree.TypeTreeParentID + " order by TypeTree_OrderNum desc";
                myReader = Tools.DoSqlReader(sql);
                while (myReader.Read())
                {
                    //Change By Galen Mu  2008.8.25
                    //将content.DoSelect(..)  改为 Tools.DoSql(..) 
                    Tools.DoSql("update Content_Type_TypeTree set TypeTree_OrderNum = " + _Type_TypeTree.TypeTreeOrderNum + " where TypeTree_ID = " + myReader.GetInt32(0).ToString());
                    Tools.DoSql("update Content_Type_TypeTree set TypeTree_OrderNum = " + myReader.GetInt32(1).ToString() + " where TypeTree_ID = " + TypeTree_ID);
                    PushSystem(myReader.GetInt32(0));
                }
                myReader.Close();

                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //下移
            case "doMoveDown":
                TypeTree_ID = int.Parse(Request.QueryString["TypeTree_ID"].ToString());
                _Type_TypeTree.Init(TypeTree_ID);

                sql = "select top 1 TypeTree_ID,TypeTree_OrderNum from Content_Type_TypeTree where TypeTree_OrderNum > " + _Type_TypeTree.TypeTreeOrderNum + " and TypeTree_ParentID =" + _Type_TypeTree.TypeTreeParentID + " order by TypeTree_OrderNum";
                myReader = Tools.DoSqlReader(sql);
                while (myReader.Read())
                {
                    //Change By Galen Mu  2008.8.25
                    //将content.DoSelect(..)  改为 Tools.DoSql(..) 
                    Tools.DoSql("update Content_Type_TypeTree set TypeTree_OrderNum = " + _Type_TypeTree.TypeTreeOrderNum + " where TypeTree_ID = " + myReader.GetInt32(0).ToString());
                    Tools.DoSql("update Content_Type_TypeTree set TypeTree_OrderNum = " + myReader.GetInt32(1).ToString() + " where TypeTree_ID = " + TypeTree_ID);
                    PushSystem(myReader.GetInt32(0));
                }
                myReader.Close();
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;
            //移动
            case "preMoveChannel":
                TypeTree_ID = int.Parse(Request.QueryString["TypeTree_ID"].ToString());
                TypeTree_ParentID = int.Parse(Request.QueryString["parent"].ToString());
                //Change By Galen Mu  2008.8.25
                //将content.DoSelect(..)  改为 Tools.DoSql(..) 
                Tools.DoSql("update Content_Type_TypeTree set TypeTree_ParentID = " + TypeTree_ParentID + " where TypeTree_ID = " + TypeTree_ID);
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //拷贝
            case "preCopyChannel":
                TypeTree_ID = int.Parse(Request.QueryString["TypeTree_ID"].ToString());
                TypeTree_ParentID = int.Parse(Request.QueryString["parent"].ToString());
                MakepreCopyChannel(TypeTree_ID, TypeTree_ParentID);
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;
            case "PushOnlyLinks":
                TypeTree_ID = int.Parse(Request.QueryString["TypeTree_ID"].ToString());
                PushSystemAll(TypeTree_ID);//附带发布
                break;
        }
    }

    private void MakepreCopyChannel(int TypeTree_ID, int TypeTree_ParentID)
    {

        _Type_TypeTree.Init(TypeTree_ID);
        _Type_TypeTree.TypeTreeParentID = TypeTree_ParentID;
        _Type_TypeTree.Create();
        TypeTree_ParentID = _Type_TypeTree.TypeTree_ID;

        string[] ops;
        char sSplit = ',';
        ops = _Type_TypeTree.SonTypeTree(TypeTree_ID).Split(sSplit);

        for (int j = 0; j < ops.Length; j++)
        {
            if (!string.IsNullOrEmpty(ops[j]))
            {
                MakepreCopyChannel(int.Parse(ops[j].ToString()), TypeTree_ParentID);
            }
        }
    }

    private void PushSystem(int Content_ID)
    {
        _ContentCls.Init(Content_ID);
        _Type_TypeTree.Init(_ContentCls.TypeTree_ID);

        //_CreateFiles.CreateChannelFiles(_ContentCls.TypeTree_ID,Content_ID,UrlString(_Type_TypeTree.TypeTreeListTemplate),UrlString(_Type_TypeTree.TypeTreeListURL),_Type_TypeTree.Listamount,_Type_TypeTree.TypeTreeListURL);
        _CreateFiles.CreateChannelFiles(_ContentCls.TypeTree_ID);
        _CreateFiles.CreateLinkPushFiles(_ContentCls.TypeTree_ID);

    }
    private void PushSystemAll(int TypeTree_ID)
    {
        _CreateFiles.CreateChannelFiles(TypeTree_ID);
        _CreateFiles.CreateLinkPushFiles(TypeTree_ID);

    }

    private string UrlString(string FilesUrl)
    {
        FilesUrl = FilesUrl.Replace("/", "//");
        FilesUrl = Server.MapPath(FilesUrl);
        return FilesUrl;
    }

    // 文件读取
    public string FilesIn(string TemplatesUrl)
    {
        StringBuilder htmltext = new StringBuilder();
        try
        {

            TemplatesUrl = TemplatesUrl.Replace("/", "//");
            TemplatesUrl = Server.MapPath(TemplatesUrl);
            using (StreamReader sr = new StreamReader(TemplatesUrl, System.Text.Encoding.Default))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    htmltext.Append(line + "\r\n");
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
    public bool FilesOut(string FilesUrl, string ContentText)
    {
        FilesUrl = FilesUrl.Replace("/", "//");
        FilesUrl = Server.MapPath(FilesUrl);

        try
        {

            using (StreamWriter sw = new StreamWriter(FilesUrl, false, System.Text.Encoding.GetEncoding("GB2312")))
            {
                sw.WriteLine(ContentText);
                sw.Flush();
                sw.Close();
            }
            return true;
        }
        catch
        {
            return false;
        }

    }
}
