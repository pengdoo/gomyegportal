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
using System.IO;
using Gomye.CommonClassLib.Text;
using Gomye.CommonClassLib.FileAndDir;
using System.Text;
using System.Web.UI.MobileControls;
using System.Collections.Generic;

public partial class Content_Tools_FileMain : System.Web.UI.Page
{

    #region 常量
    public string[] gNoFile = new string[] { "asp", "aspx" };//禁止访问的脚本
    public string[,] sFor
    {
        get
        {
            string[,] sfor = new string[29, 2];
            sfor[0, 0] = "txt"; sfor[0, 1] = "1";
            sfor[1, 0] = "chm"; sfor[1, 1] = "2";
            sfor[2, 0] = "hlp"; sfor[2, 1] = "2";
            sfor[3, 0] = "doc"; sfor[3, 1] = "3";
            sfor[4, 0] = "pdf"; sfor[4, 1] = "4";
            sfor[5, 0] = "gif"; sfor[5, 1] = "6";
            sfor[6, 0] = "jpg"; sfor[6, 1] = "6";
            sfor[7, 0] = "png"; sfor[7, 1] = "6";
            sfor[8, 0] = "bmp"; sfor[8, 1] = "6";
            sfor[9, 0] = "asp"; sfor[9, 1] = "7";
            sfor[10, 0] = "jsp"; sfor[10, 1] = "7";
            sfor[11, 0] = "js"; sfor[11, 1] = "7";
            sfor[12, 0] = "htm"; sfor[12, 1] = "8";
            sfor[13, 0] = "html"; sfor[13, 1] = "8";
            sfor[14, 0] = "shtml"; sfor[14, 1] = "8";
            sfor[15, 0] = "zip"; sfor[15, 1] = "9";
            sfor[16, 0] = "rar"; sfor[16, 1] = "9";
            sfor[17, 0] = "exe"; sfor[17, 1] = "10";
            sfor[18, 0] = "avi"; sfor[18, 1] = "11";
            sfor[19, 0] = "mpg"; sfor[19, 1] = "11";
            sfor[20, 0] = "ra"; sfor[20, 1] = "12";
            sfor[21, 0] = "ram"; sfor[21, 1] = "12";
            sfor[22, 0] = "mid"; sfor[22, 1] = "13";
            sfor[23, 0] = "wav"; sfor[23, 1] = "13";
            sfor[24, 0] = "mp3"; sfor[24, 1] = "13";
            sfor[25, 0] = "asf"; sfor[25, 1] = "11";
            sfor[26, 0] = "php"; sfor[26, 1] = "7";
            sfor[27, 0] = "php3"; sfor[27, 1] = "7";
            sfor[28, 0] = "aspx"; sfor[28, 1] = "7";
            return sfor;
        }
    }
    public int gPageSize = 100;
    #endregion 常量

    #region 从QueryString获取的变量
    /// <summary>
    /// 根目录
    /// </summary>

    public int gPage
    {
        get
        {
            return int.Parse(getVar("page", "num", "1"));
        }
    }

    public string gAct
    {
        get
        {
            return getVar("act", "str", "");
        }
    }

    public string gPath
    {
        get
        {
            string res = string.Empty;
            res= getVar("path", "str", "");
            if (res.Length > 0 && res[res.Length - 1] != '/')
            {
                res += "/";
            }
            else if (res.Length == 0)
            {
                res = "/";
            }
            if (res.IndexOf(gRootUrl) != 0)
            {
                res = gRootUrl + res;
            }
            return res;
        }
    }

    public string gFilter
    {
        get
        {
            return getVar("filter", "str", "");
        }
    }
    #endregion 从QueryString获取的变量

    #region 从Session和其他获取的变量
    public string gRootUrl
    {
        get
        {
            string res = string.Empty;
            if(Session["webeditbase"]!=null)
            {
                res = Session["webeditbase"].ToString();
            }
            if (res.Length > 0 && res[res.Length - 1] != '/')
            {
                res += "/";
            }
            else if (res.Length == 0)
            {
                res = "/";
            }
            return res;
        }
    }
   


    public string gFilePath
    {
        get
        {
            string res = string.Empty;
            res = Request.ServerVariables["SCRIPT_NAME"].ToString();
            res = DirectoryEx.GetFolderPath(res);
            if (res.Length > 0 && res[res.Length - 1] != '/')
            {
                res = gRootUrl + res;
            }
           
            return res;
        }
    }

    public string gFileName
    {
        get 
        {
            string res = Request.ServerVariables["SCRIPT_NAME"];
            res = DirectoryEx.GetFileName(res);
            return res;
        }
    }


    public DirectoryInfo gFolder
    {
        get
        {

            //string UrlAuthority = Request.Url.GetLeftPart(UriPartial.Authority);
            //if (Request.ApplicationPath   !=   "/" ) 
            //{
            //    UrlAuthority += Request.ApplicationPath;
            //}
            string path = MapPath( gPath);
            return new DirectoryInfo(path);
        }
    }
    #endregion 从Session和其他获取的变量

    protected void Page_Load(object sender, EventArgs e)
    { 
        string sTemp=procCheckDir(gPath,1);
        if(sTemp!="ok") htmend(sTemp,1,"");

        this.curpath.Value = this.gPath.Trim('~');
        if (!Page.IsPostBack)
        {
            DirectoryInfo[] dirs = gFolder.GetDirectories();
            this.rep_folders.DataSource = dirs;
            this.lib_dirCount.Text = dirs.Length.ToString();
            this.rep_folders.DataBind();

            FileInfo[] files = gFolder.GetFiles();
            int bgIndex = (gPage - 1) * gPageSize;
            int edIndex = (gPage - 1) * gPageSize + gPageSize - 1;
            if (edIndex > files.Length - 1) edIndex = files.Length - 1;
            List<FileInfo> contentfiles = new List<FileInfo>();
            long size = 0;
            for (int i = bgIndex; i <= edIndex; i++)
            {
                contentfiles.Add(files[i]);
                size += files[i].Length;
            }
            this.lit_fileCount.Text = contentfiles.Count.ToString();
            this.rep_files.DataSource = contentfiles;
            this.rep_files.DataBind();

            this.lit_filePageCount.Text = ((files.Length / gPageSize) + 1).ToString();
            this.lit_fileSizeCount.Text = ((int)(size / 1024)).ToString();
            this.lit_fileTotleCount.Text = files.Length.ToString();

            this.currentpage.Value = gPage.ToString();
            Act();
        }
    }

   

    #region 处理动作
    public void Act()
    {
        switch (gAct)
        {
            case "renf":
                procRename();
                break;
            case "md":
                procMakeDir();
                break;
            case "rend":
                procRenameDir();
                break;
           
        }
    }

    private string procCheckfile(object p,int p_2)
    {
 	    throw new Exception("The method or operation is not implemented.");
    } 

   


    private string procCheckFile(object p, int p_2)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    private void htmEnd(string p, int p_2, string p_3)
    {
        throw new Exception("The method or operation is not implemented.");
    }
    
   
    private void procRenameDir()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    private void procMakeDir()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    private void procRename()
    {
        throw new Exception("The method or operation is not implemented.");
    }

   

    private string procCheckDir(string sPath, int mode)
    {
       string res="ok";
       string[] errorchar= new string[]{"'","\"","\\","..","*","?","&","|","<",">"};
       if(String.IsNullOrEmpty(sPath))
       {
           res="目录不能为空!";
           return res;
       }
       for(int i=0;i<errorchar.Length;i++)
       {
           if (sPath.IndexOf(errorchar[i]) > 0)
           {
               res = "目录名中含有非法字符";
               return res;
           }
       }
       int len = gFilePath.Length >= sPath.Length ?  sPath.Length:gFilePath.Length ;
       if (gFilePath == sPath.Substring(0, len))
       {
           res="没有权限访问此目录!";
           return res;
       }
       if (mode==0) return res;
       if(!Directory.Exists(Server.MapPath(sPath)))
       {
           res="目录"+sPath+"没有找到!!";
           return res;

       }
       if(sPath.Substring(0,gRootUrl.Length)!=gRootUrl)
       {
           res="目录"+sPath+"没有找到!!";
           return res;
       }
        return res;
        
    }
    #endregion 处理动作

    private void htmend(string info, int isback, string dir)
    {
       if (info!="")  Response.Write("<script language=\"javascript\">alert('"+info+"');</script>");
       switch(isback)
       {
           case 1:
               Response.Write("<script language=\"javascript\">history.back();</script>");
               break;
           case 2:
              Response.Write("<script language=\"javascript\">location.href='"+dir+"';</script>") ;
               break;
       }
       Response.Write("</body></html>");
       Response.End();
    }
  


    public string gBaseUrl
    {
        get
        {
            string res = string.Empty;
            res = gFileName + "?page=" + gPage + "+path=" + gPath;
            if (gFilter != "") res += "+filter=" + gFilter;
            return res;
        }
    }

    public void chdir(string dir,int mode)
    {
        string res = string.Empty;
        
        if (mode == 1)
        {
            if (string.IsNullOrEmpty(gFilter))
            {
                res = string.Format("{0}?path={1}{2}", gFileName, gPath,dir);
            }
            else
            {
                res = string.Format("{0}?path={1}{2}&filter={3}",gFileName,gPath, dir, gFilter);
            }
        }
        else if(mode==0)
        {
            if (string.IsNullOrEmpty(gFilter))
            {
                res = string.Format("{0}?path={1}&page={2}", gFileName, dir,this.currentpage.Value);
            }
            else
            {
                res = string.Format("{0}?path={1}&filter={2}&page={3}", gFileName, dir, gFilter, this.currentpage.Value);
            }
        }
        Response.Redirect(res);
    }


    public string getVar(string theStr, string strType, string defValue)
    {
        string res=string.Empty;
        switch( strType)
        {
            case "str":
               if (string.IsNullOrEmpty(Request.QueryString[theStr]))
               {
                     res=defValue;
               }else
               {
                     res=Request.QueryString[theStr];
               }
                break;
   
            case "num":
               if (string.IsNullOrEmpty(Request.QueryString[theStr])||! Gomye.CommonClassLib.Text.Validate.IsNumber(Request.QueryString[theStr]))
               {
                res=defValue;
               }
               else
               {
                res=Request.QueryString[theStr];
               }
               break;
           default: res = defValue;
                break;
        }
        return res;
    }
    public string procGetFormat(string sName)
    {
        string res="0";
        if (sName.IndexOf(".")==-1) return res; 
        string str=Gomye.CommonClassLib.FileAndDir.DirectoryEx.GetFileExName(sName);
        for(int i=0;i<sFor.GetLowerBound(1);i++)
        {
            if(str==sFor[i,0]) res=sFor[i,1];
        }
        return res;
    }
   
    protected void btn_newDir_ServerClick(object sender, EventArgs e)
    {
        //onclick="mkdir();"
    }

    protected void rep_folders_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch(e.CommandName)
        {
            case "chdir":
                chdir(gPath+ e.CommandArgument.ToString(), 0);
                break;
        }
    }


    protected void rep_files_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "select":
                this.curpath.Value = this.gPath.Trim('~') + e.CommandArgument.ToString();
                break;
        }
    }
    protected void lkbtn_backToRoot_Click(object sender, EventArgs e)
    {
        if (gPath != gRootUrl && gPath != "~")
        {
            string tmp = gPath.Substring(0, gPath.TrimEnd('/').LastIndexOf('/'));
            chdir(tmp, 0);
        }
       
    }
    protected void btn_goto_ServerClick(object sender, EventArgs e)
    {
        chdir(gPath, 0);
    }
}
