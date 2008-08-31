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

public partial class Content_Config_ChannelFieldsAdd : GCMS.PageCommonClassLib.PageBase
{
    //订阅页面的自定义事件
    protected override void OnPreInit(EventArgs e)
    {
        this.SessionAtuhFaiedEvent += new SessionAuthHandler(OnSessionAtuhFaiedEvent);//注册验证错误处理
        this.SessionOrQueryGetFaiedEvent+=new ParameterAuthHandler(OnSessionOrQueryGetFaiedEvent);
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
    private String strType;
    private String Fields_ID;
    Content_FieldsContent _Content_FieldsContent = new Content_FieldsContent();
    string FieldsName_ID = "";
    ContentTable _ContentTable = new ContentTable();
    Content_FieldsName _Content_FieldsName = new Content_FieldsName();

    protected void Page_Load(object sender, EventArgs e)
    {
        FieldsName_ID = this.Request.QueryString["FieldsName_ID"].ToString();
        _Content_FieldsName.Init(int.Parse(FieldsName_ID));


        if (!this.IsPostBack)
        {
            strType =this.GetQueryString("OrderType",null);
            this.inpFieldsName_ID.Value = FieldsName_ID;
            this.OrderType.Value = strType;
            if (strType == "Updata")
            {
                Fields_ID = this.GetQueryString("Fields_ID", null); //#缺少错误判断和错误处理#

                _Content_FieldsContent.Init(int.Parse(Fields_ID));
                this.Property_Name.Text = _Content_FieldsContent.Property_Name;
                this.Property_Alias.Text = _Content_FieldsContent.Property_Alias;
                //this.Property_Type.Text = _Content_FieldsContent.Property_Type;				
                this.Property_InputType.Value = _Content_FieldsContent.Property_InputType;
                this.Property_InputOptions.Text = _Content_FieldsContent.Property_InputOptions;
                this.OldName.Value = _Content_FieldsContent.Property_Name;
                if (_Content_FieldsContent.Property_InputType == "SELECT" || _Content_FieldsContent.Property_InputType == "LABEL" || _Content_FieldsContent.Property_InputType == "TREES")
                {
                    this.Property_InputOptions.Enabled = true;
                }
                this.Mssg.Text = "更新目录";
            }

            if (strType == "Delete")
            {
                Fields_ID = this.Request.QueryString["Fields_ID"].ToString();
                _Content_FieldsContent.Init(int.Parse(Fields_ID));

                _ContentTable.TableName = "ContentUser_" + _Content_FieldsName.FieldsBase_Name;
                _ContentTable.ColumnName = _Content_FieldsContent.Property_Name;

                bool bFlag = _Content_FieldsContent.Delete(int.Parse(Fields_ID));
                if (bFlag)
                {
                    this.saveResult.Text = "成功";
                    Tools.DoSql(_ContentTable.cAlterTable(1));
                }
                else
                {
                    this.saveResult.Text = "失败";
                }
                Page.RegisterStartupScript("保存目录", "<script language=javascript>closethiswindows();</script>");
            }
        }
    }
    protected void Toolsbar1_ButtonClick(object sender, System.EventArgs e)
    {

        //文件上传处理，上传文件不为空
        //			Microsoft.Web.UI.WebControls.ToolbarButton tb = new Microsoft.Web.UI.WebControls.ToolbarButton();
        //			tb=(Microsoft.Web.UI.WebControls.ToolbarButton)sender;

        if (this.Property_Name.Text == "Name") { this.Mssg.Text = "对不起！Name为系统保留字，请更换扩展字段名称。"; return; }
        if (this.Property_Name.Text == "ContentID") { this.Mssg.Text = "对不起！ContentID为系统保留字，请更换扩展字段名称。"; return; }
        if (this.Property_Name.Text == "DerivationLink") { this.Mssg.Text = "对不起！DerivationLink为系统保留字，请更换扩展字段名称。"; return; }
        if (this.Property_Name.Text == "Derivation") { this.Mssg.Text = "对不起！Derivation为系统保留字，请更换扩展字段名称。"; return; }
        if (this.Property_Name.Text == "PictureName") { this.Mssg.Text = "对不起！PictureName为系统保留字，请更换扩展字段名称。"; return; }
        if (this.Property_Name.Text == "PublishDate") { this.Mssg.Text = "对不起！PublishDate为系统保留字，请更换扩展字段名称。"; return; }
        if (this.Property_Name.Text == "PictureNotes") { this.Mssg.Text = "对不起！PictureNotes为系统保留字，请更换扩展字段名称。"; return; }
        if (this.Property_Name.Text == "ChannelID") { this.Mssg.Text = "对不起！ChannelID为系统保留字，请更换扩展字段名称。"; return; }
        if (this.Property_Name.Text == "Content") { this.Mssg.Text = "对不起！Content为系统保留字，请更换扩展字段名称。"; return; }
        if (this.Property_Name.Text == "Url") { this.Mssg.Text = "对不起！Url为系统保留字，请更换扩展字段名称。"; return; }
        if (this.Property_Name.Text == "Author") { this.Mssg.Text = "对不起！Author为系统保留字，请更换扩展字段名称。"; return; }

        _Content_FieldsContent.Property_Name = this.Property_Name.Text;
        _Content_FieldsContent.Property_Alias = this.Property_Alias.Text;
        _Content_FieldsContent.Property_InputType = this.Property_InputType.Value.ToString();
        _Content_FieldsContent.FieldsName_ID = int.Parse(FieldsName_ID);
        _Content_FieldsContent.Property_InputOptions = this.Property_InputOptions.Text;


        strType = this.OrderType.Value;
        switch (_Content_FieldsContent.Property_InputType)
        {
            case "TEXT":
                _ContentTable.ColumnType = "varchar(100)";
                break;
            case "IMAGE":
                _ContentTable.ColumnType = "varchar(100)";
                break;
            case "FILE":
                _ContentTable.ColumnType = "varchar(100)";
                break;
            case "DATETIME":
                _ContentTable.ColumnType = "varchar(100)";
                break;
            case "TEXTAREA":
                _ContentTable.ColumnType = "text";
                break;
            case "TREES":
                _ContentTable.ColumnType = "varchar(100)";
                break;
            case "SELECT":
                _ContentTable.ColumnType = "varchar(100)";
                break;
            case "LABEL":
                _ContentTable.ColumnType = "text";
                break;
            case "NUMBER":
                _ContentTable.ColumnType = "float";
                break;

        }
        _ContentTable.TableName = "ContentUser_" + _Content_FieldsName.FieldsBase_Name;
        _ContentTable.ColumnName = _Content_FieldsContent.Property_Name;

        if (strType == "Create")
        {
            bool bFlag = _Content_FieldsContent.Create();
            if (bFlag)
            {
                this.saveResult.Text = "成功";
                Tools.DoSql(_ContentTable.cAlterTable(0));

            }
            else
            {
                this.saveResult.Text = "失败";

            }
            Page.RegisterStartupScript("保存目录", "<script language=javascript>closethiswindows();</script>");
            //							break;
        }

        if (strType == "Updata")
        {
            Fields_ID = this.GetQueryString("Fields_ID", null); 
           // string sql = "update Content_FieldsContent set Property_Name='" + this.Property_Name.Text + "', Property_Alias='" + this.Property_Alias.Text + "', Property_InputType='" + this.Property_InputType.Value + "', Property_InputOptions='" + this.Property_InputOptions.Text + "' where Fields_ID=" + Fields_ID;
            bool bFlag = _Content_FieldsContent.Update(Fields_ID,this.Property_Name.Text,this.Property_Alias.Text,this.Property_InputType.Value,this.Property_InputOptions.Text);
            if (bFlag)
            {
                this.saveResult.Text = "成功";
                _ContentTable.ColumnNewName = _Content_FieldsContent.Property_Name;
                _ContentTable.ColumnName = OldName.Value;
                if (_ContentTable.ColumnNewName == _ContentTable.ColumnName)
                { Tools.DoSql(_ContentTable.cAlterTable(3)); }
                else
                {
                    Tools.DoSql(_ContentTable.cAlterTable(2));
                }

            }
            else
            {
                this.saveResult.Text = "失败";
            }
            Page.RegisterStartupScript("保存目录", "<script language=javascript>closethiswindows();</script>");

        }
    }
}
