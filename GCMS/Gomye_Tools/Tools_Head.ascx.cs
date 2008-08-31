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
using GCMS.PageCommonClassLib;

public partial class Gomye_Tools_Tools_Head : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (Session["Master_UserName"] == null || Session["Master_ID"] == null || Session["Roles"] == null)
        {
            GSystem.SystemState = EnumTypes.SystemStates.Overtime;
            this.Response.Write("<script language=javascript>parent.parent.parent.window.navigate('../Logon.aspx');</script>");
            return;
        }
    }
}
