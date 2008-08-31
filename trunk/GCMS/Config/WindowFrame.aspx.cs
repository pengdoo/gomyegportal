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

public partial class Content_WindowFrame : GCMS.PageCommonClassLib.PageBase
{
    //订阅页面的自定义事件
    protected override void OnPreInit(EventArgs e)
    {
        this.SessionAtuhFaiedEvent += new SessionAuthHandler(OnSessionAtuhFaiedEvent);//注册验证错误处理
        this.SessionOrQueryGetFaiedEvent += new ParameterAuthHandler(OnSessionOrQueryGetFaiedEvent);
        base.OnPreInit(e);
    }

    /// <summary>
    /// 验证失败事件响应
    /// </summary>
    void OnSessionAtuhFaiedEvent()
    {
        GSystem.SystemState = EnumTypes.SystemStates.Overtime;
       // this.Response.Write("<script language=javascript>parent.parent.parent.window.navigate('../Logon.aspx');</script>");
        return;
    }
    /// <summary>
    /// Session或Query的访问失败默认响应
    /// </summary>
    /// <param name="key"></param>
    void OnSessionOrQueryGetFaiedEvent(string key)
    {
        //#未完成代码#
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string file = this.GetQueryString("loadfile", null);
        string url = string.Format("{0}?{1}", file, Request.QueryString.ToString()); 
        Response.Write(string.Format("<iframe scrolling=no src='{0}' id='funcArea' style='width:100%;height:100%;border:none'></iframe>",url));
    }
}
