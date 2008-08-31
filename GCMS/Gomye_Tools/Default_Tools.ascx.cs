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

public partial class Gomye_Tools_Default_Tools : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Toolsbar1_ButtonClick(object sender, System.EventArgs e)
    {
        Session.RemoveAll();
        this.Response.Write("<script language='javascript'> location.href='Logon.aspx';</script>");
    } 
}
