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
using System.Xml;
using System.Data.SqlClient;
using GCMS.PageCommonClassLib;

public partial class Content_Type_Add : GCMS.PageCommonClassLib.PageBase
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
    Type_TypeTree typeTree = new Type_TypeTree();

    XmlDocument xmldoc;
    XmlNode xmlnode;
    string txtXML = "";
    XmlElement xmlelem;
    private String strType;
    private String strTypeTreeID;
    private int tmpTypeTreeParentID;
    private int tmpOrderNum;

    protected void Page_Load(object sender, EventArgs e)
    {
        Type_TypeTree typeTree = new Type_TypeTree();
        strType = this.Request.QueryString["OrderType"].ToString();
        this.OrderType.Value = strType;

        //创建根目录
        if (strType.Equals("root"))
        {
            tmpTypeTreeParentID = -1;
            tmpOrderNum = typeTree.QueryMaxTypeTreeOrderNum(tmpTypeTreeParentID);
            this.Mssg.Text = "创建根目录";
        }

        //创建子目录
        if (strType.Equals("son"))
        {
            strTypeTreeID = this.Request.QueryString["TypeTree_ID"].ToString();
            this.saveResult.Text = strTypeTreeID;
            tmpTypeTreeParentID = int.Parse(strTypeTreeID);
            tmpOrderNum = typeTree.QueryMaxTypeTreeOrderNum(int.Parse(strTypeTreeID));
            this.Mssg.Text = "创建子目录";
        }

        //更新
        if (!this.IsPostBack)
        {
            SortedList ListText = new SortedList();

            for (int i = 2; i > -1; i--)
            {
                ListText.Add(typeTree.strTypeTreeIssuance(i), i);
            }

            TypeTree_Issuance.DataSource = ListText;
            TypeTree_Issuance.DataTextField = "Key";
            TypeTree_Issuance.DataValueField = "Value";
            TypeTree_Issuance.DataBind();


            SortedList ListTextType = new SortedList();
            for (int i = 2; i > -1; i--)
            {
                ListTextType.Add(typeTree.strTypeTreeType(i), i);
            }

            TypeTree_Type.DataSource = ListTextType;
            TypeTree_Type.DataTextField = "Key";
            TypeTree_Type.DataValueField = "Value";
            TypeTree_Type.DataBind();


            if (strType.Equals("Update"))
            {
                strTypeTreeID = this.Request.QueryString["TypeTree_ID"].ToString();
                if (!this.IsPostBack)
                {
                    typeTree.Init(int.Parse(strTypeTreeID));
                    this.TypeTree_CName.Text = typeTree.TypeTreeCName;
                    this.TypeTree_EName.Text = typeTree.TypeTreeEName;
                    this.TypeTree_PictureURL.Value = typeTree.TypeTreePictureURL;
                    this.TypeTree_Explain.Text = typeTree.TypeTreeExplain;
                    this.List_Amount.Text = typeTree.Listamount.ToString();
                    this.TypeTree_ListTemplate.Value = typeTree.TypeTreeListTemplate;
                    this.TypeTree_ListURL.Value = typeTree.TypeTreeListURL;
                    this.TypeTree_Template.Value = typeTree.TypeTreeTemplate;
                    this.TypeTree_URL.Value = typeTree.TypeTreeURL;
                    this.TypeTree_Issuance.SelectedValue = typeTree.TypeTreeIssuance.ToString();
                    this.TypeTree_Images.Value = typeTree.TypeTreeImages;
                    this.TypeTree_Language.SelectedValue = typeTree.TypeTree_Language;

                    if (int.Parse(typeTree.TypeTree_Type.ToString()) < 3)
                    {
                        this.TypeTree_Type.SelectedValue = typeTree.TypeTree_Type.ToString();
                    }
                    this.Mssg.Text = "更新目录 - " + typeTree.TypeTreeCName;
                    this.inpTypeTree_XML.Value = typeTree.TypeTree_Xml;
                    txtXML = this.inpTypeTree_XML.Value;
                    this.inpTypeTree_TypeFields.Value = typeTree.TypeTree_TypeFields.ToString();
                    if (typeTree.MailMsg == "3")
                    {
                        MailMsg.Checked = true;
                    }

                }
            }

        }
        if (strType == "Update")
        {
            strTypeTreeID = this.Request.QueryString["TypeTree_ID"].ToString();
            //			typeTree.Init(int.Parse(strTypeTreeID));
            //			txtXML = typeTree.TypeTree_Xml;
            txtXML = this.inpTypeTree_XML.Value;
            AddFieldsWriteTxt(int.Parse(this.inpTypeTree_TypeFields.Value));
        }
    }

    protected void AddFieldsWriteTxt(int FieldsName_ID)
    {
        SqlDataReader myReader;
        string sql = "SELECT Fields_ID,Property_Name,Property_InputType,Property_Alias,Property_InputOptions FROM Content_FieldsContent WHERE FieldsName_ID =" + FieldsName_ID + " order by Property_Order";
        string ToolsPut = "";
        myReader = Tools.DoSqlReader(sql);


        while (myReader.Read())
        {

            switch (myReader.GetString(2))
            {
                case "TEXT":

                    TextBox nTextBoxTEXT = new TextBox();
                    //nTextBoxTEXT.ID = "PROPERTY"+ myReader.GetInt32(0).ToString();
                    nTextBoxTEXT.ID = myReader.GetString(1).ToString();
                    nTextBoxTEXT.CssClass = "inputtext250";

                    if (strType == "Update")
                    {
                        if (InitXML(myReader.GetString(1).ToString()) != "")
                        { nTextBoxTEXT.Text = InitXML(myReader.GetString(1).ToString()); }
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
                    Table4.Rows.Add(trTEXT);

                    break;

                case "NUMBER":

                    TextBox nTextBoxNUMBER = new TextBox();
                    //nTextBoxTEXT.ID = "PROPERTY"+ myReader.GetInt32(0).ToString();
                    nTextBoxNUMBER.ID = myReader.GetString(1).ToString();
                    nTextBoxNUMBER.CssClass = "inputtext250";

                    if (strType == "Update")
                    {
                        if (InitXML(myReader.GetString(1).ToString()) != "")
                        { nTextBoxNUMBER.Text = InitXML(myReader.GetString(1).ToString()); }
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
                    Table4.Rows.Add(trNUMBER);

                    break;

                case "IMAGE":

                    TextBox nTextBoxIMAGE = new TextBox();
                    nTextBoxIMAGE.ID = myReader.GetString(1).ToString();
                    nTextBoxIMAGE.CssClass = "inputtext250";

                    if (strType == "Update")
                    {
                        if (InitXML(myReader.GetString(1).ToString()) != "")
                        { nTextBoxIMAGE.Text = InitXML(myReader.GetString(1).ToString()); }
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
                    Table4.Rows.Add(trIMAGE);

                    break;

                case "FILE":

                    TextBox nTextBoxFILE = new TextBox();
                    nTextBoxFILE.ID = myReader.GetString(1).ToString();
                    nTextBoxFILE.CssClass = "inputtext250";

                    if (strType == "Update")
                    {
                        if (InitXML(myReader.GetString(1).ToString()) != "")
                        { nTextBoxFILE.Text = InitXML(myReader.GetString(1).ToString()); }
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
                    Table4.Rows.Add(trFILE);

                    break;

                case "DATETIME":

                    TextBox nTextBoxDATETIME = new TextBox();
                    nTextBoxDATETIME.ID = myReader.GetString(1).ToString();
                    nTextBoxDATETIME.CssClass = "inputtext250";

                    if (strType == "Update")
                    {
                        if (InitXML(myReader.GetString(1).ToString()) != "")
                        {
                            nTextBoxDATETIME.Text = System.DateTime.Parse(InitXML(myReader.GetString(1).ToString())).ToShortDateString(); //formatdatetime
                        }
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
                    Table4.Rows.Add(trDATETIME);

                    break;

                case "TREES":

                    TextBox nTextBoxTREES = new TextBox();
                    nTextBoxTREES.ID = myReader.GetString(1).ToString();
                    nTextBoxTREES.CssClass = "inputtext250";

                    if (strType == "Update")
                    {
                        if (InitXML(myReader.GetString(1).ToString()) != "")
                        {
                            nTextBoxTREES.Text = InitXML(myReader.GetString(1).ToString());
                        }
                    }

                    ToolsPut = "&nbsp;<img src='../Admin_Public/Images/RepeatedRegion.gif' onclick='selectTree(" + myReader.GetString(1).ToString() + "," + myReader.GetString(4).ToString() + ");'> ";

                    TableRow trTREES = new TableRow();
                    TableCell tc1TREES = new TableCell();
                    TableCell tc2TREES = new TableCell();
                    TableCell tc3TREES = new TableCell();
                    tc1TREES.HorizontalAlign = HorizontalAlign.Right;
                    tc1TREES.Text = myReader.GetString(3).ToString() + "：&nbsp;";
                    tc2TREES.Controls.Add(nTextBoxTREES);
                    tc3TREES.Text = ToolsPut;
                    tc2TREES.Width = 260;

                    trTREES.Cells.Add(tc1TREES);
                    trTREES.Cells.Add(tc2TREES);
                    trTREES.Cells.Add(tc3TREES);
                    Table4.Rows.Add(trTREES);

                    break;

                case "TEXTAREA":

                    TextBox nTextBoxTEXTAREA = new TextBox();
                    nTextBoxTEXTAREA.ID = myReader.GetString(1).ToString();
                    nTextBoxTEXTAREA.Width = 250;
                    nTextBoxTEXTAREA.Height = 50;

                    if (strType == "Update")
                    {
                        if (InitXML(myReader.GetString(1).ToString()) != "")
                        { nTextBoxTEXTAREA.Text = InitXML(myReader.GetString(1).ToString()); }
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
                    Table4.Rows.Add(trTEXTAREA);

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


                    if (strType == "Update")
                    {
                        if (InitXML(myReader.GetString(1).ToString()).Trim() != "")
                        {
                            if (InitXML(myReader.GetString(1).ToString().Trim()) != "") { ListSELECT.SelectedValue = InitXML(myReader.GetString(1).ToString().Trim()); };
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
                    Table4.Rows.Add(trSELECT);

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
                    Table4.Rows.Add(trLABEL);

                    break;

                default:
                    ToolsPut = "数据错误！";
                    break;
            }
        }
        myReader.Close();
       
    }

    protected void Toolsbar1_ButtonClick(object sender, System.EventArgs e)
    {
        strType = OrderType.Value;
        string sql1 = "";
        string sql2 = "";
        string sqlcc = "";


        typeTree.TypeTreeParentID = tmpTypeTreeParentID;
        typeTree.TypeTreeCName = this.TypeTree_CName.Text;
        typeTree.TypeTreeEName = this.TypeTree_EName.Text;
        typeTree.TypeTreeExplain = this.TypeTree_Explain.Text;
        typeTree.TypeTreeOrderNum = tmpOrderNum;
        typeTree.TypeTreeURL = this.TypeTree_URL.Value;
        typeTree.TypeTreeTemplate = this.TypeTree_Template.Value;
        typeTree.TypeTreeListTemplate = this.TypeTree_ListTemplate.Value;
        typeTree.TypeTreeListURL = this.TypeTree_ListURL.Value;
        typeTree.TypeTreeIssuance = int.Parse(this.TypeTree_Issuance.SelectedValue);
        typeTree.TypeTreePictureURL = this.TypeTree_PictureURL.Value;
        typeTree.TypeTreeImages = this.TypeTree_Images.Value;
        typeTree.TypeTree_Language = this.TypeTree_Language.SelectedValue;
        typeTree.TypeTree_Type = int.Parse(this.TypeTree_Type.SelectedValue);
        if (this.MailMsg.Checked == true)
        { typeTree.MailMsg = "3"; }

        int intList_Amount = 20;
        string TxtValue = "";

        if (this.List_Amount.Text != "") { intList_Amount = int.Parse(this.List_Amount.Text.ToString()); }
        typeTree.Listamount = intList_Amount;

        if (strType.Equals("Update"))
        {
            //				typeTree.Init(int.Parse(strTypeTreeID));	
            //				strTypeTreeID = this.Request.QueryString["TypeTree_ID"].ToString();


            xmldoc = new XmlDocument();
            xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xmldoc.AppendChild(xmlnode);
            xmlelem = xmldoc.CreateElement("", "Employees", "");
            xmldoc.AppendChild(xmlelem);


            if (this.inpTypeTree_TypeFields.Value != "0")
            {
                SqlDataReader reader = null;
                string sql = "SELECT Fields_ID,Property_Name,Property_InputType,Property_Alias,Property_InputOptions,Property_Order FROM Content_FieldsContent WHERE FieldsName_ID =" + this.inpTypeTree_TypeFields.Value + " order by Fields_ID";
                reader = Tools.DoSqlReader(sql);

                while (reader.Read())
                {
                    switch (reader.GetString(2))
                    {
                        case "TEXT":
                            if (((TextBox)Page.FindControl(reader.GetString(1).ToString())) != null)
                            {
                                TxtValue = ((TextBox)Page.FindControl(reader.GetString(1).ToString())).Text;
                                //UpdateXML(reader.GetString(1).ToString(),TxtValue);

                                XmlNode root = xmldoc.SelectSingleNode("Employees");//查找<Employees> 
                                XmlElement xe1 = xmldoc.CreateElement("Node");//创建一个<Node>节点 

                                xe1.SetAttribute("GName", reader.GetString(1).ToString());//设置该节点genre属性 
                                xe1.SetAttribute("OrderNum", reader.GetInt32(5).ToString());//设置该节点ISBN属性 

                                XmlElement xesub1 = xmldoc.CreateElement("Value");
                                xesub1.InnerText = TxtValue;
                                xe1.AppendChild(xesub1);
                                root.AppendChild(xe1);

                            }
                            break;

                        case "NUMBER":
                            if (((TextBox)Page.FindControl(reader.GetString(1).ToString())) != null)
                            {
                                TxtValue = ((TextBox)Page.FindControl(reader.GetString(1).ToString())).Text;
                                //UpdateXML(reader.GetString(1).ToString(),TxtValue);

                                XmlNode root = xmldoc.SelectSingleNode("Employees");//查找<Employees> 
                                XmlElement xe1 = xmldoc.CreateElement("Node");//创建一个<Node>节点 

                                xe1.SetAttribute("GName", reader.GetString(1).ToString());//设置该节点genre属性 
                                xe1.SetAttribute("OrderNum", reader.GetInt32(5).ToString());//设置该节点ISBN属性 

                                XmlElement xesub1 = xmldoc.CreateElement("Value");
                                xesub1.InnerText = TxtValue;
                                xe1.AppendChild(xesub1);
                                root.AppendChild(xe1);

                            }
                            break;
                        case "IMAGE":
                            if (((TextBox)Page.FindControl(reader.GetString(1).ToString())) != null)
                            {
                                TxtValue = ((TextBox)Page.FindControl(reader.GetString(1).ToString())).Text;

                                XmlNode root = xmldoc.SelectSingleNode("Employees");//查找<Employees> 
                                XmlElement xe1 = xmldoc.CreateElement("Node");//创建一个<Node>节点 

                                xe1.SetAttribute("GName", reader.GetString(1).ToString());//设置该节点genre属性 
                                xe1.SetAttribute("OrderNum", reader.GetInt32(5).ToString());//设置该节点ISBN属性 

                                XmlElement xesub1 = xmldoc.CreateElement("Value");
                                xesub1.InnerText = TxtValue;
                                xe1.AppendChild(xesub1);
                                root.AppendChild(xe1);

                                //UpdateXML(reader.GetString(1).ToString(),TxtValue);
                            }
                            break;
                        case "FILE":
                            if (((TextBox)Page.FindControl(reader.GetString(1).ToString())) != null)
                            {
                                TxtValue = ((TextBox)Page.FindControl(reader.GetString(1).ToString())).Text;
                                XmlNode root = xmldoc.SelectSingleNode("Employees");//查找<Employees> 
                                XmlElement xe1 = xmldoc.CreateElement("Node");//创建一个<Node>节点 

                                xe1.SetAttribute("GName", reader.GetString(1).ToString());//设置该节点genre属性 
                                xe1.SetAttribute("OrderNum", reader.GetInt32(5).ToString());//设置该节点ISBN属性 

                                XmlElement xesub1 = xmldoc.CreateElement("Value");
                                xesub1.InnerText = TxtValue;
                                xe1.AppendChild(xesub1);
                                root.AppendChild(xe1);
                            }
                            break;
                        case "DATETIME":
                            if (((TextBox)Page.FindControl(reader.GetString(1).ToString())) != null)
                            {
                                TxtValue = ((TextBox)Page.FindControl(reader.GetString(1).ToString())).Text;
                                XmlNode root = xmldoc.SelectSingleNode("Employees");//查找<Employees> 
                                XmlElement xe1 = xmldoc.CreateElement("Node");//创建一个<Node>节点 

                                xe1.SetAttribute("GName", reader.GetString(1).ToString());//设置该节点genre属性 
                                xe1.SetAttribute("OrderNum", reader.GetInt32(5).ToString());//设置该节点ISBN属性 

                                XmlElement xesub1 = xmldoc.CreateElement("Value");
                                xesub1.InnerText = TxtValue;
                                xe1.AppendChild(xesub1);
                                root.AppendChild(xe1);
                            }
                            break;
                        case "TREES":
                            if (((TextBox)Page.FindControl(reader.GetString(1).ToString())) != null)
                            {
                                TxtValue = ((TextBox)Page.FindControl(reader.GetString(1).ToString())).Text;
                                XmlNode root = xmldoc.SelectSingleNode("Employees");//查找<Employees> 
                                XmlElement xe1 = xmldoc.CreateElement("Node");//创建一个<Node>节点 

                                xe1.SetAttribute("GName", reader.GetString(1).ToString());//设置该节点genre属性 
                                xe1.SetAttribute("OrderNum", reader.GetInt32(5).ToString());//设置该节点ISBN属性 

                                XmlElement xesub1 = xmldoc.CreateElement("Value");
                                xesub1.InnerText = TxtValue;
                                xe1.AppendChild(xesub1);
                                root.AppendChild(xe1);
                            }
                            break;
                        case "TEXTAREA":
                            if (((TextBox)Page.FindControl(reader.GetString(1).ToString())) != null)
                            {
                                TxtValue = ((TextBox)Page.FindControl(reader.GetString(1).ToString())).Text;
                                XmlNode root = xmldoc.SelectSingleNode("Employees");//查找<Employees> 
                                XmlElement xe1 = xmldoc.CreateElement("Node");//创建一个<Node>节点 

                                xe1.SetAttribute("GName", reader.GetString(1).ToString());//设置该节点genre属性 
                                xe1.SetAttribute("OrderNum", reader.GetInt32(5).ToString());//设置该节点ISBN属性 

                                XmlElement xesub1 = xmldoc.CreateElement("Value");
                                xesub1.InnerText = TxtValue;
                                xe1.AppendChild(xesub1);
                                root.AppendChild(xe1);
                            }
                            break;
                        case "SELECT":
                            if (((DropDownList)Page.FindControl(reader.GetString(1).ToString())) != null)
                            {
                                TxtValue = ((DropDownList)Page.FindControl(reader.GetString(1).ToString())).SelectedValue;
                                XmlNode root = xmldoc.SelectSingleNode("Employees");//查找<Employees> 
                                XmlElement xe1 = xmldoc.CreateElement("Node");//创建一个<Node>节点 

                                xe1.SetAttribute("GName", reader.GetString(1).ToString());//设置该节点genre属性 
                                xe1.SetAttribute("OrderNum", reader.GetInt32(5).ToString());//设置该节点ISBN属性 

                                XmlElement xesub1 = xmldoc.CreateElement("Value");
                                xesub1.InnerText = TxtValue.ToString().Trim();
                                xe1.AppendChild(xesub1);
                                root.AppendChild(xe1);
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

                }
                typeTree.TypeTree_Xml = xmldoc.OuterXml;
                reader.Close();
            }




            bool bFlag = typeTree.Update(int.Parse(strTypeTreeID));
            if (bFlag)
            {
                this.saveResult.Text = "成功";
            }
            else
            {
                this.saveResult.Text = "失败";
            }
            if (this.inpTypeTree_TypeFields.Value != "0")
            {
                Content_FieldsName _Content_FieldsName = new Content_FieldsName();
                _Content_FieldsName.Init(int.Parse(this.inpTypeTree_TypeFields.Value));
                sql1 = sql1 + "TypeTree_ID";
                sql2 = sql2 + strTypeTreeID;

                sqlcc = "insert into ContentUser_" + _Content_FieldsName.FieldsBase_Name + " (" + sql1 + ") values (" + sql2 + ")";

                Tools.DoSql("delete from ContentUser_" + _Content_FieldsName.FieldsBase_Name + " where TypeTree_ID = " + strTypeTreeID);
                Tools.DoSql(sqlcc);
            }
        }

        else
        {
            if (typeTree.Create())
            {
                this.saveResult.Text = "成功";
            }
            else
            {
                this.saveResult.Text = "失败";
            }
            strTypeTreeID = typeTree.TypeTree_ID.ToString();
            string sSQL = "insert into Content_RolesConnect(TypeTree_ID,Roles_ID) values(" + strTypeTreeID
                + "," + int.Parse(Session["Roles"].ToString()) + ")";
            Tools.DoSql(sSQL);
        }
        Response.Redirect("Type_TypeView.aspx?TypeTree_ID=" + strTypeTreeID);

    }

    private void ButtonSave_Click(object sender, System.EventArgs e)
    {
        strType = OrderType.Value;

        typeTree.TypeTreeParentID = tmpTypeTreeParentID;
        typeTree.TypeTreeCName = this.TypeTree_CName.Text;
        typeTree.TypeTreeEName = this.TypeTree_EName.Text;
        typeTree.TypeTreeExplain = this.TypeTree_Explain.Text;
        typeTree.TypeTreeOrderNum = tmpOrderNum;
        typeTree.TypeTreeURL = this.TypeTree_URL.Value;
        typeTree.TypeTreeTemplate = this.TypeTree_Template.Value;
        typeTree.TypeTreeListTemplate = this.TypeTree_ListTemplate.Value;
        typeTree.TypeTreeListURL = this.TypeTree_ListURL.Value;
        typeTree.TypeTreeIssuance = int.Parse(this.TypeTree_Issuance.SelectedValue);
        typeTree.TypeTreePictureURL = this.TypeTree_PictureURL.Value;
        typeTree.TypeTreeImages = this.TypeTree_Images.Value;
        int intList_Amount = 20;
        if (this.List_Amount.Text != "") { intList_Amount = int.Parse(this.List_Amount.Text.ToString()); }
        typeTree.Listamount = intList_Amount;

        if (strType.Equals("Update"))
        {
            strTypeTreeID = this.Request.QueryString["TypeTree_ID"].ToString();

            bool bFlag = typeTree.Update(int.Parse(strTypeTreeID));
            if (bFlag)
            {
                this.saveResult.Text = "成功";
            }
            else
            {
                this.saveResult.Text = "失败";
            }
            Page.RegisterStartupScript("保存目录", "<script language=javascript>closethiswindows();</script>");

        }
        else
        {
            bool bFlag = typeTree.Create();
            if (bFlag)
            {
                this.saveResult.Text = "成功";

            }
            else
            {
                this.saveResult.Text = "失败";
            }

            Page.RegisterStartupScript("保存目录", "<script language=javascript>closethiswindows();</script>");

        }
    }

    public void UpdateXML(string GName, string TxtValue)
    {
        ContentCls _ContentCls = new ContentCls();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(this.inpTypeTree_XML.Value);
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
                        xe2.InnerText = TxtValue;//则修改
                    }
                }

            }
        }
        typeTree.TypeTree_Xml = xmlDoc.OuterXml;
        string sql = "update Content_Type_typetree set TypeTree_XML = '" + xmlDoc.OuterXml + "' where TypeTree_ID  = " + strTypeTreeID;
        //Change By Galen Mu  2008.8.25
        //将content.DoSelect(..)  改为 Tools.DoSql(..) 
        Tools.DoSql(sql);
    }

    public string InitXML(string GName)
    {

        string txtInitXML = "";
        XmlDocument xmlDoc = new XmlDocument();
        if (this.inpTypeTree_XML.Value != "")
        {
            xmlDoc.LoadXml(this.inpTypeTree_XML.Value);
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
}
