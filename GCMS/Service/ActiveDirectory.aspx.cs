//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-9-19
//
// 功能描述: 提供给客户端Js,AD用户信息
//
// 已修改问题:
// 未修改问题:
// 修改记录
//----------------------------------系统引用-------------------------------------
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
using GCMS.PageCommonClassLib;

public partial class Service_ActiveDirectory : GCMS.PageCommonClassLib.PageBase
{
    #region 自定义事件的注册和处理
    //订阅页面的自定义事件
    protected override void OnPreInit(EventArgs e)
    {
        //用户验证事件注册
        this.SessionAtuhFaiedEvent += new SessionAuthHandler(OnSessionAtuhFaiedEvent);
        //Session或QueryString获取失败事件注册
        this.SessionOrQueryGetFaiedEvent += new ParameterAuthHandler(OnSessionOrQueryGetFaiedEvent);
        base.OnPreInit(e);
    }

    /// <summary>
    /// 验证失败事件响应
    /// </summary>
    void OnSessionAtuhFaiedEvent()
    {
        GSystem.SystemState = EnumTypes.SystemStates.Overtime;
        Output(string.Empty);
        return;
    }
    /// <summary>
    /// Session或Query的访问失败默认响应
    /// </summary>
    /// <param name="key"></param>
    void OnSessionOrQueryGetFaiedEvent(string key)
    {
        //#未完成代码#
    }
    #endregion 自定义事件的注册和处理

    string Action
    {
        get
        {
            return this.GetQueryString("action", null);
        }
    }
    public string UserAuthName
    {

        get
        {
            string authType = Request.ServerVariables["AUTH_TYPE"].ToString();
            string authUser = string.Empty;
            //验证方式
            //NTLM Windows验证 用户名为系统用户名 例如 Administrator 
            //Negotiate 域验证 用户名为域用户名
            try
            {
                switch (authType)
                {
                    case "":
                        authUser = Request.ServerVariables["REMOTE_HOST"].ToString();
                        break;
                    case "Negotiate":
                        authUser = Request.ServerVariables["AUTH_USER"].ToString();
                        break;
                    case "NTLM":
                        authUser = Request.ServerVariables["AUTH_USER"].ToString();
                        break;
                    default:
                        authUser = Request.ServerVariables["AUTH_USER"].ToString();
                        break;
                }
                if (authUser.IndexOf(@"\") != -1)//去掉\前置域名或计算机名
                {
                    authUser = authUser.Substring(authUser.IndexOf(@"\") + 1).Trim();
                }
            }
            catch
            {
                authUser = "TestUser";
            }


            return authUser;
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        switch (Action)
        {
            case "GetUserName":
                Output(UserAuthName);
                break;
            case "GetAuthType":
                string authType = Request.ServerVariables["AUTH_TYPE"].ToString();
                Output(authType);
                break;
        }
    }

    void Output(string result)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);//这一行的代码可以让客户端不使用缓存，而从服务器重新读取
        Response.Write(result);
        //Response.Flush();
        //Response.Close();

    }
}

