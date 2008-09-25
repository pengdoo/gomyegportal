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
using GCMSClassLib.Public_Cls;
using GCMSClassLib.Content;
using GCMS.PageCommonClassLib;
using System.Xml;
using System.Data.SqlClient;

public partial class Content_Content_Recommend : GCMS.PageCommonClassLib.PageBase
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
        this.Response.Write("<script language=javascript>alert(\"超时操作！！！\");parent.parent.parent.window.navigate('../Logon.aspx');</script>");
        return;
    }

    string Content_List = "";
    ContentCls content = new ContentCls();
    protected void Page_Load(object sender, EventArgs e)
    {
        Content_List = Request.QueryString["Content_List"].ToString();
        this.InputContent_ID.Value = Content_List;


        if (!Page.IsPostBack)
        {
            //新闻详情绑定


            TypeTree.Url = "this.frames[\"Content_RelativeContent\"].location =\"Content_RecommendContent.aspx?TypeTree_ID=";
            TypeTree.Mode = "1";

            if (int.Parse(Session["Roles"].ToString()) == 0)
            {
                TypeTree.Sql = "select TypeTree_ID,TypeTree_CName,isnull(TypeTree_Type,'0') TypeTree_Type from Content_Type_TypeTree where TypeTree_ParentID = -1 order by TypeTree_OrderNum";
            }
            else
            {
                //TypeTree.Sql = "SELECT Content_Type_TypeTree.* FROM Content_Type_TypeTree , Content_RolesConnect WHERE Content_RolesConnect.Roles_ID = "+ int.Parse(Session["Roles"].ToString()) +" and Content_RolesConnect.TypeTree_ID=Content_Type_TypeTree.TypeTree_ID and Content_Type_TypeTree.TypeTree_ParentID= -1 ORDER BY Content_Type_TypeTree.TypeTree_OrderNum";
                TypeTree.Sql = "select Content_Type_TypeTree.TypeTree_ID,TypeTree_CName,isnull(TypeTree_Type,'0') TypeTree_Type from Content_Type_TypeTree, Content_RolesConnect where Content_RolesConnect.Roles_ID = " + int.Parse(Session["Roles"].ToString()) + " and Content_RolesConnect.TypeTree_ID=Content_Type_TypeTree.TypeTree_ID and TypeTree_ParentID = -1 order by TypeTree_OrderNum";
            }


        }
    }
    public string xmlSplit(string xmlStr) //拆分xml
    {
        string xmlStrNew = "";
        if (!String.IsNullOrEmpty(xmlStr ))
        {

            XmlDataDocument xmlDoc = new XmlDataDocument();
            xmlDoc.LoadXml(xmlStr);

            XmlNodeList nodeList = (xmlDoc.GetElementsByTagName("content"))[0].ChildNodes;


            ArrayList list = new ArrayList();
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlNode node = nodeList.Item(i);
                list.Add(nodeList.Item(i).InnerText);
            }
            xmlStrNew = nodeList.Item(0).InnerText.ToString();
        }
        return xmlStrNew;

    }


    public void ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            int TypeTree_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "TypeTree_ID"));
            string OutText = "<input type='checkbox' name='cid' value=" + TypeTree_ID + " id='" + TypeTree_ID + "'>";
            e.Item.Cells[0].Text = OutText;
            e.Item.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "TypeTree_ID"));
            e.Item.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "TypeTree_CName"));
        }
    }


    private void Submit1_ServerClick(object sender, System.EventArgs e)
    {
        string cid = Request.Form["cid"].ToString();
        if (cid == "") { Response.Write("错误操作！"); return; }

        //			Response.Write (cid);
        string[] ops;
        string[] cids;

        char sSplit = ',';

        cids = cid.Split(sSplit);
        for (int i = 0; i < cids.Length; i++)
        {
            ops = Content_List.Split(sSplit);
            for (int j = 0; j < ops.Length; j++)
            {
                if (ops[j].ToString() != "-1")
                {
                    if (!MemberUsersInRoles(ops[j], cids[i]))
                    {
                        //Change By Galen Mu  2008.8.25
                        //将content.DoSelect(..)  改为 Tools.DoSql(..) 
                        Tools.DoSql("insert into Content_Commend (Content_ID,TypeTree_ID) values (" + ops[j] + "," + cids[i] + ")");

                    }
                    PushChannel(int.Parse(cids[i]));
                }
            }
        }
        this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
    }


    private bool MemberUsersInRoles(string Content_ID, string TypeTree_ID)
    {

        SqlDataReader reader = null;
        string sql = "select * from Content_Commend where Content_ID=" + Content_ID + " and TypeTree_ID = " + TypeTree_ID;

        reader = Tools.DoSqlReader(sql);

        if (reader.Read())
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

    private void PushChannel(int TypeTree_ID)
    {
        CreateFiles _CreateFiles = new CreateFiles();
        Type_TypeTree _Type_TypeTree = new Type_TypeTree();
        _Type_TypeTree.Init(TypeTree_ID);


        _CreateFiles.CreateChannelFiles(TypeTree_ID);
        _CreateFiles.CreateLinkPushFiles(TypeTree_ID);

    }

    private string UrlString(string FilesUrl)
    {
        FilesUrl = FilesUrl.Replace("/", "//");
        FilesUrl = Server.MapPath(FilesUrl);
        return FilesUrl;
    }



    private void Submit1_Click(object sender, System.EventArgs e)
    {
        string cid = Request.Form["cid"].ToString();
        if (cid == "") { Response.Write("错误操作！"); return; }
        string[] ops;
        string[] cids;

        char sSplit = ',';

        cids = cid.Split(sSplit);
        for (int i = 0; i < cids.Length; i++)
        {
            ops = Content_List.Split(sSplit);
            for (int j = 0; j < ops.Length; j++)
            {
                if (ops[j].ToString() != "-1")
                {
                    if (!MemberUsersInRoles(ops[j], cids[i]))
                    {
                        Tools.DoSql("insert into Content_Commend (Content_ID,TypeTree_ID) values (" + ops[j] + "," + cids[i] + ")");

                    }
                    PushChannel(int.Parse(cids[i]));
                }
            }
        }
        this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
    }
}
