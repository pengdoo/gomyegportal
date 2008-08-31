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
using GCMSClassLib.Content;
using GCMS.PageCommonClassLib;

public partial class Content_Stat_ViewMember : GCMS.PageCommonClassLib.PageBase
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
    string TxtstartDate = "0";
    string TxtendDate = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Type_List();
        }
    }
    public void Type_List()
    {

        DateGridList.Attributes.Add("altRowColor", "oldlace");
        DateGridList.Attributes.Add("align", "center");

        //Type_TypeTree _Type_TypeTree = new Type_TypeTree();
        //string txtIDSonTypeTree = _Type_TypeTree.IDSonTypeTree(int.Parse(TypeTree_ID));
        string sSQL = " select Master_id,Master_Name,Master_UserName from Content_Master ";
        DateGridList.DataSource = Tools.DoSqlReader(sSQL);
        DateGridList.DataBind();
        DateGridList.Dispose();
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
            int Content_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Master_ID"));

            e.Item.Attributes.Add("onmousedown", "selectContent('" + Content_ID + "');");
            e.Item.ID = "item" + Content_ID;



            Type_TypeTree typeTree = new Type_TypeTree();
            string IDtxt = "<IMG id='status" + Content_ID + "' ondragstart='InitDrag()' onclick='return(false)' src='../Admin_Public/Images/Icon_File_New.gif' >" + Content_ID;

            //IDtxt= IDtxt + "<img id='status"+Content_ID+"' src='"+StatusImg+"' width=16 height=16 alt='"+lockText+"' lockedby='"+lockedby+"'>"+Content_ID;

            e.Item.Cells[0].Text = IDtxt;
            e.Item.Cells[1].Text = "<nobr><span class='title' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Master_Name")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Master_Name")) + "</span></nobr>";
            e.Item.Cells[2].Text = "<nobr><span class='Author' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Master_UserName")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Master_UserName")) + "</span></nobr>";
            e.Item.Cells[3].Text = "<nobr><span class='submitdate'>" + typeTree.UserCount(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Master_UserName")), TxtstartDate, TxtendDate) + "</span></nobr>";



            //		
            //					switch (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"status")))
            //					{
            //						case 1:
            //							e.Item.Cells[4].Text = "<font color=red>待编辑</font>";
            //							break;
            //						case 2:
            //							e.Item.Cells[4].Text = "<font color=black>待审批</font>";
            //							break;
            //						case 3:
            //							e.Item.Cells[4].Text = "<font color=green>待发布</font>";
            //							break;
            //						case 4:
            //							e.Item.Cells[4].Text = "<font color=gray>已发布</font>";
            //							break;
            //						case 5:
            //							e.Item.Cells[4].Text = "<font color=blue>已归档</font>";
            //							break;
            //					}
            //
            //				e.Item.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"Clicks")).ToString();

        }
    }


    protected void Button_Click(object sender, System.EventArgs e)
    {
        TxtstartDate = this.startDate.Text;
        TxtendDate = this.endDate.Text;
        Type_List();
    }
}
