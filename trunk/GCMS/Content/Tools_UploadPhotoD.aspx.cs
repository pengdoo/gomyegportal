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
using System.Drawing;
using System.IO;
using GCMSClassLib.Public_Cls;
using GCMS.PageCommonClassLib;

public partial class Content_Tools_UploadPhotoD : GCMS.PageCommonClassLib.PageBase
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
    string rootDir;
    string file = "";
    string fileExtension = ".gif";
    string TmpFile;
    //		string TmpFile1 = "";
    string TmpFiles;
    string strTypeTreeID;
    string TypeTreePictureURL;
    Type_TypeTree typeTree = new Type_TypeTree();
    string sType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        sType = Request["sType"].ToString();
        rootDir = Server.MapPath("CompanyMIS.aspx/../");
        strTypeTreeID = Session["TypeTree_ID"].ToString();
        typeTree.Init(int.Parse(strTypeTreeID));
        TypeTreePictureURL = typeTree.TypeTreePictureURL;
        if (TypeTreePictureURL == "") //默认
        {
            Response.Write("<script>javascript:alert('对不起，上传文件夹没有设置，请重输入后使用本功能！');parent.windowclose();</script>");
            return;
        }

    }
    protected void btnAttach_Click(object sender, EventArgs e)
    {
        btnCancel.Enabled = true;
        string filename;
        TmpFile = "";

        if (file1.PostedFile.FileName.ToString().Trim() != "")
        {
            filename = Path.GetFileName(file1.PostedFile.FileName.ToString());
            string _file = Tools.UploadName(filename, TypeTreePictureURL);

            file1.PostedFile.SaveAs(Server.MapPath(_file).Replace("\\", "\\\\")); //上传
            if (Thumbnail.Checked)
            {

                GetThumbnailImage(int.Parse(ThumbnailWidth.Value.ToString()), int.Parse(ThumbnailHeight.Value.ToString()), _file);

                //					TmpFiles = TmpFiles + "<a href=\""+ _file +"\" target=\""+ ThumbnailTarget.Value.ToString() +"\" alt=\""+ ThumbnailAlt.Value.ToString() +"\"><IMG src=\"" + TmpFile + "\"  border=0></a>";
                TmpFiles = TmpFiles + _file + "|" + TmpFile;
            }
            else
            {
                TmpFiles = TmpFiles + _file;
            }
        }



        //			TmpFiles = WebtoThisPic(TmpFiles);

        if (sType == "Open")
        {
            Response.Write("<script language=\"javascript\"> top.returnValue=\"" + TmpFiles + "\";</script>");
            //Page.RegisterStartupScript("aa","<script language='javascript'> top.returnValue='" + TmpFiles +"';</script>");
        }
        else
        {
            Response.Write("<script language=\"javascript\"> top.returnValue=\"" + TmpFiles + "\";</script>");
            //Page.RegisterStartupScript("aa","<script language='javascript'> top.returnValue='" + TmpFiles +"';</script>");
            //Response.Write ("<script language='javascript'> dialogArguments.insertHTML('" + TmpFiles +"');</script>");
        }
        //Page.RegisterStartupScript("aa","<script language='javascript'> top.close();</script>");
        Response.Write("<script language=\"javascript\"> top.close();</script>");
        Response.End();
    }

    void GetThumbnailImage(int width, int height, string files)
    {


        System.Drawing.Image oldimage = System.Drawing.Image.FromFile(Server.MapPath(files).Replace("\\", "\\\\"));
        int _Width = oldimage.Width;
        int _Height = oldimage.Height;
        int S_Width;
        int S_Height;

        if (width == 0)
        {
            S_Width = Convert.ToInt32(Convert.ToDouble(height) / Convert.ToDouble(_Height) * _Width);
            width = S_Width;
        }

        if (height == 0)
        {
            S_Height = Convert.ToInt32(Convert.ToDouble(width) / Convert.ToDouble(_Width) * _Height);
            height = S_Height;
        }

        System.Drawing.Image thumbnailImage = oldimage.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);
        Response.Clear();
        Bitmap output = new Bitmap(thumbnailImage);
        Graphics g = Graphics.FromImage(output);

        output.Save(Server.MapPath(files).Replace("\\", "\\\\").Replace(".", "s."), System.Drawing.Imaging.ImageFormat.Jpeg);
        Response.ContentType = "image/gif";
        TmpFile = files.Replace(".", "s.");

    }

    bool ThumbnailCallback()
    {
        return true;
    }




    //*************************** 上传 图片 *********************************
    public string Upload2Server()
    {
        string filename = Path.GetFileName(file.ToString());
        fileExtension = file.Substring(file.LastIndexOf("."));
        if (file.ToString() == "") //如果没输入上传文件，则返回
        {
            return "";
        }
        else
        {

            if ((fileExtension.Trim().ToUpper() != ".GIF") && (fileExtension.Trim().ToUpper() != ".JPG")) //判断上传文件是否为 .gif 或 .jpg 文件
            {
                Response.Write("<script>javascript:alert('对不起，你输入的文件不支持上传，请重新输入！')</script>");
                Response.Write("<script>javascript:alert('" + file + "')</script>");
                return "";
            }
            else
            {
                //****************************** 图片改名 + upload *************************************


                Random Rnd1 = new Random();
                double dbl = Rnd1.Next();
                dbl = Rnd1.Next();
                //					tmpLogo.Value = dbl.ToString() + fileExtension; //改名
                //TmpFile = rootDir + TmpDir.Value + "/" + tmpLogo.Value; //上传完整文件路径

                Type_TypeTree typeTree = new Type_TypeTree();
                typeTree.Init(int.Parse(strTypeTreeID));
                string TypeTreePictureURL = typeTree.TypeTreePictureURL;

                if (TypeTreePictureURL == "") //默认
                {

                }

                TmpFile = TypeTreePictureURL + dbl.ToString() + fileExtension; //改名


                string TmpFile1 = TmpFile.Replace("/", "//");
                TmpFile1 = Server.MapPath(TmpFile1);

                file1.PostedFile.SaveAs(TmpFile1); //上传

                //缩略图

                //					if(Request.Form["Thumbnail"] != "")
                //					{
                //						string TmpFile2 = TmpFile1.Replace(".","s.");
                //						//图片大小转换 将TargetFileNameStr的图片放宽为IntWidth，高为IntHeight 
                //						SourceImage=System.Drawing.Image.FromFile(TmpFile1);
                //						System.Drawing.Image.GetThumbnailImageAbort myAbort = new System.Drawing.Image.GetThumbnailImageAbort(imageAbort);
                //						int IntWidth = int.Parse(ThumbnailWidth.Value.ToString()); //新的图片宽
                //						int IntHeight = int.Parse(ThumbnailHeight.Value.ToString()); //新的图片高
                //						TargetImage = SourceImage.GetThumbnailImage(IntWidth,IntHeight,myAbort,IntPtr.Zero);
                //						FileStream myOutput = new FileStream(TmpFile2,FileMode.Create, FileAccess.Write, FileShare.Write);
                //						TargetImage.Save(myOutput,ImageFormat.Jpeg);
                //						myOutput.Close();
                //					}
                //****************************** 图片改名 + upload *************************************

                // file1.PostedFile.SaveAs(//IMGPath + "/" + changeName);

                System.IO.FileInfo picName = new FileInfo(TmpFile1); //+++++++++++++++++++++++++++++++++++

                string fileBulk = picName.Length.ToString();
                int bulk = int.Parse(fileBulk); //获得文件大小 , string → int 格式转换
                System.Drawing.Image image = System.Drawing.Image.FromFile(TmpFile1); //获得图片规格

                //if (bulk > 100000) // 判断上传文件大小不能超过 100 K 
                //{
                //	Response.Write ("<script>javascript:alert('对不起，你上传的文件超过了100K，上传失败！')</script>");
                //	return;
                //}
                //else
                //{ 
                //	if ((image.Width != 265 )||(image.Height != 45 )) //图片的规格是：300 * 250 
                //	{
                //		Response.Write ("<script>javascript:alert('对不起，你输入的图片规格不合，上传失败！')</script>");
                //		return;
                //	}
                //	else
                //	{
                TmpFile = "<IMG src=\"" + TmpFile + "\">";
                image.Dispose();
                return TmpFile;
            }
        }
    }

    bool imageAbort()
    {
        return false;
    }


    //****************************** 图片改名 + upload *************************************
    public void AdjPhoto()
    {
        Random Rnd1 = new Random();
        double dbl = Rnd1.Next();
        dbl = Rnd1.Next();
        //tmpLogo.Value = dbl.ToString() + fileExtension; //改名
        //TmpFile = rootDir + TmpDir.Value + "/" + tmpLogo.Value; //上传完整文件路径

        Type_TypeTree typeTree = new Type_TypeTree();
        typeTree.Init(int.Parse(strTypeTreeID));
        string TypeTreePictureURL = typeTree.TypeTreePictureURL;

        if (TypeTreePictureURL == "") //默认
        {
            TypeTreePictureURL = "/UploadImages/";
        }

        TmpFile = TmpFile.Replace("/", "//");
        TmpFile = Server.MapPath(TmpFile);

        file1.PostedFile.SaveAs(TmpFile); //上传

    }


    //******************************建立临时目录****************************
    public void BuildTMP()
    {
        /*Random Rnd1 = new Random();
        double dbl = Rnd1.Next();
        dbl = Rnd1.Next();
        if (TmpDir.Value  =="")
        {
            //TmpDir.Value = "/Updata_Pictures/tmp" + dbl.ToString();
            //DelDir = "Tmp/tmp" + dbl.ToString() ;
            //TMPpath = rootDir + TmpDir.Value + "/";
            TMPpath = "/Updata_Pictures/";
            if (!System.IO.Directory.Exists(TMPpath))
            {
                System.IO.Directory.CreateDirectory(TMPpath);
                // Response.Write("<script>javascript:parent.document.location = 'BodyFrame.aspx?id=" +dbl.ToString() + "'</script>");
            }
        }
        TMPpath =  rootDir + "Updata_Pictures/";
        System.IO.Directory.CreateDirectory(TMPpath);*/
    }
}
