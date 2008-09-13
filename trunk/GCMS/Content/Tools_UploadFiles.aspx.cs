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
using System.IO;
using GCMS.PageCommonClassLib;

public partial class Content_Tools_UploadFiles : GCMS.PageCommonClassLib.PageBase
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
    string strTypeTreeID;
    string file;
    string fileExtension = ".gif";
    string TmpFile;
    string FilesPath;
    string TypeTreePictureURL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["TypeTree_ID"] != null)
        {
            strTypeTreeID = Session["TypeTree_ID"].ToString();
            Type_TypeTree typeTree = new Type_TypeTree();
            typeTree.Init(int.Parse(strTypeTreeID));
            TypeTreePictureURL = typeTree.TypeTreePictureURL;
        }
    }
    protected void Submit1_ServerClick(object sender, EventArgs e)
    {
        if (this.File1.Value != "")
        {
            Upload2Server();
        }
        else
        {
            Label1.Text = "对不起，上传文件不能为空！！！";
        }
    }

    public void Upload2Server()
    {

        file = File1.PostedFile.FileName; //+++++++++++++++++++++++++++++++++++
        //Response.Write(file);
        if (file.ToString() == "") //如果没输入上传文件，则返回
        {
            //Response.Write ("<script>javascript:alert('请输入要上传的文件名 ')</script>");
            return;
        }
        else
        {

            Random Rnd1 = new Random();
            double dbl = Rnd1.Next();
            dbl = Rnd1.Next();
            fileExtension = file.Substring(file.LastIndexOf("."));

            FilesPath = dbl.ToString() + fileExtension; //改名
            //FilesPath = dbl.ToString() + file.ToString(); //改名
            //TmpFile = rootDir + TmpDir.Value + "/" + FilesPath; //上传完整文件路径



            if (TypeTreePictureURL == "") //默认
            {
                TypeTreePictureURL = "/Images_GCMSUpload/";
            }

            TmpFile = TypeTreePictureURL + FilesPath;


            string TmpFile1 = TmpFile.Replace("/", "//");
            TmpFile1 = Server.MapPath(TmpFile1);

            File1.PostedFile.SaveAs(TmpFile1); //上传


            //****************************** 图片改名 + upload *************************************

            System.IO.FileInfo picName = new FileInfo(TmpFile1); //+++++++++++++++++++++++++++++++++++

            string fileBulk = picName.Length.ToString();
            int bulk = int.Parse(fileBulk); //获得文件大小 , string → int 格式转换
            //System.Drawing.Image image = System.Drawing.Image.FromFile(TmpFile1); //获得图片规格
            Response.Write("<script language='javascript'>javascript:confirm('恭喜你，上传成功了！');</script>");
            Response.Write("<script>top.returnValue = '" + TmpFile + "'</script>");
            Response.Write("<script>top.close();</script>");
            //image.Dispose();
            //				}
        }
    }
}
