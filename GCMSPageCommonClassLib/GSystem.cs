﻿//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-8-31
//
// 功能描述:系统参数和状态的操作类
//
// 已修改问题:
// 未修改问题:
// 修改记录
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using GCMSClassLib.Public_Cls;
using System.IO;
namespace GCMS.PageCommonClassLib
{
    public class GSystem
    {
        public static EnumTypes.SystemStates SystemState = EnumTypes.SystemStates.Normal;
        public static string GetSysteStateMsg()
        {
            string msg = string.Empty;
            switch(GSystem.SystemState)
            {
                case EnumTypes.SystemStates.Normal:
                    msg = "欢迎使用GCMS 2008系统，该系统可以实现内容动态管理和静态发布";
                    break;
                case EnumTypes.SystemStates.Overtime:
                    msg = "<font Color='red'>超时或非法操作！</font>";
                    break;
                case EnumTypes.SystemStates.Nolicensed:
                    msg = "<font Color='red'>您使用的是未授权版本，请联系古美公司购买正版系统或进行技术支持  <br/><a href='http://www.gomye.net ' target='_blank'>www.gomye.net</a></font>";
                    break;
            }
            return msg;
        }
        /// <summary>
        /// 载入系统的脚本模板
        /// </summary>
        /// <param name="url"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static string LoadTemplate(string url,Dictionary<string,string> strParam)
        {
            StringBuilder txt = new StringBuilder();
            string path = Tools.FilesUrl(url);
            using (StreamReader sr = new StreamReader(path))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    txt.AppendLine(line);
                }
                sr.Close();
            }
            //将形如$id$的参数，换成实际值
            foreach (KeyValuePair<string, string> p in strParam)
            {
                txt = txt.Replace(string.Format("${0}$", p.Key), p.Value);
            }
            return txt.ToString();
        }

    }
}
