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
using System.Data.SqlClient;
using System.Collections.Specialized;
using GCMS.PageCommonClassLib;

public partial class Content_Content_Relative : GCMS.PageCommonClassLib.PageBase
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
        this.Response.Write("<script language=javascript>parent.parent.parent.window.navigate('../Logon.aspx');</script>");
        return;
    }

    ContentCls content = new ContentCls();
    string Content_ID, TypeTree_ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        Content_ID = Request.QueryString["Content_ID"].ToString(); //必须
        TypeTree_ID = Request.QueryString["TypeTree_ID"].ToString(); //必须

        this.InputContent_ID.Value = Content_ID;
        this.InputTypeTree_ID.Value = TypeTree_ID;

        if (!this.IsPostBack)
        {
            string sSQL = "select Content_Contact.Other_ID,content_content.Name from Content_Contact,content_content where Content_Contact.Content_ID=" + Content_ID + " and content_content.Content_ID = Content_Contact.Other_ID and Relative_ID = " + TypeTree_ID;
            Type_List(sSQL);
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

    public void ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            int Other_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Other_ID"));
            string OutText = "<input type='checkbox' name='SelectedID' value=" + Other_ID + " id='SelectedID' checked>";
            e.Item.Cells[1].Text = OutText;
            e.Item.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Name"));
        }
    }

    protected void Submit1_ServerClick(object sender, EventArgs e)
    {
        Tools.DoSql("Delete Content_Contact where Content_ID = " + Content_ID + " and Relative_ID = " + TypeTree_ID);
        bool bRe = CheckForm();
        if (bRe)
        {
            string strIDs = this.Request["SelectedID"].ToString();
            char myChar = ',';
            int Relative_ID = 0;
            string[] ids = strIDs.Split(myChar);
            for (int j = 0; j < ids.Length; j++)
            {
                if (!MemberUsersInRoles(Content_ID, int.Parse(ids[j].ToString()), Relative_ID))
                {
                    Tools.DoSql("insert into Content_Contact ( Content_ID,Other_ID,Relative_ID) values (" + Content_ID + "," + int.Parse(ids[j].ToString()) + "," + TypeTree_ID + ")");
                }

            }

        }
        this.Response.Redirect("Content_Relative.aspx?Content_ID=" + Content_ID + "&TypeTree_ID=" + TypeTree_ID);
    }

    private bool MemberUsersInRoles(string Content_ID, int Other_ID, int Relative_ID)
    {
        SqlDataReader reader = null;
        string sql = "select * from Content_Contact where Content_ID=" + Content_ID + " and Other_ID = " + Content_ID + " and Relative_ID = " + Relative_ID;

        reader = Tools.DoSqlReader(sql);

        if (reader.Read())
        {
            reader.Close();
            return true;
        }
        else
        {
            reader.Close();
            return false;
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
}
