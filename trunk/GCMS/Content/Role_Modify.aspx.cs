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
using GCMS.PageCommonClassLib;

public partial class Content_Role_Modify : GCMS.PageCommonClassLib.PageBase
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
    private int iRoles_ID;
    private GCMSClassLib.Content.Roles roles=new GCMSClassLib.Content.Roles() ;

    protected void Page_Load(object sender, EventArgs e)
    {
        iRoles_ID = int.Parse(this.Request.QueryString["Roles_ID"].ToString());
        string sMaster_ID = this.Request.QueryString["Master_ID"].ToString();
        txtTypeTree_ID.Value = iRoles_ID.ToString();

        if (!this.IsPostBack)
        {
            InitGrid();
            //初始化角色已经添加的用户
            if (sMaster_ID == "Null")
            {
                InitUserGrid();
            }
            else
            {
                SaveRolesMaster(sMaster_ID);
                InitUserGrid();
            }
        }
    }
    public void InitGrid()
    {
        if (roles.Init(iRoles_ID))
        {
            this.Roles_Name.Text = roles.RolesName.ToString();
            this.Roles_Explan.Text = roles.RolesExplan.ToString();
        }
    }

    public void InitUserGrid()
    {
        xpTable.Attributes.Add("altRowColor", "oldlace");
        xpTable.Attributes.Add("align", "center");

        string sSQL = "select * from Content_Master where Master_ID in (select Master_ID from Content_RolesMaster where Roles_ID=" + iRoles_ID + ")";
        xpTable.DataSource = Tools.DoSqlReader(sSQL);
        xpTable.DataBind();
    }

    public void SaveRoles()
    {
        string sRe = InputCheck();
        switch (sRe)
        {
            case "True":
                {
                    try
                    {
                        string sSQL = "update Content_Roles set Roles_Name = '" + this.Roles_Name.Text.ToString().Trim()
                            + "',Roles_Explan = '" + this.Roles_Explan.Text.ToString().Trim() + "' where Roles_ID = " + iRoles_ID;
                        if (Tools.DoSqlRowsAffected(sSQL) < 1)
                        {
                            this.textMsg.Text = "修改角色存储错误!";
                        }
                        Page.RegisterStartupScript("保存角色", "<script language=javascript>closethiswindows();</script>");
                    }
                    catch (Exception Ex)
                    {
                        this.textMsg.Text = Ex.Message;
                    }
                    break;
                }
            case "Name":
                this.textMsg.Text = "请录入角色名称";
                break;
        }

    }
    protected void Toolsbar1_ButtonClick(Object sender, EventArgs e)
    {
        SaveRoles();
    }


    protected void Toolsbar2_ButtonClick(Object sender, EventArgs e)
    {
        DelUser();
    }

    /// <summary>
    /// 保存时检查录入值
    /// </summary>
    /// <returns></returns>
    public string InputCheck()
    {
        string sReturn = "True";
        if (this.Roles_Name.Text.ToString() == "")
        {
            sReturn = "Name";
            return sReturn;
        }
        return sReturn;
    }

    public void SaveRolesMaster(string sID)
    {
        char myChar = ',';
        string[] ids = sID.Split(myChar);
        string sSQL;
        try
        {
            for (int i = 0; i < ids.Length; i++)
            {
                sSQL = "insert into Content_RolesMaster(Roles_ID,Master_ID) values(" + iRoles_ID
                    + "," + ids[i] + ")";
                if (!(Tools.DoSqlRowsAffected(sSQL) > 0))
                {
                    this.textMsg.Text = "保存提交的用户列表错误";
                }
            }
        }
        catch (Exception e)
        {
            this.textMsg.Text = e.Message;
        }
    }

    public void DelUser()
    {
        if (CheckForm())
        {
            string strIDs = this.Request["SelectedID"].ToString();
            try
            {
                string sSQL = "delete from Content_RolesMaster where Roles_ID=" + iRoles_ID + " and Master_ID in (" + strIDs + ")";
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
        InitUserGrid();
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
