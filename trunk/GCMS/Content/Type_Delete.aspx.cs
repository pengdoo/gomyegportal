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
using GCMS.PageCommonClassLib;

public partial class Content_Type_Delete : GCMS.PageCommonClassLib.PageBase
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

        int TypeTree_ID = int.Parse(this.Request.QueryString["TypeTree_ID"].ToString());
        Type_TypeTree typeTree = new Type_TypeTree();

        //判断是否包含子目录
        bool isExistSonType = typeTree.IsExistSonType(TypeTree_ID);

        //包含子目录或目录中有文章，不能删除
        if (isExistSonType)
        {
            this.Page.RegisterStartupScript("不能删除", "<script language=javascript>NoPermitDelete(" + TypeTree_ID + ");</script>");
        }
        else
        {
            //删除成功
            bool bFlag = typeTree.Delete(TypeTree_ID);
            if (bFlag)
            {
                this.Page.RegisterStartupScript("删除成功，刷新页面", "<script language=javascript>ReloadWindow();</script>");
            }
        }
    }
}
