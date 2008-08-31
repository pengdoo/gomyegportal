using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Gomye.CommonClassLib.FileAndDir
{
    public class DirectoryEx
    {
        public static int DirectoryName(string DirectoryPath)//��ȡ�ļ���������ȡ��\�� 
        {
            int j = 0; char[] c = DirectoryPath.ToCharArray();
            for (int i = c.Length - 1; i >= 0; i--)//�Ӻ����ȡ 
            {
                j = i;
                if (c[i] == '\\')
                {
                    break;//��"\"����,������"\"��λ�� 
                }
            }
            return j + 1;
        }

        public static void CopyDirectory(string DirectoryPath, string DirAddress)//�����ļ��У� 
        {
            #region//�ݹ�
            string s = DirectoryPath.Substring(DirectoryName(DirectoryPath));//��ȡ�ļ����� 
            if (Directory.Exists(DirAddress + "\\" + s))
            {
                Directory.Delete(DirAddress + "\\" + s, true);//���ļ��д��ڣ�����Ŀ¼�Ƿ�Ϊ�գ�ɾ�� 
                Directory.CreateDirectory(DirAddress + "\\" + s);//ɾ�������´����ļ��� 
            }
            else
            {
                Directory.CreateDirectory(DirAddress + "\\" + s);//�ļ��в����ڣ����� 
            }
            DirectoryInfo DirectoryArray = new DirectoryInfo(DirectoryPath);
            FileInfo[] Files = DirectoryArray.GetFiles();//��ȡ���ļ����µ��ļ��б� 
            DirectoryInfo[] Directorys = DirectoryArray.GetDirectories();//��ȡ���ļ����µ��ļ����б� 
            foreach (FileInfo inf in Files)//��������ļ� 
            {
                System.IO.File.Copy(DirectoryPath + "\\" + inf.Name, DirAddress + "\\" + s + "\\" + inf.Name);
            }
            foreach (DirectoryInfo Dir in Directorys)//�����ȡ�ļ������ƣ����ݹ���÷������� 
            {
                CopyDirectory(DirectoryPath + "\\" + Dir.Name, DirAddress + "\\" + s);
            }
            #endregion
        }

        /// <summary>
        /// ��ȡ�ļ���
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
        /// ��ȡ����ҳ����ļ���·��
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
        /// ��ȡ��չ��
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
