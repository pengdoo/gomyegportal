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

public partial class Content_Content_RelativeList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            TypeTree.Url = "parent.frames[\"Content_RelativeContent\"].location =\"Content_RelativeContent.aspx?TypeTree_ID=";
            TypeTree.Sql = "SELECT Content_Type_TypeTree.* FROM Content_Type_TypeTree , Content_RolesConnect WHERE Content_RolesConnect.Roles_ID = " + int.Parse(Session["Roles"].ToString()) + " and Content_RolesConnect.TypeTree_ID=Content_Type_TypeTree.TypeTree_ID and Content_Type_TypeTree.TypeTree_ParentID= -1 ORDER BY Content_Type_TypeTree.TypeTree_OrderNum";
            TypeTree.Mode = "2";
        }
    }
}
