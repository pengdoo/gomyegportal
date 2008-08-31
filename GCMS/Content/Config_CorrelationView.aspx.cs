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

public partial class Content_Config_CorrelationView : GCMS.PageCommonClassLib.PageBase
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
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        this.PageHeader.Value = "相关内容管理";

        if (!this.IsPostBack)
        {
            //InitaGrid();
        }
    }

    //public void InitaGrid()
    //{
    //    xpTable.Attributes.Add("altRowColor", "oldlace");
    //    xpTable.Attributes.Add("align", "center");

    //    string cnString = "select * from Content_Correlation ORDER BY Correlation_ID DESC";
    //    xpTable.DataSource = Tools.DoSqlReader(cnString);
    //    xpTable.DataBind();
    //    DataConn.Dispose();

    //}
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
