//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-7-9
//
// 功能描述:页面的基类管理
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

namespace GCMS.PageCommonClassLib
{
    public class PageBase : System.Web.UI.Page
    {
        /// <summary>
        /// 获取当前页面的区域设置
        /// </summary>
        private string CurrentCulture
        {
            get
            {
                string currentCulture = string.Empty;
                currentCulture = ConfigurationManager.AppSettings["Language"];
                return currentCulture;
            }
        }

        /// <summary>
        /// 获取当前程序的验证状态
        /// </summary>
        public EnumTypes.CopyAuthState CopyAuthState
        {
            get
            {
                EnumTypes.CopyAuthState copyAuthState = EnumTypes.CopyAuthState.Illegal;
                if (Authenticator.IsLegalCopy())
                {
                    copyAuthState = EnumTypes.CopyAuthState.Normal;
                }
                return copyAuthState;
            }
        }
        public string GetInfo(string key)
        {
            Language lang = new Language(CurrentCulture);
            return lang.GetResource(key);
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string GCMSUserName
        {
            get
            {
                string uname = string.Empty;//#此处含有测试时使用数据,正式发布时注意#
                return uname;
            }
        }

        #region 重载基类页面事件函数
        /// <summary>
        /// 显示前的预处理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            if (!AuthSession())
            {
                if (SessionAtuhFaiedEvent != null)
                {
                    SessionAtuhFaiedEvent();
                }
                return;
            }
            //base.OnPreRender(e);//#此处含有测试时使用数据,正式发布时注意#
        }

        /// <summary>
        /// 错误预处理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnError(EventArgs e)
        {
            base.OnError(e);//#此处含有测试时使用数据,正式发布时注意#
        }
        #endregion 重载基类页面事件函数

        #region 页面自定义代理
        //Session验证的事件委托
        public delegate void SessionAuthHandler();
        //参数获取的事件委托
        public delegate void ParameterAuthHandler(string key);
        //Session验证失败事件
        public event SessionAuthHandler SessionAtuhFaiedEvent;
        //Session或QueryString参数获取失败事件
        public event ParameterAuthHandler SessionOrQueryGetFaiedEvent;
        #endregion 页面自定义代理

        protected bool AuthSession()
        {
            bool res=Session["Master_UserName"] == null || Session["Master_ID"] == null || Session["Roles"] == null;
            return !res;
        }

        /// <summary>
        /// 从Session中获取参数,当默认值为Null时，获取失败会触发SessionOrQueryGetFaied事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        protected string GetSession(string key, string defaultValue)
        {
            if (Session[key] != null)
            {
                return Session[key].ToString();
            }
            else
            {
                if (SessionOrQueryGetFaiedEvent != null && defaultValue == null)
                {
                    SessionOrQueryGetFaiedEvent(key);
                }
                return defaultValue;
            }
        }
        /// <summary>
        /// 从QueryString中获取参数,当默认值为Null时，获取失败会触发SessionOrQueryGetFaied事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        protected string GetQueryString(string key, string defaultValue)
        {
            if (this.Request.QueryString[key] != null)
            {
                return Request.QueryString[key].ToString();
            }
            else
            {
                if (SessionOrQueryGetFaiedEvent != null && defaultValue == null)
                {
                    SessionOrQueryGetFaiedEvent(key);
                }
                return defaultValue;
            }
        }
    }
}
