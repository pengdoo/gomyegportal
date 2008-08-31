//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-8-26
//
// 功能描述: 选择相关文章(未完成)
//
// 已修改问题:
// 未修改问题:
// 修改记录
//   2008-8-26 添加注释
//   2008-8-31  规范【自定义事件】【字符处理】【页面参数获取】代码
//----------------------------------系统引用-------------------------------------
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
using System.Data.SqlClient;
//----------------------------------项目引用-----------------------------------
using GCMSClassLib.Public_Cls;
using GCMSClassLib.Content;
using GCMS.PageCommonClassLib;
//------------------------------------------------------------------------------

public partial class Content_Content_AddMain : GCMS.PageCommonClassLib.PageBase
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
        this.Response.Write("<script language=javascript>alert(\"超时操作！！！\");parent.parent.parent.window.navigate('../Logon.aspx');</script>");
        return;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Type_List();
            TxtContent_ID.Value = this.Request["Content_ID"].ToString();
        }
    }

    public void Type_List()
    {
        string Content_ID = this.Request["Content_ID"].ToString();
        int TypeTree_ID = int.Parse(this.Request["TypeTree_ID"]);
        Type_TypeTree _Type_TypeTree = new Type_TypeTree();
        _Type_TypeTree.Init(TypeTree_ID);

        LabelSonContent.Text = "";
        int i = 0;

        if (!String.IsNullOrEmpty(_Type_TypeTree.TypeTree_XMLContent ))
        {
            SqlDataReader myReader;
            // string sql = "select TypeTree_ID ,TypeTree_CName FROM Content_type_TypeTree where Typetree_ID in (SELECT distinct TypeTree_ID FROM Content_Content WHERE Content_PID =" + Content_ID+")";
            string sql = "select TypeTree_ID ,TypeTree_CName FROM Content_type_TypeTree where Typetree_ID in (" + _Type_TypeTree.TypeTree_XMLContent.Replace(" ", ",") + ")";

            myReader = Tools.DoSqlReader(sql);
            while (myReader.Read())
            {
                i = i++;
                LabelSonContent.Text = LabelSonContent.Text + "<div class='parent' id='md" + i + "Parent'>";
                LabelSonContent.Text = LabelSonContent.Text + "<IMG src='../Admin_Public/Images/Tree_white.gif' align='absMiddle' border='0'><IMG src='../Admin_Public/Images/Tree_white.gif' align='absMiddle' border='0'><SPAN onmouseup=OpenFolder('m3','Content_son.aspx?Content_ID=" + Content_ID + "&TypeTree_ID=" + myReader.GetInt32(0).ToString() + "','COLUMN_3'); onmouseover=IsonMouseOver('m3'); onmouseout=IsonMouseOut('m3');>";
                LabelSonContent.Text = LabelSonContent.Text + "<IMG src='../Admin_Public/Images/dc.gif' align='absMiddle' border='0' name='m3Pic'>&nbsp;<A class='item' href='#nothisanchor' name='m3Folder'>&nbsp;" + myReader.GetString(1).ToString() + "</A></SPAN>";
                LabelSonContent.Text = LabelSonContent.Text + "</div>";

                LabelConnectContent.Text = LabelConnectContent.Text + "<div class='parent' id='md" + i + "Parent'>";
                LabelConnectContent.Text = LabelConnectContent.Text + "<IMG src='../Admin_Public/Images/Tree_white.gif' align='absMiddle' border='0'><IMG src='../Admin_Public/Images/Tree_white.gif' align='absMiddle' border='0'><SPAN onmouseup=OpenFolder('m3','Content_Relative.aspx?Content_ID=" + Content_ID + "&TypeTree_ID=" + myReader.GetInt32(0).ToString() + "','COLUMN_3'); onmouseover=IsonMouseOver('m3'); onmouseout=IsonMouseOut('m3');>";
                LabelConnectContent.Text = LabelConnectContent.Text + "<IMG src='../Admin_Public/Images/dc.gif' align='absMiddle' border='0' name='m3Pic'>&nbsp;<A class='item' href='#nothisanchor' name='m3Folder'>&nbsp;" + myReader.GetString(1).ToString() + "</A></SPAN>";
                LabelConnectContent.Text = LabelConnectContent.Text + "</div>";

            }
            myReader.Close();
        }

    }
}
