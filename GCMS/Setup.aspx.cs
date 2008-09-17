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
using System.Text;
using System.IO;
public partial class Setup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string _HTTP = "GomyeGomye" + Request.ServerVariables["HTTP_HOST"].ToString() + ".net.net";
        string _HTTPs = FormsAuthentication.HashPasswordForStoringInConfigFile(_HTTP.ToString(), "MD5");
        key.Value = _HTTPs;

        
    }
    
    protected void btnGoInstal_Click(object sender, EventArgs e)
    {
        Stream stream = fileSql.FileContent;
        StreamReader reader = new StreamReader(stream);
        StringBuilder sb=new StringBuilder();
        long totleCount = stream.Length;
        int prsint;
        while (!reader.EndOfStream)
        {
            string line=reader.ReadLine();
            sb.AppendLine(line);

           double pre = (double)stream.Position *100/ (double)totleCount;
            prsint=(int)pre<=100?(int)pre:100;
           SetProgress("spaceused4", 50);
        }

        
    }

    void CreateSql()
    {
 
    }

    public void SetProgress(string barid,int num)
    {
        string scriptStr = string.Empty;
        scriptStr+="<script>";
        scriptStr+="$(document).ready(function() {";
        scriptStr+="$(\"#" + barid + "\").progressBar({ barImage: 'js/jquery.progressbar/images/progressbg_yellow.gif'} );";
        scriptStr+="$(\"#" + barid + "\").progressBar(" + num.ToString() + ");";
        scriptStr+="});";
        scriptStr+=" </script>";
        this.RegisterClientScriptBlock("setProgress", scriptStr.ToString());

    }

}
