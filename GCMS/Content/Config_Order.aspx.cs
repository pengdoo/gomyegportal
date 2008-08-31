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
    string OrderType, sql;
    int Fields_ID, FieldsName_ID;

    SqlDataReader myReader;

    Content_FieldsContent _Content_FieldsContent = new Content_FieldsContent();



    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        OrderType = Request.QueryString["OrderType"].ToString(); //命令
        switch (OrderType)
        {
            //拖拽
            case "MoveBefore":
                Fields_ID = int.Parse(Request.QueryString["Fields_ID"].ToString());

                int tarid = int.Parse(Request.QueryString["tarid"].ToString());
                FieldsName_ID = int.Parse(Request.QueryString["FieldsName_ID"].ToString());

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

                sql = "SELECT Fields_ID,Property_Order FROM Content_FieldsContent WHERE FieldsName_ID =" + FieldsName_ID + " and (Property_Order between " + StartNum + " and " + EndNum + ") and Fields_ID <> " + Fields_ID + " order by Property_Order " + Order;
                myReader = Tools.DoSqlReader(sql);
                while (myReader.Read())
                {
                    Tools.DoSql("update Content_FieldsContent set Property_Order = " + TempOrderNum + " where Fields_ID = " + myReader.GetInt32(0).ToString());
                    TempOrderNum = int.Parse(myReader.GetInt32(1).ToString());
                }
                Tools.DoSql("update Content_FieldsContent set Property_Order = " + OrderNum2 + " where Fields_ID = " + Fields_ID);

                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //下移
            case "MoveDown":
                Fields_ID = int.Parse(Request.QueryString["Fields_ID"].ToString());
                int OrderNum3 = _Content_FieldsContent.OrderNumInit(Fields_ID);
                FieldsName_ID = int.Parse(Request.QueryString["FieldsName_ID"].ToString());

                sql = "select top 1 Fields_ID,Property_Order from Content_FieldsContent where Property_Order > " + OrderNum3 + " and FieldsName_ID =" + FieldsName_ID + " order by Property_Order";
                myReader = Tools.DoSqlReader(sql);
                while (myReader.Read())
                {
                    Tools.DoSql("update Content_FieldsContent set Property_Order = " + OrderNum3 + " where Fields_ID = " + myReader.GetInt32(0).ToString());
                    Tools.DoSql("update Content_FieldsContent set Property_Order = " + myReader.GetInt32(1).ToString() + " where Fields_ID = " + Fields_ID);
                }
                myReader.Close();

                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //上移
            case "MoveUp":
                Fields_ID = int.Parse(Request.QueryString["Fields_ID"].ToString());
                int OrderNum4 = _Content_FieldsContent.OrderNumInit(Fields_ID);
                FieldsName_ID = int.Parse(Request.QueryString["FieldsName_ID"].ToString());

                sql = "select top 1 Fields_ID,Property_Order from Content_FieldsContent where Property_Order < " + OrderNum4 + " and FieldsName_ID =" + FieldsName_ID + " order by Property_Order desc";
                myReader = Tools.DoSqlReader(sql);
                while (myReader.Read())
                {
                    Tools.DoSql("update Content_FieldsContent set Property_Order = " + OrderNum4 + " where Fields_ID = " + myReader.GetInt32(0).ToString());
                    Tools.DoSql("update Content_FieldsContent set Property_Order = " + myReader.GetInt32(1).ToString() + " where Fields_ID = " + Fields_ID);
                }
                myReader.Close();
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;

            //删除
            case "DelFieldsName":
                FieldsName_ID = int.Parse(Request.QueryString["FieldsName_ID"].ToString());
                Content_FieldsName _Content_FieldsName = new Content_FieldsName();
                _Content_FieldsName.Init(FieldsName_ID);
                ContentTable _ContentTable = new ContentTable();
                _ContentTable.TableName = "ContentUser_" + _Content_FieldsName.FieldsBase_Name;
                Tools.DoSql(_ContentTable.cDelTableName());

                Tools.DoSql("Delete Content_FieldsName where FieldsName_ID = " + FieldsName_ID);
                this.Response.Write("<script language='javascript'>parent.windowclose();</script>");
                break;
        }
    }
   
}
