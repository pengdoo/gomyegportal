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
using System.Data.SqlClient;

public partial class Member_Setup_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (this.IsPostBack)
        {
            this.SubmitChar();
        }
        else
        {
            InitPage();
        }
    }
    public void InitPage()
    {
        SqlDataReader Reader = null;
        Reader = Tools.DoSqlReader("select * from Member_Setup");
        if (Reader.Read())
        {
            this.BadWords.Text = Reader["BadWords"].ToString();
        }
        else
        {
            Reader.Close();
        }
    }

    public void SubmitChar()
    {
        SqlDataReader Reader = null;
        Reader = Tools.DoSqlReader("select * from Member_Setup");
        if (Reader.Read())
        {
            Tools.DoSql("Update Member_Setup set BadWords = '" + this.BadWords.Text + "'");
            //#缺少错误判断和错误处理#
        }
        else
        {
            Reader.Close();
            Tools.DoSql("insert into Member_Setup(BadWords) values('" + this.BadWords.Text + "')");
            //#缺少错误判断和错误处理#
        }
        Response.Redirect("Setup_View.aspx");

    }
}
