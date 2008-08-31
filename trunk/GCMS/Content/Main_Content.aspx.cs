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

public partial class Content_Main_Content : GCMS.PageCommonClassLib.PageBase
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
        this.Response.Write("<script language=javascript>alert(\"超时操作！！！\");parent.parent.parent.window.navigate('../Logon.aspx');</script>");
        return;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        int RightID = int.Parse(this.Request["RightID"].ToString());
        string MainUrl;

        switch (RightID)
        {
            case 0:
                MainUrl = "../Gomye_Tools/Default_Welcome.ascx";
                break;
            case 1:
                MainUrl = "ContentType/Type_View.ascx";
                break;
            case 2:
                MainUrl = "ContentContent/Content_List.ascx";
                break;
            case 3:
                MainUrl = "ContentContent/Content_Main.ascx";
                break;

            case 4:
                MainUrl = "反馈 (收集客户反馈资料)";
                break;

            case 5:
                MainUrl = "商城 (GShop系统支持)";
                break;

            case 6:
                MainUrl = "博客 (GBlog系统支持)";
                break;

            case 7:
                MainUrl = "论坛 (GForums系统支持)";
                break;

            case 8:
                MainUrl = "图片 (GPhoto系统支持)";
                break;

            default:
                MainUrl = "../Gomye_Tools/Default_Welcome.ascx";
                break;
        }
        //			Response.Write(RightID);
        //			Response.End();
        Control CtrContent = Page.LoadControl(MainUrl);
        //Control CtrContent = Page.LoadControl("Gomye_Tools/Default_Welcome.ascx");
        CtrContent.ID = "ControlName";  //申明控件名
        ContentRight.EnableViewState = false;  //指定是否启用ViewState
        ContentRight.Controls.Add(CtrContent); //输出控件
    }
}
