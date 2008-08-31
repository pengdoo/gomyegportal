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

public partial class Gomye_Tools_Tools_PageHeader : System.Web.UI.UserControl
{
        #region private
		private string _Value;
		private string _Mod;
		#endregion
		#region public property


		/// <summary>
		/// 初始化编辑器的内容
		/// </summary>
		public string Value
		{
			get {return _Value;}
			set {_Value = value;
			this.LabTitleText.Text = value;
			}
		}
		public string Mod
		{
			get {return _Mod;}
			set 
			{
				_Mod = value;
			}
		}
		#endregion
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (Mod == "3")
        {
            LeView.Visible = true;
            LeMain.Visible = false;
        }
        if (Mod == "2")
        {
            LeView.Visible = false;
            LeMain.Visible = true;
        }
    }
}
