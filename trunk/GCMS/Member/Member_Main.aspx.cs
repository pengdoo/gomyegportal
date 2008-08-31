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
using GCMSClassLib.Member;

public partial class Member_Member_Main : System.Web.UI.Page
{
    MemberCls _MemberCls = new MemberCls();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Type_List();
        }
    }
    public void Type_List()
    {
        DataSet ds = new DataSet();


        DateGridList.Dispose();
        DateGridList.Attributes.Add("altRowColor", "oldlace");
        DateGridList.Attributes.Add("align", "center");

        string sSQL = "select * from Member_Roles order by RoleID";
        DateGridList.DataSource =Tools.DoSqlReader(sSQL);

        DateGridList.DataBind();

        //xpath=Server.MapPath(xmlfile);
    }
    public void ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {

        string StatusImg;

        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            int Content_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "RoleID"));
            e.Item.ID = "item" + Content_ID;
            e.Item.Attributes.Add("onmousedown", "selectContent('" + Content_ID + "');");
            e.Item.Attributes.Add("ondblclick", "openContent('" + Content_ID + "');");
            e.Item.Attributes.Add("ondragenter", "dragEnter();");
            e.Item.Attributes.Add("ondragleave", "dragLeave();");
            e.Item.Attributes.Add("ondragover", "dragOver();");
            e.Item.Attributes.Add("ondrop", "FinishDrag(" + Content_ID + ");");

            if (Content_ID <= 10)
            {
                StatusImg = " src='../Admin_Public/Images/Icon_Roles.gif'";
            }
            else
            {
                StatusImg = " src='../Admin_Public/Images/Icon_Master_on.gif'";
            }


            string IDtxt = "<IMG id='status" + Content_ID + "' ondragstart='InitDrag()' onclick='return(false)'" + StatusImg + ">" + Content_ID;
            e.Item.Cells[0].Text = IDtxt;
            if (Content_ID <= 10)
            {
                e.Item.Cells[1].Text = "<nobr><span title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "name")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "name")) + "</span></nobr>";
            }
            else
            {
                e.Item.Cells[1].Text = "<nobr><span title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "name")) + "><u>" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "name")) + "</u></span></nobr>";
            }
        }
    }

    
}
