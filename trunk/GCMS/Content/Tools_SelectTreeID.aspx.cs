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

public partial class Content_Tools_SelectTreeID : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(" <script language=\"javascript\">");
        sb.AppendLine("if (document.getElementById) {");
        sb.AppendLine("	var tree = new WebFXTree(' ');");
        sb.AppendLine("tree.setBehavior('explorer');");   
        sb.AppendLine(string.Format("var aNode=tree.add(new WebFXTreeItem(\"选择目录\",\"N\",'{0}'));",Request.QueryString["TypeTree_ID"].ToString()));   
        sb.AppendLine("aNode.add(new WebFXTreeItem(\"Loading\",\"Y\"));");   
     	sb.AppendLine("document.write(tree);");
	    sb.AppendLine("}");
	    sb.AppendLine("function OpenFolder(path){");
        sb.AppendLine("CurrentNode = path;");
	    sb.AppendLine("CName.value = path;");
        sb.AppendLine("}");
        sb.AppendLine("</script>");
        Type cstype = this.GetType();

        ClientScriptManager cs = Page.ClientScript;

        //if (!cs.IsStartupScriptRegistered(cstype, "mainScript"))
        //{
        //    cs.RegisterStartupScript(cstype, "mainScript", sb.ToString(), true);
        //}
        //lit_mainScript.Text = sb.ToString();

    }
}
