USE [GCMS]
GO
/****** 对象:  Table [dbo].[Content_Commend]    脚本日期: 09/11/2008 11:58:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Content_Commend](
	[Content_ID] [int] NULL,
	[TypeTree_ID] [int] NULL
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Content_Contact]    脚本日期: 09/11/2008 11:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Content_Contact](
	[Content_ID] [int] NULL,
	[Other_ID] [int] NULL,
	[Relative_ID] [int] NULL
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Content_Content]    脚本日期: 09/11/2008 11:58:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Content_Content](
	[Content_Id] [int] NOT NULL,
	[TypeTree_ID] [int] NULL,
	[Name] [varchar](255) NULL,
	[Author] [varchar](50) NULL,
	[KeyWord] [varchar](500) NULL,
	[Original] [varchar](50) NULL,
	[SubmitDate] [datetime] NULL,
	[Approver] [varchar](50) NULL,
	[ApproveDate] [datetime] NULL,
	[Status] [varchar](50) NULL,
	[Description] [text] NULL,
	[Clicks] [int] NULL,
	[OrderNum] [int] NULL,
	[PublishDate] [datetime] NULL,
	[Derivation] [varchar](50) NULL,
	[DerivationLink] [varchar](50) NULL,
	[Head_news] [char](1) NULL,
	[Picture_news] [char](1) NULL,
	[Picture_Notes] [varchar](2000) NULL,
	[Picture_Name] [varchar](50) NULL,
	[Picture_DName] [varchar](50) NULL,
	[URL] [varchar](200) NULL,
	[Lockedby] [varchar](50) NULL,
	[ReCount] [int] NULL,
	[Distillate] [int] NULL,
	[Commend] [int] NULL,
	[ContentType] [char](10) NULL,
	[User_id] [int] NULL,
	[AtTop] [int] NULL,
	[IsBallot] [int] NULL,
	[Album] [varchar](500) NULL,
	[content_xml] [text] NULL,
	[Content_PID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Content_ContentRemark]    脚本日期: 09/11/2008 11:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Content_ContentRemark](
	[Remark_ID] [int] NOT NULL,
	[Content_ID] [int] NOT NULL,
	[Remark_Name] [varchar](200) NULL,
	[Remark] [text] NULL,
	[Remark_Date] [datetime] NULL,
	[Status] [int] NULL,
	[Author] [varchar](50) NULL,
	[User_id] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Content_FieldsContent]    脚本日期: 09/11/2008 11:58:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Content_FieldsContent](
	[Fields_ID] [int] NULL,
	[FieldsName_ID] [int] NULL,
	[Property_Name] [varchar](50) NULL,
	[Property_Alias] [varchar](50) NULL,
	[Property_InputType] [varchar](50) NULL,
	[Property_InputOptions] [text] NULL,
	[Property_Order] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Content_FieldsName]    脚本日期: 09/11/2008 11:58:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Content_FieldsName](
	[FieldsName_ID] [int] NULL,
	[FieldsName_Name] [varchar](100) NULL,
	[FieldsName_State] [int] NULL,
	[FieldsBase_Name] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Content_ID]    脚本日期: 09/11/2008 11:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Content_ID](
	[ID_Name] [varchar](50) NULL,
	[ID_Number] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Content_Log]    脚本日期: 09/11/2008 11:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Content_Log](
	[Log_ID] [int] NULL,
	[Content_ID] [int] NULL,
	[Log_Txt] [text] NULL,
	[Log_Date] [datetime] NULL,
	[Log_Action] [varchar](500) NULL,
	[Master_ID] [int] NULL,
	[Master_Name] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Content_Master]    脚本日期: 09/11/2008 11:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Content_Master](
	[Master_ID] [int] NOT NULL,
	[Master_Name] [varchar](50) NULL,
	[Master_UserName] [varchar](50) NULL,
	[Master_Password] [varchar](50) NULL,
	[Master_Email] [varchar](50) NULL,
	[Master_Tel] [varchar](50) NULL,
	[Master_Usableness] [char](1) NULL,
	[Master_Note] [varchar](500) NULL,
	[Master_AddDate] [datetime] NULL,
	[Add_ID] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Content_Message]    脚本日期: 09/11/2008 11:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Content_Message](
	[id] [int] NOT NULL,
	[UserName] [char](50) NULL,
	[User_Message] [varchar](256) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Content_PhotoUpfile]    脚本日期: 09/11/2008 11:58:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Content_PhotoUpfile](
	[ID] [int] NULL,
	[User_ID] [int] NULL,
	[AD_Name] [nvarchar](50) NULL,
	[AD_Path] [ntext] NULL,
	[AD_State] [int] NULL,
	[AD_Size] [nvarchar](50) NULL,
	[AD_Type] [text] NULL,
	[UploadDate] [datetime] NULL,
	[UploadName] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Content_Property]    脚本日期: 09/11/2008 11:58:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Content_Property](
	[Value_ID] [int] NOT NULL,
	[Property_ID] [int] NULL,
	[Content_ID] [int] NULL,
	[BOOLEAN_VALUE] [int] NULL,
	[NUMBER_VALUE] [int] NULL,
	[DOUBLE_VALUE] [float] NULL,
	[STRING_VALUE] [nvarchar](255) NULL,
	[DATETIME_VALUE] [datetime] NULL,
	[TEXT_VALUE] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Content_Roles]    脚本日期: 09/11/2008 11:58:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Content_Roles](
	[Roles_ID] [int] NOT NULL,
	[Roles_Name] [varchar](50) NULL,
	[Roles_Explan] [varchar](500) NULL,
	[Add_ID] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Content_RolesConnect]    脚本日期: 09/11/2008 11:58:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Content_RolesConnect](
	[Roles_ID] [int] NULL,
	[TypeTree_ID] [int] NULL
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Content_RolesMaster]    脚本日期: 09/11/2008 11:58:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Content_RolesMaster](
	[Master_ID] [int] NOT NULL,
	[Roles_ID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Content_RolesPopedom]    脚本日期: 09/11/2008 11:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Content_RolesPopedom](
	[Roles_ID] [int] NULL,
	[Popedom_EName] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Content_Schema]    脚本日期: 09/11/2008 11:58:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Content_Schema](
	[Property_ID] [int] NOT NULL,
	[TypeTree_ID] [int] NULL,
	[Property_Name] [varchar](50) NULL,
	[Property_Alias] [varchar](50) NULL,
	[Property_Type] [varchar](50) NULL,
	[Property_InputType] [varchar](50) NULL,
	[Property_InputOptions] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Content_Search]    脚本日期: 09/11/2008 11:58:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Content_Search](
	[Search_id] [int] NULL,
	[Search_Name] [varchar](50) NULL,
	[Search_TypeTree] [varchar](5000) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Content_SearchConnect]    脚本日期: 09/11/2008 11:58:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Content_SearchConnect](
	[Search_ID] [int] NULL,
	[TypeTree_ID] [int] NULL
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Content_System]    脚本日期: 09/11/2008 11:58:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Content_System](
	[System_Name] [varchar](50) NULL,
	[System_Tools] [text] NULL,
	[JMail_MailServerUserName] [varchar](50) NULL,
	[JMail_MailServerPassWord] [varchar](50) NULL,
	[JMail_From] [varchar](50) NULL,
	[JMail_Server] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Content_Type_LinkPush]    脚本日期: 09/11/2008 11:58:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Content_Type_LinkPush](
	[Link_ID] [int] NOT NULL,
	[LinkName] [varchar](50) NULL,
	[TypeTree_ID] [int] NULL,
	[TypeTree_URL] [varchar](100) NULL,
	[TypeTree_Template] [varchar](100) NULL,
	[List_Amount] [int] NULL,
	[LinkType] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Content_Type_TypeTree]    脚本日期: 09/11/2008 11:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Content_Type_TypeTree](
	[TypeTree_ID] [int] NOT NULL,
	[TypeTree_ParentID] [int] NULL,
	[TypeTree_CName] [varchar](50) NULL,
	[TypeTree_EName] [varchar](50) NULL,
	[TypeTree_Explain] [varchar](500) NULL,
	[TypeTree_OrderNum] [int] NULL,
	[TypeTree_Issuance] [int] NULL,
	[TypeTree_URL] [varchar](100) NULL,
	[TypeTree_Template] [varchar](100) NULL,
	[TypeTree_PictureURL] [varchar](100) NULL,
	[TypeTree_ListTemplate] [varchar](100) NULL,
	[TypeTree_ListURL] [varchar](100) NULL,
	[List_amount] [int] NULL,
	[TypeTree_Images] [varchar](100) NULL,
	[TypeTree_Language] [varchar](100) NULL,
	[TypeTree_Type] [int] NULL,
	[TypeTree_XML] [text] NULL,
	[TypeTree_XMLContent] [text] NULL,
	[TypeTree_TypeFields] [int] NULL,
	[TypeTree_ContentFields] [int] NULL,
	[MailMsg] [varchar](50) NULL,
	[TypeTree_Show] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Content_Upload]    脚本日期: 09/11/2008 11:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Content_Upload](
	[File_ID] [int] NULL,
	[User_ID] [int] NULL,
	[Url] [varchar](200) NULL,
	[AddDate] [datetime] NULL,
	[Type] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Member_Collection]    脚本日期: 09/11/2008 11:59:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Member_Collection](
	[User_ID] [char](10) NULL,
	[Content_ID] [char](10) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Member_Info]    脚本日期: 09/11/2008 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Member_Info](
	[UserID] [int] NULL,
	[Photo] [varchar](100) NULL,
	[Website] [varchar](100) NULL,
	[Birthday] [datetime] NULL,
	[Shuxiang] [varchar](10) NULL,
	[Constellation] [varchar](10) NULL,
	[Province] [varchar](20) NULL,
	[City] [varchar](20) NULL,
	[County] [varchar](20) NULL,
	[QQ] [varchar](50) NULL,
	[MSN] [varchar](50) NULL,
	[Stature] [int] NULL,
	[Avoirdupois] [int] NULL,
	[Sex] [int] NULL,
	[Marriage] [int] NULL,
	[Album] [varchar](500) NULL,
	[Intro] [varchar](500) NULL,
	[Name] [varchar](50) NULL,
	[bloard] [varchar](10) NULL,
	[BSize] [varchar](50) NULL,
	[Company] [varchar](200) NULL,
	[IDCard] [varchar](20) NULL,
	[Mobile] [varchar](50) NULL,
	[ILike] [varchar](500) NULL,
	[Show_Info] [varchar](500) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Member_Roles]    脚本日期: 09/11/2008 11:59:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member_Roles](
	[RoleID] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](512) NOT NULL
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Member_RolesConnect]    脚本日期: 09/11/2008 11:59:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member_RolesConnect](
	[Roles_ID] [int] NULL,
	[TypeTree_ID] [int] NULL,
	[User_ID] [int] NULL
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Member_Setup]    脚本日期: 09/11/2008 11:59:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Member_Setup](
	[BadWords] [varchar](225) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Member_User]    脚本日期: 09/11/2008 11:59:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Member_User](
	[userid] [int] NOT NULL,
	[username] [varchar](50) NOT NULL,
	[PasswordFormat] [int] NULL,
	[password] [varchar](50) NULL,
	[question] [varchar](200) NULL,
	[answer] [varchar](200) NULL,
	[useremail] [varchar](50) NULL,
	[Mark_money] [int] NULL,
	[Mark_experience] [int] NULL,
	[Mark_people] [int] NULL,
	[Mark_literary] [int] NULL,
	[state] [int] NULL,
	[AddDate] [datetime] NULL,
	[LastDate] [datetime] NULL,
	[LastIP] [varchar](50) NULL,
	[LogonCount] [int] NULL,
	[NickName] [varchar](50) NULL,
	[telephone] [varchar](50) NULL,
	[userState] [int] NULL,
	[Click] [int] NULL,
	[IntroduceUserID] [int] NULL,
	[MaleLevel] [int] NULL,
	[DateCreated] [datetime] NULL,
	[LastLogin] [datetime] NULL,
	[LastActivity] [datetime] NULL,
	[LastAction] [varchar](1024) NULL,
	[Scores] [int] NULL,
	[RegisterIP] [varchar](128) NULL,
	[IsFormalAuthor] [bit] NULL,
	[EffectiveTime] [datetime] NULL,
	[PhotoLevel] [int] NULL,
	[Consignee_ID] [int] NULL,
	[UserFace] [varchar](200) NULL,
	[Sex] [int] NULL,
	[u_type] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Member_UsersInRoles]    脚本日期: 09/11/2008 11:59:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member_UsersInRoles](
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Upload_Files]    脚本日期: 09/11/2008 11:59:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Upload_Files](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [varchar](150) NULL,
	[FileSize] [int] NULL,
	[URL] [varchar](250) NULL,
	[UploadTime] [datetime] NULL,
	[UploadUser] [varchar](50) NULL,
	[IP] [varchar](50) NULL,
	[FileFrom] [int] NULL,
	[FileWidth] [int] NULL,
	[FileHeight] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  StoredProcedure [dbo].[p_Content_ExtendField_GetValue]    脚本日期: 09/11/2008 11:57:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------
--Copyright (C) 2008 Gomye.net 版权所有
--作者: 慕焘
--说明：获取某个扩展表的扩展字段值
--时间：2008-8-20 11:58:00
-----------------------------------------------
CREATE PROCEDURE [dbo].[p_Content_ExtendField_GetValue]
    @Content_ID int,
    @Words nvarchar(255),
	@TypeTree_ID int
AS

BEGIN 
  --获取自定义表名
   declare @BaseNames nvarchar(250)
   select Top 1 @BaseNames=max(FieldsBase_Name)
   from Content_FieldsName ,Type_TypeTree
   where TypeTree_ID=@TypeTree_ID and FieldsName_ID = TypeTree_ContentFields 
   
   --构造SQl
   declare @sql nvarchar(max)
   Set @sql='select '+ @Words +' from ContentUser_'+@BaseNames+' where content_Id = '+@Content_ID;
   print @sql
   exec(@sql)
END
GO
/****** 对象:  Table [dbo].[ContentUser_Test1]    脚本日期: 09/11/2008 11:59:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ContentUser_Test1](
	[Content_ID] [int] NULL,
	[TypeTree_id] [int] NULL,
	[Content_PId] [int] NULL,
	[Author] [varchar](50) NULL,
	[Status] [int] NULL,
	[Clicks] [int] NULL,
	[OrderNum] [int] NULL,
	[lockedby] [varchar](50) NULL,
	[User_ID] [int] NULL,
	[AtTop] [int] NULL,
	[PublishDate] [datetime] NULL,
	[SubmitDate] [datetime] NULL,
	[Url] [varchar](200) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[ContentUser_Test2]    脚本日期: 09/11/2008 11:59:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ContentUser_Test2](
	[Content_ID] [int] NULL,
	[TypeTree_id] [int] NULL,
	[Content_PId] [int] NULL,
	[Author] [varchar](50) NULL,
	[Status] [int] NULL,
	[Clicks] [int] NULL,
	[OrderNum] [int] NULL,
	[lockedby] [varchar](50) NULL,
	[User_ID] [int] NULL,
	[AtTop] [int] NULL,
	[PublishDate] [datetime] NULL,
	[SubmitDate] [datetime] NULL,
	[Url] [varchar](200) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[ContentUser_Comment]    脚本日期: 09/11/2008 11:59:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ContentUser_Comment](
	[Content_ID] [int] NULL,
	[TypeTree_id] [int] NULL,
	[Content_PId] [int] NULL,
	[Author] [varchar](50) NULL,
	[Status] [int] NULL,
	[Clicks] [int] NULL,
	[OrderNum] [int] NULL,
	[lockedby] [varchar](50) NULL,
	[User_ID] [int] NULL,
	[AtTop] [int] NULL,
	[PublishDate] [datetime] NULL,
	[SubmitDate] [datetime] NULL,
	[Url] [varchar](200) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  StoredProcedure [dbo].[p_Content_Content_S_Update]    脚本日期: 09/11/2008 11:57:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------
--Copyright (C) 2008 gomye.net 版权所有
--作者: 慕焘
--说明：修改一条记录
--时间：2008-8-25 9:56:14
-----------------------------------------------
CREATE PROCEDURE [dbo].[p_Content_Content_S_Update]
    @Content_Id int,
    @Name varchar(255),
    @KeyWord varchar(500),
    @Original varchar(50),
    @SubmitDate datetime,
    @Approver varchar(50),
    @Status varchar(50),
    @Description text,
    @Derivation varchar(50),
    @DerivationLink varchar(50),
    @Head_news char(1),
    @Picture_news char(1),
    @Picture_Notes varchar(2000),
    @Picture_Name varchar(50),
    @Picture_DName varchar(50),
    @URL varchar(200),
    @Lockedby varchar(50),
    @ContentType char(10),
    @IsBallot int,
    @Album varchar(500),
    @content_xml text
AS

UPDATE [Content_Content]
SET
    [Name]=@Name,
    [KeyWord]=@KeyWord,
    [Original]=@Original,
    [SubmitDate]=@SubmitDate,
    [Approver]=@Approver,
    [Status]=@Status,
    [Description]=@Description,
    [Derivation]=@Derivation,
    [DerivationLink]=@DerivationLink,
    [Head_news]=@Head_news,
    [Picture_news]=@Picture_news,
    [Picture_Notes]=@Picture_Notes,
    [Picture_Name]=@Picture_Name,
    [Picture_DName]=@Picture_DName,
    [URL]=@URL,
    [Lockedby]=@Lockedby,
    [ContentType]=@ContentType,
    [IsBallot]=@IsBallot,
    [content_xml]=@content_xml
WHERE
    [Content_Id]=@Content_Id
GO
/****** 对象:  StoredProcedure [dbo].[p_Content_Content_Delete]    脚本日期: 09/11/2008 11:57:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------
--Copyright (C) 2008 gomye.net 版权所有
--作者: 慕焘
--说明：删除一条记录
--时间：2008-8-25 9:56:14
-----------------------------------------------
CREATE PROCEDURE [dbo].[p_Content_Content_Delete]
    @Content_Id int
AS

DELETE Content_Content
WHERE 
    [Content_Id] = @Content_Id
GO
/****** 对象:  StoredProcedure [dbo].[p_Content_Content_Exists]    脚本日期: 09/11/2008 11:57:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------
--Copyright (C) 2008 gomye.net 版权所有
--作者: 慕焘
--说明：是否已经存在
--时间：2008-8-25 9:56:14
-----------------------------------------------
CREATE PROCEDURE [dbo].[p_Content_Content_Exists]
    @Content_Id int
AS

DECLARE @TempID int
SELECT @TempID = count(1) FROM Content_Content WHERE [Content_Id] = @Content_Id
IF @TempID = 0
    RETURN 0
ELSE
    RETURN 1
GO
/****** 对象:  StoredProcedure [dbo].[p_Content_Content_S_GetModel]    脚本日期: 09/11/2008 11:57:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------
--Copyright (C) 2008 gomye.net 版权所有
--作者: 慕焘
--说明：得到一个实体
--时间：2008-8-25 9:56:14
-----------------------------------------------

CREATE PROCEDURE [dbo].[p_Content_Content_S_GetModel]
    @Content_Id int
AS

select 
Content_Id,
TypeTree_ID,
Name,
Author,
SubmitDate,
Approver, 
isnull(ApproveDate,'') ApproveDate ,
Status,
Description,
Clicks,
OrderNum,
PublishDate,
Derivation,
DerivationLink,
Head_news,
Picture_news,
Picture_Notes,
Picture_Name,
Picture_DName,
Url,KeyWord,
Original,
isnull(ContentType,'') ContentType,
isnull(ReCount,'0') ReCount,
isnull(User_ID,'0') User_ID,
isnull(Distillate,'0') Distillate ,
isnull(Commend,'0') Commend ,
isnull(AtTop,'0') AtTop,
isnull(IsBallot,'0') IsBallot,
isnull(Content_Xml,'') Content_Xml,
isnull(Content_PID,'0') Content_PID 
from 
Content_Content 
where Content_Id=@Content_Id
GO
/****** 对象:  StoredProcedure [dbo].[p_Content_Content_GetList]    脚本日期: 09/11/2008 11:57:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------
--Copyright (C) 2008 gomye.net 版权所有
--说明：得到所有实体
--时间：2008-8-25 9:56:14
-----------------------------------------------

CREATE PROCEDURE [dbo].[p_Content_Content_GetList]
AS

SELECT
    Content_Id,
    TypeTree_ID,
    Name,
    Author,
    KeyWord,
    Original,
    SubmitDate,
    Approver,
    ApproveDate,
    Status,
    Description,
    Clicks,
    OrderNum,
    PublishDate,
    Derivation,
    DerivationLink,
    Head_news,
    Picture_news,
    Picture_Notes,
    Picture_Name,
    Picture_DName,
    URL,
    Lockedby,
    ReCount,
    Distillate,
    Commend,
    ContentType,
    User_id,
    AtTop,
    IsBallot,
    Album,
    content_xml,
    Content_PID
FROM
    Content_Content
GO
/****** 对象:  StoredProcedure [dbo].[p_Content_FieldsContent_S_Update]    脚本日期: 09/11/2008 11:58:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------
--Copyright (C) 2008 gomye.com 版权所有
--说明：修改一条记录
--时间：2008-8-20 11:58:00
-----------------------------------------------
CREATE PROCEDURE [dbo].[p_Content_FieldsContent_S_Update]
    @Fields_ID int,
    @Property_Name varchar(50),
    @Property_Alias varchar(50),
    @Property_InputType varchar(50),
    @Property_InputOptions text
AS

UPDATE [Content_FieldsContent]
SET

    [Property_Name]=@Property_Name,
    [Property_Alias]=@Property_Alias,
    [Property_InputType]=@Property_InputType,
    [Property_InputOptions]=@Property_InputOptions

WHERE
    [Fields_ID]=@Fields_ID
GO
/****** 对象:  StoredProcedure [dbo].[p_Content_FieldsContent_Delete]    脚本日期: 09/11/2008 11:57:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------
--Copyright (C) 2008 gomye.com版权所有
--说明：删除一条记录
--时间：2008-8-20 11:58:00
-----------------------------------------------
CREATE PROCEDURE [dbo].[p_Content_FieldsContent_Delete]
    @Fields_ID int
AS

DELETE Content_FieldsContent
WHERE 
    [Fields_ID] = @Fields_ID
GO
/****** 对象:  StoredProcedure [dbo].[p_Content_FieldsContent_Exists]    脚本日期: 09/11/2008 11:57:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------
--Copyright (C) 2008 gomye.net 版权所有
--说明：是否已经存在
--时间：2008-8-20 11:58:00
-----------------------------------------------
CREATE PROCEDURE [dbo].[p_Content_FieldsContent_Exists]
    @Fields_ID int
AS

DECLARE @TempID int
SELECT @TempID = count(1) FROM Content_FieldsContent WHERE [Fields_ID] = @Fields_ID
IF @TempID = 0
    RETURN 0
ELSE
    RETURN 1
GO
/****** 对象:  StoredProcedure [dbo].[p_Content_FieldsContent_GetModel]    脚本日期: 09/11/2008 11:57:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------
--Copyright (C) 2008 Socansoft.com 版权所有
--说明：得到一个实体
--时间：2008-8-20 11:58:00
-----------------------------------------------

CREATE PROCEDURE [dbo].[p_Content_FieldsContent_GetModel]
    @Fields_ID int
AS

SELECT
    Fields_ID,
    FieldsName_ID,
    Property_Name,
    Property_Alias,
    Property_InputType,
    Property_InputOptions,
    Property_Order
FROM
    Content_FieldsContent
WHERE
    [Fields_ID] = @Fields_ID
GO
/****** 对象:  StoredProcedure [dbo].[p_Content_FieldsContent_GetList]    脚本日期: 09/11/2008 11:57:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------
--Copyright (C) 2008 gomye.net 版权所有
--说明：得到所有实体
--时间：2008-8-20 11:58:00
-----------------------------------------------

CREATE PROCEDURE [dbo].[p_Content_FieldsContent_GetList]
AS

SELECT
    Fields_ID,
    FieldsName_ID,
    Property_Name,
    Property_Alias,
    Property_InputType,
    Property_InputOptions,
    Property_Order
FROM
    Content_FieldsContent
GO
/****** 对象:  StoredProcedure [dbo].[p_Content_FieldsContent_S_GetModelByProperty]    脚本日期: 09/11/2008 11:57:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------
--Copyright (C) 2008 gomye.com版权所有
--说明：得到一个实体
--时间：2008-8-20 11:58:00
-----------------------------------------------

CREATE PROCEDURE [dbo].[p_Content_FieldsContent_S_GetModelByProperty]
    @FieldsName_ID int,
     @Property_Name nvarchar(100)
AS

SELECT
    Fields_ID,
    FieldsName_ID,
    Property_Name,
    Property_Alias,
    Property_InputType,
    Property_InputOptions,
    Property_Order
FROM
    Content_FieldsContent
WHERE
    [FieldsName_ID] = @FieldsName_ID and  [Property_Name] = @Property_Name
GO
/****** 对象:  StoredProcedure [dbo].[p_Content_ID_UpdateMaxId]    脚本日期: 09/11/2008 11:58:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------
--Copyright (C) 2008 Gomye.net 版权所有
--作者: 慕焘
--说明：更新指定表的编号
--时间：2008-8-20 11:58:00
-----------------------------------------------
CREATE PROCEDURE [dbo].[p_Content_ID_UpdateMaxId]
	@Filedname nvarchar(100)
AS

BEGIN TRANSACTION
	update Content_ID set ID_Number=ID_Number+1 where ID_Name =@Filedname
COMMIT TRANSACTION
GO
/****** 对象:  UserDefinedFunction [dbo].[fn_GetMaxIndexByTable]    脚本日期: 09/11/2008 11:59:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------
--Copyright (C) 2008 Gomye.net 版权所有
--作者: 慕焘
--说明：获得表的索引ID
--时间：2008-8-20 11:58:00
-----------------------------------------------
CREATE FUNCTION [dbo].[fn_GetMaxIndexByTable]
(
	@tableName nvarchar(50)

)
RETURNS int AS
begin
   declare @indexId int
   select @indexId= max(  ID_Number )from Content_ID where ID_Name =@tableName
   return @indexId
end
GO
/****** 对象:  StoredProcedure [dbo].[p_Content_Content_ADD]    脚本日期: 09/11/2008 11:57:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------
--Copyright (C) 2008 gomye.net 版权所有
--作者: 慕焘
--说明：增加一条记录
--时间：2008-8-25 9:56:14
-----------------------------------------------
CREATE PROCEDURE [dbo].[p_Content_Content_ADD]
    @TypeTree_ID int,
    @Name varchar(255),
    @Author varchar(50),
    @KeyWord varchar(500),
    @Original varchar(50),
    @SubmitDate datetime,
    @Approver varchar(50),
    @Status varchar(50),
    @Description text,
    @Clicks int,
    @OrderNum int,
    @Derivation varchar(50),
    @DerivationLink varchar(50),
    @Head_news char(1),
    @Picture_news char(1),
    @Picture_Notes varchar(2000),
    @Picture_Name varchar(50),
    @Picture_DName varchar(50),
    @URL varchar(200),
    @ContentType char(10),
    @User_id int,
    @IsBallot int,
    @Album varchar(500),
    @content_xml text,
    @Content_Id int output
AS
BEGIN TRANSACTION
Declare @maxFields_ID int
Select @maxFields_ID=dbo.fn_GetMaxIndexByTable('Content_Id')+1
Set @Content_Id=@maxFields_ID
INSERT INTO [Content_Content](
    [Content_Id],
    [TypeTree_ID],
    [Name],
    [Author],
    [KeyWord],
    [Original],
    [SubmitDate],
    [Approver],
    [Status],
    [Description],
    [Clicks],
    [OrderNum],
    [PublishDate],
    [Derivation],
    [DerivationLink],
    [Head_news],
    [Picture_news],
    [Picture_Notes],
    [Picture_Name],
    [Picture_DName],
    [URL],
    [ReCount],
    [ContentType],
    [User_id],
    [IsBallot],
    [Album],
    [content_xml]
)VALUES(
    @maxFields_ID,
    @TypeTree_ID,
    @Name,
    @Author,
    @KeyWord,
    @Original,
    @SubmitDate,
    @Approver,
    @Status,
    @Description,
    @Clicks,
    @OrderNum,
    getdate(),
    @Derivation,
    @DerivationLink,
    @Head_news,
    @Picture_news,
    @Picture_Notes,
    @Picture_Name,
    @Picture_DName,
    @URL,
    0,
    @ContentType,
    @User_id,
    @IsBallot,
    @Album,
    @content_xml
)

exec dbo.p_Content_ID_UpdateMaxId 'Content_Id' 

COMMIT TRANSACTION
GO
/****** 对象:  StoredProcedure [dbo].[p_Content_FieldsContent_ADD]    脚本日期: 09/11/2008 11:57:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------
--Copyright (C) 2008 Gomye.net 版权所有
--作者: 慕焘
--说明：使用事务增加一条记录
--时间：2008-8-20 11:58:00
-----------------------------------------------
CREATE PROCEDURE [dbo].[p_Content_FieldsContent_ADD]
    @FieldsName_ID int,
    @Property_Name varchar(50),
    @Property_Alias varchar(50),
    @Property_InputType varchar(50),
    @Property_InputOptions text,
    @Property_Order int,
    @Fields_ID int output
AS
BEGIN TRANSACTION
Declare @maxFields_ID int
Select @maxFields_ID=dbo.fn_GetMaxIndexByTable('Fields_ID')+1
Set @Fields_ID=@maxFields_ID
INSERT INTO [Content_FieldsContent](
    [Fields_ID],
    [FieldsName_ID],
    [Property_Name],
    [Property_Alias],
    [Property_InputType],
    [Property_InputOptions],
    [Property_Order]
)VALUES(
    @maxFields_ID,
    @FieldsName_ID,
    @Property_Name,
    @Property_Alias,
    @Property_InputType,
    @Property_InputOptions,
    @Property_Order
)
exec dbo.p_Content_ID_UpdateMaxId 'Fields_ID' 

COMMIT TRANSACTION
GO
/****** 对象:  StoredProcedure [dbo].[p_Content_Master_ADD]    脚本日期: 09/11/2008 11:58:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------
--Copyright (C) 2008 Socansoft.com 版权所有
--作者：慕焘
--说明：增加一条记录
--时间：2008-8-27 11:08:40
-----------------------------------------------
CREATE PROCEDURE [dbo].[p_Content_Master_ADD]
    @Master_ID  int output,
    @Master_Name varchar(50),
    @Master_UserName varchar(50),
    @Master_Password varchar(50),
    @Master_Email varchar(50),
    @Master_Tel varchar(50),
    @Master_Usableness char(1),
    @Master_Note varchar(500),
    @Add_ID int
AS
BEGIN TRANSACTION
Declare @maxFields_ID int
Select @maxFields_ID=dbo.fn_GetMaxIndexByTable('Master_ID')+1
Set @Master_ID=@maxFields_ID
INSERT INTO [Content_Master](
    [Master_ID],
    [Master_Name],
    [Master_UserName],
    [Master_Password],
    [Master_Email],
    [Master_Tel],
    [Master_Usableness],
    [Master_Note],
    [Master_AddDate],
    [Add_ID]
)VALUES(
    @Master_ID,
    @Master_Name,
    @Master_UserName,
    @Master_Password,
    @Master_Email,
    @Master_Tel,
    @Master_Usableness,
    @Master_Note,
    getdate(),
    @Add_ID
)
exec dbo.p_Content_ID_UpdateMaxId 'Master_ID' 

COMMIT TRANSACTION
GO
/****** 对象:  StoredProcedure [dbo].[p_Content_Roles_ADD]    脚本日期: 09/11/2008 11:58:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------
--Copyright (C) 2008 gomye.net 版权所有
--作者: 慕焘
--说明：增加一条记录
--时间：2008-8-27 10:56:30
-----------------------------------------------
CREATE PROCEDURE [dbo].[p_Content_Roles_ADD]
    @Roles_Name varchar(50),
    @Roles_Explan varchar(500),
    @Add_ID int,
    @Roles_ID int output
AS
BEGIN TRANSACTION
Declare @maxFields_ID int
Select @maxFields_ID=dbo.fn_GetMaxIndexByTable('Roles_ID')+1
Set @Roles_ID=@maxFields_ID
INSERT INTO [Content_Roles](
    [Roles_ID],
    [Roles_Name],
    [Roles_Explan],
    [Add_ID]
)VALUES(
    @Roles_ID,
    @Roles_Name,
    @Roles_Explan,
    @Add_ID
)
exec dbo.p_Content_ID_UpdateMaxId 'Roles_ID' 

COMMIT TRANSACTION
GO
/****** 对象:  StoredProcedure [dbo].[p_Content_ID_GetMaxId]    脚本日期: 09/11/2008 11:58:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------
--Copyright (C) 2008 Gomye.net 版权所有
--作者: 慕焘
--说明：获得指定表的编号
--时间：2008-8-20 11:58:00
-----------------------------------------------
CREATE PROCEDURE [dbo].[p_Content_ID_GetMaxId]
	@Filedname nvarchar(100)

AS
BEGIN
    declare @maxid int
    set @maxid=dbo.fn_GetMaxIndexByTable(@Filedname)
    return @maxid
END
GO
