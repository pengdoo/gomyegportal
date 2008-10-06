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
//   2008-9-23  优化代码，删除两个用于保存往返数据的页面控件
//----------------------------------系统引用-------------------------------------
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
//----------------------------------项目引用-------------------------------------
using GCMSClassLib.Public_Cls;
using GCMSClassLib.Content;
using GCMS.PageCommonClassLib;
using System.Text;

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
        get{
            m_typetree_id = int.Parse(this.GetQueryString("TypeTree_ID", null));
            return m_typetree_id;
        }
        set { m_typetree_id = value; }
    }

    Type_TypeTree Current_TypeTree;
    void InitCurrentTypeTree()
    {
        Current_TypeTree = new Type_TypeTree();
        Current_TypeTree.Init(m_typetree_id);
    }
    #endregion 当页的全局变量
    string sTextSearch = "";
    string sSQL = "";
    Content_FieldsName _Content_FieldsName = new Content_FieldsName();
    Content_FieldsContent _Content_FieldsContent = new Content_FieldsContent();
    ContentCls _ContentCls = new ContentCls();

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
        if (Current_TypeTree_ID==0)//sTypeTree_ID == "0"
        {
            this.Response.Redirect("Main_Content.aspx?RightID=0");
            return;
        }
        InitCurrentTypeTree();

        if (!this.IsPostBack)
        {
            DateGridList.CurrentPageIndex = 0;
            sTypeTree_Show.Value = Current_TypeTree.TypeTree_Show;
            
            string TypeTreeIssuanceName = Current_TypeTree.strTypeTreeIssuance(Current_TypeTree.TypeTreeIssuance);
            TypeTree_ID.Value = Current_TypeTree_ID.ToString();
            this.PageHeader.Value =string.Format( "当前目录 - {0}    状态 - {1}" , Current_TypeTree.TypeTreeCName,TypeTreeIssuanceName);
            loadPagingIndex();
            loadDataList(Current_TypeTree); 

        }
    }
    public void loadPagingIndex()
    {

        RecCount = GetCalc();//通过Calc()函数获取总记录数
        PageCount = RecCount / PageSize + OverPage();//计算总页数（加上OverPage()函数防止有余数造成显示数据不完整）
        ViewState["PageCounts"] = RecCount / PageSize -

        GetModPage();//保存总页参数到ViewState（减去ModPage()函数防止SQL语句执行时溢出查询范围，可以用存储过程分页算法来理解这句）
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

    public void loadDataList(Type_TypeTree cTypeTree)
    {

        DateGridList.Attributes.Add("altRowColor", "oldlace");
        DateGridList.Attributes.Add("align", "center");
        string Sfrom = "";
        string Swhere = "";
        if (cTypeTree.IsFullExtenFields)
        {
            //载入右键菜单脚本
            sMenuContent.Text = GSystem.LoadTemplate("~//SysScriptTep//Content_Content_List.MenuC.txt", new Dictionary<string, string>());
            if (!cTypeTree.HasExtentFields)  {
                this.Response.Redirect("Main_Content.aspx?RightID=0");
                return;
            }
            else{
                Sfrom = " left outer join " + cTypeTree.ExtentFieldTableName;
                Swhere = " on " + cTypeTree.ExtentFieldTableName + ".Content_ID = Content_Content.Content_ID ";
            }

            SelectDropDownList.Items.Clear();
            SelectDropDownList.Items.Add(new ListItem("ID", "Content_ID"));

            string[] ops = (sTypeTree_Show.Value + ",status").Split(',');
            for (int j = 0; j < ops.Length; j++)
            {
                BoundColumn bc = new BoundColumn();
                bc.DataField = ops[j];
                string fname = _Content_FieldsContent.InitName(cTypeTree.ExtentFieldsId, ops[j].ToString());
                bc.HeaderText = fname;
                
                bc.ItemStyle.Width = Unit.Pixel(250);
                DateGridList.Columns.Add(bc);

                SelectDropDownList.Items.Add(new ListItem(fname, ops[j]));
            }
            string WhereSql = " Status in (" + Tools.txtStatus + ") and TypeTree_ID = '" + Current_TypeTree_ID.ToString() + "'" + sTextSearch;
            //					sSQL = "select Top " + top_count + " * , ISNULL(AtTop, 0) AS AtTop1  from ContentUser_"+ _Content_FieldsName.FieldsBase_Name +" where Status in ("+Tools.txtStatus+") and TypeTree_ID = '"+TypeTree_ID+"'"+ sTextSearch +" order by Content_ID desc";
            //					sSQL = "select Top " + DateGridList.PageSize + " * , ISNULL(AtTop, 0) AS AtTop1  from (Select Top "+top_count+" * From  ContentUser_"+ _Content_FieldsName.FieldsBase_Name +" order by Content_ID DESC) As t1  where Content_ID Not In(Select top "+Remove_count+" Content_ID From ContentUser_"+ _Content_FieldsName.FieldsBase_Name +" order by Content_ID DESC) and Status in ("+Tools.txtStatus+") and TypeTree_ID = '"+TypeTree_ID+"'"+ sTextSearch +" order by Content_ID desc";
            sSQL = "Select Top " + PageSize + " * , ISNULL(AtTop, 0) AS AtTop1 from  " + cTypeTree.ExtentFieldTableName + " where Content_ID not in(select top " + PageSize * CurrentPage + " Content_ID from " + cTypeTree.ExtentFieldTableName + " where " + WhereSql + " order by OrderNum desc) and " + WhereSql + " order by OrderNum desc";
        }

        if (Current_TypeTree.IsCommonPublish)
        {
            //载入右键菜单脚本
            sMenuContent.Text = GSystem.LoadTemplate("~//SysScriptTep//Content_Content_List.MenuC.txt", new Dictionary<string, string>());
            string[] names = new string[] { "名称", "作者", "发布时间", "状态", "头条", "图文" };
            string[] cssnames = new string[] { "title", "author", "submitdate", "status", "putintopx", "isimagenews" };
            for (int i = 0; i < names.Length; i++)
            {
                BoundColumn bc = new BoundColumn();
                bc.HeaderText = names[i];
                bc.ItemStyle.CssClass = cssnames[i];
                DateGridList.Columns.Add(bc);
            }

            SelectDropDownList.Items.Clear();
            SelectDropDownList.Items.Add(new ListItem("名称", "name"));
            SelectDropDownList.Items.Add(new ListItem("ID", "Content_ID"));
            SelectDropDownList.Items.Add(new ListItem("作者", "Author"));

            sSQL = "select Top " + PageSize + " Content_Content.* , ISNULL(Content_Content.AtTop, 0) AS AtTop1 from Content_Content " +
                Sfrom + " " + Swhere + " where  Content_ID not in(select top " + PageSize * CurrentPage + " Content_ID from Content_Content where Content_Content.Status in (" + Tools.txtStatus + ") and Content_Content.TypeTree_ID = '" + Current_TypeTree_ID.ToString() + "' order by Content_ID desc) and Content_Content.Status in (" + Tools.txtStatus + ") and Content_Content.TypeTree_ID = '" + Current_TypeTree_ID.ToString() + "'" +
                sTextSearch + " order by Content_Content.AtTop desc ,Content_Content.OrderNum desc";
        }

        if (Current_TypeTree.IsReCommandPublish)
        {
            //载入右键菜单脚本
            sMenuContent.Text = GSystem.LoadTemplate("~//SysScriptTep//Content_Content_List.MenuR.txt", new Dictionary<string, string>()); 
            string[] names = new string[] { "名称", "作者", "发布时间", "状态"};
            string[] cssnames = new string[] { "title", "author", "submitdate", "status" };
            for (int i = 0; i < names.Length; i++)
            {
                BoundColumn bc = new BoundColumn();
                bc.HeaderText = names[i];
                bc.ItemStyle.CssClass = cssnames[i];
                DateGridList.Columns.Add(bc);
            }

            sSQL = "select Top " + DateGridList.PageSize + " Content_Content.*,ISNULL(AtTop, 0) AS AtTop from Content_Content,Content_Commend where Content_Commend.TypeTree_ID = '" + Current_TypeTree_ID.ToString() + "'" + sTextSearch + " and Content_Content.Status in (1,2,3,4,5) and Content_Commend.Content_ID = Content_Content.Content_ID order by Content_Content.OrderNum desc";
            
        }

        DateGridList.CurrentPageIndex = 0;
        txtSql.Value = sSQL;
        SqlDataReader reader=Tools.DoSqlReader(sSQL);

        DateGridList.DataSource = reader;
        DateGridList.DataBind();

        //显示Label控件LCurrentPaget和文本框控件gotoPage状态
        LCurrentPage.Text = (CurrentPage + 1).ToString();
        gotoPage.Text = (CurrentPage + 1).ToString();
    }

    protected void Toolsbar1_ButtonClick(object sender, System.EventArgs e)
    {
        string extentf = new Content_FieldsContent().GetFieldNameListForID(Current_TypeTree.ExtentFieldsId);
        
        
        string sql = string.Empty;
        if (Current_TypeTree.IsCommonPublish)
        {
            if (Current_TypeTree.HasExtentFields)
            {
                extentf = extentf.Replace(",", "," + Current_TypeTree.ExtentFieldTableName + ".");
                sql = string.Format("Select Content_Content.Content_ID,Content_Content.Name,Content_Content.Author,Content_Content.Description{0} from Content_Content,{1} Where Content_Content.Content_ID={1}.Content_ID", extentf, Current_TypeTree.ExtentFieldTableName);
            }
            else 
            {
                sql = string.Format("Select Content_Content.Content_ID,Content_Content.Name,Content_Content.Author,Content_Content.Description{0} from Content_Content", extentf);
            }
        }
        else if (Current_TypeTree.IsFullExtenFields)
        {
            if (Current_TypeTree.HasExtentFields)
            {
                
                sql = string.Format("Select {0} from {1}", extentf.Trim(','), Current_TypeTree.ExtentFieldTableName);
            }
        }
        DataTable dt = Tools.DoSqlTable(sql);
        StringWriter sw = new StringWriter();
        string title = "自动编号,名称,作者,内容";
        extentf = extentf.Replace( "," + Current_TypeTree.ExtentFieldTableName + ".",",");
    

        sw.WriteLine(title + extentf);
        
        foreach (DataRow dr in dt.Rows)
        {
            string Contents = string.Empty;
            if (Current_TypeTree.IsCommonPublish)
            {
                 Contents = dr["Content_ID"] + "," + dr["Name"] + "," + dr["Author"] + "," + Tools.DBToWeb(dr["Description"].ToString());
            }
            if (Current_TypeTree.HasExtentFields)
            {
                string[] ops = extentf.Trim(',').Split(',');
                for (int j = 0; j < ops.Length; j++)
                {
                    Contents = Contents + "," + dr[ops[j].ToString()];
                }
            }
            sw.WriteLine(Contents.Trim(','));
        }
        sw.Close();
        Response.Clear();
        Response.BufferOutput = true;

        Response.Charset = "GB2312";
        Response.AppendHeader("Content-Disposition", "attachment;filename=content.csv");

        Response.ContentEncoding = Encoding.GetEncoding("GB2312");
        Response.ContentType = "application/ms-excel";

        this.EnableViewState = false; 
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

            e.Item.Attributes.Add("onmousedown", "selectContent('" + Content_ID + "');");
            e.Item.Attributes.Add("ondblclick", "openContent('" + Content_ID + "');");
            e.Item.Attributes.Add("ondragenter", "dragEnter();");
            e.Item.Attributes.Add("ondragleave", "dragLeave();");
            e.Item.Attributes.Add("ondragover", "dragOver();");

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


            int StatusColIndex = -1;
            for (int i = 0; i < DateGridList.Columns.Count; i++)
            {
                if (DateGridList.Columns[i].HeaderText == "状态") {
                    StatusColIndex = i;
                    break;
                }
            }
            if (StatusColIndex != -1)
            {
                switch (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "status")))
                {
                    case 1:
                        e.Item.Cells[StatusColIndex].Text = "<font color=red>草 稿</font>";
                        break;
                    case 2:
                        e.Item.Cells[StatusColIndex].Text = "<font color=black>待审批</font>";
                        break;
                    case 3:
                        e.Item.Cells[StatusColIndex].Text = "<font color=green>待发布</font>";
                        break;
                    case 4:
                        e.Item.Cells[StatusColIndex].Text = "<font color=gray>已发布</font>";
                        break;
                    case 5:
                        e.Item.Cells[StatusColIndex].Text = "<font color=blue>已归档</font>";
                        break;
                }
            }

            if (Current_TypeTree.IsCommonPublish)//TypeTree_Type != 2
            {

                e.Item.Cells[1].Text = "<nobr><span class='title' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "name")) + ">" + Tools.DBToWeb(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "name"))) + "</span></nobr>";
                e.Item.Cells[2].Text = "<nobr><span class='Author' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Author")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Author")) + "</span></nobr>";
                e.Item.Cells[3].Text = "<nobr><span class='submitdate' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "submitdate")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "submitdate")) + "</span></nobr>";


                e.Item.Cells[5].Text = Convert.ToChar(DataBinder.Eval(e.Item.DataItem, "Head_news")).ToString() == "1" ? "是" : "否";
                e.Item.Cells[6].Text = Convert.ToChar(DataBinder.Eval(e.Item.DataItem, "Picture_news")).ToString() == "1" ? "是" : "否";
            }
            if (Current_TypeTree.IsReCommandPublish)//TypeTree_Type != 2
            {
                e.Item.Cells[1].Text = "<nobr><span class='title' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "name")) + ">" + Tools.DBToWeb(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "name"))) + "</span></nobr>";
                e.Item.Cells[2].Text = "<nobr><span class='Author' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Author")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Author")) + "</span></nobr>";
                e.Item.Cells[3].Text = "<nobr><span class='submitdate' title=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "submitdate")) + ">" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "submitdate")) + "</span></nobr>";
            }


        }
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
        //string FieldsBase = "Content_Content";
        string Fields = "name";
        
        if (Current_TypeTree.IsFullExtenFields)//TypeTree_Type == 2
        {
            //FieldsBase = "ContentUser_" + _Content_FieldsName.FieldsBase_Name;
            Fields = SelectDropDownList.SelectedValue;
        }

        sTextSearch = TextSearch.Text;
        if (!String.IsNullOrEmpty(sTextSearch ))
        {
            sTextSearch = " and ( " + Current_TypeTree.MainFieldTableName + "." + Fields + " like '%" + sTextSearch + "%') ";
        }
        loadDataList(Current_TypeTree);
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
        loadDataList(Current_TypeTree);//调用数据绑定函数TDataBind()
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
                loadDataList(Current_TypeTree);//调用数据绑定函数TDataBind()再次进行数据绑定运算
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

    /// <summary>
    /// 计算余页，防止SQL语句执行时溢出查询范围
    /// </summary>
    /// <returns></returns>
    public int GetModPage()
    {
        int pages = 0;
        if (RecCount % PageSize == 0 && RecCount != 0)
            pages = 1;
        else
            pages = 0;
        return pages;
    }


    /// <summary>
    /// 计算总记录的静态函数
    /// 在这里使用静态函数的理由是：如果引用的是静态数据或静态函数，连接器会优化生成代码，去掉动态重定位项（对海量数据表分页效果更明显）。
    /// </summary>
    /// <returns></returns>
    public int GetCalc()
    {
        if (Current_TypeTree.IsCommonPublish)//TypeTree_Type == 0
        {
            countSql = "Select count(*) as co from Content_Content where Status in (" + Tools.txtStatus + ") and TypeTree_ID = '" + Current_TypeTree_ID.ToString() + "'" + sTextSearch;
        }

        if (Current_TypeTree.IsFullExtenFields)//TypeTree_Type == 2
        {
            if (!Current_TypeTree.HasExtentFields) return 0;

            countSql = "Select count(*) as co from  " + Current_TypeTree.ExtentFieldTableName + " where Status in (" + Tools.txtStatus + ") and TypeTree_ID = '" + Current_TypeTree_ID.ToString() + "'" + sTextSearch;
            
           
        }
        //Change By Galen Mu 2008.7.29
        if (Current_TypeTree.IsReCommandPublish)//Current_TypeTree.TypeTree_Type == 1
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
        loadPagingIndex();
        loadDataList(Current_TypeTree);
    }
    protected void PageHeader_Load(object sender, EventArgs e)
    {

    }
}
