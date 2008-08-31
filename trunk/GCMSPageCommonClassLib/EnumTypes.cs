//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-8-31
//
// 功能描述:存放系统各项静态定义的枚举值定义
//
// 已修改问题:
// 未修改问题:
// 修改记录
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace GCMS.PageCommonClassLib
{
    public class EnumTypes
    {
        public enum SystemStates { Normal = 1, Overtime = 3, Nolicensed = 5 };
        /// <summary>
        /// 当前拷贝的验证状态
        /// Illegal-非法,Normal-正常,Overtime-过期
        /// </summary>
        public enum CopyAuthState { Illegal = 0, Normal = 1, Overtime = 2 };
    }
}
