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
using System.Threading;
using System.Web.SessionState;
public partial class Setup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string _HTTP = "GomyeGomye" + Request.ServerVariables["HTTP_HOST"].ToString() + ".net.net";
        string _HTTPs = FormsAuthentication.HashPasswordForStoringInConfigFile(_HTTP.ToString(), "MD5");
        key.Value = _HTTPs;
        if(Session["progress_runsql"]==null)
        {
            Session["progress_runsql"]=0;
        }
        else
        {
            int pp = int.Parse(Session["progress_runsql"].ToString());

        }
        
    }
    
    protected void btnGoInstal_Click(object sender, EventArgs e)
    {
        SetSqlProgress();
        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(CreateSql));
        thread.Start((object)this.Session);
       
        //SetProgress("spaceused4", 50);
        
    }


    void CreateSql(object objsession)
    {
        HttpSessionState cSession = objsession as HttpSessionState;
        //Stream stream = fileSql.FileContent;
        ////stream.Position = 0;
        //StreamReader reader = new StreamReader(stream);
        //StringBuilder sb = new StringBuilder();
        //long totleCount = stream.Length;
        
        //lock (stream)
        //{
        //while (stream.Position != totleCount)
        //    {
        //        string line = reader.ReadLine();
        //        sb.AppendLine(line);

        //        double pre = (double)stream.Position * 100 / (double)totleCount;

        //        cSession["progress_runsql"] = pre;
        //        Thread.Sleep(500);
        //    }
        //    reader.Close();
        //    //stream.Close();
        //}
        lock (cSession)
        {
            for (int pre = 0; pre <= 100; pre++)
            {

                Thread.Sleep(1000);
                cSession["progress_runsql"] = pre;
                SetSqlProgress();
            }
        }
    }

    public void SetSqlProgress()
    {
        //string scriptStr = "<script> $(\"#progress_sql\").progressBar(" + Session["progress_runsql"] .ToString()+ ");</script>";
        string scriptStr = "<script> window.setInterval(\"showsqlprog()\", 500);</script>";
        //string scriptStr = "
        //scriptStr+="<script>";
        //scriptStr+="$(document).ready(function() {";
        //scriptStr+="$(\"#" + barid + "\").progressBar({ barImage: 'js/jquery.progressbar/images/progressbg_yellow.gif'} );";
        //scriptStr+="$(\"#" + barid + "\").progressBar(" + num.ToString() + ");";
        //scriptStr+="});";
        //scriptStr+=" </script>";
        this.RegisterClientScriptBlock("begSqlProgress", scriptStr);

    }

}
