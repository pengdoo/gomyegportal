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
using System.Data.SqlClient;
using GCMSClassLib.Public_Cls;
using GCMS.PageCommonClassLib;

public partial class Content_Content_ViewOrder : GCMS.PageCommonClassLib.PageBase
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
        this.Response.Write("<script language=javascript>alert(\"超时或非法操作！！！\");parent.windowclose();</script>");
        return;
    }
    string OrderType, Content_List, sql;
    int Content_ID, TypeTree_ID;
    ContentCls content = new ContentCls();
    string[] ops;
    char sSplit;
    SqlDataReader myReader;
    SqlDataReader reader = null;
   
    int TypeTree_IDs;
    string FieldsName = "Content_Content";
    Type_TypeTree _Type_TypeTree = new Type_TypeTree();
    Content_FieldsName _Content_FieldsName = new Content_FieldsName();
    protected void Page_Load(object sender, EventArgs e)
    {
        OrderType = Request.QueryString["OrderType"].ToString(); //命令
        if ((Request["Content_ID"] != "") && (Request["Content_ID"] != null))
        {
            Content_ID = int.Parse(Request.QueryString["Content_ID"].ToString());
        }
        TypeTree_ID = int.Parse(Request.QueryString["TypeTree_ID"].ToString());
        Type_TypeTree _Type_TypeTree = new Type_TypeTree();
        Content_FieldsName _Content_FieldsName = new Content_FieldsName();

        if (TypeTree_ID != 0)
        {
            _Type_TypeTree.Init(TypeTree_ID);
            if (_Type_TypeTree.TypeTree_ContentFields != 0)
            {
                _Content_FieldsName.Init(_Type_TypeTree.TypeTree_ContentFields);
                FieldsName = "ContentUser_" + _Content_FieldsName.FieldsBase_Name;
            }
        }
        else
        {
            return;
        }

        if (FieldsName == "")
        { FieldsName = "Content_Content"; }



        switch (OrderType)
        {
            //锁定
            case "lock":
                Content_ID = int.Parse(Request.QueryString["Content_ID"].ToString());

                //Change By Galen Mu  2008.8.25
                //将content.DoSelect(..)  改为 Tools.DoSql(..) 
                Tools.DoSql("update Content_Content set lockedby = '" + Session["Master_UserName"] + "' where Content_ID = " + Content_ID);
                this.Response.Redirect("Tools_Postform.htm");
                break;

            //解锁
            case "unlock":
                Content_ID = int.Parse(Request.QueryString["Content_ID"].ToString());

                //Change By Galen Mu  2008.8.25
                //将content.DoSelect(..)  改为 Tools.DoSql(..) 
                Tools.DoSql("update Content_Content set lockedby = '' where Content_ID = " + Content_ID);
                this.Response.Redirect("Tools_Postform.htm");
                break;

            //置顶
            case "AtTop":
                Content_ID = int.Parse(Request.QueryString["Content_ID"].ToString());

                //Change By Galen Mu  2008.8.25
                //将content.DoSelect(..)  改为 Tools.DoSql(..) 
                Tools.DoSql("update Content_Content set AtTop = '1' where Content_ID = " + Content_ID);
                PushSystem(TypeTree_ID, Content_ID);
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //取消置顶
            case "UnAtTop":
                Content_ID = int.Parse(Request.QueryString["Content_ID"].ToString());

                //Change By Galen Mu  2008.8.25
                //将content.DoSelect(..)  改为 Tools.DoSql(..) 
                Tools.DoSql("update Content_Content set AtTop = NULL where Content_ID = " + Content_ID);
                PushSystem(TypeTree_ID, Content_ID);
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;


            //拖拽
            case "MoveBefore":
                Content_ID = int.Parse(Request.QueryString["Content_ID"].ToString());

                int tarid = int.Parse(Request.QueryString["tarid"].ToString());
                TypeTree_ID = int.Parse(Request.QueryString["TypeTree_ID"].ToString());

                int OrderNum1 = content.OrderNumInit(Content_ID);
                int OrderNum2 = content.OrderNumInit(tarid);

                int StartNum, EndNum;
                string Order;
                int TempOrderNum;


                if (OrderNum2 > OrderNum1)
                {
                    EndNum = OrderNum2;
                    StartNum = OrderNum1;
                    Order = "";
                    TempOrderNum = OrderNum1;
                }
                else
                {
                    EndNum = OrderNum1;
                    StartNum = OrderNum2;
                    Order = "desc";
                    TempOrderNum = OrderNum1;
                }

                sql = "SELECT Content_ID,OrderNum FROM Content_Content WHERE TypeTree_ID =" + TypeTree_ID + " and (OrderNum between " + StartNum + " and " + EndNum + ") and Content_ID <> " + Content_ID + " order by OrderNum " + Order;
                myReader = Tools.DoSqlReader(sql);
                while (myReader.Read())
                {
                    //Change By Galen Mu  2008.8.25
                    //将content.DoSelect(..)  改为 Tools.DoSql(..) 
                    Tools.DoSql("update Content_Content set OrderNum = " + TempOrderNum + " where Content_ID = " + myReader.GetInt32(0).ToString());
                    TempOrderNum = int.Parse(myReader.GetInt32(1).ToString());
                }
                //Change By Galen Mu  2008.8.25
                //将content.DoSelect(..)  改为 Tools.DoSql(..) 
                Tools.DoSql("update Content_Content set OrderNum = " + OrderNum2 + " where Content_ID = " + Content_ID);
                PushSystem(TypeTree_ID, Content_ID);

                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //删除
            case "Delete":
                Content_List = Request.QueryString["Content_List"].ToString();

                sSplit = ',';
                ops = Content_List.Split(sSplit);

                for (int j = 0; j < ops.Length; j++)
                {
                    if (ops[j].ToString() != "-1")
                    {
                        //Change By Galen Mu  2008.8.25
                        //将content.DoSelect(..)  改为 Tools.DoSql(..) 
                        Tools.DoSql("Update "+FieldsName+" set Status = '-1' where Content_ID = " + ops[j].ToString());
                        PushSystem(TypeTree_ID, int.Parse(ops[j].ToString()));
                    }
                }
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //签发
            case "Approval":
                Content_List = Request.QueryString["Content_List"].ToString();
                int Content_sID = 0;

                sSplit = ',';
                ops = Content_List.Split(sSplit);

                for (int j = 0; j < ops.Length; j++)
                {
                    if (ops[j].ToString() != "-1")
                    {
                        //Change By Galen Mu  2008.8.25
                        //将content.DoSelect(..)  改为 Tools.DoSql(..) 
                        Tools.DoSql("Update " + FieldsName + " set Status = '4' where Content_ID = " + ops[j].ToString());
                        PushContent(TypeTree_ID, int.Parse(ops[j].ToString()));
                        Content_sID = int.Parse(ops[j].ToString());
                    }
                }
                if (Content_sID != 0)
                { PushSystem(TypeTree_ID, Content_sID); };
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //上移
            case "MoveUp":
                Content_ID = int.Parse(Request.QueryString["Content_ID"].ToString());
                int OrderNum3 = content.OrderNumInit(Content_ID);
                TypeTree_ID = int.Parse(Request.QueryString["TypeTree_ID"].ToString());

                sql = "select top 1 Content_ID,OrderNum from Content_Content where OrderNum > " + OrderNum3 + " and TypeTree_ID =" + TypeTree_ID + " order by OrderNum";
                myReader = Tools.DoSqlReader(sql);
                while (myReader.Read())
                {
                    //Change By Galen Mu  2008.8.25
                    //将content.DoSelect(..)  改为 Tools.DoSql(..) 
                    Tools.DoSql("update Content_Content set OrderNum = " + OrderNum3 + " where Content_ID = " + myReader.GetInt32(0).ToString());
                    Tools.DoSql("update Content_Content set OrderNum = " + myReader.GetInt32(1).ToString() + " where Content_ID = " + Content_ID);
                    PushSystem(TypeTree_ID, myReader.GetInt32(0));
                }
                myReader.Close();
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //下移
            case "MoveDown":
                Content_ID = int.Parse(Request.QueryString["Content_ID"].ToString());
                int OrderNum4 = content.OrderNumInit(Content_ID);
                TypeTree_ID = int.Parse(Request.QueryString["TypeTree_ID"].ToString());

                sql = "select top 1 Content_ID,OrderNum from Content_Content where OrderNum < " + OrderNum4 + " and TypeTree_ID =" + TypeTree_ID + " order by OrderNum desc";
                myReader = Tools.DoSqlReader(sql);
                while (myReader.Read())
                {
                    //Change By Galen Mu  2008.8.25
                    //将content.DoSelect(..)  改为 Tools.DoSql(..) 
                    Tools.DoSql("update Content_Content set OrderNum = " + OrderNum4 + " where Content_ID = " + myReader.GetInt32(0).ToString());
                    Tools.DoSql("update Content_Content set OrderNum = " + myReader.GetInt32(1).ToString() + " where Content_ID = " + Content_ID);
                    PushSystem(TypeTree_ID, myReader.GetInt32(0));
                }
                myReader.Close();
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //关联
            case "Relative":
                Content_ID = int.Parse(Request.QueryString["Content_ID"].ToString());
                string retV = Request.QueryString["retV"].ToString();
                TypeTree_ID = int.Parse(Request.QueryString["TypeTree_ID"].ToString());

                sSplit = ',';
                ops = retV.Split(sSplit);
                int Relative_ID = TypeTree_ID;

                for (int j = 0; j < ops.Length; j++)
                {
                    if (ops[j].ToString() != "-1")
                    {
                        //Change By Galen Mu  2008.8.25
                        //将content.DoSelect(..)  改为 Tools.DoSql(..) 
                        Tools.DoSql("insert into Content_Contact ( Content_ID,Other_ID,Relative_ID) values (" + Content_ID + "," + int.Parse(ops[j].ToString()) + "," + Relative_ID + ")");
                    }
                }
                //					PushSystem(Content_ID);
                //					CreateFiles _CreateFiles = new CreateFiles();
                //					_CreateFiles.CreateContentFiles(int.Parse(this.LabelTypeID.Text),Content_ID,UrlString(this.TypeTree_Template.Text),UrlString(content.Url));
                this.Response.Redirect("Content_Relative.aspx?Content_ID=" + Content_ID + "&TypeTree_ID=" + TypeTree_ID);
                break;

            //子文章
            case "Son":
                Content_ID = int.Parse(Request.QueryString["Content_ID"].ToString());
                string rets = Request.QueryString["retV"].ToString();
                TypeTree_ID = int.Parse(Request.QueryString["TypeTree_ID"].ToString());
                FieldsName = "Content_Content";

                _Type_TypeTree.Init(TypeTree_ID);
                if (_Type_TypeTree.TypeTree_Type == 2)
                {
                    _Content_FieldsName.Init(_Type_TypeTree.TypeTree_ContentFields);
                    FieldsName = "ContentUser_" + _Content_FieldsName.FieldsBase_Name;
                }

                Tools.DoSql("update " + FieldsName + " set Content_PID =" + Content_ID + " where Content_ID in (" + rets + ")");

                this.Response.Redirect("Content_Relative.aspx?Content_ID=" + Content_ID + "&TypeTree_ID=" + TypeTree_ID);
                break;


            //映射
            case "Recommend":

                string cid = Request["cid"];
                if (cid == "") { Response.Write("错误操作！"); return; }
                Content_List = Request.QueryString["Content_List"].ToString();
                string[] ops1;
                string[] cids;

                char sSplit1 = ',';

                cids = cid.Split(sSplit1);
                for (int i = 0; i < cids.Length; i++)
                {
                    ops1 = Content_List.Split(sSplit1);
                    for (int j = 0; j < ops1.Length; j++)
                    {
                        if (ops1[j].ToString() != "-1")
                        {
                            if (!MemberUsersInRoles(ops1[j], cids[i]))
                            {
                                //Change By Galen Mu  2008.8.25
                                //将content.DoSelect(..)  改为 Tools.DoSql(..) 
                                Tools.DoSql("insert into Content_Commend (Content_ID,TypeTree_ID) values (" + ops1[j] + "," + cids[i] + ")");

                            }
                            PushChannel(int.Parse(cids[i]));
                        }
                    }
                }
                //this.Response.Redirect ("Content_Recommend.aspx?Content_List="+Content_List);
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;



            //粘贴
            case "Paste":
                Content_List = Request.QueryString["Content_List"].ToString();

                sSplit = ',';
                ops = Content_List.Split(sSplit);

                for (int j = 0; j < ops.Length; j++)
                {
                    if (ops[j].ToString() != "-1")
                    {
                        TypeTree_ID = int.Parse(Request.QueryString["TypeTree_ID"].ToString());
                        Content_ID = int.Parse(ops[j].ToString());

                        content.Init(Content_ID);
                        TypeTree_IDs = content.TypeTree_ID;
                        content.OrderNum = content.QueryMaxContentID();
                        content.Author = Session["Master_UserName"].ToString();
                        content.Status = "3";
                        content.TypeTree_ID = TypeTree_ID;
                        content.Create();
                        //int UpdateContent_ID = content.ContentId;


                        //sql = "SELECT Property_ID,Property_Name,Property_InputType,Property_Alias,Property_InputOptions,Property_Type FROM Content_Schema WHERE TypeTree_ID =" + content.TypeTree_ID + " order by Property_ID";
                        //myReader = Tools.DoSqlReader(sql);
                        //while (myReader.Read())
                        //{

                        //    if (TypeAdd.PropertyInit(Content_ID, myReader.GetInt32(0), myReader.GetString(5)))
                        //    {
                        //        string sql1 = "SELECT * FROM Content_Schema WHERE Property_Name ='" + myReader.GetString(1) + "' and TypeTree_ID =" + TypeTree_ID;
                        //        reader = Tools.DoSqlReader(sql1);
                        //        if (reader.Read())
                        //        {
                        //            TypeAdd.UpSchema(int.Parse(reader["Property_ID"].ToString()), content.ContentId, reader["Property_Type"].ToString());

                        //        }
                        //        reader.Close();
                        //        _DataConn.Close();
                        //        _DataConn.Dispose();
                        //    }

                        //}
                        myReader.Close();
                    }
                }
                this.Response.Redirect("Tools_Postform.htm");
                break;

            //预览
            case "Preview":
                if (this.Request["Content_ID"] == null) return;
                
                sql = "select top 1 Url from " + FieldsName + " where Content_ID = " + Request["Content_ID"];
                string Url = "";
                myReader = Tools.DoSqlReader(sql);
                while (myReader.Read())
                {
                    Url = myReader["Url"].ToString();
                }
                if (String.IsNullOrEmpty(Url))//为兼容AOC旧版数据库增加
                {
                    sql = "select top 1 Url from Content_Content where Content_ID = " + Request["Content_ID"];
                    myReader = Tools.DoSqlReader(sql);
                    while (myReader.Read())
                    {
                        Url = myReader["URL"].ToString();
                    }
                }
               

                myReader.Close();
                this.Response.Redirect(Url);
                break;

            //栏目移动
            case "preMoveContent":
                Content_List = Request.QueryString["Content_List"].ToString();

                sSplit = ',';
                ops = Content_List.Split(sSplit);

                for (int j = 0; j < ops.Length; j++)
                {
                    if (ops[j].ToString() != "-1")
                    {
                        TypeTree_ID = int.Parse(Request.QueryString["columnid"].ToString());
                        Content_ID = int.Parse(ops[j].ToString());

                        //Change By Galen Mu  2008.8.25
                        //将content.DoSelect(..)  改为 Tools.DoSql(..) 
                        Tools.DoSql("update Content_Content set TypeTree_ID = " + TypeTree_ID + " where Content_ID = " + Content_ID);
                    }
                }

                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //栏目拷贝
            case "preCopyContent":

                Content_List = Request.QueryString["Content_List"].ToString();

                sSplit = ',';
                ops = Content_List.Split(sSplit);

                for (int j = 0; j < ops.Length; j++)
                {
                    if (ops[j].ToString() != "-1")
                    {
                        TypeTree_ID = int.Parse(Request.QueryString["columnid"].ToString());
                        Content_ID = int.Parse(ops[j].ToString());

                        content.Init(Content_ID);
                        TypeTree_IDs = content.TypeTree_ID;
                        content.OrderNum = content.QueryMaxContentID();
                        content.Author = Session["Master_UserName"].ToString();
                        content.Status = "3";
                        content.TypeTree_ID = TypeTree_ID;
                        content.Create();
                        //;
                        //sql = "SELECT Property_ID,Property_Name,Property_InputType,Property_Alias,Property_InputOptions,Property_Type FROM Content_Schema WHERE TypeTree_ID =" + TypeTree_IDs + " order by Property_ID";
                        //myReader = Tools.DoSqlReader(sql);
                        //while (myReader.Read())
                        //{

                        //    if (TypeAdd.PropertyInit(Content_ID, myReader.GetInt32(0), myReader.GetString(5)))
                        //    {
                        //        string sql1 = "SELECT * FROM Content_Schema WHERE Property_Name ='" + myReader.GetString(1) + "' and TypeTree_ID =" + TypeTree_ID;
                        //        reader = Tools.DoSqlReader(sql1);
                        //        if (reader.Read())
                        //        {
                        //            TypeAdd.UpSchema(int.Parse(reader["Property_ID"].ToString()), content.ContentId, reader["Property_Type"].ToString());

                        //        }
                        //        reader.Close();
                        //        _DataConn.Close();
                        //        _DataConn.Dispose();
                        //    }

                        //}
                        //myReader.Close();

                    }
                }

                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //映射=========================================================================================================================================

            //撤销映射
            case "UnRel":
                Content_List = Request.QueryString["Content_List"].ToString();
                TypeTree_ID = int.Parse(Request.QueryString["TypeTree_ID"].ToString());

                sSplit = ',';
                ops = Content_List.Split(sSplit);

                for (int j = 0; j < ops.Length; j++)
                {
                    if (ops[j].ToString() != "-1")
                    {
                        //Change By Galen Mu  2008.8.25
                        //将content.DoSelect(..)  改为 Tools.DoSql(..) 
                        Tools.DoSql("Delete Content_Commend where Content_ID = " + ops[j].ToString() + " and Typetree_ID = " + TypeTree_ID);
                        PushSystem(TypeTree_ID, int.Parse(ops[j].ToString()));
                    }
                }
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //生成映射列表
            case "ApprovalRel":
                //Content_List = Request.QueryString["Content_List"].ToString();
                TypeTree_ID = int.Parse(Request.QueryString["TypeTree_ID"].ToString());
                PushChannel(TypeTree_ID);
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;
        }
    }

    private bool MemberUsersInRoles(string Content_ID, string TypeTree_ID)
    {
        SqlDataReader reader = null;
        string sql = "select * from Content_Commend where Content_ID=" + Content_ID + " and TypeTree_ID = " + TypeTree_ID;

        reader = Tools.DoSqlReader(sql);

        if (reader.Read())
        {
            reader.Close();
            return true;
        }
        else
        {
            reader.Close();
            return false;
        }
    }


    private void PushContent(int TypeTree_ID, int Content_ID)
    {
        //			ContentCls _ContentCls = new ContentCls();
        CreateFiles _CreateFiles = new CreateFiles();
        //			Type_TypeTree _Type_TypeTree = new Type_TypeTree();
        //			_ContentCls.Init(Content_ID);
        //			_Type_TypeTree.Init(_ContentCls.TypeTree_ID);
        //
        //			if (_ContentCls.Url == "" || _ContentCls.Url.Equals(null))
        //			{
        //				string Url = _Type_TypeTree.TypeTreeURL.Replace("{@UID}",Content_ID.ToString());
        //				_ContentCls.DoSelect("update Content_Content set Url = '"+Url+"' where Content_ID = "+Content_ID);
        //				_ContentCls.Url = Url;
        //			}
        //			int TypeTree_IDs = _ContentCls.TypeTree_ID ;
        _CreateFiles.CreateContentFiles(TypeTree_ID, Content_ID);

    }

    private void PushSystem(int TypeTree_ID, int Content_ID)
    {

        //			ContentCls _ContentCls = new ContentCls();
        CreateFiles _CreateFiles = new CreateFiles();
        //			Type_TypeTree _Type_TypeTree = new Type_TypeTree();
        //
        //			_ContentCls.Init(Content_ID);
        //			_Type_TypeTree.Init(_ContentCls.TypeTree_ID);

        _CreateFiles.CreateChannelFiles(TypeTree_ID);
        _CreateFiles.CreateLinkPushFiles(TypeTree_ID);

    }

    private void PushChannel(int TypeTree_ID)
    {

        CreateFiles _CreateFiles = new CreateFiles();
        Type_TypeTree _Type_TypeTree = new Type_TypeTree();
        _Type_TypeTree.Init(TypeTree_ID);


        _CreateFiles.CreateChannelFiles(TypeTree_ID);
        _CreateFiles.CreateLinkPushFiles(TypeTree_ID);

    }

    private string UrlString(string FilesUrl)
    {
        FilesUrl = FilesUrl.Replace("/", "//");
        FilesUrl = Server.MapPath(FilesUrl);
        return FilesUrl;
    }

}
