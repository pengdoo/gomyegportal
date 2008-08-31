//------------------------------------------------------------------------------
// ������ʶ: Copyright (C) 2008 Gomye.com.cn ��Ȩ����
// ��������: Galen Mu ������ 2008-7-9
//
// ��������:�����԰�ʵ�֣����԰����嶨��ΪLanguagePack�еĶ�Ӧresx�ļ�
//
// ���޸�����:
// δ�޸�����:
// �޸ļ�¼
//       1   2008-7-10 ���ע��
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Globalization;
using Gomye.CommonClassLib.Data;
using System.Resources;

namespace GCMS.PageCommonClassLib
{
    public class Language
    {
        private string resourcefile;
        /// <summary>
        /// ��ʼ����ǰ����
        /// </summary>
        /// <param name="cultureStr"></param>
        public Language(string cultureStr)
        {
            switch (cultureStr)
            {
                case "zh-Chs":
                    resourcefile = "zh-Chs";
                    break;
                case "en":
                    resourcefile = "en";
                    break;
                default:
                    resourcefile = cultureStr;
                    break;
            }
        }
        /// <summary>
        /// ��ȡ��Ӧָ�����԰��е��ı���Դ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public  string GetResource(string key)
        {

            //��ȡ��ǰ������
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            //��Դ�ĸ�����
            string resourceRootName = string.Format("GCMS.PageCommonClassLib.LanguagePack.{0}",resourcefile);
            //ʵ������Դ������
            ResourceManager resourceManager = new ResourceManager(resourceRootName, currentAssembly);
            string res = string.Empty;
            res = resourceManager.GetString(key);
            return res;
  
        }
    }
}
