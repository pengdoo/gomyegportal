USE [GCMS]
GO
/****** 对象:  Table [dbo].[Content_Commend]    脚本日期: 07/29/2008 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Content_Commend](
	[Content_ID] [int] NULL,
	[TypeTree_ID] [int] NULL
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Content_Contact]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Content_Content]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Content_ContentRemark]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Content_FieldsContent]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Content_FieldsName]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Content_ID]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Content_Log]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Content_Master]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Content_Message]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Content_PhotoUpfile]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Content_Property]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Content_Roles]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Content_RolesConnect]    脚本日期: 07/29/2008 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Content_RolesConnect](
	[Roles_ID] [int] NULL,
	[TypeTree_ID] [int] NULL
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Content_RolesMaster]    脚本日期: 07/29/2008 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Content_RolesMaster](
	[Master_ID] [int] NOT NULL,
	[Roles_ID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Content_RolesPopedom]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Content_Schema]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Content_Search]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Content_SearchConnect]    脚本日期: 07/29/2008 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Content_SearchConnect](
	[Search_ID] [int] NULL,
	[TypeTree_ID] [int] NULL
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Content_System]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Content_Type_LinkPush]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Content_Type_TypeTree]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Content_Upload]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Member_Collection]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Member_Info]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Member_Roles]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Member_RolesConnect]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Member_Setup]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Member_User]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[Member_UsersInRoles]    脚本日期: 07/29/2008 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member_UsersInRoles](
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Upload_Files]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[ContentUser_]    脚本日期: 07/29/2008 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ContentUser_](
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
/****** 对象:  Table [dbo].[ContentUser_TestExFiled]    脚本日期: 07/29/2008 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ContentUser_TestExFiled](
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
	[Url] [varchar](200) NULL,
	[TestFild_1] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[ContentUser_Comment]    脚本日期: 07/29/2008 17:46:50 ******/
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
/****** 对象:  Table [dbo].[ContentUser_Movie]    脚本日期: 07/29/2008 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ContentUser_Movie](
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
	[Url] [varchar](200) NULL,
	[source] [varchar](100) NULL,
	[addTime] [varchar](100) NULL,
	[online1] [text] NULL,
	[online2] [text] NULL,
	[online3] [text] NULL,
	[xunlei] [text] NULL,
	[BT] [text] NULL,
	[Emule] [text] NULL,
	[Volume] [varchar](100) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[ContentUser_letter]    脚本日期: 07/29/2008 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ContentUser_letter](
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
	[Url] [varchar](200) NULL,
	[names] [varchar](100) NULL,
	[keyPerson] [text] NULL,
	[states] [varchar](100) NULL,
	[langage] [varchar](100) NULL,
	[introduction] [text] NULL,
	[Director] [varchar](100) NULL,
	[year] [varchar](100) NULL,
	[collect] [varchar](100) NULL,
	[area] [varchar](100) NULL,
	[BT] [text] NULL,
	[xunlei] [text] NULL,
	[Emule] [text] NULL,
	[letter] [varchar](100) NULL,
	[SURL] [text] NULL,
	[Images] [varchar](100) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO




--初始化数据

insert into content_ID(ID_Name,ID_Number) values('TypeTree_ID',1)
insert into content_ID(ID_Name,ID_Number) values('Content_ID',1)
insert into content_ID(ID_Name,ID_Number) values('Upload_ID',1)
insert into content_ID(ID_Name,ID_Number) values('Link_ID',1)
insert into content_ID(ID_Name,ID_Number) values('Property_ID',1)
insert into content_ID(ID_Name,ID_Number) values('Value_ID',1)
insert into content_ID(ID_Name,ID_Number) values('Master_ID',1)
insert into content_ID(ID_Name,ID_Number) values('Roles_ID',1)
insert into content_ID(ID_Name,ID_Number) values('File_ID',1)
insert into content_ID(ID_Name,ID_Number) values('Search_ID',1)
insert into content_ID(ID_Name,ID_Number) values('Spider_ID',1)
insert into content_ID(ID_Name,ID_Number) values('Remark_ID',1)
insert into content_ID(ID_Name,ID_Number) values('Member_ID',1)
insert into content_ID(ID_Name,ID_Number) values('Log_ID',1)
insert into content_ID(ID_Name,ID_Number) values('Consignee_ID',1)
insert into content_ID(ID_Name,ID_Number) values('PreSale_ID',1)
insert into content_ID(ID_Name,ID_Number) values('Consignee_ID',1)
insert into content_ID(ID_Name,ID_Number) values('PreSale_ID',1)
insert into content_ID(ID_Name,ID_Number) values('Ex_ID',1)
insert into content_ID(ID_Name,ID_Number) values('EI_ID',1)
insert into content_ID(ID_Name,ID_Number) values('FieldsName_ID',1)
insert into content_ID(ID_Name,ID_Number) values('Fields_ID',1)


insert into content_Master(Master_ID,Master_Name,Master_UserName,Master_Password,Master_Email,Master_Tel,Master_Usableness,Master_Note,Master_AddDate) 
values(0,'系统管理员','admin','3B816EA9A783BE2E038440E4AFC1FADC','alan@gomye.net','58236066',1,'系统管理员',getdate())

insert into Content_Roles(Roles_ID,Roles_Name) values(0,'Administrator')

insert into Content_RolesPopedom(Roles_ID,Popedom_EName) values(0,'Navigation')
insert into Content_RolesPopedom(Roles_ID,Popedom_EName) values(0,'Whiter')
insert into Content_RolesPopedom(Roles_ID,Popedom_EName) values(0,'Editor')
insert into Content_RolesPopedom(Roles_ID,Popedom_EName) values(0,'Popedom')
insert into Content_RolesPopedom(Roles_ID,Popedom_EName) values(0,'Setup')
insert into Content_RolesPopedom(Roles_ID,Popedom_EName) values(0,'Stat')
insert into Content_RolesPopedom(Roles_ID,Popedom_EName) values(0,'Dustbin')

insert into Content_RolesMaster(Master_ID,Roles_ID) values(0,0)
