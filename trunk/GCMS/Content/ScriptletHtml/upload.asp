<!--#include virtual="/Catalog/inc/UserManager.asp" -->
<%
Function getFileExtName(FileName)
    Dim pos
    pos = InStrRev(FileName, ".")
    If pos > 0 Then
        getFileExtName = Mid(FileName, pos + 1)
    Else
        getFileExtName = ""
    End If
End Function

On Error Resume Next
  Dim objUpload
  Set objUpload = Server.CreateObject("iTOMUpload.upload")

  Dim objVFS,curPath,physicalPath
  Set objVFS = Server.CreateObject("CatalogSystem.SiteFolder")
  
  '------------------------------------
  '  从系统配置里取出倒低存在什么地方
  '-----------------------------------
  curPath = Request.Cookies("curUploadDir")
  
  curPath = Replace(curPath,"\","/")
  If Right(curPath,1)<>"/" Then
    curPath = curPath & "/"
  End If
  
  objVFS.MakeFolderSure curPath
  physicalPath = objVFS.uriMap(curPath)
  
  On Error Resume Next

  '-------------------------------------
  '  2002.1.23 修改，系统自动取一个文件名
  '-------------------------------------
  Dim FileName,ExtName,OldName
  Dim comIDManager
  Set comIDManager = Server.CreateObject("CatalogSystem.comIDManager")
  OldName = objUpload.Files.Item(1).FileName
  ExtName = getFileExtName(oldName)
  FileName = "UploadFile" & comIDManager.NewID("UploadFile") & "." & ExtName
  
  objUpload.Files(1).SaveAs physicalPath & "\" & Cstr(FileName)
  
  If Err.Number<>0 Then
%>
<script language="javascript">
alert("<%=Err.description & Err.Source%>");
top.close();
</script>
<%
    SysLog.RecordLog LOGTYPE_FILE,"文件上传",LOGCATA_ERROR,CurrentLoginUser,"文件上传时出错:" & Err.Description
    Response.End
  End If
  
SysLog.RecordLog LOGTYPE_FILE,"文件上传",LOGCATA_INFO,CurrentLoginUser,"文件上传成功:" & curPath & FileName
%>
<script language="javascript">
top.returnValue="<%=curPath%><%=FileName%>";
top.close();
</script>