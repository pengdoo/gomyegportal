<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default_Welcome.ascx.cs" Inherits="Gomye_Tools_Default_Welcome" %>
<%@ Register TagPrefix="WebAppControls" TagName="Tools_PageHeader" Src="Tools_PageHeader.ascx" %>
		<script>
				function tick() {
				var hours, minutes, seconds, ap;
				var intHours, intMinutes, intSeconds;
				var today;
				today = new Date();
				intHours = today.getHours();
				intMinutes = today.getMinutes();
				intSeconds = today.getSeconds();
				if (intHours == 0) {
				hours = "12:";
				ap = "Midnight";
				} else if (intHours < 12) { 
				hours = intHours+":";
				ap = "A.M.";
				} else if (intHours == 12) {
				hours = "12:";
				ap = "Noon";
				} else {
				hours = intHours + ":";
				ap = "P.M.";
				}
				if (intMinutes < 10) {
				minutes = "0"+intMinutes+":";
				} else {
				minutes = intMinutes+":";
				}
				if (intSeconds < 10) {
				seconds = "0"+intSeconds+" ";
				} else {
				seconds = intSeconds+" ";
				} 
				timeString = hours+minutes+seconds+ap;
				Clock.innerHTML = timeString;
				window.setTimeout("tick();", 1000);
				}
				window.onload = tick;
		</script>
		
					<WEBAPPCONTROLS:TOOLS_PAGEHEADER id="PageHeader" runat="server" Value="欢迎使用古美系统..." MenuStatus="3" Mod="3"></WEBAPPCONTROLS:TOOLS_PAGEHEADER>
			<table style="WIDTH: 100%; HEIGHT: 100%" cellSpacing="0" cellPadding="0" border="0">
				<TBODY>
					<tr>
						<td valign="top" bgColor="#ffffff">
							<!-- 工具栏 over-->
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td borderColor="#666666" align="center"><span style="COLOR: #336699"><B>G</B>omye <B>C</B>ontent
											<B>M</B>anagement <B>S</B>ystem 2008 V1.01H&nbsp;|&nbsp;</span>
										<span style="FONT: menu">
											<span id="Clock"></span>
										</span>
									</td>
								</tr>
			</table>