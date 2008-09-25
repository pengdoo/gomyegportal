//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-8-26
//
// 功能描述: 添加文件相关操作
//
// 已修改问题:
// 未修改问题:
// 修改记录
//    1 2008-9-4 修改了两个Push的方法，改变引用到CreateFiles.PushList
//    2 2008-9-14 封装了页面全局变量逻辑，删除了PushContent函数
//------------------------------------------------------------------------------
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
using System.Collections.Generic;

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

    #region 当页的全局变量
    int m_typetree_id;
    int Current_TypeTree_ID
    {
        get
        {
            if (!string.IsNullOrEmpty(this.GetQueryString("TypeTree_ID", string.Empty)))
            {
                m_typetree_id = int.Parse(this.GetQueryString("TypeTree_ID", null));
            }
            return m_typetree_id;
        }
        set
        {
            m_typetree_id = value;
        }
    }
    int m_content_id;
    int Current_Content_ID
    {
        get
        {
            m_content_id = int.Parse(this.GetQueryString("Content_ID", null));
            return m_content_id;
        }
        set
        {
            m_content_id = value;
        }
    }
    string[] Current_ContetnList
    {
        get {
            string str = string.Empty;
            if (this.GetQueryString("Content_List", "Null") != "Null")
            {
                str = Request.QueryString["Content_List"].ToString();
            }
            return str.Split(',');
        }
    }
    Type_TypeTree Current_TypeTree;
    void InitCurrentTypeTree()
    {
        Current_TypeTree = new Type_TypeTree();
        Current_TypeTree.Init(m_typetree_id);
    }

    #endregion 当页的全局变量

    string OrderType, Content_List, sql;
    //int Content_ID, TypeTree_ID;
    ContentCls content = new ContentCls();
    SqlDataReader myReader;

    CreateFiles createFiles = new CreateFiles();
    int TypeTree_IDs;
    protected void Page_Load(object sender, EventArgs e)
    {
        OrderType =this.GetQueryString("OrderType",null); //Request.QueryString["OrderType"].ToString(); //命令
       

        if (Current_TypeTree_ID==0)//TypeTree_ID != 0
        {
            //return;
        }
        InitCurrentTypeTree();
        //if (string.IsNullOrEmpty(FieldsName)) FieldsName = "Content_Content"; 

        switch (OrderType)
        {
            //锁定
            case "lock":
                Tools.DoSql("update " + Current_TypeTree.MainFieldTableName + " set lockedby = '" + Session["Master_UserName"] + "' where Content_ID = " + Current_Content_ID.ToString());
                this.Response.Redirect("Tools_Postform.htm");
                break;

            //解锁
            case "unlock":
                Tools.DoSql("update " + Current_TypeTree.MainFieldTableName + " set lockedby = '' where Content_ID = " + Current_Content_ID.ToString());
                this.Response.Redirect("Tools_Postform.htm");
                break;

            //置顶
            case "AtTop":
                Tools.DoSql("update " + Current_TypeTree.MainFieldTableName + " set AtTop = '1' where Content_ID = " + Current_Content_ID.ToString());
                createFiles.PushList(Current_TypeTree_ID);
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //取消置顶
            case "UnAtTop":
                Tools.DoSql("update " + Current_TypeTree.MainFieldTableName + " set AtTop = NULL where Content_ID = " + Current_Content_ID.ToString());
                createFiles.PushList(Current_TypeTree_ID);
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;


            //拖拽
            case "MoveBefore":            
                int tarid = int.Parse(Request.QueryString["tarid"].ToString());
                int OrderNum1 = content.OrderNumInit(Current_Content_ID);
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

                sql = "SELECT Content_ID,OrderNum FROM " + Current_TypeTree.MainFieldTableName + " WHERE TypeTree_ID =" + Current_TypeTree_ID.ToString() + " and (OrderNum between " + StartNum + " and " + EndNum + ") and Content_ID <> " + Current_Content_ID.ToString() + " order by OrderNum " + Order;
                myReader = Tools.DoSqlReader(sql);
                while (myReader.Read())
                {
                    Tools.DoSql("update " + Current_TypeTree.MainFieldTableName + " set OrderNum = " + TempOrderNum + " where Content_ID = " + myReader.GetInt32(0).ToString());
                    TempOrderNum = int.Parse(myReader.GetInt32(1).ToString());
                }
                Tools.DoSql("update " + Current_TypeTree.MainFieldTableName + " set OrderNum = " + OrderNum2 + " where Content_ID = " + Current_Content_ID.ToString());
                createFiles.PushList(Current_TypeTree_ID);
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //删除
            case "Delete":
                for (int j = 0; j < Current_ContetnList.Length; j++)
                {
                    if (Current_ContetnList[j].ToString() != "-1")
                    {
                        Tools.DoSql("Update " + Current_TypeTree.MainFieldTableName + " set Status = '-1' where Content_ID = " + Current_ContetnList[j]);
                    }
                }
                createFiles.PushList(Current_TypeTree_ID);
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //签发
            case "Approval":
               
                int Content_sID = 0;
                for (int j = 0; j < Current_ContetnList.Length; j++)
                {
                    if (Current_ContetnList[j]!= "-1")
                    {
                        Tools.DoSql("Update " + Current_TypeTree.MainFieldTableName + " set Status = '4' where Content_ID = " + Current_ContetnList[j]);
                        createFiles.CreateContentFiles(Current_TypeTree_ID, int.Parse(Current_ContetnList[j]),true);
                        Content_sID = int.Parse(Current_ContetnList[j]);
                    }
                }
                if (Content_sID != 0)
                { createFiles.PushList(Current_TypeTree_ID); };
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //上移
            case "MoveUp":
                int OrderNum3 = content.OrderNumInit(Current_Content_ID);
                sql = "select top 1 Content_ID,OrderNum from Content_Content where OrderNum > " + OrderNum3 + " and TypeTree_ID =" + Current_TypeTree_ID.ToString() + " order by OrderNum";
                myReader = Tools.DoSqlReader(sql);
                while (myReader.Read())
                {
                    Tools.DoSql("update " + Current_TypeTree.MainFieldTableName + " set OrderNum = " + OrderNum3 + " where Content_ID = " + myReader.GetInt32(0).ToString());
                    Tools.DoSql("update " + Current_TypeTree.MainFieldTableName + " set OrderNum = " + myReader.GetInt32(1).ToString() + " where Content_ID = " + Current_Content_ID);
                    createFiles.PushList(Current_TypeTree_ID);
                }
                myReader.Close();
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //下移
            case "MoveDown":       
                int OrderNum4 = content.OrderNumInit(Current_Content_ID);
                sql = "select top 1 Content_ID,OrderNum from Content_Content where OrderNum < " + OrderNum4 + " and TypeTree_ID =" + Current_TypeTree_ID.ToString() + " order by OrderNum desc";
                myReader = Tools.DoSqlReader(sql);
                while (myReader.Read())
                {
                    Tools.DoSql("update " + Current_TypeTree.MainFieldTableName + " set OrderNum = " + OrderNum4 + " where Content_ID = " + myReader.GetInt32(0).ToString());
                    Tools.DoSql("update " + Current_TypeTree.MainFieldTableName + " set OrderNum = " + myReader.GetInt32(1).ToString() + " where Content_ID = " + Current_Content_ID);
                    createFiles.PushList(Current_TypeTree_ID);
                }
                myReader.Close();
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //关联
            case "Relative":
                string retV = Request.QueryString["retV"].ToString();
                string[] ops = retV.Split(',');
                int Relative_ID = Current_TypeTree_ID;

                for (int j = 0; j < ops.Length; j++)
                {
                    if (ops[j] != "-1")
                    {
                        Tools.DoSql("insert into Content_Contact ( Content_ID,Other_ID,Relative_ID) values (" + Current_Content_ID.ToString() + "," + int.Parse(ops[j].ToString()) + "," + Relative_ID + ")");
                    }
                }
                //					PushSystem(Content_ID);
                //					CreateFiles _CreateFiles = new CreateFiles();
                //					_CreateFiles.CreateContentFiles(int.Parse(this.LabelTypeID.Text),Content_ID,UrlString(this.TypeTree_Template.Text),UrlString(content.Url));
                this.Response.Redirect("Content_Relative.aspx?Content_ID=" + Current_Content_ID.ToString() + "&TypeTree_ID=" + Current_TypeTree_ID.ToString());
                break;

            //子文章
            case "Son":     
                string rets = Request.QueryString["retV"].ToString();
                //FieldsName = "Content_Content";
                //_Type_TypeTree.Init(TypeTree_ID);
                //if (Current_TypeTree.IsFullExtenFields)//_Type_TypeTree.TypeTree_Type == 2
                //{
                //    _Content_FieldsName.Init(Current_TypeTree.TypeTree_ContentFields);
                //    FieldsName = "ContentUser_" + _Content_FieldsName.FieldsBase_Name;
                //}
                Tools.DoSql("update " + Current_TypeTree.MainFieldTableName + " set Content_PID =" + Current_Content_ID.ToString() + " where Content_ID in (" + rets + ")");
                this.Response.Redirect("Content_Relative.aspx?Content_ID=" + Current_Content_ID.ToString() + "&TypeTree_ID=" + Current_TypeTree_ID.ToString());
                break;


            //映射
            case "Recommend":

                string cid = Request["cid"];
                if (string.IsNullOrEmpty(cid)) { Response.Write("错误操作！"); return; }
                //Content_List = Request.QueryString["Content_List"].ToString();
                //string[] ops1;
                ////string[] cids;

                //char sSplit1 = ',';

                string[] cids = cid.Split(',');
                for (int i = 0; i < cids.Length; i++)
                {
                    //ops1 = Current_ContetnList.Split(',');
                    for (int j = 0; j < Current_ContetnList.Length; j++)
                    {
                        if (Current_ContetnList[j] != "-1")
                        {
                            if (!MemberUsersInRoles(Current_ContetnList[j], cids[i]))
                            {
                                Tools.DoSql("insert into Content_Commend (Content_ID,TypeTree_ID) values (" + Current_ContetnList[j] + "," + cids[i] + ")");

                            }
                            createFiles.PushList(int.Parse(cids[i]));//#此处含有可优化的内容, 重构时注意#
                        }
                    }
                }
                //this.Response.Redirect ("Content_Recommend.aspx?Content_List="+Content_List);
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;



            //粘贴
            case "Paste":
             
                for (int j = 0; j <this.Current_ContetnList.Length; j++)
                {
                    if (Current_ContetnList[j].ToString() != "-1")
                    {
                        int Content_ID = int.Parse(Current_ContetnList[j].ToString());
                        if (Current_TypeTree.IsCommonPublish)//复制标准字段
                        {
                            content.Init(Content_ID);
                            TypeTree_IDs = content.TypeTree_ID;
                            content.OrderNum = content.QueryMaxContentID();
                            content.Author = Session["Master_UserName"].ToString();
                            content.Status = "3";
                            content.TypeTree_ID = Current_TypeTree_ID;
                            content.Create();
                        }
                        if (Current_TypeTree.HasExtentFields)//复制扩展字段
                        {
                            List<string> sqls = new List<string>();
                            int newContentID = Tools.QueryMaxID("Content_ID") + 1;
                            sqls.Add(string.Format("select *  into #tmpContent from {0} Where Content_ID={1}", Current_TypeTree.ExtentFieldTableName, Content_ID));
                            sqls.Add(string.Format("update #tmpContent Set Content_ID={0},TypeTree_ID={1}",newContentID,Current_TypeTree_ID ));
                            sqls.Add(string.Format("insert into {0} select * from #tmpContent Where Content_ID={1}", Current_TypeTree.ExtentFieldTableName, newContentID));
                            sqls.Add("drop table #tmpContent");
                            Tools.DoSqlTrans(sqls);
                            Tools.UpdateMaxID("Content_ID");
                        }

                    }
                }
                this.Response.Redirect("Tools_Postform.htm");
                break;

            //预览
            case "Preview":
                if (this.Request["Content_ID"] == null) return;
                
                sql = "select top 1 Url from " + Current_TypeTree.MainFieldTableName + " where Content_ID = " + Request["Content_ID"];
                string Url = string.Empty;
                myReader = Tools.DoSqlReader(sql);
                while (myReader.Read())
                {
                    Url = myReader["Url"].ToString();
                }
                //if (String.IsNullOrEmpty(Url))//为兼容AOC旧版数据库增加
                //{
                //    sql = "select top 1 Url from Content_Content where Content_ID = " + Request["Content_ID"];
                //    myReader = Tools.DoSqlReader(sql);
                //    while (myReader.Read())
                //    {
                //        Url = myReader["URL"].ToString();
                //    }
                //}
                myReader.Close();
                this.Response.Redirect(Url);
                break;

            //栏目移动
            case "preMoveContent":
               
                for (int j = 0; j < this.Current_ContetnList.Length; j++)
                {
                    if (Current_ContetnList[j] != "-1")
                    {
                        //int TypeTree_ID = int.Parse(this.GetQueryString("columnid", null));
                        int Content_ID = int.Parse(Current_ContetnList[j].ToString());
                        Tools.DoSql("update "+Current_TypeTree.MainFieldTableName+" set TypeTree_ID = " + Current_TypeTree_ID + " where Content_ID = " + Content_ID);
                    }
                }

                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //栏目拷贝
            case "preCopyContent":
               
                for (int j = 0; j < Current_ContetnList.Length; j++)
                {
                    if (Current_ContetnList[j] != "-1")
                    {
                        //int TypeTree_ID = int.Parse(this.GetQueryString("columnid", null));
                        int Content_ID = int.Parse(Current_ContetnList[j]);
               
                        if (Current_TypeTree.IsCommonPublish)//复制标准字段
                        {
                            content.Init(Content_ID);
                            TypeTree_IDs = content.TypeTree_ID;
                            content.OrderNum = content.QueryMaxContentID();
                            content.Author = Session["Master_UserName"].ToString();
                            content.Status = "3";
                            content.TypeTree_ID = Current_TypeTree_ID;
                            content.Create();
                        }
                        if (Current_TypeTree.HasExtentFields)//复制扩展字段
                        {
                            List<string> sqls = new List<string>();
                            int newContentID = Tools.QueryMaxID("Content_ID") + 1;
                            sqls.Add(string.Format("select *  into #tmpContent from {0} Where Content_ID={1}", Current_TypeTree.ExtentFieldTableName, Content_ID));
                            sqls.Add(string.Format("update #tmpContent Set Content_ID={0},TypeTree_ID={1}", newContentID, Current_TypeTree_ID));
                            sqls.Add(string.Format("insert into {0} select * from #tmpContent Where Content_ID={1}", Current_TypeTree.ExtentFieldTableName, newContentID));
                            sqls.Add("drop table #tmpContent");
                            Tools.DoSqlTrans(sqls);
                            Tools.UpdateMaxID("Content_ID");
                        }


                    }
                }

                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //映射=========================================================================================================================================

            //撤销映射
            case "UnRel":
      
                for (int j = 0; j < this.Current_ContetnList.Length; j++)
                {
                    if (Current_ContetnList[j].ToString() != "-1")
                    {
                        Tools.DoSql("Delete Content_Commend where Content_ID = " + Current_ContetnList[j] + " and Typetree_ID = " + Current_TypeTree_ID.ToString());
                       
                    }
                }
                createFiles.PushList(Current_TypeTree_ID);//Change By Galen Mu  2008.9.4 移出循环
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //生成映射列表
            case "ApprovalRel":
                createFiles.PushList(Current_TypeTree_ID);
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
   

}
