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
using GCMS.PageCommonClassLib;

public partial class Content_Type_AddRoseList : GCMS.PageCommonClassLib.PageBase
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

    int TypeTree_ID; 
    protected void Page_Load(object sender, EventArgs e)
    {
        TypeTree_ID = int.Parse(this.Request.QueryString["TypeTree_ID"].ToString());

        if (!this.IsPostBack)
        {
            InitaGrid();
        }
    }
    public void InitaGrid()
    {

        xpTable.Attributes.Add("altRowColor", "oldlace");
        xpTable.Attributes.Add("align", "center");

        string cnString = "select * from Content_Roles where Roles_ID not in (select Roles_ID from Content_RolesConnect where TypeTree_ID =" + TypeTree_ID + ")";
        xpTable.DataSource = Tools.DoSqlReader(cnString);
        xpTable.DataBind();
    }

    public void SaveUser()
    {
        if (CheckForm())
        {
            string sID = this.Request["SelectedID"].ToString();
          

            char myChar = ',';
            string[] ids = sID.Split(myChar);
            string sSQL;

            for (int i = 0; i < ids.Length; i++)
            {
                sSQL = "insert into Content_RolesConnect(TypeTree_ID,Roles_ID) values(" + TypeTree_ID
                    + "," + ids[i] + ")";
                Tools.DoSql(sSQL);//#缺少错误判断和错误处理#

            }
            this.Response.Redirect("Type_RoleAdd.aspx?TypeTree_ID=" + TypeTree_ID);


        }
        else
        {
            //string sUrl = "Role_Modify.aspx?Roles_ID="+iRolesID+"&Master_ID=Null";
            //Page.RegisterStartupScript("添加用户","<script language=javascript>OpenParent('"+sUrl+"');</script>");
        }
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
            //this.Response.Write("Form:" + fStr[i] + "<br/>");
            if (fStr[i] == "SelectedID")
            {
                bSel = true;
            }
        }
        return bSel;
    }
}
