//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-8-25
//
// 功能描述: 发布内容
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
using System.Xml;
using System.Net;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//----------------------------------项目引用-----------------------------------
using GCMSClassLib.Content;
using GCMSClassLib.Public_Cls;
using System.Data.SqlClient;
using GCMSClassLib.SystemCls;
using jmail;
using GCMS.PageCommonClassLib;
//------------------------------------------------------------------------------

public partial class Content_Content_Add : GCMS.PageCommonClassLib.PageBase
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
        this.Response.Write("<script language=javascript>parent.parent.parent.window.navigate('../Logon.aspx');</script>");
        return;
    }
    #endregion 自定义事件的注册和处理

    Hashtable contentHt = new Hashtable();

    Type_TypeTree typeTree = new Type_TypeTree();
    string Url;
    int UpdateContent_ID;

    ContentCls content = new ContentCls();
    int TypeTree_ID;
    
    int Content_ID;
    string TypeTree_ListTemplate = "";
    string TypeTreeListURL = "";
    int List_amount;
    string LTypeTree_PictureURL = "";


    int TypeTree_TypeFields = 0;
    int TypeTree_ContentFields = 0;
    string txtXML = "";
    XmlDocument xmldoc;
    XmlNode xmlnode;
    XmlElement xmlelem;
    string flag = "";
    int ispost = 0;


    DataTable _DataTable = new DataTable();
    protected void Page_Load(object sender, System.EventArgs e)
    {
       
        SysLogon syslogon = new SysLogon();
        syslogon.RolesPopedom(int.Parse(Session["Roles"].ToString()));
        string Popedom_EName = syslogon.Popedom_EName;
        if (Popedom_EName.IndexOf("Whiter") > 0)
        {
            Panel2.Visible = true;
            Panel1.Visible = false;
        }
        if (Popedom_EName.IndexOf("Editor") > 0)
        {
            Panel2.Visible = false;
            Panel1.Visible = true;
        }

        TypeTree_ID = int.Parse(this.Request["TypeTree_ID"].ToString());

        Session["TypeTree_ID"] = TypeTree_ID.ToString();
        this.LabelTypeID.Text = TypeTree_ID.ToString();

        typeTree.Init(TypeTree_ID);
        string LTypeTree_CName = typeTree.TypeTreeCName;
        string LTypeTree_EName = typeTree.TypeTreeEName;
        string LTypeTree_ID = typeTree.TypeTree_ID.ToString();
        LTypeTree_PictureURL = typeTree.TypeTreePictureURL;
        string LTypeTree_Images = typeTree.TypeTreeImages;
        string LTypeTree_Explain = typeTree.TypeTreeExplain;
        string LTypeTree_Issuance = typeTree.strTypeTreeIssuance(int.Parse(typeTree.TypeTreeIssuance.ToString()));
        this.TypeTree_URL.Text = typeTree.TypeTreeURL;
        this.TypeTree_Template.Text = typeTree.TypeTreeTemplate;
        TypeTree_ListTemplate = typeTree.TypeTreeListTemplate;
        TypeTreeListURL = typeTree.TypeTreeListURL;
        List_amount = typeTree.Listamount;
        TypeTree_TypeFields = typeTree.TypeTree_TypeFields;
        TypeTree_ContentFields = typeTree.TypeTree_ContentFields;

        string TypeTreePictureURL = typeTree.TypeTreePictureURL;

        if (typeTree.TypeTreeIssuance.ToString() == "5")
        {
            LabTxt1.Text = "产品价格：";
            LabTxt2.Text = "产品规格：";
            LabTxt3.Text = "产品图片：";
            LabTxt4.Text = "市场价格：";
            LabTxt5.Text = "特殊规格：";
        }

        if (typeTree.TypeTree_Type != 2)
        {
            PanelName.Visible = true;
            PanelContent.Visible = true;
        }


        if (!this.IsPostBack)
        {
            if (this.Request["flag"] != null && this.Request["flag"].Equals("edit"))
            {
                /*修改*/
                this.LabelFlag.Text = "edit";
                if (typeTree.TypeTree_Type == 0) { showEditData(); }
                Content_ID = int.Parse(Request["Content_ID"].ToString());
                this.LabelEditContentID.Text = Content_ID.ToString();
                PageHeader.Value = "修改内容";
                ispost = 1;
                DataList1.DataSource = Tools.DoSqlReader("select * from Content_Log where Content_ID=" + Content_ID);
                DataList1.DataBind();
            }
            else
            {

                this.LabelCurPage.Text = "1";
                this.TextBoxDate.Text = DateTime.Now.ToShortDateString();
                ArrayList contentList = new ArrayList();
                //					contentList.Add("");
                Headnews.Checked = true;
                PageHeader.Value = "添加内容";

            }
        }
        else
        {
            ispost = 0;
            TypeTree_ID = int.Parse(this.LabelTypeID.Text);
        }


        if (typeTree.TypeTree_ContentFields != 0)
        {
            AddFieldsWriteTxt(typeTree.TypeTree_ContentFields);
        }
        //AddFieldsWriteTxt(TypeTree_TypeFields); // 扩展字段
        //Headnews.Checked = true;

    }

    private void showEditData()
    {
        if (this.Request["Content_ID"] == null) return;

        int Content_ID = int.Parse(this.Request["Content_ID"]);
        ContentCls content = new ContentCls();
        content.Init(Content_ID);

        //------------------------------------------------------

        this.TextBoxTitle.Text = Tools.DBToWeb(content.Name);
        this.TextBoxLink.Text = content.DerivationLink;
        this.TextBoxFrom.Text = content.Derivation;
        this.TextBoxPic.Text = content.PictureName;
        this.TextBoxPicD.Text = content.PictureNameD;
        this.TextBoxDate.Text = content.SubmitDate.ToShortDateString();
        this.Picture_Notes.Text = content.PictureNotes;
        this.KeyWord.Text = content.KeyWord;
        this.Original.Text = content.Original;
        txtXML = Tools.DBToWeb(content.Content_Xml);
        //this.LabelTypeID.Text		= content.TypeTreeID.ToString();

        if (content.HeadNews == "0")
        {
            Headnews.Checked = false;
        }
        else
        {
            Headnews.Checked = true;
        }

        if (content.PictureNews == "1")
        {
            PictureNews.Checked = true;
        }
        else
        {
            PictureNews.Checked = false;
        }

        //时间

        this.LabelCurPage.Text = "1";
        if (!string.IsNullOrEmpty(content.Description))
        {
            //EditorControl1.Value = Server.HtmlEncode(content.Description.ToString());
            setPageData(Tools.DBToWeb(content.Description));
        }
        //this.EditorControl1.Value =content.Description;
        //this.LabelTab.Text=" <table STYLE='width:100%;height:20px;' CELLPADDING=0 CELLSPACING=0 >  <tr><td  class='selTab'>第1页</td></tr></table>";
        TypeTree_ID = int.Parse(content.TypeTree_ID.ToString());
        LabelTypeID.Text = TypeTree_ID.ToString();
        //---------------------------

    }

    // 动态添加控件

    protected void AddFieldsWriteTxt(int FieldsName_ID)
    {
        SqlDataReader myReader;
        string sql = "SELECT Fields_ID,Property_Name,Property_InputType,Property_Alias,Property_InputOptions FROM Content_FieldsContent WHERE FieldsName_ID =" + FieldsName_ID + " order by Property_Order";
        string ToolsPut = "";
        myReader = Tools.DoSqlReader(sql);
        flag = this.Request["flag"];

        while (myReader.Read())
        {

            switch (myReader.GetString(2))
            {
                case "TEXT":

                    TextBox nTextBoxTEXT = new TextBox();
                    //nTextBoxTEXT.ID = "PROPERTY"+ myReader.GetInt32(0).ToString();
                    nTextBoxTEXT.ID = myReader.GetString(1).ToString();
                    nTextBoxTEXT.CssClass = "inputtext250";

                    if (flag == "edit" && ispost == 1)
                    {
                        nTextBoxTEXT.Text = content.Contents(Content_ID, myReader.GetString(1).ToString(), TypeTree_ID);
                    }

                    TableRow trTEXT = new TableRow();
                    TableCell tc1TEXT = new TableCell();
                    TableCell tc2TEXT = new TableCell();
                    tc1TEXT.Width = 100;
                    //						tc2TEXT.Width = 260;
                    tc1TEXT.HorizontalAlign = HorizontalAlign.Right;
                    tc1TEXT.Text = myReader.GetString(3).ToString() + "：&nbsp;";
                    tc2TEXT.Controls.Add(nTextBoxTEXT);
                    trTEXT.Cells.Add(tc1TEXT);
                    trTEXT.Cells.Add(tc2TEXT);
                    Table2.Rows.Add(trTEXT);

                    break;

                case "NUMBER":

                    TextBox nTextBoxNUMBER = new TextBox();
                    //nTextBoxTEXT.ID = "PROPERTY"+ myReader.GetInt32(0).ToString();
                    nTextBoxNUMBER.ID = myReader.GetString(1).ToString();
                    nTextBoxNUMBER.CssClass = "inputtext250";

                    if (flag == "edit" && ispost == 1)
                    {
                        nTextBoxNUMBER.Text = content.Contents(Content_ID, myReader.GetString(1).ToString(), TypeTree_ID);
                    }

                    TableRow trNUMBER = new TableRow();
                    TableCell tc1NUMBER = new TableCell();
                    TableCell tc2NUMBER = new TableCell();
                    tc1NUMBER.Width = 100;
                    //						tc2NUMBER.Width = 260;
                    tc1NUMBER.HorizontalAlign = HorizontalAlign.Right;
                    tc1NUMBER.Text = myReader.GetString(3).ToString() + "：&nbsp;";
                    tc2NUMBER.Controls.Add(nTextBoxNUMBER);
                    trNUMBER.Cells.Add(tc1NUMBER);
                    trNUMBER.Cells.Add(tc2NUMBER);
                    Table2.Rows.Add(trNUMBER);

                    break;

                case "IMAGE":

                    TextBox nTextBoxIMAGE = new TextBox();
                    nTextBoxIMAGE.ID = myReader.GetString(1).ToString();
                    nTextBoxIMAGE.CssClass = "inputtext250";

                    if (flag == "edit" && ispost == 1)
                    {
                        nTextBoxIMAGE.Text = content.Contents(Content_ID, myReader.GetString(1).ToString(), TypeTree_ID);
                    }

                    ToolsPut = "&nbsp;<input type='button' value='上传' onclick='selectImages(" + myReader.GetString(1).ToString() + ");' class='button'> ";
                    ToolsPut = ToolsPut + "<input type='button' value='下载' onclick='doDownload(" + myReader.GetString(1).ToString() + ");' class='button'> ";
                    ToolsPut = ToolsPut + "<input type='button' value='预览' onclick='doPreview(" + myReader.GetString(1).ToString() + ");' class='button'> ";

                    TableRow trIMAGE = new TableRow();
                    TableCell tc1IMAGE = new TableCell();
                    TableCell tc2IMAGE = new TableCell();
                    TableCell tc3IMAGE = new TableCell();
                    tc1IMAGE.HorizontalAlign = HorizontalAlign.Right;
                    tc1IMAGE.Text = myReader.GetString(3).ToString() + "：&nbsp;";
                    tc2IMAGE.Controls.Add(nTextBoxIMAGE);
                    tc3IMAGE.Text = ToolsPut;
                    trIMAGE.Cells.Add(tc1IMAGE);
                    trIMAGE.Cells.Add(tc2IMAGE);
                    trIMAGE.Cells.Add(tc3IMAGE);
                    Table2.Rows.Add(trIMAGE);

                    break;

                case "FILE":

                    TextBox nTextBoxFILE = new TextBox();
                    nTextBoxFILE.ID = myReader.GetString(1).ToString();
                    nTextBoxFILE.CssClass = "inputtext250";

                    if (flag == "edit" && ispost == 1)
                    {
                        nTextBoxFILE.Text = content.Contents(Content_ID, myReader.GetString(1).ToString(), TypeTree_ID);
                    }

                    ToolsPut = "&nbsp;<input type='button' value='上传' onclick='selectImages(" + myReader.GetString(1).ToString() + ");' class='button'> ";
                    ToolsPut = ToolsPut + "<input type='button' value='下载' onclick='doDownload(" + myReader.GetString(1).ToString() + ");' class='button'> ";
                    ToolsPut = ToolsPut + "<input type='button' value='预览' onclick='doPreview(" + myReader.GetString(1).ToString() + ");' class='button'> ";

                    TableRow trFILE = new TableRow();
                    TableCell tc1FILE = new TableCell();
                    TableCell tc2FILE = new TableCell();
                    TableCell tc3FILE = new TableCell();
                    tc1FILE.HorizontalAlign = HorizontalAlign.Right;
                    tc1FILE.Text = myReader.GetString(3).ToString() + "：&nbsp;";
                    tc2FILE.Controls.Add(nTextBoxFILE);
                    tc3FILE.Text = ToolsPut;
                    trFILE.Cells.Add(tc1FILE);
                    trFILE.Cells.Add(tc2FILE);
                    trFILE.Cells.Add(tc3FILE);
                    Table2.Rows.Add(trFILE);

                    break;

                case "DATETIME":

                    TextBox nTextBoxDATETIME = new TextBox();
                    nTextBoxDATETIME.ID = myReader.GetString(1).ToString();
                    nTextBoxDATETIME.CssClass = "inputtext250";

                    if (flag == "edit" && ispost == 1)
                    {
                        nTextBoxDATETIME.Text = content.Contents(Content_ID, myReader.GetString(1).ToString(), TypeTree_ID);
                    }

                    ToolsPut = "&nbsp;<img src='../Admin_Public/Images/Icon_calendar.gif' onclick='selectdate(" + myReader.GetString(1).ToString() + ");'> ";

                    TableRow trDATETIME = new TableRow();
                    TableCell tc1DATETIME = new TableCell();
                    TableCell tc2DATETIME = new TableCell();
                    TableCell tc3DATETIME = new TableCell();
                    tc1DATETIME.HorizontalAlign = HorizontalAlign.Right;
                    tc1DATETIME.Text = myReader.GetString(3).ToString() + "：&nbsp;";
                    tc2DATETIME.Controls.Add(nTextBoxDATETIME);
                    tc3DATETIME.Text = ToolsPut;
                    tc2DATETIME.Width = 260;

                    trDATETIME.Cells.Add(tc1DATETIME);
                    trDATETIME.Cells.Add(tc2DATETIME);
                    trDATETIME.Cells.Add(tc3DATETIME);
                    Table2.Rows.Add(trDATETIME);

                    break;

                case "TREES":

                    TextBox nTextBoxTHREE = new TextBox();
                    nTextBoxTHREE.ID = myReader.GetString(1).ToString();
                    nTextBoxTHREE.CssClass = "inputtext250";

                    if (flag == "edit" && ispost == 1)
                    {
                        nTextBoxTHREE.Text = content.Contents(Content_ID, myReader.GetString(1).ToString(), TypeTree_ID);
                    }

                    ToolsPut = "&nbsp;<img src='../Admin_Public/Images/RepeatedRegion.gif' onclick='selectTree(" + myReader.GetString(1).ToString() + "," + myReader.GetString(4).ToString() + ");'> ";

                    TableRow trTHREE = new TableRow();
                    TableCell tc1THREE = new TableCell();
                    TableCell tc2THREE = new TableCell();
                    TableCell tc3THREE = new TableCell();
                    tc1THREE.HorizontalAlign = HorizontalAlign.Right;
                    tc1THREE.Text = myReader.GetString(3).ToString() + "：&nbsp;";
                    tc2THREE.Controls.Add(nTextBoxTHREE);
                    tc3THREE.Text = ToolsPut;
                    tc2THREE.Width = 260;

                    trTHREE.Cells.Add(tc1THREE);
                    trTHREE.Cells.Add(tc2THREE);
                    trTHREE.Cells.Add(tc3THREE);
                    Table2.Rows.Add(trTHREE);

                    break;

                case "TEXTAREA":

                    TextBox nTextBoxTEXTAREA = new TextBox();
                    nTextBoxTEXTAREA.ID = myReader.GetString(1).ToString();
                    nTextBoxTEXTAREA.Width = 250;
                    nTextBoxTEXTAREA.Height = 50;

                    if (flag == "edit" && ispost == 1)
                    {
                        nTextBoxTEXTAREA.Text = content.Contents(Content_ID, myReader.GetString(1).ToString(), TypeTree_ID);
                    }

                    nTextBoxTEXTAREA.TextMode = TextBoxMode.MultiLine;
                    TableRow trTEXTAREA = new TableRow();
                    TableCell tc1TEXTAREA = new TableCell();
                    TableCell tc2TEXTAREA = new TableCell();
                    tc1TEXTAREA.Width = 100;
                    tc1TEXTAREA.HorizontalAlign = HorizontalAlign.Right;
                    tc1TEXTAREA.Text = myReader.GetString(3).ToString() + "：&nbsp;<br/><input type='button' value='HTML' onclick='editHTML(" + myReader.GetString(1).ToString() + ");'>&nbsp;";
                    tc2TEXTAREA.Controls.Add(nTextBoxTEXTAREA);
                    trTEXTAREA.Cells.Add(tc1TEXTAREA);
                    trTEXTAREA.Cells.Add(tc2TEXTAREA);
                    Table2.Rows.Add(trTEXTAREA);

                    break;

                case "SELECT":

                    DropDownList ListSELECT = new DropDownList();
                    ListSELECT.ID = myReader.GetString(1).ToString();


                    string[] ops;
                    string opss;
                    char sSplit = ',';
                    opss = myReader.GetString(4);

                    int i = 10;
                    char c = (char)i;			//相当于vb中的chr(10)

                    opss = opss.Replace(c, sSplit);
                    ops = opss.Split(sSplit);

                    for (int j = 0; j < ops.Length; j++)
                    {
                        ListSELECT.Items.Add(ops[j].ToString().Trim());
                    }


                    if (flag == "edit" && ispost == 1)
                    {
                        if (!string.IsNullOrEmpty(content.Contents(Content_ID, myReader.GetString(1).ToString(), TypeTree_ID).Trim()))
                        {
                            if (myReader.GetString(4).IndexOf(content.Contents(Content_ID, myReader.GetString(1).ToString(), TypeTree_ID)) != -1) { ListSELECT.SelectedValue = content.Contents(Content_ID, myReader.GetString(1).ToString(), TypeTree_ID); };
                        }

                    }

                    TableRow trSELECT = new TableRow();
                    TableCell tc1SELECT = new TableCell();
                    TableCell tc2SELECT = new TableCell();
                    tc1SELECT.HorizontalAlign = HorizontalAlign.Right;
                    tc1SELECT.Width = 100;
                    tc1SELECT.Text = myReader.GetString(3).ToString() + "：&nbsp;";
                    tc2SELECT.Controls.Add(ListSELECT);
                    trSELECT.Cells.Add(tc1SELECT);
                    trSELECT.Cells.Add(tc2SELECT);
                    Table2.Rows.Add(trSELECT);

                    break;

                case "LABEL":

                    TableRow trLABEL = new TableRow();
                    TableCell tc1trLABEL = new TableCell();
                    TableCell tc2trLABEL = new TableCell();
                    tc1trLABEL.Width = 100;
                    //						tc2TEXT.Width = 260;
                    tc1trLABEL.HorizontalAlign = HorizontalAlign.Right;
                    tc1trLABEL.Text = myReader.GetString(3).ToString() + "：&nbsp;";
                    tc2trLABEL.Text = myReader.GetString(4).ToString();
                    trLABEL.Cells.Add(tc1trLABEL);
                    trLABEL.Cells.Add(tc2trLABEL);
                    Table2.Rows.Add(trLABEL);

                    break;

                default:
                    ToolsPut = "数据错误！";
                    break;
            }
        }
        myReader.Close();
    }


    // 动态添加控件 over

    private void setPageData(string xmlStr) //拆分xml
    {
        this.EditorControl1.Value = Tools.DBToWeb(Server.HtmlEncode(xmlStr));
    }

    public string InitXML(string GName)
    {

        string txtInitXML = "";
        XmlDocument xmlDoc = new XmlDocument();
        if (!string.IsNullOrEmpty(txtXML ))
        {
            xmlDoc.LoadXml(txtXML);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("Employees").ChildNodes;
            foreach (XmlNode xn in nodeList)//遍历所有子节点 
            {
                XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型 
                if (xe.GetAttribute("GName") == GName)//如果genre属性值为“张三” 
                {
                    XmlNodeList nls = xe.ChildNodes;//继续获取xe子节点的所有子节点 
                    foreach (XmlNode xn1 in nls)//遍历 
                    {
                        XmlElement xe2 = (XmlElement)xn1;//转换类型 
                        if (xe2.Name == "Value")//如果找到 
                        {
                            txtInitXML = xe2.InnerText;
                        }
                    }

                }
            }
        }
        return txtInitXML;
    }


    public string InitContent(string GName)
    {
        string txtInitXML = "";
        return txtInitXML;
    }
    protected void Toolsbar1_ButtonClick(object sender, System.EventArgs e)
    {
        SaveContent("1");
    }

    private void SaveContent(string Status)
    {
        if (typeTree.TypeTree_Type != 2)
        {

            if (this.TextBoxTitle.Text == "")
            {
                this.LabNameMust.Text = "文章名称为必添项目！请添写！";
                return;
            }


            //--------------------------

            string contentList = Request.Form["content1"];
            string FormCount = Request.Form["FormCount"];
            if (!string.IsNullOrEmpty(FormCount))
            {
                if (FormCount != "0" && !String.IsNullOrEmpty(FormCount ))
                {
                    for (int i = 1; i <= int.Parse(FormCount); i++)
                    {
                        contentList = contentList + Request.Form["content1" + i];
                    }
                }
            }

            contentList = contentList.Replace("http://" + Request.ServerVariables["HTTP_HOST"].ToString(), "");

            //---[发布前准备]----------------------

            if (this.Headnews.Checked == true)
            { content.HeadNews = "1"; }
            else
            { content.HeadNews = "0"; }

            if (this.PictureNews.Checked == true)
            { content.PictureNews = "1"; }
            else
            { content.PictureNews = "0"; }

            if (this.CheckBoxWebtoThisPic.Checked == true)
            { contentList = WebtoThisPic(contentList); }

            content.Name = Tools.WebToDB(this.TextBoxTitle.Text);
            //Response.Write ("<script>alert('"+contentList+"');</script>");
            //content.Description		=Tools.WebToDB(contentList);
            content.Description = Tools.WebToDB(contentList.ToString());
            //content.Description = Tools.WebToDB(Request.Form["content1"].ToString());
            content.Original = this.Original.Text;
            content.KeyWord = this.KeyWord.Text;
            content.Status = Status;
            content.DerivationLink = this.TextBoxLink.Text;
            content.Derivation = Tools.WebToDB(this.TextBoxFrom.Text);
            content.PictureNotes = this.Picture_Notes.Text;
            content.PictureName = this.TextBoxPic.Text;
            content.PictureNameD = this.TextBoxPicD.Text;
            content.SubmitDate = System.DateTime.Parse(Request["TextBoxDate"].ToString() + " " + DateTime.Now.ToShortTimeString());


            xmldoc = new XmlDocument();
            xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xmldoc.AppendChild(xmlnode);
            xmlelem = xmldoc.CreateElement("", "Employees", "");
            xmldoc.AppendChild(xmlelem);

        }
        //扩展字段入库

        string TxtValue = "";
        string sql1 = "", sql2 = "", sqlcc = "", sql3 = "";
        if (typeTree.TypeTree_ContentFields != 0)
        {
            SqlDataReader reader = null;
            string sql = "SELECT Fields_ID,Property_Name,Property_InputType,Property_Alias,Property_InputOptions,Property_Order FROM Content_FieldsContent WHERE FieldsName_ID =" + typeTree.TypeTree_ContentFields + " order by Fields_ID";
            reader = Tools.DoSqlReader(sql);

            while (reader.Read())
            {
                switch (reader.GetString(2))
                {
                    case "TEXT":
                        if (((TextBox)Page.FindControl(reader.GetString(1).ToString())) != null)
                        {
                            TxtValue = ((TextBox)Page.FindControl(reader.GetString(1).ToString())).Text;
                        }
                        break;

                    case "NUMBER":
                        if (((TextBox)Page.FindControl(reader.GetString(1).ToString())) != null)
                        {
                            TxtValue = ((TextBox)Page.FindControl(reader.GetString(1).ToString())).Text;
                        }
                        break;

                    case "IMAGE":
                        if (((TextBox)Page.FindControl(reader.GetString(1).ToString())) != null)
                        {
                            TxtValue = ((TextBox)Page.FindControl(reader.GetString(1).ToString())).Text;
                        }
                        break;
                    case "FILE":
                        if (((TextBox)Page.FindControl(reader.GetString(1).ToString())) != null)
                        {
                            TxtValue = ((TextBox)Page.FindControl(reader.GetString(1).ToString())).Text;
                        }
                        break;
                    case "DATETIME":
                        if (((TextBox)Page.FindControl(reader.GetString(1).ToString())) != null)
                        {
                            TxtValue = ((TextBox)Page.FindControl(reader.GetString(1).ToString())).Text;
                        }
                        break;
                    case "TREES":
                        if (((TextBox)Page.FindControl(reader.GetString(1).ToString())) != null)
                        {
                            TxtValue = ((TextBox)Page.FindControl(reader.GetString(1).ToString())).Text;
                        }
                        break;
                    case "TEXTAREA":
                        if (((TextBox)Page.FindControl(reader.GetString(1).ToString())) != null)
                        {
                            TxtValue = ((TextBox)Page.FindControl(reader.GetString(1).ToString())).Text;
                        }
                        break;
                    case "SELECT":
                        if (((DropDownList)Page.FindControl(reader.GetString(1).ToString())) != null)
                        {
                            TxtValue = ((DropDownList)Page.FindControl(reader.GetString(1).ToString())).SelectedValue;

                        }
                        break;
                    case "LABEL":

                        break;
                    default:
                        //ToolsPut = "数据错误！";
                        break;
                }
                sql1 = sql1 + "[" + reader.GetString(1).ToString() + "],";
                sql2 = sql2 + "'" + TxtValue + "',";
                sql3 = sql3 + reader.GetString(1).ToString() + " = '" + TxtValue + "',";

            }

            reader.Close();

        }
        string str = "";

        if (this.LabelFlag.Text.Equals("edit"))
        {
            int Content_ID = int.Parse(this.LabelEditContentID.Text);
            content.lockedby = "";	//解锁
            string TypeTree_URL = this.TypeTree_URL.Text;
            Url = TypeTree_URL.Replace("{@UID}", Content_ID.ToString()); //获得URL
            content.Url = Url;

            if (typeTree.TypeTree_Type == 2)
            {
                Content_FieldsName _Content_FieldsName = new Content_FieldsName();
                _Content_FieldsName.Init(typeTree.TypeTree_ContentFields);
                sql3 = sql3 + "Status ='" + Status + "',Url = '" + Url + "'";
                sqlcc = "update ContentUser_" + _Content_FieldsName.FieldsBase_Name + " set " + sql3 + " where Content_ID = " + Content_ID;
                Tools.DoSql(sqlcc);
            }
            else
            {
                content.Update(Content_ID);
            }

            UpdateContent_ID = Content_ID;
        }
        else
        {
            content.TypeTree_ID = int.Parse(this.LabelTypeID.Text);
            content.Author = (string)this.Session["Master_UserName"];
            content.Clicks = 1;
            content.OrderNum =content.QueryMaxContentID();
            string TypeTree_URL = this.TypeTree_URL.Text;
            int Content_ID = content.QueryMaxContentID();
            Url = TypeTree_URL.Replace("{@UID}", Content_ID.ToString()); //获得URL
            content.Url = Url;

            if (typeTree.TypeTree_Type == 2)
            {
                Content_FieldsName _Content_FieldsName = new Content_FieldsName();
                _Content_FieldsName.Init(typeTree.TypeTree_ContentFields);
                sql1 = sql1 + "Content_ID,TypeTree_ID,Author,Clicks,OrderNum,SubmitDate,Url,Status";
                sql2 = sql2 + Content_ID + "," + int.Parse(this.LabelTypeID.Text) + ",'" + (string)this.Session["Master_UserName"] + "','1'," + Content_ID + ",getdate(),'" + Url + "','" + Status + "'";
                sqlcc = "insert into ContentUser_" + _Content_FieldsName.FieldsBase_Name + " (" + sql1 + ") values (" + sql2 + ")";
                Tools.DoSql(sqlcc);
            }
            else
            {
                content.Create();
            }

            UpdateContent_ID = content.ContentId;

        }
        if (typeTree.TypeTree_Type != 2)
        {
            if (typeTree.TypeTree_ContentFields != 0)
            {
                Content_FieldsName _Content_FieldsName = new Content_FieldsName();
                _Content_FieldsName.Init(typeTree.TypeTree_ContentFields);
                sql1 = sql1 + "Content_ID";
                sql2 = sql2 + UpdateContent_ID;

                sqlcc = "insert into ContentUser_" + _Content_FieldsName.FieldsBase_Name + " (" + sql1 + ") values (" + sql2 + ")";

                Tools.DoSql("delete from ContentUser_" + _Content_FieldsName.FieldsBase_Name + " where Content_ID = " + UpdateContent_ID);
                Tools.DoSql(sqlcc);
            }
        }




        //扩展字段入库 over

        this.LabelMsg.Text = str;

        //Log
        Log _Log = new Log();
        string Log_Action = "";
        switch (int.Parse(Status))
        {
            case 1:
                Log_Action = "保存为草稿";
                break;
            case 2:
                Log_Action = "提交待审批";
                break;
            case 3:
                Log_Action = "审批待发布";
                break;
            case 4:
                Log_Action = "审批已发布";
                break;
            case 5:
                Log_Action = "过期已归档";
                break;
        }

        _Log.Content_Id = UpdateContent_ID;
        _Log.Log_Action = Log_Action;
        _Log.Log_Txt = this.TextBoxLogText.Text;
        _Log.Master_ID = int.Parse(Session["Master_ID"].ToString());
        _Log.Master_Name = Session["Master_UserName"].ToString();
        _Log.Create();

        //Log over

        if (typeTree.MailMsg.IndexOf(Status, 0) != -1)
        {

            SystemCls _SystemCls = new SystemCls();
            _SystemCls.Init();



            MessageClass email = new MessageClass();
            email.Logging = true;
            email.Silent = true;
            email.Charset = "GB2312";
            email.MailServerUserName = _SystemCls.JMail_MailServerUserName;
            email.MailServerPassWord = _SystemCls.JMail_MailServerPassWord;
            email.From = _SystemCls.JMail_From;
            email.FromName = "GCMS系统邮件";
            email.ContentType = "text/html";
            email.Subject = (string)this.Session["Master_UserName"] + "新发了一片文章 ：" + content.Name + "请您审核";
            //email.AddAttachment("c:\\test.xml",true,"");

            string MailBody = "";
            MailBody = MailBody + "<p>栏目名称：" + typeTree.TypeTreeCName + "</p>";
            MailBody = MailBody + "<p>作品名：" + content.Name + "</p>";
            MailBody = MailBody + "<p>作者：" + (string)this.Session["Master_UserName"] + "</p>";
            MailBody = MailBody + "<p>请您审核</p>";
            MailBody = MailBody + "<p>（该邮件是GCMS系统发布，请勿回复） </p>";

            email.Body = MailBody;

            SqlDataReader readerMail = null;
            string sqls = "select isnull(Master_Email,'') Master_Email,Master_Name from Content_Master where Master_ID in (" +
                        "select distinct Master_ID from Content_RolesMaster where Roles_ID in ( " +
                        "select distinct rc.Roles_ID from content_RolesConnect rc,content_RolesPopedom rp " +
                        "where rc.roles_ID=rp.roles_ID and Popedom_EName = 'Editor' and typetree_ID = " + int.Parse(this.LabelTypeID.Text) + ")) ";

            readerMail =Tools.DoSqlReader(sqls);
            int iMail = 0;
            while (readerMail.Read())
            {
                if (!string.IsNullOrEmpty( readerMail.GetString(0).ToString()))
                {
                    iMail = iMail++;
                    if (iMail == 1)
                    {
                        email.AddRecipient(readerMail.GetString(0).ToString(), readerMail.GetString(1).ToString(), null);
                    }
                    else
                    {
                        email.AddRecipientCC(readerMail.GetString(0).ToString(), readerMail.GetString(1).ToString(), null);
                    }
                }

            }

            readerMail.Close();

            email.Send(_SystemCls.JMail_Server, false);
            //Response.Write (email.ErrorMessage) ;
            email.Close();

        }

        //静发

        try
        {
            CreateFiles _CreateFiles = new CreateFiles();
            _CreateFiles.CreateContentFiles(int.Parse(this.LabelTypeID.Text), UpdateContent_ID);
            _CreateFiles.CreateChannelFiles(int.Parse(this.LabelTypeID.Text));
            _CreateFiles.CreateLinkPushFiles(int.Parse(this.LabelTypeID.Text));

            Page.RegisterStartupScript("保存目录", "<script language=javascript>top.window.close();</script>");

        }
        catch (Exception ex)
        {
            PageHeader.Value = PageHeader.Value + "<font color=red>" + ex.Message + "</font>";
        }

        //Response.Redirect ("Content_List.aspx?TypeTree_ID="+TypeTree_ID);


    }

    protected void Toolsbar2_ButtonClick(object sender, System.EventArgs e)
    {
        SaveContent("3");
    }

    protected void Toolsbar3_ButtonClick(object sender, System.EventArgs e)
    {
        SaveContent("5");
    }

    protected void Toolsbar4_ButtonClick(object sender, System.EventArgs e)
    {
        SaveContent("1");
    }

    protected void Toolsbar5_ButtonClick(object sender, System.EventArgs e)
    {
        SaveContent("3");
    }

    protected void Toolsbar6_ButtonClick(object sender, System.EventArgs e)
    {
        SaveContent("4");
    }

    private string UrlString(string FilesUrl)
    {
        FilesUrl = FilesUrl.Replace("/", "//");
        FilesUrl = Server.MapPath(FilesUrl);
        return FilesUrl;
    }

    //将网站上的图片粘贴到本地

    private string WebtoThisPic(string Contents)
    {
        string NewContents = Contents;
        int p1 = NewContents.IndexOf("<IMG", 0);

        if (p1 < 0) { return Contents; };
        do
        {
            p1 = NewContents.IndexOf("src", p1);
            int p2 = NewContents.IndexOf("\"", p1);
            p2 = p2 + 1;
            int p3 = NewContents.IndexOf("\"", p2);

            string ContentSub = NewContents.Substring(p2, p3 - p2);
            int httpint = ContentSub.IndexOf("ttp://");

            if (httpint > 0)
            {
                string filename = ContentSub.Substring(ContentSub.LastIndexOf("/"));
                string _file = Tools.UploadName(filename, LTypeTree_PictureURL);

                WebClient wc = new WebClient();
                wc.DownloadFile(ContentSub, Server.MapPath(_file).Replace("\\", "\\\\"));
                Contents = Contents.Replace(ContentSub, _file);
            }

            NewContents = NewContents.Substring(p3);
            p1 = NewContents.IndexOf("<IMG", 1);

        }
        while (p1 > 0);
        return Contents;
    }
}
