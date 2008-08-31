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
using System.Collections.Specialized;
using GCMS.PageCommonClassLib;

public partial class Config_Role_View : GCMS.PageCommonClassLib.PageBase
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
        this.PageHeader.Value = "角色管理";

        if (!this.IsPostBack)
        {
            InitaGrid();
        }
    }
    public void InitaGrid()
    {

        xpTable.Attributes.Add("altRowColor", "oldlace");
        xpTable.Attributes.Add("align", "center");

        string cnString = "SELECT * FROM Content_Roles ORDER BY Roles_ID DESC";
        xpTable.DataSource = Tools.DoSqlTable(cnString);
        xpTable.DataBind();


    }

    public void DelRoles()
    {
        bool bRe = CheckForm();
        if (bRe)
        {
            if (this.Request["SelectedID"] != null)
            {
                string strIDs = this.Request["SelectedID"].ToString();
                string sSQL = "delete from Content_Roles where Roles_ID in (" + strIDs + ")";
                Page.RegisterStartupScript("删除角色", "<script language=javascript>DelRole('" + sSQL + "');</script>");
                //this.InitaGrid();
            }
        }
        else
        {
            string sMsg = "请选择角色！";
            Page.RegisterStartupScript("删除角色", "<script language=javascript>fMsg('" + sMsg + "');</script>");
        }
    }

    public bool CheckForm()
    {
        int i;
        bool bSel = false;
        NameValueCollection coll;

        coll = this.Request.Form;
        String[] fStr = coll.AllKeys;

        for (i = 0; i < fStr.Length; i++)
        {
            //this.Response.Write("Form:" + fStr[i] + "<br/>");
            if (fStr[i] == "SelectedID")
            {
                bSel = true;
            }
        }
        return bSel;
    }

    public void ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {


        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            int Roles_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Roles_ID"));
            string Roles_Name = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Roles_Name"));

            e.Item.Attributes.Add("onmousedown", "selectContent('" + Roles_ID + "');");
            e.Item.Attributes.Add("ondblclick", "openContent('" + Roles_ID + "');");
            e.Item.ID = "item" + Roles_ID;



            string IDtxt = "<IMG id='img' src='../Admin_Public/Images/Icon_Master_on.gif'>";
            e.Item.Cells[0].Text = IDtxt;
            e.Item.Cells[1].Text = "<nobr><span class='submitdate' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Roles_Name")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Roles_Name")) + "</span></nobr>";
            e.Item.Cells[2].Text = "<nobr><span class='title' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Roles_Explan")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Roles_Explan")) + "</span></nobr>";

        }
    }
}
