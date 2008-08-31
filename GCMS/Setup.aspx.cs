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
using GCMS.PageCommonClassLib;
using GCMSClassLib.Content;
using Microsoft.VisualBasic;

public partial class Setup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string _HTTP = "GomyeGomye" + Request.ServerVariables["HTTP_HOST"].ToString() + ".net.net";
        string _HTTPs = FormsAuthentication.HashPasswordForStoringInConfigFile(_HTTP.ToString(), "MD5");
        key.Value = _HTTPs;
        //CreateFiles _CreateFiles = new CreateFiles();
        //Type_TypeTree _Type_TypeTree = new Type_TypeTree();
        //_CreateFiles.CreateLinkPushFiles(89);
        //GCMSContentCreate.GCMS gcms = new GCMSContentCreate.GCMS();
        //gcms.GetChannel = "124";
        
        //foreach (GCMSContentCreate.GCMS.Child item in gcms.Top(7))
        //{
        //    //gcms.ContentID = item[0];
        //}
        
    }
}
