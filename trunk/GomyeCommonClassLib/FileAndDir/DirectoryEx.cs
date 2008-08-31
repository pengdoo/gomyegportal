using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Gomye.CommonClassLib.FileAndDir
{
    public class DirectoryEx
    {
        public static int DirectoryName(string DirectoryPath)//获取文件夹名，截取“\” 
        {
            int j = 0; char[] c = DirectoryPath.ToCharArray();
            for (int i = c.Length - 1; i >= 0; i--)//从后面截取 
            {
                j = i;
                if (c[i] == '\\')
                {
                    break;//遇"\"调处,并返回"\"的位置 
                }
            }
            return j + 1;
        }

        public static void CopyDirectory(string DirectoryPath, string DirAddress)//复制文件夹， 
        {
            #region//递归
            string s = DirectoryPath.Substring(DirectoryName(DirectoryPath));//获取文件夹名 
            if (Directory.Exists(DirAddress + "\\" + s))
            {
                Directory.Delete(DirAddress + "\\" + s, true);//若文件夹存在，不管目录是否为空，删除 
                Directory.CreateDirectory(DirAddress + "\\" + s);//删除后，重新创建文件夹 
            }
            else
            {
                Directory.CreateDirectory(DirAddress + "\\" + s);//文件夹不存在，创建 
            }
            DirectoryInfo DirectoryArray = new DirectoryInfo(DirectoryPath);
            FileInfo[] Files = DirectoryArray.GetFiles();//获取该文件夹下的文件列表 
            DirectoryInfo[] Directorys = DirectoryArray.GetDirectories();//获取该文件夹下的文件夹列表 
            foreach (FileInfo inf in Files)//逐个复制文件 
            {
                System.IO.File.Copy(DirectoryPath + "\\" + inf.Name, DirAddress + "\\" + s + "\\" + inf.Name);
            }
            foreach (DirectoryInfo Dir in Directorys)//逐个获取文件夹名称，并递归调用方法本身 
            {
                CopyDirectory(DirectoryPath + "\\" + Dir.Name, DirAddress + "\\" + s);
            }
            #endregion
        }

        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileName(string path)
        {
            string tmp=path.Replace('\\','/').Trim();
            int pos=tmp.LastIndexOf('/');
            
            string res = string.Empty;
            if (pos > 0)
            {
                res = tmp.Substring(pos+1);
            }
            return res;
        }

        /// <summary>
        /// 获取连接页面的文件夹路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFolderPath(string path)
        {
            string tmp = path.Replace('\\', '/').Trim();
            int pos = tmp.LastIndexOf('/');

            string res = string.Empty;
            if (pos > 0)
            {
                res = tmp.Substring(0,pos+1);
            }
            return res;
        }
        /// <summary>
        /// 获取扩展名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileExName(string path)
        {
            string tmp = path.Trim();
            int pos = tmp.LastIndexOf('.');

            string res = string.Empty;
            if (pos > 0)
            {
                res = tmp.Substring(pos);
            }
            return res;
        }

    }
}
