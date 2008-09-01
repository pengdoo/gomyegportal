//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-8-26
//
// 功能描述: 添加扩展字段设置
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
//----------------------------------项目引用-----------------------------------
using GCMSClassLib.Content;
using GCMSClassLib.Public_Cls;
using Gomye.CommonClassLib.Data;
using GCMS.PageCommonClassLib;
//------------------------------------------------------------------------------
public partial class Content_Config_ChannelAdd : GCMS.PageCommonClassLib.PageBase
{
    #region 自定义事件的注册和处理
    //订阅页面的自定义事件
    protected override void OnPreInit(EventArgs e)
    {
        //用户验证事件注册
        this.SessionAtuhFaiedEvent += new SessionAuthHandler(OnSessionAtuhFaiedEvent);
        //Session或QueryString获取失败事件注册
        this.SessionOrQueryGetFaiedEvent += new ParameterAuthHandler(OnSessionOrQueryGetFaiedEvent);
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
    /// <summary>
    /// Session或Query的访问失败默认响应
    /// </summary>
    /// <param name="key"></param>
    void OnSessionOrQueryGetFaiedEvent(string key)
    {
        //#未完成代码#
    }
    #endregion 自定义事件的注册和处理

    #region 当前页面注册的SQL字符串
    const string SQL_FieldsContentGetList = "SELECT * FROM Content_FieldsContent where FieldsName_ID = {0} ORDER BY Property_Order";
    const string SQL_FieldsContentUpdate = "update Content_FieldsName set FieldsName_Name='{0}', FieldsName_State='{1}', FieldsBase_Name='{2}' where FieldsName_ID={3}";
    #endregion 当前页面注册的SQL字符串

    private int FieldsName_ID;
    private String strType;
    Content_FieldsName _Content_FieldsName = new Content_FieldsName();
    protected void Page_Load(object sender, EventArgs e)
    {
        strType = this.GetQueryString("OrderType", null);//#缺少错误判断和错误处理#

        //修改
        if (!this.IsPostBack)
        {
            if (strType.Equals("Modify"))
            {
                FieldsName_ID = int.Parse(this.GetQueryString("FieldsName_ID", null));//#缺少错误判断和错误处理#
                this.txtFieldsName_ID.Value = FieldsName_ID.ToString();
                InitGrid();
            }
            else
            {
                Panel1.Visible = false;
            }
            xpTable.Attributes.Add("altRowColor", "oldlace");
            xpTable.Attributes.Add("align", "center");

            string cnString =string.Format(SQL_FieldsContentGetList,FieldsName_ID); 
            xpTable.DataSource = Tools.DoSqlReader(cnString);
            xpTable.DataBind();
        }

    }

    public void InitGrid()
    {

        if (_Content_FieldsName.Init(FieldsName_ID))
        {
            this.FieldsName_Name.Text = _Content_FieldsName.FieldsName_Name;
            this.FieldsBase_Name.Text = _Content_FieldsName.FieldsBase_Name;
            this.FieldsName_State.Value = _Content_FieldsName.FieldsName_State.ToString();
            this.inpFieldsName_Name.Value = _Content_FieldsName.FieldsBase_Name;

        }
    }

    public void ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            int Roles_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Fields_ID"));
            string Roles_Name = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Property_Name"));
            e.Item.Attributes.Add("ondragenter", "dragEnter();");
            e.Item.Attributes.Add("ondragleave", "dragLeave();");
            e.Item.Attributes.Add("ondragover", "dragOver();");
            e.Item.Attributes.Add("onmousedown", "selectContent('" + Roles_ID + "');");
            e.Item.Attributes.Add("ondblclick", "openContent('" + Roles_ID + "');");
            e.Item.Attributes.Add("ondrop", "FinishDrag(" + Roles_ID + ");");
            e.Item.ID = "item" + Roles_ID;

            string IDtxt = "<IMG id='img' src='../Admin_Public/Images/Icon_Master_on.gif' ondragstart='InitDrag()' onclick='return(false)' >";
            e.Item.Cells[0].Text = IDtxt;
            e.Item.Cells[1].Text = "<nobr><span class='submitdate' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Property_Name")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Property_Name")) + "</span></nobr>";
            e.Item.Cells[2].Text = "<nobr><span class='submitdate' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Property_Alias")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Property_Alias")) + "</span></nobr>";
            e.Item.Cells[3].Text = "<nobr><span class='submitdate' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Property_InputType")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Property_InputType")) + "</span></nobr>";
        }
    }

    protected void ToolsbarMain_ButtonClick(object sender, System.EventArgs e)
    {
        ContentTable _ContentTable = new ContentTable();

        _Content_FieldsName.FieldsName_Name = this.FieldsName_Name.Text;
        _Content_FieldsName.FieldsName_State = int.Parse(this.FieldsName_State.Value);
        _Content_FieldsName.FieldsBase_Name = this.FieldsBase_Name.Text;

        strType = this.Request.QueryString["OrderType"].ToString();
        if (strType.Equals("Modify"))
        {
            FieldsName_ID = int.Parse(this.GetQueryString("FieldsName_ID", null));//#缺少错误判断和错误处理#
            string sql = string.Format(
                SQL_FieldsContentUpdate,
                _Content_FieldsName.FieldsName_Name,
                _Content_FieldsName.FieldsName_State,
                _Content_FieldsName.FieldsBase_Name,
                FieldsName_ID);
            _ContentTable.TableName = "ContentUser_" + this.inpFieldsName_Name.Value;
            _ContentTable.TableNewName = "ContentUser_" + _Content_FieldsName.FieldsBase_Name;
            Tools.DoSql(_ContentTable.cAlterTableName());
        }

        if (strType.Equals("Create"))
        {
            _Content_FieldsName.Create();
            _ContentTable.TableName = "ContentUser_" + _Content_FieldsName.FieldsBase_Name;
            Tools.DoSql(_ContentTable.cCreateTable());
            //Tools.UpdateMaxID("FieldsName_ID");
        }
        Response.Redirect("Config_ChannelView.aspx");

    }
	
}
