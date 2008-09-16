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
using GCMSClassLib.Content;
using GCMSClassLib.Public_Cls;
using System.Data.SqlClient;
using System.Collections.Specialized;
using GCMS.PageCommonClassLib;

public partial class Content_Type_TypeEdit : GCMS.PageCommonClassLib.PageBase
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
        this.Page.Visible = false;
        return;
    }

    #region 当页的全局变量
   
    int Current_TypeTree_ID
    {
        get
        {
            txtTypeTree_ID.Value = this.GetQueryString("TypeTree_ID", "0");
            return int.Parse(txtTypeTree_ID.Value);
        }
        set
        {
            txtTypeTree_ID.Value = value.ToString();
        }
    }
    #endregion 当页的全局变量

    //private int TypeTree_ID = 0;
    Type_TypeTree typeTree = new Type_TypeTree();
    ContentCls _ContentCls = new ContentCls();
    protected void Page_Load(object sender, EventArgs e)
    {
        //TypeTree_ID = int.Parse(this.Request.QueryString["TypeTree_ID"].ToString());
        txtType.Value = this.Request.QueryString["Type"].ToString();
        //this.txtTypeTree_ID.Value = TypeTree_ID.ToString();
        if (!this.IsPostBack)
        {
            AddFieldsWriteTxt(Current_TypeTree_ID);
        }
    }

    protected void AddFieldsWriteTxt(int TypeTree_ID)
    {
        string sql1 = "";
        Type_TypeTree _Type_TypeTree = new Type_TypeTree();
        _Type_TypeTree.Init(TypeTree_ID);
        if (this.Request.QueryString["Type"].ToString() == "Type") { sql1 = _Type_TypeTree.TypeTree_TypeFields.ToString(); };
        if (this.Request.QueryString["Type"].ToString() == "Content") { sql1 = _Type_TypeTree.TypeTree_ContentFields.ToString(); };

        SqlDataReader myReader;
        string sql = "SELECT Fields_ID,Property_Name,Property_InputType,Property_Alias,Property_InputOptions FROM  Content_FieldsContent WHERE FieldsName_ID =" + sql1 + "order by Property_Order";
        string ToolsPut;
        myReader = Tools.DoSqlReader(sql);
        while (myReader.Read())
        {

            switch (myReader.GetString(2))
            {
                case "TEXT":
                    ToolsPut = "<input type='text' size='30' class='inputtext' name=" + myReader.GetString(1) + ">";
                    break;
                case "IMAGE":
                    ToolsPut = "<input type='text' size='30' class='inputtext' " + myReader.GetString(1) + "> <input type='button' value='...'>";
                    break;
                case "FILE":
                    ToolsPut = "<input type='text' size='30' class='inputtext' " + myReader.GetString(1) + "> <input type='button' value='...'>";
                    break;
                case "DATETIME":
                    ToolsPut = "<input type='text' size='30' class='inputtext' name=" + myReader.GetString(1) + "><img src='../Admin_Public/Images/Icon_calendar.gif'>";
                    break;
                case "TREES":
                    ToolsPut = "<input type='text' size='30' class='inputtext' name=" + myReader.GetString(1) + "><img src='../Admin_Public/Images/RepeatedRegion.gif'>";
                    break;

                case "TEXTAREA":
                    ToolsPut = "<textarea name=" + myReader.GetString(1) + " rows='6' cols='30'></textarea>";
                    break;
                case "SELECT":

                    string[] ops;
                    string opss;
                    char sSplit = ',';
                    opss = myReader.GetString(4);

                    int i = 10;
                    char c = (char)i;			//相当于vb中的chr(10)

                    opss = opss.Replace(c, sSplit);
                    ops = opss.Split(sSplit);
                    ToolsPut = "<select size='1' name='" + myReader.GetString(1) + "' class='inputtext'>";

                    for (int j = 0; j < ops.Length; j++)
                    {
                        ToolsPut = ToolsPut + "<option value=" + ops[j].ToString() + ">" + ops[j].ToString() + "</option>";
                    }
                    ToolsPut = ToolsPut + "<select>";
                    break;
                case "LABEL":
                    ToolsPut = myReader.GetString(4);
                    break;
                case "NUMBER":
                    ToolsPut = "<input type='text' size='30' class='inputtext' name=" + myReader.GetString(1) + ">";
                    break;

                default:
                    ToolsPut = "数据错误！";
                    break;
            }

            string SelectIDs = "";
            string sSQL = "select ISNULL(TypeTree_Show, '') AS TypeTree_Show from Content_Type_TypeTree where TypeTree_ID=" + TypeTree_ID;
            SqlDataReader myRead = Tools.DoSqlReader(sSQL);
            while (myRead.Read())
            {
                if (myRead.GetString(0) != "")
                {
                    if (myRead.GetString(0).IndexOf(myReader.GetString(1)) != -1)
                    {
                        SelectIDs = "Checked";
                    }
                }
            }
            myRead.Close();

            this.AddFieldsWrite.Text = this.AddFieldsWrite.Text +
                "<TR valign='top'><TD style='WIDTH: 20px;'><input type=checkbox name='SelectedID' " + SelectIDs + "  value='" + myReader.GetString(1) + "'></TD><TD style='WIDTH: 120px;table-layout:fixed;word-wrap:break-word;'>" + myReader.GetString(3) +
                "：</TD><TD style='WIDTH: 250px;table-layout:fixed;word-wrap:break-word;'>" + ToolsPut +
                "</TD><TD></TD></TR>";

        }
        myReader.Close();
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

    protected void Toolsbar1_ButtonClick(object sender, System.EventArgs e)
    {
    
        string sID = this.Request["SelectedID"];

        string sSQL = string.Format("Update Content_Type_TypeTree set TypeTree_Show = '{0}' where TypeTree_Id = {1}", sID, Current_TypeTree_ID);
        Tools.DoSql(sSQL);
        Page.RegisterStartupScript("保存目录", "<script language=javascript>top.window.close();</script>");
    }


    protected void Toolsbar3_ButtonClick(object sender, System.EventArgs e)
    {
        string sql1 = "";
        string TypeTree_ID = this.Request.QueryString["TypeTree_ID"].ToString();
        typeTree.Init(int.Parse(TypeTree_ID));
        if (this.Request.QueryString["Type"].ToString() == "Type") { sql1 = " TypeTree_TypeFields = " + typeTree.TypeTree_TypeFields.ToString(); };
        if (this.Request.QueryString["Type"].ToString() == "Content") { sql1 = " TypeTree_ContentFields = " + typeTree.TypeTree_ContentFields.ToString(); };

        SqlDataReader myReader;
        myReader = Tools.DoSqlReader("SELECT TypeTree_ID FROM  Content_Type_TypeTree WHERE TypeTree_ParentID =" + typeTree.TypeTreeParentID);
        while (myReader.Read())
        {
            if (myReader.GetInt32(0).ToString() != "" && myReader.GetInt32(0).ToString() != TypeTree_ID)
            {
                string sql = "update Content_Type_typetree set " + sql1 + " where TypeTree_ID  = " + myReader.GetInt32(0);
                //Change By Galen Mu  2008.8.25
                //将content.DoSelect(..)  改为 Tools.DoSql(..) 
                Tools.DoSql(sql);
            }
        }
        myReader.Close();
    }

    protected void Toolsbar2_ButtonClick(object sender, System.EventArgs e)
    {

        string sql1 = "";
        string TypeTree_ID = this.Request.QueryString["TypeTree_ID"].ToString();
        typeTree.Init(int.Parse(TypeTree_ID));
        if (this.Request.QueryString["Type"].ToString() == "Type") { sql1 = " TypeTree_TypeFields = " + typeTree.TypeTree_TypeFields.ToString(); };
        if (this.Request.QueryString["Type"].ToString() == "Content") { sql1 = " TypeTree_ContentFields = " + typeTree.TypeTree_ContentFields.ToString(); };
        string IDSonTypeTree = "";
        IDSonTypeTree = typeTree.IDSonTypeTree(int.Parse(TypeTree_ID));
        string[] ops;
        char sSplit = ',';
        ops = IDSonTypeTree.Split(sSplit);
        for (int j = 0; j < ops.Length; j++)
        {
            if (ops[j].ToString() != "" && ops[j].ToString() != TypeTree_ID)
            {
                //AddFields.Deletes(int.Parse(ops[j].ToString()));
                string sql = "update Content_Type_typetree set " + sql1 + " where TypeTree_ID  = " + int.Parse(ops[j].ToString());
                Tools.DoSql(sql);
            }
        }
    }

    protected void toolsbar_delectexfd_ButtonClick(object sender, System.EventArgs e)
    {
        string sql = string.Format("update Content_Type_TypeTree set TypeTree_ContentFields=0,TypeTree_TypeFields=0,TypeTree_Show='' Where TypeTree_ID={0}", Current_TypeTree_ID);
        Tools.DoSql(sql);
        Page.RegisterStartupScript("保存目录", "<script language=javascript>top.window.close();</script>");
    }
    public void ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {

        string StatusImg;
        string lockText;

        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            int Content_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Content_ID"));
            string lockedby = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "lockedby"));

            e.Item.Attributes.Add("onmousedown", "selectContent('" + Content_ID + "');");
            e.Item.Attributes.Add("ondblclick", "openContent('" + Content_ID + "');");
            e.Item.Attributes.Add("ondragenter", "dragEnter();");
            e.Item.Attributes.Add("ondragleave", "dragLeave();");
            e.Item.Attributes.Add("ondragover", "dragOver();");
            e.Item.Attributes.Add("ondrop", "FinishDrag(" + Content_ID + ");");
            e.Item.ID = "item" + Content_ID;


            if (lockedby == "")
            {
                lockText = "锁定状态：当前没有锁定";
                StatusImg = "src='../Admin_Public/Images/Icon_File_New.gif' alt='" + lockText + "' lockedby='" + lockedby + "'";
            }
            else if (lockedby == Session["Master_UserName"].ToString())
            {
                lockText = "锁定状态：文章由您锁定，您可以执行操作";
                StatusImg = "src='../Admin_Public/Images/ic_lockuser.gif' alt='" + lockText + "' lockedby='" + lockedby + "'";
            }
            else
            {
                lockText = "锁定状态：文章由 " + lockedby + "锁定，您不能执行操作";
                StatusImg = "src='../Admin_Public/Images/ic_lock.gif' alt='" + lockText + "' lockedby='" + lockedby + "'";
            }
            if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "AtTop1")) == 1)
            {
                lockText = "顶";
                StatusImg = "src='../Admin_Public/Images/sort_uparrow.gif' alt='" + lockText + "' lockedby='" + lockedby + "'";
            }

            string IDtxt = "<IMG id='status" + Content_ID + "' ondragstart='InitDrag()' onclick='return(false)'  " + StatusImg + ">" + Content_ID;
            //IDtxt= IDtxt + "<img id='status"+Content_ID+"' src='"+StatusImg+"' width=16 height=16 alt='"+lockText+"' lockedby='"+lockedby+"'>"+Content_ID;

            e.Item.Cells[0].Text = IDtxt;
            e.Item.Cells[1].Text = "<nobr><span class='title' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "name")) + ">" + Tools.DBToWeb(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "name"))) + "</span></nobr>";
            e.Item.Cells[2].Text = "<nobr><span class='Author' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Author")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Author")) + "</span></nobr>";
            e.Item.Cells[3].Text = "<nobr><span class='submitdate' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "submitdate")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "submitdate")) + "</span></nobr>";

            switch (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "status")))
            {
                case 1:
                    e.Item.Cells[4].Text = "<font color=red>草 稿</font>";
                    break;
                case 2:
                    e.Item.Cells[4].Text = "<font color=black>待审批</font>";
                    break;
                case 3:
                    e.Item.Cells[4].Text = "<font color=green>待发布</font>";
                    break;
                case 4:
                    e.Item.Cells[4].Text = "<font color=gray>已发布</font>";
                    break;
                case 5:
                    e.Item.Cells[4].Text = "<font color=blue>已归档</font>";
                    break;
            }

            if (Convert.ToChar(DataBinder.Eval(e.Item.DataItem, "Head_news")).ToString() == "1")
            {
                e.Item.Cells[5].Text = "是";
            }
            else
            {
                e.Item.Cells[5].Text = "否";
            }

            if (Convert.ToChar(DataBinder.Eval(e.Item.DataItem, "Picture_news")).ToString() == "1")
            {
                e.Item.Cells[6].Text = "是";
            }
            else
            {
                e.Item.Cells[6].Text = "否";
            }
        }
    }
}
