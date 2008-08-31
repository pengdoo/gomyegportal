using System.Diagnostics;
using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

namespace GCMSContentCreate
{
	[Guid("89BD820E-35EF-4065-A8CE-3FF34E5E0BB9")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class TemplateResponse
	{
		
		
		public string OutputBuffer;
		
		public void Clear()
		{
			OutputBuffer = "";
		}
		
		public void Output(string text)
		{
			OutputBuffer = OutputBuffer + text;
		}
		
	}
}
