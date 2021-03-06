
GO
/****** 对象:  StoredProcedure [dbo].[p_Content_ExtendField_GetValue]    脚本日期: 08/27/2008 18:29:51 ******/
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
/****** 对象:  StoredProcedure [dbo].[p_Content_Content_S_Update]    脚本日期: 08/27/2008 18:29:51 ******/
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
/****** 对象:  StoredProcedure [dbo].[p_Content_Content_Delete]    脚本日期: 08/27/2008 18:29:48 ******/
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
/****** 对象:  StoredProcedure [dbo].[p_Content_Content_Exists]    脚本日期: 08/27/2008 18:29:49 ******/
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
/****** 对象:  StoredProcedure [dbo].[p_Content_Content_S_GetModel]    脚本日期: 08/27/2008 18:29:49 ******/
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
/****** 对象:  StoredProcedure [dbo].[p_Content_Content_GetList]    脚本日期: 08/27/2008 18:29:49 ******/
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
/****** 对象:  StoredProcedure [dbo].[p_Content_FieldsContent_S_Update]    脚本日期: 08/27/2008 18:29:54 ******/
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
/****** 对象:  StoredProcedure [dbo].[p_Content_FieldsContent_Delete]    脚本日期: 08/27/2008 18:29:52 ******/
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
/****** 对象:  StoredProcedure [dbo].[p_Content_FieldsContent_Exists]    脚本日期: 08/27/2008 18:29:53 ******/
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
/****** 对象:  StoredProcedure [dbo].[p_Content_FieldsContent_GetModel]    脚本日期: 08/27/2008 18:29:53 ******/
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
/****** 对象:  StoredProcedure [dbo].[p_Content_FieldsContent_GetList]    脚本日期: 08/27/2008 18:29:53 ******/
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
/****** 对象:  StoredProcedure [dbo].[p_Content_FieldsContent_S_GetModelByProperty]    脚本日期: 08/27/2008 18:29:54 ******/
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
/****** 对象:  StoredProcedure [dbo].[p_Content_ID_UpdateMaxId]    脚本日期: 08/27/2008 18:29:55 ******/
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
/****** 对象:  UserDefinedFunction [dbo].[fn_GetMaxIndexByTable]    脚本日期: 08/27/2008 18:29:56 ******/
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
/****** 对象:  StoredProcedure [dbo].[p_Content_Content_ADD]    脚本日期: 08/27/2008 18:29:48 ******/
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
/****** 对象:  StoredProcedure [dbo].[p_Content_FieldsContent_ADD]    脚本日期: 08/27/2008 18:29:52 ******/
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
/****** 对象:  StoredProcedure [dbo].[p_Content_Master_ADD]    脚本日期: 08/27/2008 18:29:56 ******/
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
/****** 对象:  StoredProcedure [dbo].[p_Content_Roles_ADD]    脚本日期: 08/27/2008 18:29:56 ******/
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
/****** 对象:  StoredProcedure [dbo].[p_Content_ID_GetMaxId]    脚本日期: 08/27/2008 18:29:54 ******/
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
