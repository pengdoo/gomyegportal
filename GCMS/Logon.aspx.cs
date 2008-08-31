//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-7-9
//
// 功能描述: 登陆页
//
// 已修改问题:
// 未修改问题:
// 修改记录
//       1   2008-7-10 添加注释
//------------------------------------------------------------------------------
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
using GCMSClassLib.Content;
using GCMSClassLib.Public_Cls;
using GCMS.PageCommonClassLib;

public partial class Logon : System.Web.UI.Page
{
    private SysLogon syslogon = new SysLogon();
    string Msg = "";

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!String.IsNullOrEmpty(GSystem.GetSysteStateMsg()))
        {
            Msg = GSystem.GetSysteStateMsg();
            GSystem.SystemState = EnumTypes.SystemStates.Normal;
        }
        lblMsg.Text = Msg;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //管理员登录


        try
        {
            string adminName = this.txtAdminName.Text.Trim().Replace("'", "");
            string adminPwd = this.txtAdminPwd.Text.Trim().Replace("'", "");



            adminPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(adminPwd, "MD5");

            if (syslogon.Init(adminName, adminPwd))
            {
                //this.Response.Redirect ("default.aspx");
                Session["Master_ID"] = syslogon.MasterID;
                Session["Master_UserName"] = syslogon.MasterUserName;

                syslogon.Roles(syslogon.MasterID);
                Session["Roles"] = syslogon.Roles_ID;
                Session.Timeout = 30;

                HttpCookie cookie = new HttpCookie("Gportal");
                cookie.Values.Add("Master_ID", Session["Master_ID"].ToString());
                cookie.Values.Add("Master_UserName", Session["Master_UserName"].ToString());
                cookie.Values.Add("Roles", Session["Roles"].ToString());
                Response.AppendCookie(cookie);


                //Page.RegisterStartupScript("进入系统","<script language=javascript> window.open('Default.aspx','','left=0,top=0,menubar=0,toolbar=0,directories=0,location=0,status=yes,scrollbars=0,resizable=yes');closeit();</script>");
                this.Response.Redirect("default.aspx");
            }
            else
            {
                this.lblMsg.Text = "<b>对不起！不能登录</b><br/>可能是用户名或密码出错";
                this.txtAdminName.Text = "";
            }

        }
        catch (Exception ex)
        {
            this.lblMsg.Text = ex.Message;
            //Response.Redirect("err.aspx?err=出现错误，可能执行了非法操作！<br/>请联系技术人员解决");
        }
    }
}
