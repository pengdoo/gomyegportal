//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-8-26
//
// 功能描述: 相关内容设置(功能未完成)
//
// 已修改问题:
// 未修改问题:
// 修改记录
//   2008-8-26 添加注释
//   2008-8-31  规范【自定义事件】【SQL引用】【字符处理】【页面参数获取】代码
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
//----------------------------------项目引用-----------------------------------
using GCMSClassLib.Public_Cls;
using GCMS.PageCommonClassLib;
//------------------------------------------------------------------------------

public partial class Content_Config_CorrelationView : GCMS.PageCommonClassLib.PageBase
{
    #region 自定义事件的注册和处理
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
    #endregion 自定义事件的注册和处理

    protected void Page_Load(object sender, System.EventArgs e)
    {
        this.PageHeader.Value = "相关内容管理";
    }

    public void ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            int Roles_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Correlation_ID"));
            string Roles_Name = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Correlation_Name"));

            e.Item.Attributes.Add("onmousedown", "selectContent('" + Roles_ID + "');");
            e.Item.Attributes.Add("ondblclick", "openContent('" + Roles_ID + "');");
            e.Item.ID = "item" + Roles_ID;

            string IDtxt = "<IMG id='img' src='../Admin_Public/Images/Icon_Master_on.gif'>";
            e.Item.Cells[0].Text = IDtxt;
            e.Item.Cells[1].Text = "<nobr><span class='submitdate' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Correlation_Name")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Correlation_Name")) + "</span></nobr>";

        }
    }
}
