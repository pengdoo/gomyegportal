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

public partial class Content_Type_TypeAddField : System.Web.UI.Page
{
    private string txtType;
    string TypeTree_ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        txtType = this.Request.QueryString["txtType"].ToString();
        TypeTree_ID = this.Request.QueryString["TypeTree_ID"].ToString();

        if (!this.IsPostBack)
        {
            InitaGrid();
        }
    }
    public void InitaGrid()
    {

        xpTable.Attributes.Add("altRowColor", "oldlace");
        xpTable.Attributes.Add("align", "center");

        string cnString = "select * from Content_FieldsName";
        xpTable.DataSource =Tools.DoSqlReader(cnString);
        xpTable.DataBind();
    }

    public void SaveUser()
    {
        string txtSql = "";
        if (txtType == "Content")
        { txtSql = " TypeTree_ContentFields = "; }
        if (txtType == "Type")
        { txtSql = " TypeTree_TypeFields = "; }

        string sID = this.Request["FieldsName_ID"].ToString();

        string sql = "Update Content_Type_Typetree set " + txtSql + sID + " where TypeTree_ID = " + TypeTree_ID;
        Tools.DoSql(sql);

        Page.RegisterStartupScript("保存目录", "<script language=javascript>closethiswindows();</script>");
    }

    protected void Toolsbar1_ButtonClick(Object sender, EventArgs e)
    {
        SaveUser();
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
}
