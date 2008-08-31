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

public partial class Content_Type_SelectURL : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       string oldValue,startPoint,extName;
        oldValue=Request.QueryString["oldValue"];
        startPoint = string.Empty;
        extName = string.Empty;
        if(!String.IsNullOrEmpty(oldValue))
        {
            startPoint=GetFilePath(oldValue);
            extName="."+GetFileType(oldValue);
        }
        this.startpoint.Value=startPoint;
        this.extname.Value = extName;
        this.namemethod.Value = oldValue;
    }

    private string GetFilePath(string oldValue)
    {
        string fulldir = oldValue.Trim();
        string res = string.Empty;
        if (string.IsNullOrEmpty(fulldir))
        {
            res = "/";
        }
        else if (fulldir.LastIndexOf('/') <= 0)
        {
            res = "/";
        }
        else
        {
            string temp;
            temp = fulldir.Substring(0,fulldir.LastIndexOf('/')+1);
            if (String.IsNullOrEmpty(temp))
            {
                res = "";
            }
            else
            {
                res = temp;
            }


        } 
        return res;
    }

    private string GetFileType(string FileName)
    {

        int pos = FileName.LastIndexOf('.');
        string res=string.Empty;
        if(pos>0)
        {
            res = FileName.Substring(pos + 1);
        }
        return res;
    }
}
