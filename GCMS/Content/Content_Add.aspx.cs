//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-8-25
//
// 功能描述: 发布内容
//
// 已修改问题:
// 未修改问题:
//     1 TypeTree_ID的传递方式易丢失
//     2 SaveContent 方法可以优化
//     3 发送邮件、下载图片方法应该单独封装
// 修改记录
//   2008-8-26 添加注释
//   2008-8-31  规范【自定义事件】【SQL引用】【字符处理】【页面参数获取】代码
//              精简封装动态生成控件部分代码
//   2008-9-13  封装全局变量逻辑，删除InitXml等两废弃函数
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
        this.Page.Visible = false;
        return;
    }
    #endregion 自定义事件的注册和处理

    #region 当前页面注册的SQL字符串
    const string SQL_FieldsContentGetList1="SELECT Fields_ID,Property_Name,Property_InputType,Property_Alias,Property_InputOptions FROM Content_FieldsContent WHERE FieldsName_ID ={0} order by Property_Order";
    const string SQL_FieldsContentGetList2= "SELECT Fields_ID,Property_Name,Property_InputType,Property_Alias,Property_InputOptions,Property_Order FROM Content_FieldsContent WHERE FieldsName_ID ={0} order by Fields_ID";
    const string SQL_ContentUserUpdate="update ContentUser_{0} set {1} where Content_ID = {2}" ;
    const string SQL_ContentUserAdd = "insert into ContentUser_{0} ({1}) values ({2})";
    const string SQL_ContentUserDelete = "delete from ContentUser_{0} where Content_ID = {1}";
    const string SQL_MasterGetList = "select isnull(Master_Email,'') Master_Email,Master_Name from Content_Master where Master_ID in (select distinct Master_ID from Content_RolesMaster where Roles_ID in ( select distinct rc.Roles_ID from content_RolesConnect rc,content_RolesPopedom rp  where rc.roles_ID=rp.roles_ID and Popedom_EName = 'Editor' and typetree_ID = {0})) ";
    Hashtable contentHt = new Hashtable();
    #endregion 当前页面注册的SQL字符串

    Type_TypeTree typeTree = new Type_TypeTree();
    string Url;
    int UpdateContent_ID;

    ContentCls content = new ContentCls();
    
    
    //int Content_ID;
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
    //string flag = "";//Change By Galen 2008.9.13 
    int ispost = 0;


    DataTable _DataTable = new DataTable();

    #region 控制页面显示状态的函数
    /// <summary>
    /// 根据用户角色选择显示Panel
    /// </summary>
    /// <param name="Popedom_EName"></param>
    public void ShowTabButtonPanel(string Popedom_EName)
    {
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
    }

    public void ShowContentPanel() 
    {
        if (typeTree.TypeTreeIssuance == 5)
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

    }

    private void ShowEditData()
    {
        if (Current_Content_ID==-1)return;//this.Request["Content_ID"] == null

        //int Content_ID = int.Parse(this.Request["Content_ID"]);
        ContentCls content = new ContentCls();
        content.Init(Current_Content_ID);

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

        Headnews.Checked = (content.HeadNews != "0");
        PictureNews.Checked = (content.PictureNews == "1");
        //时间
        this.LabelCurPage.Text = "1";
        if (!string.IsNullOrEmpty(content.Description))
        {
            setPageData(Tools.DBToWeb(content.Description));
        }
        Current_TypeTree_ID = int.Parse(content.TypeTree_ID.ToString());
        //LabelTypeID.Text = TypeTree_ID.ToString();

    }

    public void ShowPageHeader()
    {
        if (Current_Flag.Equals("edit"))//this.Request["flag"] != null && this.Request["flag"].Equals("edit")//Change By Galen 2008.9.13 
        {
            /*修改*/
            //this.LabelFlag.Text = "edit";//Change By Galen 2008.9.13 
            if (Current_TypeTree_Type == 0) { ShowEditData(); }//typeTree.TypeTree_Type
            //Content_ID = int.Parse(Request["Content_ID"].ToString());//Change By Galen 2008.9.13 
            this.LabelEditContentID.Text = Current_Content_ID.ToString(); //Content_ID.ToString();
            PageHeader.Value = "修改内容";
            ispost = 1;
            DataList1.DataSource = Tools.DoSqlReader("select * from Content_Log where Content_ID=" + Current_Content_ID);
            DataList1.DataBind();
        }
        else
        {
            this.LabelCurPage.Text = "1";
            this.TextBoxDate.Text = DateTime.Now.ToShortDateString();
            ArrayList contentList = new ArrayList();
            Headnews.Checked = true;
            PageHeader.Value = "添加内容";

        }
    }
    #endregion 控制页面显示状态的函数

    #region 当页的全局变量
    int Current_TypeTree_ID
    {
        get {
            if (string.IsNullOrEmpty(this.LabelTypeID.Text) || this.Request["TypeTree_ID"]!=null)
            {
                this.LabelTypeID.Text = this.Request["TypeTree_ID"].ToString();
            }
            
            int tree_id = int.Parse(this.LabelTypeID.Text);
            if (tree_id != 0)
            {
                //InitCurrentTypeTree();
            }
            return tree_id;
        }
        set { this.LabelTypeID.Text = value.ToString(); }
    }
    int Current_TypeTree_Type
    {
        get { return typeTree.TypeTree_Type; }
    }
    string Current_Flag
    {
        get
        {
            if (string.IsNullOrEmpty(this.LabelFlag.Text) && this.Request["flag"]!=null)
            {
                this.LabelFlag.Text = this.Request["flag"].ToString();
            }
            return this.LabelFlag.Text;
        }
    }

    int Current_Content_ID
    {
        get
        {
            if (Request["Content_ID"] != null)
            {
                return int.Parse(Request["Content_ID"].ToString());
            }
            else
            {
                return -1;
            }
        }
    }
    #endregion 当页的全局变量
    void InitCurrentTypeTree()
    {
        /// 根据TypeTree_ID获取栏目
        typeTree.Init(Current_TypeTree_ID);//Change By Galen 2008.9.1 删除了7行过时变量
        LTypeTree_PictureURL = typeTree.TypeTreePictureURL;
        /// 载入栏目模板，路径
        this.TypeTree_URL.Text = typeTree.TypeTreeURL;
        this.TypeTree_Template.Text = typeTree.TypeTreeTemplate;
        TypeTree_ListTemplate = typeTree.TypeTreeListTemplate;
        TypeTreeListURL = typeTree.TypeTreeListURL;
        List_amount = typeTree.Listamount;
        TypeTree_TypeFields = typeTree.TypeTree_TypeFields;
        TypeTree_ContentFields = typeTree.TypeTree_ContentFields;
    }
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (this.Page.Visible == false)
        {
            OnSessionAtuhFaiedEvent();
            return;
        }
        /// 根据用户角色选择显示
        SysLogon syslogon = new SysLogon();
        syslogon.RolesPopedom(int.Parse(this.GetSession("Roles", null)));//#缺少错误判断和错误处理#
        string Popedom_EName = syslogon.Popedom_EName;
        /// 载入功能按钮
        ShowTabButtonPanel(Popedom_EName);

        /// 获取、保存TypeTree_ID
        /// 
        //TypeTree_ID = int.Parse(this.Request["TypeTree_ID"].ToString());
        //Session["TypeTree_ID"] = TypeTree_ID.ToString();
        //this.LabelTypeID.Text = TypeTree_ID.ToString();//Change By Galen 2008.9.13 改用属性获取

       
        /// 载入内容Panel
        ShowContentPanel();

        if (!this.IsPostBack)
        {
            ShowPageHeader();
        }
        else
        {
            ispost = 0;
            //TypeTree_ID = int.Parse(this.LabelTypeID.Text);//Change By Galen 2008.9.13 
        }

        InitCurrentTypeTree();
        if (typeTree.TypeTree_ContentFields != 0)
        {
            AddFieldsWriteTxt(typeTree.TypeTree_ContentFields);
        }

    }


    #region 动态添加控件
    void CreateTextNumberControl(string controlId, string controlValue, int width, int height)
    {
        TextBox nBox = new TextBox();
        nBox.ID =controlId;
        nBox.CssClass = "inputtext250";

        if (Current_Flag == "edit" && ispost == 1)
        {
            nBox.Text = content.Contents(Current_Content_ID, controlId, Current_TypeTree_ID);
        }

        TableRow tr = new TableRow();
        TableCell tc1 = new TableCell();
        TableCell tc2 = new TableCell();
        if (width != -1)
        {
            tc1.Width = width;
        }
        if (height != -1)
        {
            tc1.Height = height;
        }
        tc1.HorizontalAlign = HorizontalAlign.Right;
        tc1.Text = controlValue+ "：&nbsp;";
        tc2.Controls.Add(nBox);
        tr.Cells.Add(tc1);
        tr.Cells.Add(tc2);
        Table2.Rows.Add(tr);

    }
    void CreateImgFileControl(string controlId, string controlValue,string buttonshtml,int width,int height)
    {

        TextBox nBox = new TextBox();
        nBox.ID = controlId;
        nBox.CssClass = "inputtext250";

        if (Current_Flag == "edit" && ispost == 1)
        {
            nBox.Text = content.Contents(Current_Content_ID, controlId, Current_TypeTree_ID);
        }

        
        TableRow tr = new TableRow();
        TableCell tc1 = new TableCell();
        TableCell tc2 = new TableCell();
        TableCell tc3 = new TableCell();
        tc1.HorizontalAlign = HorizontalAlign.Right;
        tc1.Text = controlValue + "：&nbsp;";
        tc2.Controls.Add(nBox);
        if (width != -1)
        {
            tc2.Width = width;
        }
        if (height != -1)
        {
            tc2.Height = height;
        }
        tc3.Text = buttonshtml;
        tr.Cells.Add(tc1);
        tr.Cells.Add(tc2);
        tr.Cells.Add(tc3);
        Table2.Rows.Add(tr);
        

    }
   
    protected void AddFieldsWriteTxt(int FieldsName_ID)
    {
        string sql = string.Format(SQL_FieldsContentGetList1,FieldsName_ID);
        SqlDataReader myReader = Tools.DoSqlReader(sql);

        string ToolsPut = string.Empty;
        //flag = this.Request["flag"];

        while (myReader.Read())
        {
            switch (myReader.GetString(2))
            {
                case "TEXT":
                    CreateTextNumberControl(myReader.GetString(1), myReader.GetString(3).ToString(),100,-1);              
                    break;
                case "NUMBER":
                    CreateTextNumberControl(myReader.GetString(1), myReader.GetString(3).ToString(), 100, -1);    
                    break;
                case "IMAGE":
                    ToolsPut = "&nbsp;<input type='button' value='上传' onclick='selectImages(" + myReader.GetString(1) + ");' class='button'> ";
                    ToolsPut = ToolsPut + "<input type='button' value='下载' onclick='doDownload(" + myReader.GetString(1) + ");' class='button'> ";
                    ToolsPut = ToolsPut + "<input type='button' value='预览' onclick='doPreview(" + myReader.GetString(1) + ");' class='button'> ";
                    CreateImgFileControl(myReader.GetString(1), myReader.GetString(3), ToolsPut,-1,-1);
                    break;
                case "FILE":
                    ToolsPut = "&nbsp;<input type='button' value='上传' onclick='selectImages(" + myReader.GetString(1) + ");' class='button'> ";
                    ToolsPut = ToolsPut + "<input type='button' value='下载' onclick='doDownload(" + myReader.GetString(1) + ");' class='button'> ";
                    ToolsPut = ToolsPut + "<input type='button' value='预览' onclick='doPreview(" + myReader.GetString(1) + ");' class='button'> ";
                    CreateImgFileControl(myReader.GetString(1), myReader.GetString(3), ToolsPut, - 1, -1);
                    break;
                case "DATETIME":
                    ToolsPut = "&nbsp;<img src='../Admin_Public/Images/Icon_calendar.gif' onclick='selectdate(" + myReader.GetString(1) + ");'> ";
                    CreateImgFileControl(myReader.GetString(1), myReader.GetString(3), ToolsPut, 260, -1);   
                    break;

                case "TREES":
                    ToolsPut = "&nbsp;<img src='../Admin_Public/Images/RepeatedRegion.gif' onclick='selectTree(" + myReader.GetString(1) + "," + myReader.GetString(4).ToString() + ");'> ";
                    CreateImgFileControl(myReader.GetString(1), myReader.GetString(3), ToolsPut, 260, -1);   
                    break;
                case "TEXTAREA":
                    TextBox nTextBoxTEXTAREA = new TextBox();
                    nTextBoxTEXTAREA.ID = myReader.GetString(1);
                    nTextBoxTEXTAREA.Width = 250;
                    nTextBoxTEXTAREA.Height = 50;

                    if (Current_Flag == "edit" && ispost == 1)
                    {
                        nTextBoxTEXTAREA.Text = content.Contents(Current_Content_ID, myReader.GetString(1), Current_TypeTree_ID);
                    }
                    nTextBoxTEXTAREA.TextMode = TextBoxMode.MultiLine;
                    TableRow trTEXTAREA = new TableRow();
                    TableCell tc1TEXTAREA = new TableCell();
                    TableCell tc2TEXTAREA = new TableCell();
                    tc1TEXTAREA.Width = 100;
                    tc1TEXTAREA.HorizontalAlign = HorizontalAlign.Right;
                    tc1TEXTAREA.Text = myReader.GetString(3).ToString() + "：&nbsp;<br/><input type='button' value='HTML' onclick='editHTML(" + myReader.GetString(1) + ");'>&nbsp;";
                    tc2TEXTAREA.Controls.Add(nTextBoxTEXTAREA);
                    trTEXTAREA.Cells.Add(tc1TEXTAREA);
                    trTEXTAREA.Cells.Add(tc2TEXTAREA);
                    Table2.Rows.Add(trTEXTAREA);
                    break;

                case "SELECT":
                    DropDownList ListSELECT = new DropDownList();
                    ListSELECT.ID = myReader.GetString(1);
                    string opss= myReader.GetString(4);
                    opss = opss.Replace(Convert.ToChar(10), ',');
                    string[] ops = opss.Split(',');

                    for (int j = 0; j < ops.Length; j++)
                    {
                        ListSELECT.Items.Add(ops[j].Trim());
                    }

                    if (Current_Flag == "edit" && ispost == 1)
                    {
                        if (!string.IsNullOrEmpty(content.Contents(Current_Content_ID, myReader.GetString(1), Current_TypeTree_ID).Trim()))
                        {
                            if (myReader.GetString(4).IndexOf(content.Contents(Current_Content_ID, myReader.GetString(1), Current_TypeTree_ID)) != -1) { ListSELECT.SelectedValue = content.Contents(Current_Content_ID, myReader.GetString(1), Current_TypeTree_ID); };
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
    #endregion 动态添加控件

    private void setPageData(string xmlStr) //拆分xml
    {
        this.EditorControl1.Value = Tools.DBToWeb(Server.HtmlEncode(xmlStr));
    }

    #region 发布
    private void SaveContent(string Status)
    {
        if (this.Page.Visible == false)
        {
            OnSessionAtuhFaiedEvent();
            return;
        }
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
            content.HeadNews = this.Headnews.Checked ? "1" : "0";
            content.PictureNews = this.PictureNews.Checked ? "1" : "0";
           
            if (this.CheckBoxWebtoThisPic.Checked == true)
            { contentList = Tools.WebtoLocalPic(contentList, LTypeTree_PictureURL); }

            content.Name = Tools.WebToDB(this.TextBoxTitle.Text);
            content.Description = Tools.WebToDB(contentList);
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
            string sql = string.Format(SQL_FieldsContentGetList2, typeTree.TypeTree_ContentFields);
            reader = Tools.DoSqlReader(sql);

            while (reader.Read())
            {
                switch (reader.GetString(2))
                {
                    case "TEXT":
                        if (((TextBox)Page.FindControl(reader.GetString(1))) != null)
                        {
                            TxtValue = ((TextBox)Page.FindControl(reader.GetString(1))).Text;
                        }
                        break;

                    case "NUMBER":
                        if (((TextBox)Page.FindControl(reader.GetString(1))) != null)
                        {
                            TxtValue = ((TextBox)Page.FindControl(reader.GetString(1))).Text;
                        }
                        break;

                    case "IMAGE":
                        if (((TextBox)Page.FindControl(reader.GetString(1))) != null)
                        {
                            TxtValue = ((TextBox)Page.FindControl(reader.GetString(1))).Text;
                        }
                        break;
                    case "FILE":
                        if (((TextBox)Page.FindControl(reader.GetString(1))) != null)
                        {
                            TxtValue = ((TextBox)Page.FindControl(reader.GetString(1))).Text;
                        }
                        break;
                    case "DATETIME":
                        if (((TextBox)Page.FindControl(reader.GetString(1))) != null)
                        {
                            TxtValue = ((TextBox)Page.FindControl(reader.GetString(1))).Text;
                        }
                        break;
                    case "TREES":
                        if (((TextBox)Page.FindControl(reader.GetString(1))) != null)
                        {
                            TxtValue = ((TextBox)Page.FindControl(reader.GetString(1))).Text;
                        }
                        break;
                    case "TEXTAREA":
                        if (((TextBox)Page.FindControl(reader.GetString(1))) != null)
                        {
                            TxtValue = ((TextBox)Page.FindControl(reader.GetString(1))).Text;
                        }
                        break;
                    case "SELECT":
                        if (((DropDownList)Page.FindControl(reader.GetString(1))) != null)
                        {
                            TxtValue = ((DropDownList)Page.FindControl(reader.GetString(1))).SelectedValue;

                        }
                        break;
                    case "LABEL":

                        break;
                    default:
                        //ToolsPut = "数据错误！";
                        break;
                }
                sql1 = sql1 + "[" + reader.GetString(1) + "],";
                sql2 = sql2 + "'" + TxtValue + "',";
                sql3 = sql3 + reader.GetString(1) + " = '" + TxtValue + "',";

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
                sqlcc =string.Format(SQL_ContentUserUpdate,_Content_FieldsName.FieldsBase_Name ,sql3,Content_ID);
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
            content.Author = this.GetSession("Master_UserName", null);
            content.Clicks = 1;
            content.OrderNum =content.QueryMaxContentID();
            string TypeTree_URL = this.TypeTree_URL.Text;
            int Content_ID = content.QueryMaxContentID() + 1;
            Url = TypeTree_URL.Replace("{@UID}", Content_ID.ToString()); //获得URL
            content.Url = Url;

            if (typeTree.TypeTree_Type == 2)
            {
                Content_FieldsName _Content_FieldsName = new Content_FieldsName();
                _Content_FieldsName.Init(typeTree.TypeTree_ContentFields);
                sql1 = sql1 + "Content_ID,TypeTree_ID,Author,Clicks,OrderNum,SubmitDate,Url,Status";
                //Content_ID++;
                sql2 = sql2 + Content_ID + "," + int.Parse(this.LabelTypeID.Text) + ",'" +this.GetSession("Master_UserName",null)  + "','1'," + Content_ID + ",getdate(),'" + Url + "','" + Status + "'";
                sqlcc = string.Format(SQL_ContentUserAdd, _Content_FieldsName.FieldsBase_Name, sql1, sql2);

                if (Tools.DoSql(sqlcc))
                {
                    Tools.UpdateMaxID("Content_ID");
                    content.ContentId = Content_ID;
                }
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

                sqlcc = string.Format(SQL_ContentUserAdd, _Content_FieldsName.FieldsBase_Name, sql1, sql2);
                Tools.DoSql(string.Format(SQL_ContentUserDelete, _Content_FieldsName.FieldsBase_Name,UpdateContent_ID));
                Tools.DoSql(sqlcc);
            }
        }

        //扩展字段入库 over

        this.LabelMsg.Text = str;

        //Log
        Log _Log = new Log();
        string Log_Action = string.Empty;
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
            string sqls = string.Format(SQL_MasterGetList, int.Parse(this.LabelTypeID.Text));

            readerMail =Tools.DoSqlReader(sqls);
            int iMail = 0;
            while (readerMail.Read())
            {
                if (!string.IsNullOrEmpty( readerMail.GetString(0).ToString()))
                {
                    iMail = iMail++;
                    if (iMail == 1)
                    {
                        email.AddRecipient(readerMail.GetString(0).ToString(), readerMail.GetString(1), null);
                    }
                    else
                    {
                        email.AddRecipientCC(readerMail.GetString(0).ToString(), readerMail.GetString(1), null);
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

    protected void Toolsbar1_ButtonClick(object sender, System.EventArgs e)
    {
        SaveContent("1");
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
    #endregion 发布

    private string UrlString(string FilesUrl)
    {
        FilesUrl = FilesUrl.Replace("/", "//");
        FilesUrl = Server.MapPath(FilesUrl);
        return FilesUrl;
    }

    
}
