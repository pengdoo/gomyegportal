//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-8-25
//
// 功能描述: 树形菜单
//
// 已修改问题:
// 未修改问题:

// 修改记录
//   2008-9-23 封装插入脚本
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
using GCMSClassLib.Content;
using GCMSClassLib.Public_Cls;
using System.Data.SqlClient;
using System.Collections.Generic;
using GCMS.PageCommonClassLib;
using System.Text;

public partial class Gomye_Tools_Tools_GTree : System.Web.UI.UserControl
{
    private string _Sql;
    public string Sql
    {
        get { return _Sql; }
        set { _Sql = value; }
    }

    private string _Url;
    public string Url
    {
        get { return _Url; }
        set { _Url = value; }
    }

    private string _Target;
    public string Target
    {
        get { return _Target; }
        set { _Target = value; }
    }

    private string _Mode;
    public string Mode
    {
        get { return _Mode; }
        set { _Mode = value; }
    }

    private string _JS = "";
    public string JS
    {
        get { return _JS; }
        set { _JS = value; }
    }


    protected override void Render(HtmlTextWriter output)
    {
        if (Session["Master_UserName"] == null || Session["Master_ID"] == null || Session["Roles"] == null)
        {
            return;
        }

        Type_TypeTree _Type_TypeTree = new Type_TypeTree();

        if (!this.Page.IsClientScriptBlockRegistered("clientScript"))
        {
            Dictionary<string, string> p=  new Dictionary<string, string>();

            p.Add("Mode",this._Mode);

            //计算插入条件脚本
            string strWhen=string.Empty;
            if (this.Mode != "4"){
                strWhen = GSystem.LoadTemplate("~//SysScriptTep//Gomye_Tools_Tools_GTree.When.txt", new Dictionary<string, string>());
            }
            p.Add("When Mode=4", strWhen);
            //计算插入树对象脚本
            SqlDataReader reader = Tools.DoSqlReader(this._Sql);
            StringBuilder strTreeItem = new StringBuilder();
            while (reader.Read())
            {
                strTreeItem.AppendLine("var aNode=tree.add(new WebFXTreeItem(\"" + Tools.WebToDB(reader["TypeTree_CName"].ToString()) + "\",\"N\",\"" + reader["TypeTree_ID"].ToString() + "\"));");

                if (_Type_TypeTree.HaveSon(int.Parse(reader["TypeTree_ID"].ToString()))) {
                    strTreeItem.AppendLine("aNode.add(new WebFXTreeItem(\"Loading\",\"Y\"));");
                };
            }
            reader.Close();
            p.Add("TreeItem", strTreeItem.ToString());
            //计算打开目录对象脚本
            string strOpenFolderItem = this.Url + "\" + path + \"&defaultstatus=" + this._Mode + "\";";
            p.Add("OpenFolderItme", strOpenFolderItem);
            //生成完整脚本
            string strScript = GSystem.LoadTemplate("~//SysScriptTep//Gomye_Tools_Tools_GTree.Main.txt", p);
            output.Write(strScript);
            this.Page.RegisterClientScriptBlock("clientScript", "");

        }
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
