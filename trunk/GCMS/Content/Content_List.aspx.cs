//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-8-25
//
// 功能描述: 发布内容列表
//
// 已修改问题:
// 未修改问题:

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
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using GCMSClassLib.Public_Cls;
using GCMSClassLib.Content;
using System.IO;
using System.Data.SqlClient;
using GCMS.PageCommonClassLib;

public partial class Content_Content_List : GCMS.PageCommonClassLib.PageBase
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
        this.Page.Visible = false;
        return;
    }

    #region 当页的全局变量
    int m_typetree_id;
    int Current_TypeTree_ID
    {
        get
        {
            m_typetree_id = int.Parse(this.GetQueryString("TypeTree_ID", null));
            return m_typetree_id;
        }
        set
        {
            m_typetree_id = value;
        }
    }

    Type_TypeTree Current_TypeTree;
    void InitCurrentTypeTree()
    {
        Current_TypeTree = new Type_TypeTree();
        Current_TypeTree.Init(m_typetree_id);
    }
    #endregion 当页的全局变量
    //private string sTypeTree_ID;
    //private int TypeTreeIssuanceID;
    string sTextSearch = "";
    string sSQL = "";
    Content_FieldsName _Content_FieldsName = new Content_FieldsName();
    Content_FieldsContent _Content_FieldsContent = new Content_FieldsContent();
    ContentCls _ContentCls = new ContentCls();
    //int TypeTree_Type;

    const int PageSize = 60;//定义每页显示记录
    int PageCount, RecCount, CurrentPage, Pages, JumpPage;
    public string countSql;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.Visible == false)//如果未通过权限验证，直接返回
        {
            OnSessionAtuhFaiedEvent();
            return;
        }
        //sTypeTree_ID = Request.QueryString["TypeTree_ID"].ToString(); //必须知道在那个节点下
        if (Current_TypeTree_ID==0)//sTypeTree_ID == "0"
        {
            this.Response.Redirect("Main_Content.aspx?RightID=0");
            return;
        }
        InitCurrentTypeTree();
        //Type_TypeTree _Type_TypeTree = new Type_TypeTree();
        //_Type_TypeTree.Init(int.Parse(sTypeTree_ID));
         //TypeTree_Type =this.Current_TypeTree.TypeTree_Type;

        if (!this.IsPostBack)
        {
            DateGridList.CurrentPageIndex = 0;
            //TypeTreeIssuanceID =Current_TypeTree.TypeTreeIssuance.ToString();
            sTypeTree_Show.Value = Current_TypeTree.TypeTree_Show;
            sTypeTree_ContentFields.Value
                = Current_TypeTree.TypeTree_ContentFields == 0 ?
                Current_TypeTree.TypeTree_TypeFields.ToString() :
                Current_TypeTree.TypeTree_ContentFields.ToString();//#待测试的修改#

            string TypeTreeIssuanceName = Current_TypeTree.strTypeTreeIssuance(Current_TypeTree.TypeTreeIssuance);
            TypeTree_ID.Value = Current_TypeTree_ID.ToString();
            this.PageHeader.Value =string.Format( "当前目录 - {0}    状态 - {1}" , Current_TypeTree.TypeTreeCName,TypeTreeIssuanceName);

            Pages_Load();
            Type_List(Current_TypeTree_ID, Current_TypeTree.TypeTree_Type); //调用数据绑定函数Type_List()进行数据绑定运算

        }
    }
    public void Pages_Load()
    {

        RecCount = Calc();//通过Calc()函数获取总记录数
        PageCount = RecCount / PageSize + OverPage();//计算总页数（加上OverPage()函数防止有余数造成显示数据不完整）
        ViewState["PageCounts"] = RecCount / PageSize -

        ModPage();//保存总页参数到ViewState（减去ModPage()函数防止SQL语句执行时溢出查询范围，可以用存储过程分页算法来理解这句）
        ViewState["PageIndex"] = 0;//保存一个为0的页面索引值到ViewState
        ViewState["JumpPages"] = PageCount;//保存PageCount到ViewState，跳页时判断用户输入数是否超出页码范围
        //显示LPageCount、LRecordCount的状态
        LPageCount.Text = PageCount.ToString();
        LRecordCount.Text = RecCount.ToString();
        //			//判断跳页文本框失效
        if (RecCount <= PageSize)
        { gotoPage.Enabled = false; }
        else
        { gotoPage.Enabled = true; }


    }

    public void Type_List(int TypeTree_ID, int TypeTree_Type)
    {

        //			LabelJava.Text = "<script language=\"JavaScript\" src=\"../admin_public/js/Content_ContentList.js\"></script>";
        sMenuContent.Text = "";
        sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"Lock\" doFunction=\"doLock();\">锁定文件</div>";
        sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"unLock\" doFunction=\"doUnLock();\">解锁文件</div>";
        sMenuContent.Text = sMenuContent.Text + "<hr>";
        sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"newfile\" doFunction=\"newContent();\">新文件</div>";
        //			sMenuContent.Text = sMenuContent.Text + "<div class=menuItemDisable id=newfile2 doFunction=\"newContent();\">新文件</DIV>";
        sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"openfile\" doFunction=\"doOpenFile();\">打开...</div>";
        sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"relative\" doFunction=\"doRelative();\">相关文章</div>";
        sMenuContent.Text = sMenuContent.Text + "<div class=menuItemDisable id=SonContent doFunction=\"doSonContent();\">关联子文章</DIV>";
        sMenuContent.Text = sMenuContent.Text + "<hr>";
        sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"approval\" doFunction=\"doApproval();\">签发</div>";
        //			sMenuContent.Text = sMenuContent.Text + "<div class=menuItemDisable id=approval2 doFunction=\"doApproval();\">签发</DIV>";
        sMenuContent.Text = sMenuContent.Text + "<hr>";
        sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"recommend\" doFunction=\"doRecommend();\">映射...</div>";
        //			sMenuContent.Text = sMenuContent.Text + "<div class=menuItemDisable id=recommend2 doFunction=\"doRecommend();\">推荐</DIV>";
        sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"copyFile\" doFunction=\"doCopyFile();\">拷贝</div>";
        sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"pasteFile\" doFunction=\"doPasteFile();\">粘贴</div>";
        sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"preview\" doFunction=\"doPreview();\">预览</div>";
        sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"delfile\" doFunction=\"doDelFile();\">删除</div>";
        sMenuContent.Text = sMenuContent.Text + "<hr>";
        sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"AtTop\" doFunction=\"doAtTop();\">置顶</DIV>";
        sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"UnAtTop\" doFunction=\"doUnAtTop();\">取消置顶</DIV>";
        sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"moveup\" doFunction=\"doMoveUp();\">上移一行</div>";
        sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"movedown\" doFunction=\"doMoveDown();\">下移一行</div>";

        sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"lookAdd\" doFunction=\"dolookAdd();\">查看本文评论地址...</div>";
        //sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"lookText\" doFunction=\"dolookText();\">查看本文访问记录...</div>";
        //			sMenuContent.Text = sMenuContent.Text + "<div class=menuItemDisable id=moveup2 doFunction=\"doMoveUp();\">上移一行</DIV>";
        //			sMenuContent.Text = sMenuContent.Text + "<div class=menuItemDisable id=movedown2 doFunction=\"doMoveDown();\">下移一行</DIV>";
        sMenuContent.Text = sMenuContent.Text + "<hr id=\"menuNouse\">";
        sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"reFresh\" doFunction=\"doReFresh();\">刷新</div>";
        //			sMenuContent.Text = sMenuContent.Text + "<hr>";
        //			sMenuContent.Text = sMenuContent.Text + "<div class=menuItem id=version doFunction=\"doVersion();\">版本</DIV>";
        //			sMenuContent.Text = sMenuContent.Text + "<div class=menuItemDisable id=version2 doFunction=\"doVersion();\">版本</DIV>";
        //			sMenuContent.Text = sMenuContent.Text + "<div class=menuItem id=chistory doFunction=\"doHistory();\">稿件处理历史</DIV>";
        //			sMenuContent.Text = sMenuContent.Text + "<div class=menuItemDisable id=chistory2 doFunction=\"doHistory();\">稿件处理历史</DIV>";


        //相当于vb中的chr(10)

        DateGridList.Attributes.Add("altRowColor", "oldlace");
        DateGridList.Attributes.Add("align", "center");

        string Sfrom = "";
        string Swhere = "";


        if (TypeTree_Type == 2)
        {
            _Content_FieldsName.Init(int.Parse(sTypeTree_ContentFields.Value));
            if (_Content_FieldsName.FieldsBase_Name != "" && _Content_FieldsName.FieldsBase_Name != null)
            {
                Sfrom = " left outer join ContentUser_" + _Content_FieldsName.FieldsBase_Name;
                Swhere = " on ContentUser_" + _Content_FieldsName.FieldsBase_Name + ".Content_ID = Content_Content.Content_ID ";
            }


            char sSplit = ',';
            string[] ops;

            SelectDropDownList.Items.Clear();
            SelectDropDownList.Items.Add(new ListItem("ID", "Content_ID"));

            ops = sTypeTree_Show.Value.Split(sSplit);
            for (int j = 0; j < ops.Length; j++)
            {
                BoundColumn bc1 = new BoundColumn();
                bc1.DataField = ops[j].ToString();
                bc1.HeaderText = _Content_FieldsContent.InitName(int.Parse(sTypeTree_ContentFields.Value), ops[j].ToString());
                bc1.ItemStyle.Width = Unit.Pixel(250);
                DateGridList.Columns.Add(bc1);

                SelectDropDownList.Items.Add(new ListItem(_Content_FieldsContent.InitName(int.Parse(sTypeTree_ContentFields.Value), ops[j].ToString()), ops[j].ToString()));
            }

            string WhereSql = " Status in (" + Tools.txtStatus + ") and TypeTree_ID = '" + TypeTree_ID + "'" + sTextSearch;
            //					sSQL = "select Top " + top_count + " * , ISNULL(AtTop, 0) AS AtTop1  from ContentUser_"+ _Content_FieldsName.FieldsBase_Name +" where Status in ("+Tools.txtStatus+") and TypeTree_ID = '"+TypeTree_ID+"'"+ sTextSearch +" order by Content_ID desc";
            //					sSQL = "select Top " + DateGridList.PageSize + " * , ISNULL(AtTop, 0) AS AtTop1  from (Select Top "+top_count+" * From  ContentUser_"+ _Content_FieldsName.FieldsBase_Name +" order by Content_ID DESC) As t1  where Content_ID Not In(Select top "+Remove_count+" Content_ID From ContentUser_"+ _Content_FieldsName.FieldsBase_Name +" order by Content_ID DESC) and Status in ("+Tools.txtStatus+") and TypeTree_ID = '"+TypeTree_ID+"'"+ sTextSearch +" order by Content_ID desc";
            sSQL = "Select Top " + PageSize + " * , ISNULL(AtTop, 0) AS AtTop1 from  ContentUser_" + _Content_FieldsName.FieldsBase_Name + " where Content_ID not in(select top " + PageSize * CurrentPage + " Content_ID from ContentUser_" + _Content_FieldsName.FieldsBase_Name + " where " + WhereSql + " order by OrderNum desc) and " + WhereSql + " order by OrderNum desc";



        }

        if (TypeTree_Type == 0)
        {
            BoundColumn bc1 = new BoundColumn();
            bc1.HeaderText = "名称";
            bc1.ItemStyle.CssClass = "title";
            DateGridList.Columns.Add(bc1);

            BoundColumn bc2 = new BoundColumn();
            bc2.HeaderText = "作者";
            bc2.ItemStyle.CssClass = "author";
            DateGridList.Columns.Add(bc2);

            BoundColumn bc3 = new BoundColumn();
            bc3.HeaderText = "发布时间";
            bc3.ItemStyle.CssClass = "submitdate";
            DateGridList.Columns.Add(bc3);

            BoundColumn bc4 = new BoundColumn();
            bc4.HeaderText = "状态";
            bc4.ItemStyle.CssClass = "status";
            DateGridList.Columns.Add(bc4);

            BoundColumn bc5 = new BoundColumn();
            bc5.HeaderText = "头条";
            bc5.ItemStyle.CssClass = "putintopx";
            DateGridList.Columns.Add(bc5);

            BoundColumn bc6 = new BoundColumn();
            bc6.HeaderText = "图文";
            bc6.ItemStyle.CssClass = "isimagenews";
            DateGridList.Columns.Add(bc6);

            SelectDropDownList.Items.Clear();
            SelectDropDownList.Items.Add(new ListItem("名称", "name"));
            SelectDropDownList.Items.Add(new ListItem("ID", "Content_ID"));
            SelectDropDownList.Items.Add(new ListItem("作者", "Author"));
            // or "+FieldsBase+".Author like '%"+sTextSearch+"%' or "+FieldsBase+".Content_Id like '%"+sTextSearch+"%


            sSQL = "select Top " + PageSize + " * , ISNULL(Content_Content.AtTop, 0) AS AtTop1 from Content_Content " +
                Sfrom + " " + Swhere + " where  Content_ID not in(select top " + PageSize * CurrentPage + " Content_ID from Content_Content where Content_Content.Status in (" + Tools.txtStatus + ") and Content_Content.TypeTree_ID = '" + TypeTree_ID + "' order by Content_ID desc) and Content_Content.Status in (" + Tools.txtStatus + ") and Content_Content.TypeTree_ID = '" + TypeTree_ID + "'" +
                sTextSearch + " order by Content_Content.AtTop desc ,Content_Content.OrderNum desc";
        }



        if (TypeTree_Type == 1)
        {
            sSQL = "select Top " + DateGridList.PageSize + " Content_Content.*,ISNULL(AtTop, 0) AS AtTop from Content_Content,Content_Commend where Content_Commend.TypeTree_ID = '" + TypeTree_ID + "'" + sTextSearch + " and Content_Content.Status in (1,2,3,4,5) and Content_Commend.Content_ID = Content_Content.Content_ID order by Content_Content.OrderNum desc";
            sMenuContent.Text = "";
            sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"preview\" doFunction=\"doPreview();\">预览</div>";
            sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"UnRel\" doFunction=\"doUnRel();\">撤销映射</div>";
            sMenuContent.Text = sMenuContent.Text + "<hr id=\"menuNouse\">";
            sMenuContent.Text = sMenuContent.Text + "<div class=\"menuItem\" id=\"reFresh\" doFunction=\"doReFresh();\">刷新</div>";
        }



        DateGridList.CurrentPageIndex = 0;
        txtSql.Value = sSQL;
        DateGridList.DataSource =Tools.DoSqlReader(sSQL);
        DateGridList.DataBind();

        //显示Label控件LCurrentPaget和文本框控件gotoPage状态
        LCurrentPage.Text = (CurrentPage + 1).ToString();
        gotoPage.Text = (CurrentPage + 1).ToString();
    }
    protected void Toolsbar1_ButtonClick(object sender, System.EventArgs e)
    {


        DataTable dt =Tools.DoSqlTable(txtSql.Value);
        StringWriter sw = new StringWriter();

        char sSplit = ',';
        string[] ops;

        sw.WriteLine("自动编号,名称,作者," + sTypeTree_Show.Value);
        foreach (DataRow dr in dt.Rows)
        {
            //sw.WriteLine(dr["Content_ID"]+"\t"+dr["Name"]+"\t"+dr["Author"]); 
            string Contents = dr["Content_ID"] + "," + dr["Name"] + "," + dr["Author"];
            ops = sTypeTree_Show.Value.Split(sSplit);
            for (int j = 0; j < ops.Length; j++)
            {
                Contents = Contents + "," + dr[ops[j].ToString()];
            }


            sw.WriteLine(Contents);


        }
        sw.Close();
        Response.AddHeader("Content-Disposition", "attachment; filename=Content.csv");
        Response.ContentType = "application/ms-excel";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.Write(sw);
        Response.End();
    }

    public void ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {

        string StatusImg;
        string lockText;

        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            int Content_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Content_ID"));
            string lockedby = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "lockedby"));

            //				int AtTop = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"AtTop"));
            //				e.Item.Attributes.Add("onmouseover","currentcolor=this.style.backgroundColor;this.style.backgroundColor='cccccc'");
            //				e.Item.Attributes.Add("onmouseout","this.style.backgroundColor=currentcolor");
            e.Item.Attributes.Add("onmousedown", "selectContent('" + Content_ID + "');");
            e.Item.Attributes.Add("ondblclick", "openContent('" + Content_ID + "');");
            e.Item.Attributes.Add("ondragenter", "dragEnter();");
            e.Item.Attributes.Add("ondragleave", "dragLeave();");
            e.Item.Attributes.Add("ondragover", "dragOver();");
            //				e.Item.Attributes.Add("onmouseover", "elementOnMouseOver('"+Content_ID+"');");
            //				e.Item.Attributes.Add("onmouseout", "elementOnMouseOut('"+Content_ID+"');");
            e.Item.Attributes.Add("ondrop", "FinishDrag(" + Content_ID + ");");
            e.Item.ID = "item" + Content_ID;


            if (lockedby == "")
            {
                lockText = "锁定状态：当前没有锁定";
                StatusImg = "src='../Admin_Public/Images/Icon_File_New.gif' alt='" + lockText + "' lockedby='" + lockedby + "'";
            }
            else if (lockedby == Session["Master_UserName"].ToString())
            {
                lockText = "锁定状态：文章由您锁定，您可以执行操作";
                StatusImg = "src='../Admin_Public/Images/ic_lockuser.gif' alt='" + lockText + "' lockedby='" + lockedby + "'";
            }
            else
            {
                lockText = "锁定状态：文章由 " + lockedby + "锁定，您不能执行操作";
                StatusImg = "src='../Admin_Public/Images/ic_lock.gif' alt='" + lockText + "' lockedby='" + lockedby + "'";
            }
            if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "AtTop1")) == 1)
            {
                lockText = "顶";
                StatusImg = "src='../Admin_Public/Images/sort_uparrow.gif' alt='" + lockText + "' lockedby='" + lockedby + "'";
            }

            string IDtxt = "<IMG id='status" + Content_ID + "' ondragstart='InitDrag()' onclick='return(false)'  " + StatusImg + "> " + Content_ID;
            //IDtxt= IDtxt + "<img id='status"+Content_ID+"' src='"+StatusImg+"' width=16 height=16 alt='"+lockText+"' lockedby='"+lockedby+"'>"+Content_ID;
            e.Item.Cells[0].Text = IDtxt;

            if (!Current_TypeTree.IsFullExtenFields)//TypeTree_Type != 2
            {

                e.Item.Cells[1].Text = "<nobr><span class='title' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "name")) + ">" + Tools.DBToWeb(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "name"))) + "</span></nobr>";
                e.Item.Cells[2].Text = "<nobr><span class='Author' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Author")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Author")) + "</span></nobr>";
                e.Item.Cells[3].Text = "<nobr><span class='submitdate' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "submitdate")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "submitdate")) + "</span></nobr>";

                switch (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "status")))
                {
                    case 1:
                        e.Item.Cells[4].Text = "<font color=red>草 稿</font>";
                        break;
                    case 2:
                        e.Item.Cells[4].Text = "<font color=black>待审批</font>";
                        break;
                    case 3:
                        e.Item.Cells[4].Text = "<font color=green>待发布</font>";
                        break;
                    case 4:
                        e.Item.Cells[4].Text = "<font color=gray>已发布</font>";
                        break;
                    case 5:
                        e.Item.Cells[4].Text = "<font color=blue>已归档</font>";
                        break;
                }

                if (Convert.ToChar(DataBinder.Eval(e.Item.DataItem, "Head_news")).ToString() == "1")
                {
                    e.Item.Cells[5].Text = "是";
                }
                else
                {
                    e.Item.Cells[5].Text = "否";
                }

                if (Convert.ToChar(DataBinder.Eval(e.Item.DataItem, "Picture_news")).ToString() == "1")
                {
                    e.Item.Cells[6].Text = "是";
                }
                else
                {
                    e.Item.Cells[6].Text = "否";
                }

            }

        }
    }

    //取得记录总数;
    private int SetVirtualItemCount()
    {
        //			SqlConnection VerConn=new SqlConnection(ConnectionString);
        //			string sql_com="Select Count(*) From News";
        //			SqlCommand VerCmd=new SqlCommand(sql_com,VerConn);
        //			VerConn.Open();
        //			int nItemsCount=(int)VerCmd.ExecuteScalar();
        //			VerConn.Close();
        int nItemsCount = 1000;
        return nItemsCount;
    }


    private int GetPageCount(int RecordCount)
    {
        if (RecordCount % DateGridList.PageSize == 0)
        {
            return Convert.ToInt32(RecordCount / DateGridList.PageSize);
        }
        else
        {
            return Convert.ToInt32(RecordCount / DateGridList.PageSize) + 1;
        }
    }


    protected void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        string FieldsBase = "Content_Content";
        string Fields = "name";
        _Content_FieldsName.Init(int.Parse(sTypeTree_ContentFields.Value));
        if (Current_TypeTree.IsFullExtenFields)//TypeTree_Type == 2
        {
            FieldsBase = "ContentUser_" + _Content_FieldsName.FieldsBase_Name;
            Fields = SelectDropDownList.SelectedValue;
        }

        sTextSearch = TextSearch.Text;
        if (!String.IsNullOrEmpty(sTextSearch ))
        {
            sTextSearch = " and ( " + FieldsBase + "." + Fields + " like '%" + sTextSearch + "%') ";
        }
        Type_List(Current_TypeTree_ID, Current_TypeTree.TypeTree_Type);
    }



    //对四个按钮（首页、上一页、下一页、尾页）返回的CommandName值进行操作
    protected void Page_OnClick(object sender, CommandEventArgs e)
    {
        CurrentPage = (int)ViewState["PageIndex"];//从ViewState中读取页码值保存到CurrentPage变量中进行参数运算
        Pages = (int)ViewState["PageCounts"];//从ViewState中读取总页参数运算

        string cmd = e.CommandName;
        switch (cmd)//筛选CommandName
        {
            case "next":
                CurrentPage++;
                break;
            case "prev":
                CurrentPage--;
                break;
            case "last":
                CurrentPage = Pages;
                break;
            default:
                CurrentPage = 0;
                break;
        }
        ViewState["PageIndex"] = CurrentPage;//将运算后的CurrentPage变量再次保存至ViewState
        Type_List(Current_TypeTree_ID, Current_TypeTree.TypeTree_Type);//调用数据绑定函数TDataBind()
    }

    protected void gotoPage_TextChanged(object sender, System.EventArgs e)
    {
        try
        {
            JumpPage = (int)ViewState["JumpPages"];//从ViewState中读取可用页数值保存到JumpPage变量中
            //判断用户输入值是否超过可用页数范围值
            if (Int32.Parse(gotoPage.Text) > JumpPage || Int32.Parse(gotoPage.Text) <= 0)


                Response.Write("<script>alert('页码范围越界！')</script>");
            else
            {
                int InputPage = Int32.Parse(gotoPage.Text.ToString()) - 1;//转换用户输入值保存在int型InputPage变量中
                ViewState["PageIndex"] = InputPage;//写入InputPage值到ViewState["PageIndex"]中
                Type_List(Current_TypeTree_ID, Current_TypeTree.TypeTree_Type);//调用数据绑定函数TDataBind()再次进行数据绑定运算
            }
        }
        //捕获由用户输入不正确数据类型时造成的异常
        catch (Exception exp)
        {
            Response.Write("<script>alert('" + exp.Message + "')</script>");
        }
    }



    //计算余页
    public int OverPage()
    {
        int pages = 0;
        if (RecCount % PageSize != 0)
            pages = 1;
        else
            pages = 0;
        return pages;
    }

    //计算余页，防止SQL语句执行时溢出查询范围
    public int ModPage()
    {
        int pages = 0;
        if (RecCount % PageSize == 0 && RecCount != 0)
            pages = 1;
        else
            pages = 0;
        return pages;
    }


    /*
    *计算总记录的静态函数
    *在这里使用静态函数的理由是：如果引用的是静态数据或静态函数，连接器会优化生成代码，去掉动态重定位项（对海量数据表分页效果更明显）。
    */
    public int Calc()
    {
        if (Current_TypeTree.IsCommonPublish)//TypeTree_Type == 0
        {
            countSql = "Select count(*) as co from Content_Content where Status in (" + Tools.txtStatus + ") and TypeTree_ID = '" + Current_TypeTree_ID.ToString() + "'" + sTextSearch;
        }

        if (Current_TypeTree.IsFullExtenFields)//TypeTree_Type == 2
        {
            _Content_FieldsName.Init(int.Parse(sTypeTree_ContentFields.Value));

            countSql = "Select count(*) as co from  ContentUser_" + _Content_FieldsName.FieldsBase_Name + " where Status in (" + Tools.txtStatus + ") and TypeTree_ID = '" + Current_TypeTree_ID.ToString() + "'" + sTextSearch;
            
           
        }
        //Change By Galen Mu 2008.7.29
        if (Current_TypeTree.TypeTree_Type == 1)
        {
            countSql = "Select count(*) as co from Content_Content,Content_Commend where Content_Content.Status in (" + Tools.txtStatus + ") and Content_Commend.Content_ID = Content_Content.Content_ID and Content_Commend.TypeTree_ID = '" + Current_TypeTree_ID.ToString() + "'" + sTextSearch;
        }
        int RecordCount = 0;
        SqlDataReader myReader = Tools.DoSqlReader(countSql);

        if (myReader.Read())
            RecordCount = Int32.Parse(myReader["co"].ToString());
        myReader.Close();

        return RecordCount;
    }

    protected void ListStatus_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        //txtStatus = ListStatus.SelectedValue ;
        Tools.txtStatus = ListStatus.SelectedValue;
        Pages_Load();
        Type_List(Current_TypeTree_ID,Current_TypeTree.TypeTree_Type);
    }
    protected void PageHeader_Load(object sender, EventArgs e)
    {

    }
}
