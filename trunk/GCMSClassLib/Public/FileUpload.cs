//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-7-8
//
// 功能描述: 上传文件
//
// 已修改问题:
// 未修改问题:
//      1 该类针对是HtmlInputFile ，asp 2.0中有专门的Fileupload可供替换
// 修改记录
//       1   2008-7-8 添加注释
//------------------------------------------------------------------------------
using System;
using System.Web;
using System.Drawing;
using System.Threading;


namespace GCMSClassLib.Public_Cls
{
    /// <summary>
    /// FileUpload 的摘要说明。
    /// 支持缩略图的图片上传类，若不传图片则无缩略图支持
    /// path里可以用 / 表示分割，其他不支持 ;\ 会自动转换为 /
    /// </summary>
    public class FileUpload
    {
        public static System.Web.UI.Page objPage;
        private string m_strDestPath = "";
        private string m_strThumbDestPath = "";
        private int m_nThumbWidth = 0;
        private int m_nThumbHeight = 0;
        private long m_nMaxSize = 0;
        private string m_strPreFixThumb = "";
        private static FileUpload m_objFileUpload;
        private static Object m_classLock = typeof(FileUpload);
        private int m_nThumbSize = 0; //等比例

        



        /// <summary>
        /// 路径格式转换为正确格式
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        private string PathFormat(string strPath)
        {
            string strReturn = strPath;
            if (strReturn.Length <= 0)
            {
                return "";
            }
            strReturn = strReturn.Replace("\\", "/");
            if (strReturn.Substring(strReturn.Length - 1) != "/")
            {
                strReturn += "/";
            }
            return strReturn;
        }

        #region 构造及相关函数
        public static FileUpload getInstance(long nMaxFileSize, string strDestPath)
        {
            objPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
            if (null == objPage)
            {
                return null;
            }
            lock (m_classLock)
            {
                if (null == m_objFileUpload)
                {
                    m_objFileUpload = new FileUpload(nMaxFileSize, strDestPath);
                }
            }
            return m_objFileUpload;
        }


        public static FileUpload getInstance(
            long nMaxFileSize,
            string strDestPath,
            string strThumbDestPath,
            int nThumbWidth,
            int nThumbHeight,
            string strPreFixThumb
            )
        {
            objPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
            if (null == objPage)
            {
                return null;
            }

            lock (m_classLock)
            {
                //				if(null==m_objFileUpload)
                //				{
                m_objFileUpload =
                    new FileUpload(
                    nMaxFileSize,
                    strDestPath,
                    strThumbDestPath,
                    nThumbWidth,
                    nThumbHeight,
                    strPreFixThumb
                    );
                //				}
            }
            return m_objFileUpload;

        }


        //等比例缩放 2006 3 15
        public static FileUpload getInstance(
            long nMaxFileSize,
            string strDestPath,
            string strThumbDestPath,
            int nThumbSize,
            string strPreFixThumb
            )
        {
            objPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
            if (null == objPage)
            {
                return null;
            }

            lock (m_classLock)
            {
                //				if(null==m_objFileUpload)
                //				{
                m_objFileUpload =
                    new FileUpload(
                    nMaxFileSize,
                    strDestPath,
                    strThumbDestPath,
                    nThumbSize,
                    strPreFixThumb
                    );
                //				}
            }
            return m_objFileUpload;

        }

        //等比例缩放 2006 3 15 早 6：40
        public FileUpload(long nMaxFileSize,
            string strDestPath,
            string strThumbDestPath,
            int nThumbSize,
            string strPreFixThumb)
        {
            m_strDestPath = PathFormat(strDestPath);
            m_nMaxSize = nMaxFileSize;
            m_strThumbDestPath = PathFormat(strThumbDestPath);
            m_nThumbSize = nThumbSize;
            m_strPreFixThumb = strPreFixThumb;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nMaxFileSize">限制最大文件大小</param>
        /// <param name="strDestPath">文件上传目的路径</param>
        public FileUpload(long nMaxFileSize, string strDestPath)
        {
            m_strDestPath = PathFormat(strDestPath);
            m_nMaxSize = nMaxFileSize;
        }

        /// <summary>
        /// 构造标准FileUpload对象
        /// </summary>
        /// <param name="nMaxFileSize">单位KB</param>
        /// <param name="strDestPath"></param>
        /// <param name="strThumbDestPath">缩略图路径，为空串时，不生成缩略图</param>
        /// <param name="nThumbWidth"></param>
        /// <param name="nThumbHeight"></param>
        /// <param name="strPreFixThumb">缩略图前缀</param>
        public FileUpload(long nMaxFileSize,
            string strDestPath,
            string strThumbDestPath,
            int nThumbWidth,
            int nThumbHeight,
            string strPreFixThumb)
        {
            m_strDestPath = PathFormat(strDestPath);
            m_nMaxSize = nMaxFileSize;
            m_strThumbDestPath = PathFormat(strThumbDestPath);
            m_nThumbHeight = nThumbHeight;
            m_nThumbWidth = nThumbWidth;
            m_strPreFixThumb = strPreFixThumb;

        }
        #endregion 构造及相关函数

        #region 上传和构造缩略图
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="strFileInputBoxID">上传页面中，上传控件的ID</param>
        /// <param name="strDestFileName"></param>
        /// <param name="nSrcFileSize"></param>
        /// <param name="strSrcFileName"></param>
        /// <param name="strSrcFileExt"></param>
        /// <param name="strError">错误信息</param>
        /// <returns></returns>
        public bool Upload(string strFileInputBoxID,
            string strDestFileName,
            out long nSrcFileSize,
            out string strSrcFileName,
            out string strSrcFileExt,
            out string strError)
        {
            nSrcFileSize = 0;
            strSrcFileName = "";
            strSrcFileExt = "";
            strError = "";
            System.Web.UI.HtmlControls.HtmlInputFile objFileCtrl = (System.Web.UI.HtmlControls.HtmlInputFile)objPage.FindControl(strFileInputBoxID);
            if (null == objFileCtrl)
            {
                strError = "没有页对象PAGE";
                return false;
            }
            if (objFileCtrl.PostedFile.ContentLength == 0)
            {
                strError = "上传文件内容为空";
                return false;
            }

            if (m_nMaxSize <= nSrcFileSize && m_nMaxSize > 0)
            {
                strError = "上传文件太大:" + m_nMaxSize.ToString() + " < " + nSrcFileSize.ToString();
                return false;
            }
            nSrcFileSize = (objFileCtrl.PostedFile.ContentLength / 1024);
            strSrcFileName = objFileCtrl.PostedFile.FileName;
            strSrcFileExt = strSrcFileName.Substring(strSrcFileName.LastIndexOf(".") + 1);
            try
            {
                objFileCtrl.PostedFile.SaveAs(m_strDestPath + strDestFileName + "." + strSrcFileExt);
            }
            catch
            {
                strError = "上传文件保存失败，请查看[" + m_strDestPath + "]路径是否可写";
                return false;
            }
            int nSrcWidth, nSrcHeight, nSrcSize;
            if (m_strThumbDestPath.Length > 0)
            {
                if (false == GenerateThumb(m_strDestPath + strDestFileName + "." + strSrcFileExt,
                    m_strThumbDestPath,
                    m_strPreFixThumb + "_" + strDestFileName + "." + strSrcFileExt,
                    m_nThumbWidth,
                    m_nThumbHeight,
                    m_nThumbSize,
                    out nSrcWidth,
                    out nSrcHeight,
                    out nSrcSize,
                    out strError))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSrcFilePathName">文件源</param>
        /// <param name="strDestPath">缩略图路径</param>
        /// <param name="strDestFileName">小图文件名包括扩展名</param>
        /// <param name="nDestWidth">为0时以nDestHeight为标准</param>
        /// <param name="nDestHeight">为0时以nDestWidth为标准</param>
        /// <param name="strError">返回的错误信息</param>
        /// <returns></returns>
        private bool GenerateThumb(string strSrcFilePathName,
            string strDestPath,
            string strDestFileName,
            int nDestWidth,
            int nDestHeight,
            int nDestSize,
            out int nSrcWidth,
            out int nSrcHeight,
            out int nSrcSize,
            out string strError)
        {
            //'生成缩略图
            int nWidth = nDestWidth;
            int nHeight = nDestHeight;
            int nSize = nDestSize;
            nSrcWidth = 0;
            nSrcHeight = 0;
            nSrcSize = 0;
            strError = "";//#此处有文本,多语言化时注意#
            System.Drawing.Image objImg = System.Drawing.Image.FromFile(strSrcFilePathName);
            if (null == objImg)
            {
                strError = "原图不存在";
                return false;
            }

            nSrcWidth = objImg.Width;
            nSrcHeight = objImg.Height;

            if (0 == nSize)
            {
                if (0 == nWidth)
                {
                    nWidth = Convert.ToInt32((Convert.ToDouble(nSrcWidth) / Convert.ToDouble(nSrcHeight) * nHeight));
                }
                if (0 == nHeight)
                {
                    nHeight = Convert.ToInt32((Convert.ToDouble(nSrcHeight) / Convert.ToDouble(nSrcWidth) * nWidth));
                }

            }
            else
            {

                if (Convert.ToDouble(nSrcWidth) > (Convert.ToDouble(nSrcHeight)))//是否支持缩略图生成
                {
                    nHeight = Convert.ToInt32((Convert.ToDouble(nSrcHeight) / Convert.ToDouble(nSrcWidth) * nSize));
                    nWidth = nSize;

                }
                else
                {
                    nWidth = Convert.ToInt32((Convert.ToDouble(nSrcWidth) / Convert.ToDouble(nSrcHeight) * nSize));
                    nHeight = nSize;

                }
            }



            System.Drawing.Image objImgReturn;
            try
            {
                System.Drawing.Image.GetThumbnailImageAbort objFunctionCall = null;
                objImgReturn
                    = objImg.GetThumbnailImage(nWidth, nHeight, objFunctionCall, new System.IntPtr());

                objImgReturn.Save(strDestPath + strDestFileName);
            }
            catch
            {
                strError = "生成缩略图失败！确认路径可写[" + strDestPath + strDestFileName + "],预生成大小为 " + nWidth.ToString() + " x " + nHeight.ToString() + "  " + nSrcHeight.ToString() + " ";

                return false;
            }
            objImg.Dispose();
            objImgReturn.Dispose();
            return true;
        }
        #endregion 上传和构造缩略图
    }
}



//
//代码调用：
//
//Happy2006.Web.Utility.FileUpload objFile;
//
//if(!chkThumb.Checked)//是否支持缩略图生成
//{
//objFile=Happy2006.Web.Utility.FileUpload.getInstance(0,
//Server.MapPath(@"\Happy2006WebTest\TestFile"));
//}
//else
//{
//objFile=Happy2006.Web.Utility.FileUpload.getInstance(0,
//Server.MapPath(@"\Happy2006WebTest\TestFile"),
//@"D:\Happy2006\TestFile",120,0,"thumb");
//
//}
//   
//
//string strFileName;
//long nFileSize;
//string strError;
//string strFileExt;
//if (null!=objFile)
//{
//
//objFile.Upload("fileupload","xxxx",out nFileSize,out strFileName,out strFileExt,out strError);
//Response.Write(strError);
//}
