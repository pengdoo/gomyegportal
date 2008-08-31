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
using GCMS.PageCommonClassLib;

public partial class Tools_Left :GCMS.PageCommonClassLib.PageBase
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
        this.Response.Write("<script language=javascript>parent.parent.parent.window.navigate('Logon.aspx');</script>");
        return;
    }

    SysLogon syslogon = new SysLogon();
    private void Page_Load(object sender, System.EventArgs e)
    {

        syslogon.RolesPopedom(int.Parse(Session["Roles"].ToString()));
        string Popedom_EName = syslogon.Popedom_EName;

        if (Popedom_EName == null)
        {
            this.Response.Write("<script language=javascript>alert(\"您的角色不具备任何权限！！！\");parent.parent.parent.window.navigate('Logon.aspx');</script>");
            return;
        }
        this.LeftTools.Text = "<script language=\"JavaScript\"> var outlookbar=new outlook(); var tempinnertext1,tempinnertext2,outlooksmoothstat \n";
        this.LeftTools.Text = this.LeftTools.Text + "outlooksmoothstat = 0;var t;\n";
        this.LeftTools.Text = this.LeftTools.Text + "t=outlookbar.addtitle('内容管理')\n";
        if (Popedom_EName.IndexOf("Setup") > 0)
        {
            this.LeftTools.Text = this.LeftTools.Text + "outlookbar.additem('设&nbsp;&nbsp;置',t,'Content/Config_Main.aspx','Config','admin_Public/Images/Icon_New_Web.gif')\n";
        }
        if (Popedom_EName.IndexOf("Navigation") > 0)
        {
            this.LeftTools.Text = this.LeftTools.Text + "outlookbar.additem('导&nbsp;&nbsp;航',t,'Content/Type_TypeMain.aspx','channel','admin_Public/Images/Icon_New_Navigation.gif')\n";
        }
        if (Popedom_EName.IndexOf("Whiter") > 0)
        {
            this.LeftTools.Text = this.LeftTools.Text + "outlookbar.additem('发&nbsp;&nbsp;布',t,'Content/Content_Default.aspx','promotion','admin_Public/Images/Icon_New_File.gif')\n";
        }
        if (Popedom_EName.IndexOf("Stat") > 0)
        {
            this.LeftTools.Text = this.LeftTools.Text + "outlookbar.additem('统&nbsp;&nbsp;计',t,'Content/Stat_Default.aspx','Stat','admin_Public/Images/Icon_New_Test.gif')\n";
        }
        if (Popedom_EName.IndexOf("Dustbin") > 0)
        {
            this.LeftTools.Text = this.LeftTools.Text + "outlookbar.additem('回收站',t,'Content/RecycleBin_Default.aspx','LaST','admin_Public/Images/LaST_Trash.gif')\n";
        }
        if (Popedom_EName.IndexOf("Popedom") > 0)
        {
            this.LeftTools.Text = this.LeftTools.Text + "outlookbar.additem('权&nbsp;&nbsp;限',t,'Content/User_Main.aspx','Remark','admin_Public/Images/Icon_New_Poper1.gif')\n";
        }


        if (ConfigurationSettings.AppSettings["Downloads"] == "on")
        {
            this.LeftTools.Text = this.LeftTools.Text + "t=outlookbar.addtitle('网络硬盘')\n";
            if (Popedom_EName.IndexOf("Whiter") > 0)
            {
                this.LeftTools.Text = this.LeftTools.Text + "outlookbar.additem('上传管理',t,'Downloads/Upload_Main.aspx','Downloads','admin_Public/Images/Icon_New_Poper1.gif')\n";
            }
        }



        if (ConfigurationSettings.AppSettings["Member"] == "on")
        {
            this.LeftTools.Text = this.LeftTools.Text + "t=outlookbar.addtitle('用户管理')\n";
            if (Popedom_EName.IndexOf("Whiter") > 0)
            {
                this.LeftTools.Text = this.LeftTools.Text + "outlookbar.additem('常规设置',t,'Member/Member_MainSetup.aspx','Member_Setup','admin_Public/Images/Icon_New_Poper1.gif')\n";
                this.LeftTools.Text = this.LeftTools.Text + "outlookbar.additem('用户管理',t,'Member/Member_Main.aspx','Member','admin_Public/Images/Icon_New_Poper1.gif')\n";
                //						this.LeftTools.Text = this.LeftTools.Text +"outlookbar.additem('博客用户',t,'blog/Main_Blog.html','Member_Blog','admin_Public/Images/Icon_New_Poper1.gif')\n";
            }
        }


        if (ConfigurationSettings.AppSettings["Photo"] == "on")
        {
            this.LeftTools.Text = this.LeftTools.Text + "t=outlookbar.addtitle('图片系统')\n";
            if (Popedom_EName.IndexOf("Whiter") > 0)
            {
                this.LeftTools.Text = this.LeftTools.Text + "outlookbar.additem('图片抓取',t,'Photo/Photo_Main.aspx','Forums_Setup','admin_Public/Images/Icon_New_Poper1.gif')\n";
                this.LeftTools.Text = this.LeftTools.Text + "outlookbar.additem('城市排名',t,'Forums/Content_List.aspx','Forums_Channel','admin_Public/Images/Icon_New_Navigation.gif')\n";
            }
        }


        if (ConfigurationSettings.AppSettings["Club"] == "on")
        {
            this.LeftTools.Text = this.LeftTools.Text + "t=outlookbar.addtitle('报名管理')\n";

            //					if (Popedom_EName.IndexOf("Popedom") > 0)
            //					{
            if (Popedom_EName.IndexOf("Class") > 0)
            {
                this.LeftTools.Text = this.LeftTools.Text + "outlookbar.additem('培训报名',t,'/SoHuActivity/ClassManage/Class_ListMain.aspx','Class','admin_Public/Images/Icon_New_Poper1.gif')\n";
            }
            if (Popedom_EName.IndexOf("Activity") > 0)
            {
                this.LeftTools.Text = this.LeftTools.Text + "outlookbar.additem('活动报名',t,'/SoHuActivity/ClassManage/Activity_ListMain.aspx','Activity','admin_Public/Images/Icon_New_Poper1.gif')\n";
            }
            if (Popedom_EName.IndexOf("Club") > 0)
            {
                this.LeftTools.Text = this.LeftTools.Text + "outlookbar.additem('俱乐部报名',t,'/SoHuActivity/ClassManage/Club_ListMain.aspx','Club','admin_Public/Images/Icon_New_Poper1.gif')\n";
            }
            if (Popedom_EName.IndexOf("Black") > 0)
            {
                this.LeftTools.Text = this.LeftTools.Text + "outlookbar.additem('黑名单',t,'/SoHuActivity/ClassManage/Member_ListMain.aspx','Member_ListMain','admin_Public/Images/Icon_New_Poper1.gif')\n";
            }
            if ((Popedom_EName.IndexOf("Advice") > 0))
            {
                this.LeftTools.Text = this.LeftTools.Text + "outlookbar.additem('建 议',t,'/SoHuActivity/sugManager/sugManage_ListMain.aspx','Advice','admin_Public/Images/Icon_New_Poper1.gif')\n";
            }
            //
            //					}
        }

        if (int.Parse(Session["Roles"].ToString()) == 0)
        {
            this.LeftTools.Text = this.LeftTools.Text + "t=outlookbar.addtitle('系统管理')\n";
            this.LeftTools.Text = this.LeftTools.Text + "outlookbar.additem('权&nbsp;&nbsp;限',t,'Config/User_Main.aspx','users','admin_Public/Images/Icon_New_Poper1.gif')\n";
            this.LeftTools.Text = this.LeftTools.Text + "outlookbar.additem('设&nbsp;&nbsp;置',t,'Config/Config_Main.aspx','SystemConfig','admin_Public/Images/Icon_New_Web.gif')\n";
            //						this.LeftTools.Text = this.LeftTools.Text +"outlookbar.additem('内容采集',t,'Main_Spider.html','Spider','admin_Public/Images/Icon_New_Poper1.gif')\n";
        }

        //this.LeftTools.Text = this.LeftTools.Text + ConfigurationSettings.AppSettings["txtLeftTools"].ToString();
        this.LeftTools.Text = this.LeftTools.Text + "</script>";

    }

}
