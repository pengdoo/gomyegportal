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
using GCMSClassLib;
using GCMSClassLib.Content;
using GCMSClassLib.Public_Cls;
using GCMS.PageCommonClassLib;
public partial class _Default :GCMS.PageCommonClassLib.PageBase
{

    private SysLogon syslogon = new SysLogon();
    protected override void  OnPreInit(EventArgs e)
    {
        this.SessionAtuhFaiedEvent += new SessionAuthHandler(OnSessionAtuhFaiedEvent);//注册验证错误处理
        base.OnPreInit(e);
    }
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        switch(this.CopyAuthState)//判断序列证号
        {
            case EnumTypes.CopyAuthState.Illegal://验证序列号失败
                GSystem.SystemState = EnumTypes.SystemStates.Nolicensed;
                 Response.Redirect("Logon.aspx");
                break;
            case EnumTypes.CopyAuthState.Normal://验证序列号成功
                GSystem.SystemState = EnumTypes.SystemStates.Normal;
                string Master_UserName = Session["Master_UserName"].ToString();
                if (syslogon.IsRoles(int.Parse(Session["Roles"].ToString())))
                {
                    Page.RegisterStartupScript("进入系统", "<script language=javascript>defaultStatus = '当前用户：" + Master_UserName + " - 当前角色：" + syslogon.Roles_Name + "'</script>");

                }
                else
                {
                    Page.RegisterStartupScript("进入系统", "<script language=javascript>defaultStatus = '当前用户：" + Master_UserName + " - 对不起！您在该系统里没有任何角色和权限！'</script>");
                }
                break;
            default:
                GSystem.SystemState = EnumTypes.SystemStates.Nolicensed;
                break;
        }

        Control CtrTop = Page.LoadControl("Gomye_Tools/Default_Tools.ascx");
        CtrTop.ID = "ControlName";  //申明控件名
        ContentHeader.EnableViewState = false;  //指定是否启用ViewState
        ContentHeader.Controls.Add(CtrTop); //输出控件

    }

    /// <summary>
    /// 验证失败处理
    /// </summary>
    void OnSessionAtuhFaiedEvent()
    {
        GSystem.SystemState = EnumTypes.SystemStates.Overtime;
        Response.Redirect("Logon.aspx");
        return;
    }

}
