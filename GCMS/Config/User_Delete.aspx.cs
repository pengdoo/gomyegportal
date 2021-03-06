﻿using System;
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
using GCMS.PageCommonClassLib;

public partial class Content_User_Delete : GCMS.PageCommonClassLib.PageBase
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
        this.Response.Write("<script language=javascript>alert(\"超时或非法操作！！！\");parent.windowclose();</script>");
        return;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string Master_ID = this.Request.QueryString["Master_ID"].ToString();
        string sSQL = "delete from Content_Master where Master_ID in (" + Master_ID + ")";
        Del(sSQL);
    }
    public void Del(string sSQL)
    {
        try
        {
            if (Tools.DoSqlRowsAffected(sSQL) > 0)
            {
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
            }
            else
            {
                this.Response.Write("<script language=javascript>alert('删除未成功!');</script>");
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
            }
        }
        catch
        {
            this.Response.Write("<script language=javascript>alert('删除未成功!');</script>");
            this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
        }
    }
}
