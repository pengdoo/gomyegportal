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

public partial class Content_Tools_Xtree : System.Web.UI.Page
{
    
        Type_TypeTree _Type_TypeTree = new Type_TypeTree();

		int Mode;
		string sql;

		protected void Page_Load(object sender, System.EventArgs e)
		{
		
			int TypeTree_ID = int.Parse(this.Request["TypeTree_ID"].ToString());

            //#未完成代码#
            if (true)//int.Parse(Session["Roles"].ToString()) == 0
			{
				sql = "select TypeTree_ID,TypeTree_CName,isnull(TypeTree_Type,'0') TypeTree_Type from Content_Type_TypeTree where TypeTree_ParentID = "+TypeTree_ID+" order by TypeTree_OrderNum";
			}
			else
			{
				sql = "SELECT Content_Type_TypeTree.TypeTree_ID,Content_Type_TypeTree.TypeTree_CName,isnull(TypeTree_Type,'0') TypeTree_Type FROM Content_Type_TypeTree , Content_RolesConnect WHERE Content_RolesConnect.Roles_ID in( "+ int.Parse(Session["Roles"].ToString()) +",0) and Content_RolesConnect.TypeTree_ID=Content_Type_TypeTree.TypeTree_ID and Content_Type_TypeTree.TypeTree_ParentID= "+TypeTree_ID+" ORDER BY Content_Type_TypeTree.TypeTree_OrderNum";
			}
				


			if (Mode == 3)
			{
				//sql = "SELECT Content_Type_TypeTree.TypeTree_ID,isnull(TypeTree_Type,'0') TypeTree_Type FROM Content_Type_TypeTree , Content_RolesConnect WHERE Content_RolesConnect.Roles_ID = "+ int.Parse(Session["Roles"].ToString()) +" and Content_RolesConnect.TypeTree_ID=Content_Type_TypeTree.TypeTree_ID and Content_Type_TypeTree.TypeTree_ParentID= "+TypeTree_ID+" and Content_Type_TypeTree.TypeTree_Type= "+Mode+" ORDER BY Content_Type_TypeTree.TypeTree_OrderNum";
				sql = "SELECT Content_Type_TypeTree.TypeTree_ID,isnull(TypeTree_Type,'0') TypeTree_Type FROM Content_Type_TypeTree WHERE TypeTree_ParentID= "+TypeTree_ID+" and Content_Type_TypeTree.TypeTree_Type= "+Mode+" ORDER BY Content_Type_TypeTree.TypeTree_OrderNum";

			}
			if (Mode == 4)
			{
				//sql = "SELECT Content_Type_TypeTree.TypeTree_ID,isnull(TypeTree_Type,'0') TypeTree_Type FROM Content_Type_TypeTree , Content_RolesConnect WHERE Content_RolesConnect.Roles_ID = "+ int.Parse(Session["Roles"].ToString()) +" and Content_RolesConnect.TypeTree_ID=Content_Type_TypeTree.TypeTree_ID and Content_Type_TypeTree.TypeTree_ParentID= "+TypeTree_ID+" and Content_Type_TypeTree.TypeTree_Type= "+Mode+" ORDER BY Content_Type_TypeTree.TypeTree_OrderNum";
				sql = "SELECT TypeTree_ID,TypeTree_CName,isnull(TypeTree_Type,'0') TypeTree_Type FROM Content_Type_TypeTree WHERE TypeTree_ParentID= "+TypeTree_ID+" ORDER BY Content_Type_TypeTree.TypeTree_OrderNum";

			}
		}

    protected override void Render(HtmlTextWriter output)
    {
        //			DataView dv = new DataView();
        //			dv.Table = ds.Tables["tree"];
        //			dv.RowFilter = "TypeTree_ParentID= -1";
        //			gblLayer = gblLayer + 1;
        //			int gblCount = dv.Count;
        string HasSub = "";

        output.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        output.WriteLine("<folders>");
        //			foreach(DataRowView drv in dv)
        //			{
        SqlDataReader reader = null;
        reader = Tools.DoSqlReader(sql);
        while (reader.Read())
        {
            if (!this.Page.IsClientScriptBlockRegistered("clientScript"))
            {

                if (_Type_TypeTree.HaveSon(int.Parse(reader["TypeTree_ID"].ToString())))
                { HasSub = "yes"; }
                else
                { HasSub = "no"; };

                //output.WriteLine("<folder name=\"&lt;font color=gray&gt;"+ Tools.WebToDB(reader["TypeTree_CName"].ToString()) +"&lt;/font&gt;\" id=\""+ reader["TypeTree_ID"].ToString() + "\" hassubfolder=\""+HasSub+"\"/>");
                output.WriteLine("<folder name=\"" + Tools.WebToDB(reader["TypeTree_CName"].ToString()) + "\" id=\"" + reader["TypeTree_ID"].ToString() + "\" type=\"" + reader["TypeTree_Type"].ToString() + "\" hassubfolder=\"" + HasSub + "\"/>");
            }
        }
        output.WriteLine("</folders>");
        reader.Close();


    }
 
}
