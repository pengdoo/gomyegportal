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

public partial class Gomye_Tools_EditorControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region private
    private string _MenuStatus;
    private string _Value;
    private string _CssPath;
    private string _Parame;
    #endregion
    #region public property
     public string MenuStatus
		{
			get {return _MenuStatus;}
			set {_MenuStatus = value;}
		}

		/// <summary>
		/// 初始化编辑器的内容
		/// </summary>
		public string Value
		{
			get {return _Value;}
			set {_Value = value;}
		}

		/// <summary>
		/// 编辑器内容默认加载的样式
		/// </summary>
		public string CssPath
		{
			get {return _CssPath;}
			set {_CssPath = value;}
		}

		/// <summary>
		/// URL参数
		/// </summary>
		public string Parame
		{
			get {return _Parame;}
			set {_Parame = value;}
		}

		#endregion

		// 重写输出HTML文本
     	protected override void Render(HtmlTextWriter output) 
		{
			string src= "GomyeEditor/Editor.html?id=content1";
			output.Write("<!-- 编辑器控件 BEGIN (Gomye.) -->");
			output.Write("<script language=\"javascript\" type=\"text/javascript\">\n");
			output.Write("var config = new Object() ;\n");
			output.Write("config.MenuStatus = \"" + this.MenuStatus + "\";\n");
			output.Write("config.CssPath = \"" + this.CssPath + "\" ;");
			output.Write("config.Version = \"1.1.0\" ;");
			output.Write("config.ReleaseDate = \"2004-05-20\" ;");
			output.Write("config.License = \"Seasky Studio.\" ;");
			output.Write("config.StyleName = \"standard_blue\";");
			output.Write("config.StyleEditorHeader = \"\" ;");
			output.Write("config.StyleMenuHeader = \"<head><link href='Style/MenuArea.css' type='text/css' rel='stylesheet'></head><body scroll='no' onConTextMenu='event.returnValue=false;'>\";");
			output.Write("config.StyleDir = \"standard\";");
			output.Write("config.SysImage = \"Image\";");
			output.Write("config.Parame = \"" + this.Parame + "\";");
			output.Write("config.InitMode = \"EDIT\";");
			output.Write("config.AutoDetectPasteFromWord = true;\n");
			output.Write("</script>\n");
			base.Render(output);
			output.Write("<INPUT type=\"hidden\" name=\"content1\" value=\"" + this.Value + "\">");
			output.Write("<INPUT type=\"hidden\" name=\"FormCount\" value=\"0\">");
			output.Write("<IFRAME ID=\"SeaskyEditor1\" src=\"{0}\" frameborder=\"0\" scrolling=\"no\" width=\"100%\" height=\"100%\"></IFRAME>", src);
			output.Write("<!-- 编辑器控件 END (Gomye.) -->");
		}
	
}
