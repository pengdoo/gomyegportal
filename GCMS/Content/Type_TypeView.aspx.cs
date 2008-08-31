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
using GCMSClassLib.Content;
using GCMS.PageCommonClassLib;

public partial class Content_Type_TypeView : GCMS.PageCommonClassLib.PageBase
{

    protected override void OnPreInit(EventArgs e)
    {
        this.SessionAtuhFaiedEvent += new SessionAuthHandler(OnSessionAtuhFaiedEvent);//注册验证错误处理
        base.OnPreInit(e);
    }

    /// <summary>
    /// 验证失败处理
    /// </summary>
    void OnSessionAtuhFaiedEvent()
    {
        GSystem.SystemState = EnumTypes.SystemStates.Overtime;
        this.Response.Write("<script language=javascript>parent.parent.parent.window.navigate('../Logon.aspx');</script>");
        return;
    }

    Type_TypeTree typeTree = new Type_TypeTree();
    public string TypeTree_ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        TypeTree_ID = this.Request.QueryString["TypeTree_ID"].ToString();
        typeTree.Init(int.Parse(TypeTree_ID));
        LTypeTree_CNameA.Text = typeTree.TypeTreeCName;
        this.LTypeTree_Issuance.Text = typeTree.strTypeTreeIssuance(int.Parse(typeTree.TypeTreeIssuance.ToString()));
        this.LTypeTree_Type.Text = typeTree.strTypeTreeType(int.Parse(typeTree.TypeTree_Type.ToString()));

        //this.LSonType.Text = typeTree.strSonTypeTree(int.Parse(TypeTree_ID));
        //新闻详情绑定
        DataList1.DataSource = Tools.DoSqlReader("select * from Content_Type_TypeTree where TypeTree_ID=" + Request.QueryString["TypeTree_ID"].ToString());
        DataList1.DataBind();

        this.txtTypeTree_ID.Value = TypeTree_ID;
        PageHeader.Value = "当前目录 - " + typeTree.TypeTreeCName;

        if (typeTree.TypeTreeIssuance.ToString() == "1") this.Panel1.Visible = true;
        if (typeTree.TypeTreeIssuance.ToString() == "4") this.Panel4.Visible = true;
        if (typeTree.TypeTreeIssuance.ToString() == "7") this.Panelbbs.Visible = true;


        InitaGrid();
    }
    public void InitaGrid()
    {

        xpTable.Attributes.Add("altRowColor", "oldlace");
        xpTable.Attributes.Add("align", "center");

        string cnString = "SELECT * FROM Content_Type_LinkPush where TypeTree_ID =" + TypeTree_ID;
        xpTable.DataSource = Tools.DoSqlReader(cnString);
        xpTable.DataBind();

    }
    public void ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            int Link_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Link_ID"));
            string LinkName = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "LinkName"));
            string Master_Icon;
            string IDtxt = "";

            e.Item.Attributes.Add("onmousedown", "selectContent('" + Link_ID + "');");
            e.Item.Attributes.Add("ondblclick", "openContent('" + Link_ID + "');");
            e.Item.ID = "item" + Link_ID;

            if (DataBinder.Eval(e.Item.DataItem, "LinkType") != null)
            {
                if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "LinkType")) == 0)
                { Master_Icon = "Icon_Master_off.gif"; }
                else
                { Master_Icon = "Icon_Master_on.gif"; }
                IDtxt = "<IMG id='img' src='../Admin_Public/Images/" + Master_Icon + "'>";
            }
            e.Item.Cells[0].Text = IDtxt;
            e.Item.Cells[1].Text = "<nobr><span class='submitdate' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "LinkName")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "LinkName")) + "</span></nobr>";
            e.Item.Cells[2].Text = "<nobr><span class='submitdate' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "TypeTree_Template")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "TypeTree_Template")) + "</span></nobr>";
            e.Item.Cells[3].Text = "<nobr><span class='submitdate' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "TypeTree_URL")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "TypeTree_URL")) + "</span></nobr>";

        }
    }

}
