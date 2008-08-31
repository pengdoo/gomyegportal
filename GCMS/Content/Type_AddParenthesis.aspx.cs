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

public partial class Content_Type_AddParenthesis : GCMS.PageCommonClassLib.PageBase
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
    Type_Parenthesis Parenthesis = new Type_Parenthesis();
    public int Link_ID;
    private String strType;
    private String strTypeTreeID;
    protected void Page_Load(object sender, EventArgs e)
    {
      
        strType = this.Request.QueryString["OrderType"].ToString();
        this.OrderType.Value = strType;
        strTypeTreeID = this.Request.QueryString["TypeTree_ID"].ToString();


        //更新
        if (strType.Equals("Update"))
        {
            Link_ID = int.Parse(this.Request.QueryString["Link_ID"].ToString());

            if (!this.IsPostBack)
            {
                Parenthesis.Init(Link_ID);


                this.LinkName.Value = Parenthesis.LinkName;
                this.TypeTree_URL.Value = Parenthesis.TypeTree_URL;
                this.LinkType.SelectedValue = Parenthesis.LinkType.ToString();
                this.TypeTree_Template.Value = Parenthesis.TypeTree_Template;
                this.Mssg.Text = "更新附带发布 - " + Parenthesis.LinkName;
            }
        }

        //删除

        if (strType.Equals("Delete"))
        {
            Link_ID = int.Parse(this.Request.QueryString["Link_ID"].ToString());

            Parenthesis.Delete(Link_ID);
            Page.RegisterStartupScript("保存目录", "<script language=javascript>closethiswindows();</script>");

        }

    }

    protected void Toolsbar1_ButtonClick(object sender, System.EventArgs e)
    {

        strType = this.Request.QueryString["OrderType"].ToString();


        Parenthesis.LinkName = this.LinkName.Value;
        Parenthesis.TypeTree_URL = this.TypeTree_URL.Value;
        Parenthesis.TypeTree_Template = this.TypeTree_Template.Value;
        Parenthesis.LinkType = int.Parse(this.LinkType.SelectedValue);
        Parenthesis.TypeTree_ID = int.Parse(this.strTypeTreeID);

        if (strType.Equals("Update"))
        {
            Link_ID = int.Parse(this.Request.QueryString["Link_ID"].ToString());
            bool bFlag = Parenthesis.Update(Link_ID);
            if (bFlag)
            {
                this.saveResult.Text = "更新成功";
            }
            else
            {
                this.saveResult.Text = "更新失败";
            }
            //Response.Write ("<script language=javascript>alert('"+this.saveResult.Text+"')</script>");
            //Page.RegisterStartupScript("保存目录","<script language=javascript>closethiswindows();</script>");
        }
        else
        {
            bool bFlag = Parenthesis.Create();
            if (bFlag)
            {
                this.saveResult.Text = "添加成功";
            }
            else
            {
                this.saveResult.Text = "添加失败";
            }
            //Response.Write ("<script language=javascript>alert('"+this.saveResult.Text+"')</script>");
            //Page.RegisterStartupScript("保存目录","<script language=javascript>closethiswindows();</script>");
        }
        Response.Redirect("Type_TypeView.aspx?TypeTree_ID=" + strTypeTreeID);
    }
}
