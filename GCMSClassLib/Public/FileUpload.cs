//------------------------------------------------------------------------------
// ������ʶ: Copyright (C) 2008 Gomye.com.cn ��Ȩ����
// ��������: Galen Mu ������ 2008-7-8
//
// ��������: �ϴ��ļ�
//
// ���޸�����:
// δ�޸�����:
//      1 ���������HtmlInputFile ��asp 2.0����ר�ŵ�Fileupload�ɹ��滻
// �޸ļ�¼
//       1   2008-7-8 ���ע��
//------------------------------------------------------------------------------
using System;
using System.Web;
using System.Drawing;
using System.Threading;


namespace GCMSClassLib.Public_Cls
{
    /// <summary>
    /// FileUpload ��ժҪ˵����
    /// ֧������ͼ��ͼƬ�ϴ��࣬������ͼƬ��������ͼ֧��
    /// path������� / ��ʾ�ָ������֧�� ;\ ���Զ�ת��Ϊ /
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
        private int m_nThumbSize = 0; //�ȱ���

        



        /// <summary>
        /// ·����ʽת��Ϊ��ȷ��ʽ
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

        #region ���켰��غ���
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


        //�ȱ������� 2006 3 15
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

        //�ȱ������� 2006 3 15 �� 6��40
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
        /// <param name="nMaxFileSize">��������ļ���С</param>
        /// <param name="strDestPath">�ļ��ϴ�Ŀ��·��</param>
        public FileUpload(long nMaxFileSize, string strDestPath)
        {
            m_strDestPath = PathFormat(strDestPath);
            m_nMaxSize = nMaxFileSize;
        }

        /// <summary>
        /// �����׼FileUpload����
        /// </summary>
        /// <param name="nMaxFileSize">��λKB</param>
        /// <param name="strDestPath"></param>
        /// <param name="strThumbDestPath">����ͼ·����Ϊ�մ�ʱ������������ͼ</param>
        /// <param name="nThumbWidth"></param>
        /// <param name="nThumbHeight"></param>
        /// <param name="strPreFixThumb">����ͼǰ׺</param>
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
        #endregion ���켰��غ���

        #region �ϴ��͹�������ͼ
        /// <summary>
        /// �ϴ��ļ�
        /// </summary>
        /// <param name="strFileInputBoxID">�ϴ�ҳ���У��ϴ��ؼ���ID</param>
        /// <param name="strDestFileName"></param>
        /// <param name="nSrcFileSize"></param>
        /// <param name="strSrcFileName"></param>
        /// <param name="strSrcFileExt"></param>
        /// <param name="strError">������Ϣ</param>
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
                strError = "û��ҳ����PAGE";
                return false;
            }
            if (objFileCtrl.PostedFile.ContentLength == 0)
            {
                strError = "�ϴ��ļ�����Ϊ��";
                return false;
            }

            if (m_nMaxSize <= nSrcFileSize && m_nMaxSize > 0)
            {
                strError = "�ϴ��ļ�̫��:" + m_nMaxSize.ToString() + " < " + nSrcFileSize.ToString();
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
                strError = "�ϴ��ļ�����ʧ�ܣ���鿴[" + m_strDestPath + "]·���Ƿ��д";
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
        /// <param name="strSrcFilePathName">�ļ�Դ</param>
        /// <param name="strDestPath">����ͼ·��</param>
        /// <param name="strDestFileName">Сͼ�ļ���������չ��</param>
        /// <param name="nDestWidth">Ϊ0ʱ��nDestHeightΪ��׼</param>
        /// <param name="nDestHeight">Ϊ0ʱ��nDestWidthΪ��׼</param>
        /// <param name="strError">���صĴ�����Ϣ</param>
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
            //'��������ͼ
            int nWidth = nDestWidth;
            int nHeight = nDestHeight;
            int nSize = nDestSize;
            nSrcWidth = 0;
            nSrcHeight = 0;
            nSrcSize = 0;
            strError = "";//#�˴����ı�,�����Ի�ʱע��#
            System.Drawing.Image objImg = System.Drawing.Image.FromFile(strSrcFilePathName);
            if (null == objImg)
            {
                strError = "ԭͼ������";
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

                if (Convert.ToDouble(nSrcWidth) > (Convert.ToDouble(nSrcHeight)))//�Ƿ�֧������ͼ����
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
                strError = "��������ͼʧ�ܣ�ȷ��·����д[" + strDestPath + strDestFileName + "],Ԥ���ɴ�СΪ " + nWidth.ToString() + " x " + nHeight.ToString() + "  " + nSrcHeight.ToString() + " ";

                return false;
            }
            objImg.Dispose();
            objImgReturn.Dispose();
            return true;
        }
        #endregion �ϴ��͹�������ͼ
    }
}



//
//������ã�
//
//Happy2006.Web.Utility.FileUpload objFile;
//
//if(!chkThumb.Checked)//�Ƿ�֧������ͼ����
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
