//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-7-9
//
// 功能描述:验证相关函数
//
// 已修改问题:
// 未修改问题:
// 修改记录
//       1   2008-7-10 添加注释
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web.Security;

namespace GCMS.PageCommonClassLib
{
    public class Authenticator
    {
        public static string GetSerialNumber()
        {
            string serial=string.Empty;
            serial=ConfigurationManager.AppSettings["IDString"];
            return serial;
        }
        /// <summary>
        /// 根据序列号判断是否是合法版本
        /// </summary>
        /// <returns></returns>
        public static bool IsLegalCopy()
        {
            bool IsLegalCopy = false;
            //获取当前域名
            string host = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToString();
            //预处理并生成加密序列号
            string authSerial = string.Format("GomyeGomye{0}.net.net", host);
            authSerial=FormsAuthentication.HashPasswordForStoringInConfigFile(authSerial, "MD5");
            //获取当前版本序列号并比较
            string serial = GetSerialNumber();
            if (serial == authSerial
                &&!string.IsNullOrEmpty(serial))
            {
                IsLegalCopy = true;
            }

            return IsLegalCopy;
        }
    }
}
