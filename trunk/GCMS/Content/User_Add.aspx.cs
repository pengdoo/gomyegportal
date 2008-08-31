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
using GCMS.PageCommonClassLib;

public partial class Content_User_Add : GCMS.PageCommonClassLib.PageBase
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
        this.Response.Write("<script language=javascript>alert(\"超时或非法操作！！！\");parent.windowclose();</script>");
        return;
    }
    private int iMasterID;
    private Master master;
    private String strType;
    protected void Page_Load(object sender, EventArgs e)
    {
        strType = this.Request.QueryString["OrderType"].ToString();

        //修改
        if (strType.Equals("Modify"))
        {
            iMasterID = int.Parse(this.Request.QueryString["Master_ID"].ToString());
            master = new Master();

            if (!this.IsPostBack)
            {
                InitGrid();
            }
        }
        //Session倒入修改
        if (strType.Equals("ModifySession"))
        {
            iMasterID = int.Parse(Session["Master_ID"].ToString());
            master = new Master();

            if (!this.IsPostBack)
            {
                InitGrid();
            }
        }

        if (strType.Equals("AddUser"))
        {
        }

    }
    public void InitGrid()
    {
        if (master.Init(iMasterID))
        {
            this.Master_UserName.Text = master.MasterUserName.ToString();
            this.Master_Name.Text = master.MasterName.ToString();
            this.Master_Email.Text = master.MasterEmail.ToString();
            this.Master_Tel.Text = master.MasterTel.ToString();
            this.Master_Note.Text = master.MasterNote.ToString();
            this.Master_Usableness.Value = master.MasterUsableness.ToString();
        }
    }

    public void SaveUserUpdate()
    {
        if (this.Master_Password.Text != this.Master_Password1.Text)
        {
            this.textMsg.Text = "对不起！您两次输入的密码不同";
            return;
        }

        string sSQLPass = "";
        try
        {
            if (this.Master_Password.Text != "")
            {
                string Master_Password = FormsAuthentication.HashPasswordForStoringInConfigFile(this.Master_Password.Text.ToString(), "MD5");
                sSQLPass = ", Master_Password='" + Master_Password + "'";
            }

            string sSQL = "update Content_Master set Master_Name='" +
                this.Master_Name.Text.ToString() + "',Master_Email='" +
                this.Master_Email.Text.ToString() + "',Master_Tel='" +
                this.Master_Tel.Text.ToString() + "'" + sSQLPass + ",Master_Note='" +
                this.Master_Note.Text.ToString() + "',Master_Usableness='" +
                this.Master_Usableness.Value.ToString() +
                "' where Master_ID = " + iMasterID;
            if (Tools.DoSql(sSQL))
            {
                Page.RegisterStartupScript("保存用户", "<script language=javascript>closethiswindows();</script>");
            }

        }
        catch (Exception SUEx)
        {
            this.textMsg.Text = SUEx.Message;
            return;
        }
    }

    /// <summary>
    /// 保存时输入框检查
    /// </summary>
    /// <returns></returns>
    public string InputCheck()
    {
        string sRe = "True";
        if (this.Master_UserName.Text.ToString() == "")
        {
            sRe = "UserName";
            return sRe;
        }
        else if (this.Master_Password.Text.ToString() == "")
        {
            sRe = "PWD";
            return sRe;
        }
        else if (this.Master_Password.Text != this.Master_Password1.Text)
        {
            sRe = "NoEqual";
            return sRe;
        }
        return sRe;
    }


    public void CheckSaveUser()
    {
        string sReturn = InputCheck();
        switch (sReturn)
        {
            case "True":
                SaveUser();
                break;
            case "UserName":
                this.textMsg.Text = "请输入用户名!";
                break;
            case "PWD":
                this.textMsg.Text = "请输入密码!";
                break;
            case "NoEqual":
                this.textMsg.Text = "两次密码不相同，请重新输入!";
                break;
        }
    }

    public void SaveUser()
    {
        try
        {
            GCMSClassLib.Content.Master master = new Master();
            master.MasterPassword=FormsAuthentication.HashPasswordForStoringInConfigFile(this.Master_Password.Text.ToString().Trim(), "MD5");
            master.MasterName=this.Master_Name.Text.ToString().Trim();
            master.MasterUserName=this.Master_UserName.Text.ToString().Trim();
            master.MasterEmail= this.Master_Email.Text.ToString().Trim();
            master.MasterTel=this.Master_Tel.Text.ToString().Trim() ;
            master.MasterUsableness=this.Master_Usableness.Value.ToString().Trim();
            master.MasterNote=this.Master_Note.Text.ToString().Trim() ;
            master.Add_ID= int.Parse(Session["Roles"].ToString());
            master.Create();
            Page.RegisterStartupScript("保存用户", "<script language=javascript>closethiswindows();</script>");

        }
        catch (Exception SUEx)
        {
            this.textMsg.Text = SUEx.Message;
        }
    }

    protected void Toolsbar1_ButtonClick(object sender, System.EventArgs e)
    {

        strType = this.Request.QueryString["OrderType"].ToString();
        if (strType.Equals("Modify"))
        {
            SaveUserUpdate();
        }

        if (strType.Equals("ModifySession"))
        {
            SaveUserUpdate();
        }

        if (strType.Equals("AddUser"))
        {
            CheckSaveUser();
        }

    }
}

