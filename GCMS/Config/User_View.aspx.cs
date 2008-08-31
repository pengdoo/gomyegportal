﻿using System;
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
using GCMSClassLib.Content;
using System.Collections.Specialized;
using GCMS.PageCommonClassLib;

public partial class Config_User_View : GCMS.PageCommonClassLib.PageBase
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
    private Master master = new Master();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageHeader.Value = "用户管理";

        if (!this.IsPostBack)
        {
            InitaGrid();
        }
    }
    public void InitaGrid()
    {

        xpTable.Attributes.Add("altRowColor", "oldlace");
        xpTable.Attributes.Add("align", "center");

        string cnString = "SELECT * FROM Content_Master ORDER BY Master_ID DESC";
        xpTable.DataSource = Tools.DoSqlReader(cnString);
        xpTable.DataBind();

    }

    public void DelUser()
    {
        bool bRe = CheckForm();
        if (bRe)
        {
            if (this.Request["SelectedID"] != null)
            {
                string strIDs = this.Request["SelectedID"].ToString();
                string sSQL = "delete from Content_Master where Master_ID in (" + strIDs + ")";
                Page.RegisterStartupScript("删除用户", "<script language=javascript>DelUser('" + sSQL + "');</script>");
                //this.InitaGrid();
            }
        }
        else
        {
            string sMsg = "请选择用户！";
            Page.RegisterStartupScript("修改密码", "<script language=javascript>fMsg('" + sMsg + "');</script>");
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
            int Master_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Master_ID"));
            string Master_Name = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Master_Name"));
            string Master_Icon;

            e.Item.Attributes.Add("onmousedown", "selectContent('" + Master_ID + "');");
            e.Item.Attributes.Add("ondblclick", "openContent('" + Master_ID + "');");
            e.Item.ID = "item" + Master_ID;

            if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Master_Usableness")) == 0)
            { Master_Icon = "Icon_Master_off.gif"; }
            else
            { Master_Icon = "Icon_Master_on.gif"; }

            string IDtxt = "<IMG id='img' src='../Admin_Public/Images/" + Master_Icon + "'>";
            e.Item.Cells[0].Text = IDtxt;
            e.Item.Cells[1].Text = "<nobr><span class='submitdate' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Master_Name")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Master_Name")) + "</span></nobr>";
            e.Item.Cells[2].Text = "<nobr><span class='submitdate' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Master_UserName")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Master_UserName")) + "</span></nobr>";
            e.Item.Cells[3].Text = "<nobr><span class='submitdate' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Master_Tel")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Master_Tel")) + "</span></nobr>";

        }
    }	

}