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

public partial class Content_Type_TypeAddFields : GCMS.PageCommonClassLib.PageBase
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
    private String strTypeTreeID;
   // TypeAddFields AddFields = new TypeAddFields();

    Type_TypeTree _Type_TypeTree = new Type_TypeTree();
    ContentCls _ContentCls = new ContentCls();
    protected void Page_Load(object sender, EventArgs e)
    {
        strTypeTreeID = this.Request.QueryString["TypeTree_ID"].ToString();
        if (!this.IsPostBack)
        {
            _Type_TypeTree.Init(int.Parse(strTypeTreeID));
            Trees1.Text = _Type_TypeTree.TypeTree_XMLContent.ToString();
        }
    }
    protected void Toolsbar1_ButtonClick(object sender, System.EventArgs e)
    {
        Tools.DoSql("update Content_Type_TypeTree set TypeTree_XMLContent = '" + Trees1.Text.Trim() + "' where TypeTree_ID =" + strTypeTreeID);
        Response.Write("<script>top.window.close();</script>");
    }

}
