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
using System.Collections.Specialized;
using System.Data.SqlClient;
using GCMS.PageCommonClassLib;

public partial class Content_Type_RoleAdd : GCMS.PageCommonClassLib.PageBase
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
        this.Response.Write("<script language=javascript>alert(\"超时或非法操作！！！\");location.href='../Logon.aspx';</script>");
        return;
    }
    Type_TypeTree typeTree = new Type_TypeTree();

    int TypeTree_ID;
    private GCMSClassLib.Content.Roles roles;
    protected void Page_Load(object sender, EventArgs e)
    {

        TypeTree_ID = int.Parse(this.Request.QueryString["TypeTree_ID"].ToString());

        roles = new GCMSClassLib.Content.Roles();


        if (!this.IsPostBack)
        {
            InitUserGrid(TypeTree_ID);
        }
    }
    public void InitUserGrid(int TypeTree_ID)
    {
        xpTable.Attributes.Add("altRowColor", "oldlace");
        xpTable.Attributes.Add("align", "center");

        string sSQL = "select Content_Roles.* from Content_RolesConnect,Content_Roles where Content_Roles.Roles_ID = Content_RolesConnect.Roles_ID and Content_RolesConnect.TypeTree_ID=" + TypeTree_ID;
        xpTable.DataSource = Tools.DoSqlReader(sSQL);
        xpTable.DataBind();
    }

    protected void Toolsbar1_ButtonClick(object sender, System.EventArgs e)
    {

        //文件上传处理，上传文件不为空
        //			Microsoft.Web.UI.WebControls.ToolbarButton tb = new Microsoft.Web.UI.WebControls.ToolbarButton();
        //			tb=(Microsoft.Web.UI.WebControls.ToolbarButton)sender;
        int TypeTree_ID = int.Parse(this.Request.QueryString["TypeTree_ID"].ToString());

        Page.RegisterStartupScript("添加用户", "<script language=javascript>AddUser('" + TypeTree_ID + "');</script>");
    }
    protected void Toolsbar2_ButtonClick(object sender, System.EventArgs e)
    {
        DelUser();
        Page.RegisterStartupScript("添加用户", "<script language=javascript>window.location.href = 'Type_RoleAdd.aspx?TypeTree_ID=" + TypeTree_ID + "';</script>");
    }
    protected void Toolsbar3_ButtonClick(object sender, System.EventArgs e)
    {
        SqlDataReader readerSql = null;
        String sql = "select Roles_ID from Content_RolesConnect where TypeTree_ID=" + TypeTree_ID;
        string IDSonTypeTree = typeTree.IDSonTypeTree(TypeTree_ID);
        readerSql = Tools.DoSqlReader(sql);
        while (readerSql.Read())
        {
            string[] ops;
            char sSplit = ',';
            ops = IDSonTypeTree.Split(sSplit);

            for (int j = 0; j < ops.Length; j++)
            {
                if (!String.IsNullOrEmpty(ops[j]))
                {
                    if (typeTree.IsRolesConnect(int.Parse(ops[j].ToString()), readerSql.GetInt32(0)) == false)
                    {
                        typeTree.CreateRolesConnect(int.Parse(ops[j].ToString()), readerSql.GetInt32(0));
                    }
                }
            }
        }
        readerSql.Close();
    }

    



    public void DelUser()
    {
        if (CheckForm())
        {
            string strIDs = this.Request["SelectedID"].ToString();
            try
            {
                string sSQL = "delete from Content_RolesConnect where TypeTree_ID=" + TypeTree_ID + " and Roles_ID in (" + strIDs + ")";
                if (!(Tools.DoSqlRowsAffected(sSQL) > 0))
                {
                    this.textMsg.Text = "删除用户列表错误！";
                }
            }
            catch (Exception e)
            {
                this.textMsg.Text = e.Message;
            }
        }
        //InitUserGrid();
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
