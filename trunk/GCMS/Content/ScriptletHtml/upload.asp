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
  '  ��ϵͳ������ȡ�����ʹ���ʲô�ط�
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
  '  2002.1.23 �޸ģ�ϵͳ�Զ�ȡһ���ļ���
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
    SysLog.RecordLog LOGTYPE_FILE,"�ļ��ϴ�",LOGCATA_ERROR,CurrentLoginUser,"�ļ��ϴ�ʱ����:" & Err.Description
    Response.End
  End If
  
SysLog.RecordLog LOGTYPE_FILE,"�ļ��ϴ�",LOGCATA_INFO,CurrentLoginUser,"�ļ��ϴ��ɹ�:" & curPath & FileName
%>
<script language="javascript">
top.returnValue="<%=curPath%><%=FileName%>";
top.close();
</script>