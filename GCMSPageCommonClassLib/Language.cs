//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-7-9
//
// 功能描述:多语言包实现，语言包具体定义为LanguagePack中的对应resx文件
//
// 已修改问题:
// 未修改问题:
// 修改记录
//       1   2008-7-10 添加注释
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
        /// 初始化当前语言
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
        /// 获取对应指定语言包中的文本资源
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public  string GetResource(string key)
        {

            //获取当前主程序集
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            //资源的根名称
            string resourceRootName = string.Format("GCMS.PageCommonClassLib.LanguagePack.{0}",resourcefile);
            //实例化资源管理类
            ResourceManager resourceManager = new ResourceManager(resourceRootName, currentAssembly);
            string res = string.Empty;
            res = resourceManager.GetString(key);
            return res;
  
        }
    }
}
