//------------------------------------------------------------------------------
// ������ʶ: Copyright (C) 2008 Gomye.com.cn ��Ȩ����
// ��������: Galen Mu ������ 2008-7-9
//
// ��������:��֤��غ���
//
// ���޸�����:
// δ�޸�����:
// �޸ļ�¼
//       1   2008-7-10 ���ע��
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
        /// �������к��ж��Ƿ��ǺϷ��汾
        /// </summary>
        /// <returns></returns>
        public static bool IsLegalCopy()
        {
            bool IsLegalCopy = false;
            //��ȡ��ǰ����
            string host = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToString();
            //Ԥ�������ɼ������к�
            string authSerial = string.Format("GomyeGomye{0}.net.net", host);
            authSerial=FormsAuthentication.HashPasswordForStoringInConfigFile(authSerial, "MD5");
            //��ȡ��ǰ�汾���кŲ��Ƚ�
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
