#region Framework Classes Used
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing;
using System.Collections;
#endregion
namespace WebControlToolsbar 
{
	[DefaultProperty("ImageNormal"), ToolboxData("<{0}:Toolsbar runat=server></{0}:Toolsbar>")]
	public class Toolsbar : System.Web.UI.WebControls.WebControl, IPostBackEventHandler, IPostBackDataHandler 
	{
		#region Events
		public event System.EventHandler ButtonClick;
		#endregion
		#region Properties
		private string _confirmMessage;
		private string _imageNormal;
		private string _imageOnMouseOver;
		private string _imageOnMouseClick;
		private string _imageDisabled;
		private string _allText;
		private string _redirectURL;
		private string _Text;

		private bool _confirmClick = false;

		[Bindable(false), Category("Appearance"),Description("Confirm pop-up message text."),DefaultValue("")]
		public string ConfirmMessage 
		{
			get { return _confirmMessage; }
			set { _confirmMessage = value; }
		}
		[Bindable(false), Category("Appearance"),Description("Main/Default image to display."),DefaultValue("")]
		public string ImageNormal 
		{
			get { return _imageNormal; }
			set { _imageNormal = value; }
		}
		[Bindable(false), Category("Appearance"),Description("OnMouseOver image to display."),DefaultValue("")]
		public string ImageOnMouseOver 
		{
			get { return _imageOnMouseOver; }
			set { _imageOnMouseOver = value; }
		}
		[Bindable(false), Category("Appearance"),Description("OnMouseClick image to display."),DefaultValue("")]
		public string ImageOnMouseClick 
		{
			get { return _imageOnMouseClick; }
			set { _imageOnMouseClick = value; }
		}
		[Bindable(false), Category("Appearance"),Description("Disabled button image to display."),DefaultValue("")]
		public string ImageDisabled 
		{
			get { return _imageDisabled; }
			set { _imageDisabled = value; }
		}
		[Bindable(false), Category("Appearance"),Description("Alt text to appear on this image."),DefaultValue("")]
		public string AltText 
		{
			get { return _allText; }
			set { _allText = value; }
		}

		public string Text 
		{
			get { return _Text; }
			set { _Text = value; }
		}

		[Bindable(false), Category("Appearance"),Description("URL to redirect to"),DefaultValue("")]
		public string RedirectURL 
		{
			get { return _redirectURL; }
			set { _redirectURL = value; }
		}
		[Bindable(false), Category("Appearance"),Description("Adds a javascript confirmation pop-up to this button."),DefaultValue("")]
		public bool ConfirmClick 
		{
			get { return _confirmClick; }
			set { _confirmClick = value; }
		}
		#endregion
		protected override void OnInit(EventArgs e) 
		{
			const string RegistrationNameImageSwap = "ExtendedImageButton_JSwap";
			const string RegistrationNameConfirmMessage = "ExtendedImageButton_JConfirm";
			if (! this.Page.IsClientScriptBlockRegistered(RegistrationNameImageSwap))
			{
				const string sJSwapCode = "<script language='javascript' type='text/javascript'> " +
						  "<!-- \n" + 
						  "function newRollOver(targetDOMUrlName, URL) { \n" + 
						  "//alert(targetDOMUrlName + ' ' + URL); \n " + 
						  "var img=document.images; \n " + 
						  "var i=0; \n " + 
						  "for (i=0; i<img.length; i++) \n " + 
						  "if (img[i].name == targetDOMUrlName) img[i].src = URL; \n " + 
						  "} \n " + 
						  "//-->\n " + 
						  "</script>\n ";
				this.Page.RegisterClientScriptBlock(RegistrationNameImageSwap,sJSwapCode);
			}
			
			if (_confirmClick) 
			{
				if (! this.Page.IsClientScriptBlockRegistered(RegistrationNameConfirmMessage))
				{
					string sJSConfirmCode = "<script language='javascript' type='text/javascript'> " +
						"<!-- \n" +
						"function __doConfirm(btnWaiter) { \n" +
						"if (confirm('" + _confirmMessage + "')) { \n" +
						"document.body.style.cursor=\"wait\"; \n" +
						"return true; \n" +
						"} return false; } \n" +
						"//--> \n" +
						"</script> \n";
					//"btnWaiter.setAttribute('value', 'Please Wait...'); \n" +
					this.Page.RegisterClientScriptBlock(RegistrationNameConfirmMessage,sJSConfirmCode);
				}
			}
		}
		protected override void Render(HtmlTextWriter output) 
		{
			if (this.Enabled)
			{
				output.WriteBeginTag("td class='coolButton' width='"+this.Width.ToString()+"' height='"+this.Height.ToString()+"'");
				if (_redirectURL != String.Empty)
					output.WriteAttribute("onclick", _redirectURL);
				else 
					output.WriteAttribute("onclick", "javascript:" + this.Page.GetPostBackEventReference(this, "ButtonClick"));
				if (_confirmClick)
					output.WriteAttribute("onclick", "javascript:return __doConfirm(this);");
				if (this._allText != String.Empty)
					output.WriteAttribute("title", _allText);

				output.Write(">");
			}

			output.WriteBeginTag("img");
			if ((!this.Enabled) && (_imageDisabled != String.Empty))
			{
				output.WriteAttribute("src", _imageDisabled);
			}
			else
			{
				output.WriteAttribute("src", _imageNormal);
			}
			output.WriteAttribute("name", this.UniqueID);

			if (! this.BorderWidth.IsEmpty)
				output.WriteAttribute("border", this.BorderWidth.ToString());
			else
				output.WriteAttribute("border", "0");
			if (! this.BorderColor.IsEmpty)
				output.WriteAttribute("bordercolor", this.BorderColor.ToArgb().ToString());
			if (this.Style.Count > 0) 
			{
				IEnumerator keys = this.Style.Keys.GetEnumerator();
				string sStyles="";
				while (keys.MoveNext()) 
				{
					sStyles += (string)keys.Current + ":" + this.Style[(string)keys.Current ].ToString() + ";";
				}
				output.WriteAttribute("style", sStyles);
			}
//			if (this.CssClass != String.Empty)
//				output.WriteAttribute("class", this.CssClass);
//			if (! this.Height.IsEmpty)
//				output.WriteAttribute("Height", this.Height.ToString());
//			if (! this.Width.IsEmpty)
//				output.WriteAttribute("Width", this.Width.ToString());

			if (this._allText != String.Empty)
				output.WriteAttribute("alt", _allText);


//			if (this.Enabled)
//			{
//				if (this._imageOnMouseOver != String.Empty)
//					output.WriteAttribute("OnMouseOver", "javascript:newRollOver('" + this.UniqueID + "', '" + ImageOnMouseOver + "')");
//				if (this._imageNormal != String.Empty)
//					output.WriteAttribute("OnMouseOut", "javascript:newRollOver('" + this.UniqueID + "', '" + ImageNormal + "')");
//				if (this._imageOnMouseClick != String.Empty)
//					output.WriteAttribute("OnMouseDown", "javascript:newRollOver('" + this.UniqueID + "', '" + ImageOnMouseClick + "')");
//			}
			output.Write("/>");

			if (this._Text != String.Empty)
			output.Write(" "+_Text);

			if (this.Enabled)
			{
				output.WriteEndTag("td");
			}
		}
		public Toolsbar()
		{
			this._imageNormal = String.Empty;
			this._imageOnMouseOver = String.Empty;
			this._imageOnMouseClick = String.Empty;
			this._imageDisabled = String.Empty;
			this._allText = String.Empty;
			this._redirectURL = String.Empty;
			this._confirmMessage = "Do you confirm this action?";
		}
		#region PostBack
		public void RaisePostBackEvent(string eventArgument) 
		{
			string eventArgName = Page.Request.Form["__EVENTARGUMENT"];
			if (eventArgName == "ButtonClick")
				ButtonClick(this, System.EventArgs.Empty);
		}
		public void RaisePostDataChangedEvent() 
		{
		}
		public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection) 
		{
			return false;
		}
		#endregion
		#region ViewState
		protected override void TrackViewState() 
		{
			base.TrackViewState ();
		}
		protected override void LoadViewState(object savedState) 
		{
			base.LoadViewState (savedState);
		}
		protected override object SaveViewState() 
		{
			return base.SaveViewState ();
		}
		#endregion
	}
}