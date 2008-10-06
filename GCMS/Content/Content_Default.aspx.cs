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
using GCMS.PageCommonClassLib;

public partial class Content_Content_Default : GCMS.PageCommonClassLib.PageBase
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
        this.Page.Visible = false;
        return;
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------权限验证-----------------------
        if (this.Page.Visible == false)
        {
            OnSessionAtuhFaiedEvent();
            return;
        }
        if (!this.IsPostBack)
        {
            MainTree.UrlTemplete="parent.frames[\"Main_List\"].location =\"Content_List.aspx?TypeTree_ID=";
            MainTree.Mode = "2";
            if (int.Parse(this.GetSession("Roles", null)) == 0)
            {
                MainTree.Action = "GetRoot";
            }
            else
            {
                MainTree.Action = "GetRootByRole";
            }


        }
    }
}
