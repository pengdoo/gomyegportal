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

public partial class Content_Content_RelativeContent : System.Web.UI.Page
{
    private string sTypeTree_ID;
    string sSQL;
    protected void Page_Load(object sender, EventArgs e)
    {
        sTypeTree_ID = Request.QueryString["TypeTree_ID"].ToString(); //必须知道在那个节点下
        if (!this.IsPostBack)
        {
            if (sTypeTree_ID != null)
            {
                sSQL = "select * from Content_Content where TypeTree_ID = '" + sTypeTree_ID + "' order by OrderNum desc";
                Type_List(sSQL);
            }
        }
    }

    public void Type_List(string sSQL)
    {
        typeTable.Dispose();
        typeTable.Attributes.Add("altRowColor", "oldlace");
        typeTable.Attributes.Add("align", "center");
        try
        {
            typeTable.DataSource = Tools.DoSqlReader(sSQL);
            typeTable.DataBind();
        }
        catch (Exception CLEx)
        {
            throw new Exception(CLEx.Message);
        }
    }

    protected void BUTTON1_ServerClick(object sender, System.EventArgs e)
    {
        string KeyWord = this.keyword.Value;
        if (KeyWord != "")
        {
            sSQL = "select * from Content_Content where name like '%" + KeyWord + "%'or Description like '%" + KeyWord + "%' order by OrderNum desc";
            Type_List(sSQL);
        }
    }

    public void ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            int Content_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Content_ID"));
            string OutText = "<input type='checkbox' name='cid' value=" + Content_ID + " onclick='doDocClick(this);' id='Doc_" + Content_ID + "'>";
            e.Item.Cells[0].Text = OutText;
        }
    }
}
