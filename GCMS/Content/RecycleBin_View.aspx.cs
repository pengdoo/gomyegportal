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
using System.Collections.Specialized;
using GCMS.PageCommonClassLib;

public partial class Content_RecycleBin_View : GCMS.PageCommonClassLib.PageBase
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

    private string sTypeTree_ID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!this.IsPostBack)
        {

            
            Type_TypeTree typeTree = new Type_TypeTree();

            this.Mapped.Visible = false;
            this.Locked.Visible = false;
            this.Ingear.Visible = false;

            UserName.Value = Session["Master_UserName"].ToString();
            TypeTree_ID.Value = sTypeTree_ID;
            this.PageHeader.Value = "回收站";

            //				switch (TypeTreeIssuanceID)
            //				{
            //					case 1: //"发布(可编辑)";
            //						this.Mapped.Visible = true;
            //						break;
            //					case 2: //锁定(不可编辑)
            //						this.Locked.Visible = true;
            //						break;
            //					case 3: //映射(只能映射其他栏目的文章)
            //						this.Ingear.Visible = true;
            //						break;
            //					case 4: //映射(只能映射其他栏目的文章)
            //						this.Mapped.Visible = true;
            //						break;
            //					case 5: //产品();
            //						this.Mapped.Visible = true;
            //						break;
            //					case 0: //关闭(前台不可见)
            //						this.Mapped.Visible = true;
            //						break;
            //				}
            this.Mapped.Visible = true;

            Type_List();
        }
    }

        
    public void Type_List()
	{
			DateGridList.Dispose();
			DateGridList.Attributes.Add("altRowColor","oldlace");
			DateGridList.Attributes.Add("align","center");

			//string sSQL = "select * from Content_Content where Status in (-1) and TypeTree_ID = '"+TypeTree_ID+"' order by OrderNum desc";
			string sSQL = "select * from Content_Content where Status in (-1)";
			DateGridList.DataSource = Tools.DoSqlReader(sSQL);
			DateGridList.DataBind();
    }

		public void ModifyContent()
		{
			bool bRe = CheckForm();

			if (bRe)
			{
				string strIDs = this.Request["SelectedID"].ToString();
				char myChar = ',';
				string[] ids = strIDs.Split(myChar);
				if (ids.Length>1)
				{
					string sMsg = "选择了多篇文章，请选择一篇文章再修改！";
					Page.RegisterStartupScript("修改文章","<script language=javascript>fMsg('"+sMsg+"');</script>");
				}
				else
				{
					Page.RegisterStartupScript("修改文章","<script language=javascript>ModifyContent("+sTypeTree_ID+",'"+ids[0].ToString()+"');</script>");
				}
			}
			else
			{
				string sMsg = "请选择文章";
				Page.RegisterStartupScript("修改文章","<script language=javascript>fMsg('"+sMsg+"');</script>");
			}

		
		}

		public void DelContent()
		{
			bool bRe = CheckForm();
			if (bRe)
			{
				if (this.Request["SelectedID"] != null)
				{
					string strIDs = this.Request["SelectedID"].ToString();
					string sSQL = "delete from Content_Content where Content_ID in ("+strIDs+")";
					Page.RegisterStartupScript("删除用户","<script language=javascript>DelContent("+sTypeTree_ID+",'"+sSQL+"');</script>");
				}
			}
			else
			{
				string sMsg = "请选择文章！";
				Page.RegisterStartupScript("修改文章","<script language=javascript>fMsg('"+sMsg+"');</script>");
			}
		}

		public void Refeash()
		{
			Page.RegisterStartupScript("修改文章","<script language=javascript>ContentRe("+sTypeTree_ID+")</script>");
		}
    public bool CheckForm()
		{
			int i;
			bool bSel=false;
			NameValueCollection coll;
 
			coll=this.Request.Form;
			String[] fStr = coll.AllKeys;
					
			for (i = 0; i < fStr.Length; i++) 
			{
				if (fStr[i] == "SelectedID")
				{
					bSel=true; 
				}
			}
			return bSel;
		}



		public void ItemDataBound( object sender, System.Web.UI.WebControls.DataGridItemEventArgs e ) 

		{

			string StatusImg;
			string lockText;

			if(e.Item.ItemType == ListItemType.AlternatingItem||e.Item.ItemType == ListItemType.Item)
			{
				int Content_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"Content_ID"));
				string lockedby = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"lockedby"));

				e.Item.Attributes.Add("onmousedown", "selectContent('"+Content_ID+"');");
				e.Item.ID = "item"+Content_ID;


				if(lockedby=="")
				{
					lockText = "锁定状态：当前没有锁定";
					StatusImg="src='../Admin_Public/Images/Icon_File_New.gif' alt='"+lockText+"' lockedby='"+lockedby+"'";
				}
				else if(lockedby == Session["Master_UserName"].ToString())
				{
					lockText = "锁定状态：文章由您锁定，您可以执行操作";
					StatusImg="src='../Admin_Public/Images/ic_lockuser.gif' alt='"+lockText+"' lockedby='"+lockedby+"'";
				}
				else
				{
					lockText = "锁定状态：文章由 "+lockedby+ "锁定，您不能执行操作";
					StatusImg="src='../Admin_Public/Images/ic_lock.gif' alt='"+lockText+"' lockedby='"+lockedby+"'";
				}

				string IDtxt = "<IMG id='status"+Content_ID+"' ondragstart='InitDrag()' onclick='return(false)'  "+StatusImg+">"+Content_ID;
				//IDtxt= IDtxt + "<img id='status"+Content_ID+"' src='"+StatusImg+"' width=16 height=16 alt='"+lockText+"' lockedby='"+lockedby+"'>"+Content_ID;
		
				e.Item.Cells[0].Text = IDtxt;
				e.Item.Cells[1].Text = "<nobr><span class='title' title="+Convert.ToString(DataBinder.Eval(e.Item.DataItem,"name"))+">"+Convert.ToString(DataBinder.Eval(e.Item.DataItem,"name"))+"</span></nobr>";
				e.Item.Cells[2].Text = "<nobr><span class='Author' title="+Convert.ToString(DataBinder.Eval(e.Item.DataItem,"Author"))+">"+Convert.ToString(DataBinder.Eval(e.Item.DataItem,"Author"))+"</span></nobr>";
				e.Item.Cells[3].Text = "<nobr><span class='submitdate' title="+Convert.ToString(DataBinder.Eval(e.Item.DataItem,"submitdate"))+">"+Convert.ToString(DataBinder.Eval(e.Item.DataItem,"submitdate"))+"</span></nobr>";
		
					switch (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"status")))
					{
						case 1:
							e.Item.Cells[4].Text = "<font color=red>待编辑</font>";
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

				if (Convert.ToChar(DataBinder.Eval(e.Item.DataItem,"Head_news")).ToString() == "1")
				{
					e.Item.Cells[5].Text = "是";
				}
				else
				{
					e.Item.Cells[5].Text = "否";
				}

				if (Convert.ToChar(DataBinder.Eval(e.Item.DataItem,"Picture_news")).ToString() == "1")
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

