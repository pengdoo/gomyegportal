//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-8-25
//
// 功能描述: 内容管理字段设置
//
// 已修改问题:
// 未修改问题:
// 修改记录
//   2008-8-26 添加注释
//   2008-10-4 移除原来GXTree的控件引用，改用Tools_TreeMenu.ascx
//             修改表层Js方法，全部替换成标准Jquery库。
//             删除了两个过时Js方法
//----------------------------------系统引用-------------------------------------
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

public partial class Content_Type_TypeMain : GCMS.PageCommonClassLib.PageBase
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
            this.MainMenu.UrlTemplete = "parent.frames[\"Main_List\"].location =\"Type_TypeView.aspx?TypeTree_ID=";
            MainMenu.Mode = "1";
            if (int.Parse(this.GetSession("Roles",null)) == 0)
            { 
                MainMenu.Action = "GetRoot"; 
            }
            else
            {
                MainMenu.Action = "GetRootByRole";
            }

        }
    }
}
