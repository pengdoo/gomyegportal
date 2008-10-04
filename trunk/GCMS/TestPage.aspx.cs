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

public partial class TestPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MainMenu.Action = "GetRoot";
        MainMenu.UrlTemplete = "parent.frames[\"Main_List\"].location =\"Type_TypeView.aspx?TypeTree_ID=";
    }
}
