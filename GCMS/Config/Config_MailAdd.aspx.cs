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
using GCMSClassLib.SystemCls;
using GCMSClassLib.Public_Cls;
using GCMS.PageCommonClassLib;

public partial class Config_Config_MailAdd : GCMS.PageCommonClassLib.PageBase
{
    //订阅页面的自定义事件
    protected override void OnPreInit(EventArgs e)
    {
        this.SessionAtuhFaiedEvent += new SessionAuthHandler(OnSessionAtuhFaiedEvent);//注册验证错误处理
        base.OnPreInit(e);
    }

    /// <summary>
    /// 验证失败事件响应
    /// </summary>
    void OnSessionAtuhFaiedEvent()
    {
        GSystem.SystemState = EnumTypes.SystemStates.Overtime;
        this.Response.Write("<script language=javascript>parent.parent.parent.window.navigate('../Logon.aspx');</script>");
        return;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            SystemCls _SystemCls = new SystemCls();
            _SystemCls.Init();
            this.JMail_Server.Text = _SystemCls.JMail_Server;
            this.JMail_From.Text = _SystemCls.JMail_From;
            this.JMail_MailServerUserName.Text = _SystemCls.JMail_MailServerUserName;
            this.JMail_MailServerPassWord.Text = _SystemCls.JMail_MailServerPassWord;
        }
    }
    protected void Toolsbar1_ButtonClick(object sender, System.EventArgs e)
    {


        string sql = "update content_system set  " +
            "JMail_Server ='" + this.JMail_Server.Text + "', " +
            "JMail_From ='" + this.JMail_From.Text + "', ";
        if (!String.IsNullOrEmpty(this.JMail_MailServerPassWord.Text))
        {
            sql = sql + "JMail_MailServerPassWord='" + this.JMail_MailServerPassWord.Text + "', ";
        }
        sql = sql + "JMail_MailServerUserName ='" + this.JMail_MailServerUserName.Text + "'";

        Tools.DoSql(sql);


    }
}
