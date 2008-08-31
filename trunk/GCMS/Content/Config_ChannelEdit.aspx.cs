//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-8-26
//
// 功能描述:编辑扩展字段
//
// 已修改问题:
// 未修改问题:
// 修改记录
//   2008-8-26 添加注释
//   2008-8-31  规范【自定义事件】【SQL引用】【字符处理】【页面参数获取】代码
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
//----------------------------------项目引用-------------------------------------
using GCMSClassLib.Content;
using GCMSClassLib.Public_Cls;
using GCMS.PageCommonClassLib;
//------------------------------------------------------------------------------
public partial class Content_Config_ChannelEdit : GCMS.PageCommonClassLib.PageBase
{
    #region 自定义事件的注册和处理
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
    #endregion 自定义事件的注册和处理

    #region 当前页面注册的SQL字符串
    const string SQL_FieldsContentGetList = "SELECT Fields_ID,Property_Name,Property_InputType,Property_Alias,Property_InputOptions FROM  Content_FieldsContent  WHERE FieldsName_ID ={0} order by Property_Order";
    #endregion 当前页面注册的SQL字符串

    private int FieldsName_ID = 0;
    Type_TypeTree typeTree = new Type_TypeTree();

    
    protected void Page_Load(object sender, EventArgs e)
    {
        FieldsName_ID = int.Parse(this.GetQueryString("FieldsName_ID", null));//#缺少错误判断和错误处理#
        this.txtFieldsName_ID.Value = FieldsName_ID.ToString();
        if (!this.IsPostBack)
        {
            AddFieldsWriteTxt(FieldsName_ID);
        }
    }
    protected void AddFieldsWriteTxt(int FieldsName_ID)
    {
        SqlDataReader myReader;
        string sql = string.Format(SQL_FieldsContentGetList, FieldsName_ID);
        myReader = Tools.DoSqlReader(sql);
        string ToolsPut;
        while (myReader.Read())
        {

            switch (myReader.GetString(2))
            {
                case "TEXT":
                    ToolsPut = "<input type='text' size='30' class='inputtext' name=" + myReader.GetString(1) + ">";
                    break;
                case "IMAGE":
                    ToolsPut = "<input type='text' size='30' class='inputtext' name=" + myReader.GetString(1) + "> <input type='button' value='...'>";
                    break;
                case "FILE":
                    ToolsPut = "<input type='text' size='30' class='inputtext' name=" + myReader.GetString(1) + "> <input type='button' value='...'>";
                    break;
                case "DATETIME":
                    ToolsPut = "<input type='text' size='30' class='inputtext' name=" + myReader.GetString(1) + "><img src='../Admin_Public/Images/Icon_calendar.gif'>";
                    break;
                case "TEXTAREA":
                    ToolsPut = "<textarea name=" + myReader.GetString(1) + " rows='6' cols='30'></textarea>";
                    break;
                case "TREES":
                    ToolsPut = "<input type='text' size='30' class='inputtext' name=" + myReader.GetString(1) + "> <input type='button' value='...'>";
                    break;
                case "SELECT":

                    string[] ops;
                    string opss;
                    char sSplit = ',';
                    opss = myReader.GetString(4);

                    int i = 10;
                    char c = (char)i;			//相当于vb中的chr(10)

                    opss = opss.Replace(c, sSplit);
                    ops = opss.Split(sSplit);
                    ToolsPut = "<select size='1' name='" + myReader.GetString(1) + "' class='inputtext'>";

                    for (int j = 0; j < ops.Length; j++)
                    {
                        ToolsPut = ToolsPut + "<option value=" + ops[j].ToString() + ">" + ops[j].ToString() + "</option>";
                    }
                    ToolsPut = ToolsPut + "<select>";
                    break;
                case "LABEL":
                    ToolsPut = myReader.GetString(4);
                    break;
                case "NUMBER":
                    ToolsPut = "<input type='text' size='30' class='inputtext' name=" + myReader.GetString(1) + ">";
                    break;

                default:
                    ToolsPut = "数据错误！";
                    break;
            }

            this.AddFieldsWrite.Text = this.AddFieldsWrite.Text +
                "<TR valign='top'><TD style='WIDTH: 120px;table-layout:fixed;word-wrap:break-word;'>" + myReader.GetString(3) +
                "：</TD><TD style='WIDTH: 100px;table-layout:fixed;word-wrap:break-word;'>" + ToolsPut +
                "</TD><TD><INPUT TYPE='button' value='修改' onclick = 'UpdataFields(" + myReader.GetInt32(0) + ")' class=button" +
                "> <INPUT TYPE='button' value='删除' onclick = 'DeleteFields(" + myReader.GetInt32(0) + ")' class=button></TD></tr>";

        }

    }


}
