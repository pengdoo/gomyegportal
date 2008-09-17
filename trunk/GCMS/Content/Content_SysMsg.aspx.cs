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
using GCMSClassLib.Content;

public partial class Content_Content_SysMsg : System.Web.UI.Page
{
    ContentCls _ContentCls = new ContentCls();
    Type_TypeTree _Type_TypeTree = new Type_TypeTree();
    string columnid = "";
    string OrderType = "";
    string Content_List = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        OrderType = Request["OrderType"].ToString();
        columnid = Request["columnid"].ToString();
        Content_List = Request.QueryString["Content_List"].ToString();
        string strOrderType = "";
        if (OrderType == "preMoveContent") { strOrderType = "移动到 "; }
        if (OrderType == "preCopyContent") { strOrderType = "拷贝到 "; }

        _Type_TypeTree.Init(int.Parse(columnid));

        char sSplit = ',';
        string[] ops;
        ops = Content_List.Split(sSplit);
        string Names = "";
        int orgTreeID=0;
        for (int j = 0; j < ops.Length; j++)
        {
            if (ops[j].ToString() != "-1")
            {
                _ContentCls.Init(int.Parse(ops[j].ToString()));
                Names = Names + "<li>" + _ContentCls.Name + "</li><br>";
                orgTreeID = _ContentCls.TypeTree_ID;
            }
        }
        
        
        Label1.Text = "<table width='400' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td width='89' valign='top'>是否把</td>";
        Label1.Text = Label1.Text + "<td><ul>" + Names + "</ul></td></tr><tr><td width='89'>" + strOrderType + "</td><td><ul><li>" + _Type_TypeTree.TypeTreeCName + "</li></ul></td></tr></table>";
        int orgTypeTreeType, targettTypetreeType;
        targettTypetreeType = _Type_TypeTree.TypeTree_Type;
        _Type_TypeTree.Init(orgTreeID);
        orgTypeTreeType = _Type_TypeTree.TypeTree_Type;
        if (orgTypeTreeType != targettTypetreeType)
        {
            Label1.Text = "<table width='400' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td width='89' valign='top'>无法在不同类型栏目间移动或拷贝</td><td></table>";
            Button1.Visible = false;
        }
    }
    protected void Button1_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("Content_ViewOrder.aspx?OrderType=" + OrderType + "&TypeTree_ID=" + columnid + "&Content_List=" + Content_List);
    }
}
