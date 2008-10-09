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
using System.Collections.Generic;
using System.Data.SqlClient;
using GCMSClassLib.Public_Cls;
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
    DataTable dtTree;
    protected void btnClearTree_Click(object sender, EventArgs e)
    {
        string sql = "Select TypeTree_ID,TypeTree_ParentID from Content_Type_TypeTree Where TypeTree_ParentID=0";
        DataTable dt = Tools.DoSqlTable(sql);
         dtTree=Tools.DoSqlTable( "Select TypeTree_ID,TypeTree_ParentID from Content_Type_TypeTree ");
        foreach (DataRow dr in dt.Rows)
        {
            delete(int.Parse(dr["TypeTree_ID"].ToString()));
           
        }
        for (int i = 1; i < 10000; i++)
        {
            int s = Tools.DoSqlRowsAffected(" Delete from dbo.Content_Type_TypeTree Where TypeTree_ParentID  not in (Select TypeTree_ID from dbo.Content_Type_TypeTree )and TypeTree_ParentID!=-1");
            if (s == 0) break;
        }


    }
    private void delete(int tid)
    {
        Tools.DoSql("Delete from Content_Type_TypeTree Where TypeTree_ID=" + tid.ToString());
        
    }
}
