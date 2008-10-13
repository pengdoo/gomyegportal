//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-8-26
//
// 功能描述: 执行目录相关操作
//
// 已修改问题:
// 未修改问题:
// 修改记录
//   2008-8-26 添加注释
//   2008-9-5  规范【自定义事件】【字符处理】【页面参数获取】代码
//             移除_CreateFiles.FilesIn Filesout函数，更新PushSystem，PushSystemAll函数
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
using System.Text;
using System.Data.SqlClient;
using System.IO;
//----------------------------------项目引用-----------------------------------
using GCMSClassLib.Content;
using GCMS.PageCommonClassLib;
using GCMSClassLib.Public_Cls;

public partial class Content_Type_Order : GCMS.PageCommonClassLib.PageBase
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
        this.Response.Write("<script language=javascript>alert(\"超时或非法操作！！！\");parent.windowclose();</script>");
        return;
    }
    #endregion 自定义事件的注册和处理

    GCMSContentCreate.TemplateSystem ContentCreate = new GCMSContentCreate.TemplateSystem();
    StringBuilder htmltext = new StringBuilder();
    SqlDataReader myReader;
    CreateFiles _CreateFiles = new CreateFiles();
    Type_TypeTree _Type_TypeTree = new Type_TypeTree();
    string sql;
    protected void Page_Load(object sender, EventArgs e)
    {
        string OrderType =this.GetQueryString("OrderType",null); //命令
        int TypeTree_ParentID;
        int TypeTree_ID = int.Parse(this.GetQueryString("TypeTree_ID",null));
        switch (OrderType)
        {
            //生成表单  
            case "Feedback":
                
                int Content_ID = 0;

                Type_TypeTree typeTree = new Type_TypeTree();
                typeTree.Init(TypeTree_ID);
                string TypeTree_ListTemplate = typeTree.TypeTreeListTemplate;
                string TypeTreeListURL = typeTree.TypeTreeListURL;

                //					ContentCreate.GCMS.Feedback;

                string ContentText;
                if (!String.IsNullOrEmpty(TypeTree_ListTemplate))
                {

                    if (String.IsNullOrEmpty(_CreateFiles.FilesIn(TypeTree_ListTemplate, _Type_TypeTree.TypeTree_Language).ToString() ))
                    {
                        Response.Write("<Script>alert('读取文件错误')</Script>");
                        return;
                    }

                    ContentText = ContentCreate.Execute(TypeTree_ID, Content_ID, _CreateFiles.FilesIn(TypeTree_ListTemplate, _Type_TypeTree.TypeTree_Language).ToString());//Change By Galen ,2008-9-4,原先 _CreateFiles.FilesIn引用的是语言是 System.Text.Encoding.Default


                    if (!String.IsNullOrEmpty(TypeTreeListURL ))
                    {
                       _CreateFiles.FilesOut(TypeTreeListURL, ContentText,_Type_TypeTree.TypeTree_Language);
                    }

                    htmltext = null; //清空缓存
                    ContentText = "";

                }

                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //整体发布
            case "AllPush":
               
                string[] ops = _Type_TypeTree.IDSonTypeTree(TypeTree_ID).Split( ',');
                if (!_Type_TypeTree.IsReCommandPublish)
                {
                    for (int j = 0; j < ops.Length; j++)
                    {
                        if (ops[j].ToString() != "")
                        {
                            Content_ID = 0;
                            int n = 0;
                            int Countmax = 0;




                            _Type_TypeTree.Init(int.Parse(ops[j]));

                            if (_Type_TypeTree.IsReCommandPublish)
                            {
                                continue;//如果是映射栏目，直接跳过到下一轮
                               // sql = "select Content_Content.Content_ID,Content_Content.Url from " + _Type_TypeTree.MainFieldTableName + " where Content_Content.Content_ID in (Select Content_ID from Content_Commend where TypeTree_ID=" + ops[j].ToString() + ") and Content_Content.Status = 4   order by Content_Content.OrderNum desc";
                            }
                            else
                            {
                                sql = "select Content_ID,Url from " + _Type_TypeTree.MainFieldTableName + " where Status = 4 and TypeTree_ID =" + ops[j].ToString() + " order by OrderNum desc";
                            }
                            DataTable dt = Tools.DoSqlTable(sql);
                            Countmax = dt.Rows.Count;

                            foreach (DataRow dr in dt.Rows)
                            {

                                _CreateFiles.CreateContentFiles(int.Parse(ops[j].ToString()), int.Parse(dr["Content_ID"].ToString()), false);
                                n = n + 1;
                                Response.Write("<script>this.parent.progress.style.width ='" + (n * 100 / Countmax) + "%' ;this.parent.progress.innerHTML='" + (n * 100 / Countmax) + "%';this.parent.pstr.innerHTML=' 当前栏目ID： " + dr["Content_ID"].ToString() + " <br/>当前文件： " + dr["Url"].ToString() + "';</script>");
                                Response.Flush();
                            }
                            if (Content_ID != 0) _CreateFiles.PushSingle(Content_ID);//似乎多余？#此处代码需要调试验证#
                        }

                    }
                }
                _CreateFiles.PushList(TypeTree_ID);//附带发布
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //上移
            case "doMoveUp":
                _Type_TypeTree.Init(TypeTree_ID);

                sql = "select top 1 TypeTree_ID,TypeTree_OrderNum from Content_Type_TypeTree where TypeTree_OrderNum < " + _Type_TypeTree.TypeTreeOrderNum + " and TypeTree_ParentID =" + _Type_TypeTree.TypeTreeParentID + " order by TypeTree_OrderNum desc";
                myReader = Tools.DoSqlReader(sql);
                while (myReader.Read())
                {
                    //Change By Galen Mu  2008.8.25
                    //将content.DoSelect(..)  改为 Tools.DoSql(..) 
                    Tools.DoSql("update Content_Type_TypeTree set TypeTree_OrderNum = " + _Type_TypeTree.TypeTreeOrderNum + " where TypeTree_ID = " + myReader.GetInt32(0).ToString());
                    Tools.DoSql("update Content_Type_TypeTree set TypeTree_OrderNum = " + myReader.GetInt32(1).ToString() + " where TypeTree_ID = " + TypeTree_ID);
                    _CreateFiles.PushSingle(myReader.GetInt32(0));
                }
                myReader.Close();

                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //下移
            case "doMoveDown":
                _Type_TypeTree.Init(TypeTree_ID);

                sql = "select top 1 TypeTree_ID,TypeTree_OrderNum from Content_Type_TypeTree where TypeTree_OrderNum > " + _Type_TypeTree.TypeTreeOrderNum + " and TypeTree_ParentID =" + _Type_TypeTree.TypeTreeParentID + " order by TypeTree_OrderNum";
                myReader = Tools.DoSqlReader(sql);
                while (myReader.Read())
                {
                    Tools.DoSql("update Content_Type_TypeTree set TypeTree_OrderNum = " + _Type_TypeTree.TypeTreeOrderNum + " where TypeTree_ID = " + myReader.GetInt32(0).ToString());
                    Tools.DoSql("update Content_Type_TypeTree set TypeTree_OrderNum = " + myReader.GetInt32(1).ToString() + " where TypeTree_ID = " + TypeTree_ID);
                    _CreateFiles.PushSingle(myReader.GetInt32(0));
                }
                myReader.Close();
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;
            //移动
            case "preMoveChannel":
                TypeTree_ParentID = int.Parse(Request.QueryString["parent"].ToString());
                Tools.DoSql("update Content_Type_TypeTree set TypeTree_ParentID = " + TypeTree_ParentID + " where TypeTree_ID = " + TypeTree_ID);
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //拷贝
            case "preCopyChannel":
                TypeTree_ParentID = int.Parse(Request.QueryString["parent"].ToString());
                MakepreCopyChannel(TypeTree_ID, TypeTree_ParentID);
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;
            case "PushLinks":
                _CreateFiles.PushList(TypeTree_ID);//附带发布整体
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;
            case "PushOneLinks":
                int Link_ID = int.Parse(this.Request.QueryString["Link_ID"].ToString());
               
                _CreateFiles.CreateLinkPushSingleFile(Link_ID,TypeTree_ID);//附带发布一条
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;
        }
    }

    private void MakepreCopyChannel(int TypeTree_ID, int TypeTree_ParentID)
    {

        _Type_TypeTree.Init(TypeTree_ID);
        _Type_TypeTree.TypeTreeParentID = TypeTree_ParentID;
        _Type_TypeTree.Create();
        TypeTree_ParentID = _Type_TypeTree.TypeTree_ID;

        string[] ops= _Type_TypeTree.SonTypeTree(TypeTree_ID).Split(',');

        for (int j = 0; j < ops.Length; j++)
        {
            if (!string.IsNullOrEmpty(ops[j]))
            {
                MakepreCopyChannel(int.Parse(ops[j].ToString()), TypeTree_ParentID);
            }
        }
    }
}
