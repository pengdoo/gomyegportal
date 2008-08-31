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
using System.Data.SqlClient;

public partial class Gomye_Tools_TreeControl : System.Web.UI.UserControl
{
    string cmdSelect;
    SqlConnection Conn;
    SqlDataAdapter myCmd;
    DataSet ds;
    int gblLayer = 0;

    string TreeButton = "<div class=\"parent\" ondrop=\"FinishDrag('COLUMN_-1')\"><span>&nbsp;<Img src=\"../Admin_Public/Images/fo.gif\" align=\"absmiddle\" border=\"0\" >&nbsp;网站根目录</span></div>";
    protected void Page_Load(object sender, EventArgs e)
    {
        Conn = new SqlConnection( ConfigurationManager.AppSettings["ConnectionString"]);

        CreateDataSet();
        Label1.Text = InitTree("-1").ToString();
    }
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

    //建立数据集
    private DataSet CreateDataSet()
    {
        //			cmdSelect = "select * from Content_Type_TypeTree order by TypeTree_OrderNum";
        cmdSelect = this.Sql;
        myCmd = new SqlDataAdapter(cmdSelect, Conn);
        ds = new DataSet();
        myCmd.Fill(ds, "tree");
        return ds;
    }

    //建树的基本思路是：从根节点开始递归调用显示子树 
    private string InitTree(string parentId)
    {
        DataView dv = new DataView();
        dv.Table = ds.Tables["tree"];
        dv.RowFilter = "TypeTree_ParentID=" + parentId;
        gblLayer = gblLayer + 1;
        int gblCount = dv.Count;

        bool HaveBrother;

        foreach (DataRowView drv in dv)
        {
            if (gblCount > 1)
            {
                HaveBrother = true;
                gblCount = gblCount - 1;
            }
            else
            {
                HaveBrother = false;
            }

            int j = SonInitTree(drv["TypeTree_ID"].ToString());
            TreeButton = TreeButton + ((char)10 + "<div ID=\"" + "m" + drv["TypeTree_ID"].ToString() + "Parent" + "\" class=\"parent\"");
            if (this.Mode == "1") { TreeButton = TreeButton + (" ondragenter=\"dragEnter();\" ondragleave=\"dragLeave();\" ondragover=\"dragOver()\" ondrop=\"FinishDrag('" + "COLUMN_" + drv["TypeTree_ID"].ToString() + "');\" ondragstart=\"InitDrag('" + "COLUMN_" + drv["TypeTree_ID"].ToString() + "');\""); };
            TreeButton = TreeButton + (">");

            for (int i = 1; i <= gblLayer - 1; i++)
            {
                //					if (HaveBrother)
                //					{
                TreeButton = TreeButton + ("<Img src=\"../Admin_Public/Images/Tree_white.gif\" align=\"absmiddle\" border=\"0\">");
                //					}
                //					else
                //					{
                //						TreeButton = TreeButton + ("<Img src=\"../Admin_Public/Images/Tree_white.gif\" align=\"absmiddle\" border=\"0\">");
                //					}
            }

            if (j != 0)	//如果还有子孙，则需要有连接，有加号
            {
                if (HaveBrother)
                {
                    TreeButton = TreeButton + "<a name=\"" + "m" + drv["TypeTree_ID"].ToString() + "a" + "\" href=\"#nothisanchor\" onClick=\"FolderExpand('m" + drv["TypeTree_ID"].ToString() + "','" + this.Url + drv["TypeTree_ID"].ToString() + "')\">";
                    TreeButton = TreeButton + "<img name=\"" + "m" + drv["TypeTree_ID"].ToString() + "Tree" + "\" src=\"../Admin_Public/Images/Rplus.gif\" align=\"absmiddle\" border=\"0\">";
                }
                else
                {
                    TreeButton = TreeButton + "<a name=\"" + "m" + drv["TypeTree_ID"].ToString() + "a" + "\" href=\"#nothisanchor\" onClick=\"FolderExpand('m" + drv["TypeTree_ID"].ToString() + "','" + this.Url + drv["TypeTree_ID"].ToString() + "','last')\">";
                    TreeButton = TreeButton + "<img name=\"" + "m" + drv["TypeTree_ID"].ToString() + "Tree" + "\" src=\"../Admin_Public/Images/Rplus.gif\" align=\"absmiddle\" border=\"0\">";
                }
                TreeButton = TreeButton + ("</a>" + (char)10);
            }
            else			//否则，直接显示线就可以
            {
                if (HaveBrother)
                {
                    TreeButton = TreeButton + "<img name=\"" + "m" + drv["TypeTree_ID"].ToString() + "Tree" + "\" src=\"../Admin_Public/Images/Tree_white.gif\" align=\"absmiddle\" border=\"0\">" + (char)10;
                }
                else
                {
                    TreeButton = TreeButton + "<img name=\"" + "m" + drv["TypeTree_ID"].ToString() + "Tree" + "\" src=\"../Admin_Public/Images/Tree_white.gif\" align=\"absmiddle\" border=\"0\">" + (char)10;
                }

            }

            TreeButton = TreeButton + ("<span onMouseOver=\"IsonMouseOver('m" + drv["TypeTree_ID"].ToString() + "');\" onMouseOut=\"IsonMouseOut('m" + drv["TypeTree_ID"].ToString() + "');\" onmouseup=\"OpenFolder('m" + drv["TypeTree_ID"].ToString() + "','" + this.Url + drv["TypeTree_ID"].ToString() + "','" + "COLUMN_" + drv["TypeTree_ID"].ToString() + "','" + drv["TypeTree_CName"].ToString() + "');\">");
            TreeButton = TreeButton + ("<Img name=\"" + "m" + drv["TypeTree_ID"].ToString() + "Pic" + "\" src=\"../Admin_Public/Images/fc.gif\" align=\"absmiddle\" border=\"0\" >");
            TreeButton = TreeButton + "&nbsp;<a class=item href='#nothisanchor' name=\"" + "m" + drv["TypeTree_ID"].ToString() + "Folder" + "\"> ";
            TreeButton = TreeButton + (drv["TypeTree_CName"].ToString());
            TreeButton = TreeButton + ("</a></span> </div>" + (char)10);
            TreeButton = TreeButton + "<div ID=\"m" + drv["TypeTree_ID"].ToString() + "Child\" class=\"child\">" + (char)10;
            InitTree(drv["TypeTree_ID"].ToString());
        }
        gblLayer = gblLayer - 1;  //最后减1
        TreeButton = TreeButton + ("</div>" + (char)10);
        //			if(this.JS !="")
        //			{
        //				Nav = JS;
        //			}
        //			TreeButton = ("<script language='JavaScript' src=\"../admin_public/js/"+Nav+"\"></script>"+(char)10) + TreeButton;
        return TreeButton;
    }


    private int SonInitTree(string parentId)
    {

        DataView dv = new DataView();
        dv.Table = ds.Tables["tree"];
        dv.RowFilter = "TypeTree_ParentID=" + parentId;

        int j = 0;
        for (int i = 1; i <= dv.Count; i++)
        {
            j = j + 1;
        }
        return j;

    }
}
