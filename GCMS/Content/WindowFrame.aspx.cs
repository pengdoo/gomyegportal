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

public partial class Content_WindowFrame : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string file=Request.QueryString["loadfile"].ToString();
        string url = string.Format("{0}?{1}", file, Request.QueryString.ToString()); 
        Response.Write(string.Format("<iframe scrolling=no src='{0}' id='funcArea' style='width:100%;height:100%;border:none'></iframe>",url));
    }
}
