//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-10-4
//
// 功能描述: 树形菜单控件，利用Jquery插件TreeView实现
//
// 已修改问题:
// 未修改问题:
// 修改记录
//----------------------------------系统引用-------------------------------------
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
using GCMSClassLib.Public_Cls;
using GCMSClassLib.Content;

public partial class Gomye_Tools_Tools_TreeMenu : System.Web.UI.UserControl
{
    private string _action;
    public string Action
    {
        get { return _action; }
        set { _action = value; }
    }
    private string _urlTemplete;
    public string UrlTemplete
    {
        get { return _urlTemplete; }
        set { _urlTemplete = value; }
    }
    private string _mode;
    public string Mode
    {
        get { return _mode; }
        set { _mode = value; }
    }
    protected override void Render(HtmlTextWriter output)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<ul id=\"treemenu\" class=\"filetree\"><li>");
        sb.AppendLine("<span class=\"folder\">站点根目录</span>");
        sb.AppendLine("<ul>");

        string sql = string.Empty;
        switch (_action)
        {
            case "GetRoot":
                sql = Sql_GetRoot;
                break;
            case "GetRootByRole":
                sql = Sql_GetRootbyRole;
                break;
            default:
                sql =string.Format( Sql_GetRoot, this.Page.Session["Roles"].ToString());
                break;
        }
        SqlDataReader reader = Tools.DoSqlReader(sql);
        while (reader.Read())
        {
            int tid = int.Parse(reader["TypeTree_ID"].ToString());
            string tname = reader["TypeTree_CName"].ToString();
            AppendFolder(tid, tname, sb);
        }
        sb.AppendLine("</ul>");
        sb.AppendLine("</li></ul>");
        output.Write(sb.ToString());
    }

    string Sql_GetRoot = "Select TypeTree_ID,TypeTree_CName,isnull(TypeTree_Type,'0') TypeTree_Type From Content_Type_TypeTree Where TypeTree_ParentID = -1 order by TypeTree_OrderNum";
    string Sql_GetRootbyRole ="SELECT  Content_Type_TypeTree.TypeTree_ID,TypeTree_CName,isnull(TypeTree_Type,'0') TypeTree_Type FROM Content_Type_TypeTree , Content_RolesConnect WHERE Content_RolesConnect.Roles_ID = {0} and Content_RolesConnect.TypeTree_ID=Content_Type_TypeTree.TypeTree_ID and Content_Type_TypeTree.TypeTree_ParentID= -1 ORDER BY Content_Type_TypeTree.TypeTree_OrderNum";
   
    void AppendFolder(int TreeId, string TName, StringBuilder sb)
    {
        string script = this.UrlTemplete + TreeId.ToString() + "&defaultstatus=" + this._mode + "\";";
        sb.AppendLine("<li class=\"closed\">");
        sb.AppendLine(string.Format("<a><span class=\"folder\" id=\"treemenu-{0}\" name=\"{1}\" onclick='{2}'>{1}</span></a>", TreeId, TName, script));
        

        Type_TypeTree typeTree = new Type_TypeTree();
        if (typeTree.HaveSon(TreeId)){
            ArrayList list = typeTree.SelectAllSonTree(TreeId);
            for (int i = 0; i < list.Count; i++)
            {
                Type_TypeTree ctypeTree = list[i] as Type_TypeTree;
                sb.AppendLine("<ul>");
                AppendFolder(ctypeTree.TypeTree_ID, ctypeTree.TypeTreeCName, sb);
                sb.AppendLine(" </ul>");
            }
        }
        
        sb.AppendLine(" </li>");
    }

    void AppendFile(int TreeId, string TName, StringBuilder sb)
    {
        sb.AppendLine("<li>");
        sb.AppendLine(string.Format("<span class=\"file\" id=\"treemenu-{0}\">{1}</span>", TreeId, TName));
        sb.AppendLine(" </li>");
    }

  
}
