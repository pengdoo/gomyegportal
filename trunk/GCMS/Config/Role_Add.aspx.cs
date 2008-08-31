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
using GCMS.PageCommonClassLib;

public partial class Content_Role_Add : GCMS.PageCommonClassLib.PageBase
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
        this.Response.Write("<script language=javascript>alert(\"超时或非法操作！！！\");location.href='../Logon.aspx';</script>");
        return;
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string InputCheck()
    {
        string sReturn = "True";
        if (String.IsNullOrEmpty(this.Roles_Name.Text.Trim()))
        {
            sReturn = "Name";
            return sReturn;
        }
        return sReturn;
    }

    public void rolesAdd()
    {
        string sRe = InputCheck();
        switch (sRe)
        {
            case "True":
                SaveRole();
                break;
            case "Name":
                this.textMsg.Text = "请录入角色名称";
                break;
        }
    }

    public void SaveRole()
    {
     
        try
        {
            GCMSClassLib.Content.Roles roles = new GCMSClassLib.Content.Roles();
            roles.Create(this.Roles_Name.Text.ToString().Trim(), this.Roles_Explan.Text.ToString().Trim(), int.Parse(Session["Roles"].ToString()));
            Page.RegisterStartupScript("保存角色", "<script language=javascript>closethiswindows();</script>");
        }
        catch (Exception SUEx)
        {
            this.textMsg.Text = SUEx.Message;
        }
    }

    protected void Toolsbar1_ButtonClick(object sender, System.EventArgs e)
    {
        //文件上传处理，上传文件不为空
        rolesAdd();
    }
}
