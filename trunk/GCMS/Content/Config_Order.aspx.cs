//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-8-26
//
// 功能描述: 字段操作相应页
//
// 已修改问题:
// 未修改问题:
// 修改记录
//   2008-8-26 添加注释
//   2008-8-31  规范【自定义事件】【SQL引用】【字符处理】【页面参数获取】【Js引用】代码
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
using System.Data.SqlClient;
using GCMSClassLib.Content;
using GCMS.PageCommonClassLib;

public partial class Content_Config_Order : GCMS.PageCommonClassLib.PageBase
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
        this.Response.Write("<script language=javascript>alert(\"超时操作！！！\");parent.parent.parent.window.navigate('../Logon.aspx');</script>");
        return;
    }
    #endregion 自定义事件的注册和处理

    #region 当前页面注册的SQL字符串
    const string SQL_FieldsContentGetList = "SELECT Fields_ID,Property_Order FROM Content_FieldsContent WHERE FieldsName_ID ={0} and (Property_Order between {1} and {2}) and Fields_ID <> {3} order by Property_Order {4}";
    const string SQL_FieldsContentUpdate="update Content_FieldsContent set Property_Order = {0} where Fields_ID = {1}";
    const string SQL_FieldsContentGetModel1 = "select top 1 Fields_ID,Property_Order from Content_FieldsContent where Property_Order > {0} and FieldsName_ID ={1} order by Property_Order";
    const string SQL_FieldsContentGetModel2 = "select top 1 Fields_ID,Property_Order from Content_FieldsContent where Property_Order < {0} and FieldsName_ID ={1} order by Property_Order desc";
    const string SQL_FieldsContentDelect = "Delete Content_FieldsName where FieldsName_ID = {0}";

    #endregion 当前页面注册的SQL字符串

    #region 当前页面注册的js脚本
    const string JS_CloseWindow = "<script language='javascript'>parent.windowclose();</script>";
    #endregion 当前页面注册的js脚本

    string OrderType, sql;
    int Fields_ID, FieldsName_ID;

    SqlDataReader myReader;

    Content_FieldsContent _Content_FieldsContent = new Content_FieldsContent();

     

    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        OrderType = this.GetQueryString("OrderType", null); //命令#缺少错误判断和错误处理#
        switch (OrderType)
        {
            //拖拽
            case "MoveBefore":
                Fields_ID = int.Parse(this.GetQueryString("Fields_ID", null));//#缺少错误判断和错误处理#

                int tarid = int.Parse(this.GetQueryString("tarid", null));//#缺少错误判断和错误处理#
                FieldsName_ID = int.Parse(this.GetQueryString("FieldsName_ID", null));//#缺少错误判断和错误处理#

                int OrderNum1 = _Content_FieldsContent.OrderNumInit(Fields_ID);
                int OrderNum2 = _Content_FieldsContent.OrderNumInit(tarid);

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

                sql = string.Format(SQL_FieldsContentGetList,
                    FieldsName_ID,
                    StartNum,
                    EndNum,
                    Fields_ID,
                    Order);
                myReader = Tools.DoSqlReader(sql);
                while (myReader.Read())
                {
                    Tools.DoSql(string.Format(SQL_FieldsContentUpdate,TempOrderNum,myReader.GetInt32(0)));
                    TempOrderNum = int.Parse(myReader.GetInt32(1).ToString());
                }
                Tools.DoSql(string.Format(SQL_FieldsContentUpdate, OrderNum2, Fields_ID) );

                this.Response.Write(JS_CloseWindow);
                break;

            //下移
            case "MoveDown":
                Fields_ID = int.Parse(Request.QueryString["Fields_ID"].ToString());
                int OrderNum3 = _Content_FieldsContent.OrderNumInit(Fields_ID);
                FieldsName_ID = int.Parse(Request.QueryString["FieldsName_ID"].ToString());

                sql =string.Format(SQL_FieldsContentGetModel1, OrderNum3,FieldsName_ID);
                myReader = Tools.DoSqlReader(sql);
                while (myReader.Read())
                {
                    Tools.DoSql(string.Format(SQL_FieldsContentUpdate,OrderNum3,myReader.GetInt32(0)));
                    Tools.DoSql(string.Format(SQL_FieldsContentUpdate, myReader.GetInt32(1),Fields_ID));
                }
                myReader.Close();

                this.Response.Write(JS_CloseWindow);
                break;

            //上移
            case "MoveUp":
                Fields_ID = int.Parse(Request.QueryString["Fields_ID"].ToString());
                int OrderNum4 = _Content_FieldsContent.OrderNumInit(Fields_ID);
                FieldsName_ID = int.Parse(this.GetQueryString("FieldsName_ID", null));//#缺少错误判断和错误处理#

                sql = string.Format(SQL_FieldsContentGetModel2,OrderNum4,FieldsName_ID);
                myReader = Tools.DoSqlReader(sql);
                while (myReader.Read())
                {
                    Tools.DoSql(string.Format(SQL_FieldsContentUpdate,OrderNum4,myReader.GetInt32(0)));
                    Tools.DoSql(string.Format(SQL_FieldsContentUpdate, myReader.GetInt32(1), Fields_ID));
                }
                myReader.Close();
                this.Response.Write(JS_CloseWindow);
                break;

            //删除
            case "DelFieldsName":
                FieldsName_ID = int.Parse(Request.QueryString["FieldsName_ID"].ToString());
                Content_FieldsName _Content_FieldsName = new Content_FieldsName();
                _Content_FieldsName.Init(FieldsName_ID);
                ContentTable _ContentTable = new ContentTable();
                _ContentTable.TableName = "ContentUser_" + _Content_FieldsName.FieldsBase_Name;
                Tools.DoSql(_ContentTable.cDelTableName());

                Tools.DoSql(string.Format(SQL_FieldsContentDelect,FieldsName_ID));
                this.Response.Write(JS_CloseWindow);
                break;
        }
    }
   
}
