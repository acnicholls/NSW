USE [NSW]
GO
/****** Object:  StoredProcedure [dbo].[searchPost]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[searchPost]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[searchPost]
GO
/****** Object:  StoredProcedure [dbo].[RenewPost]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RenewPost]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RenewPost]
GO
/****** Object:  StoredProcedure [dbo].[modifyUserPassword]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[modifyUserPassword]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[modifyUserPassword]
GO
/****** Object:  StoredProcedure [dbo].[modifyUser]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[modifyUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[modifyUser]
GO
/****** Object:  StoredProcedure [dbo].[modifyPostCategory]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[modifyPostCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[modifyPostCategory]
GO
/****** Object:  StoredProcedure [dbo].[modifyPost]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[modifyPost]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[modifyPost]
GO
/****** Object:  StoredProcedure [dbo].[modifyLabelText]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[modifyLabelText]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[modifyLabelText]
GO
/****** Object:  StoredProcedure [dbo].[insertUser]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[insertUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[insertUser]
GO
/****** Object:  StoredProcedure [dbo].[insertSiteLog]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[insertSiteLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[insertSiteLog]
GO
/****** Object:  StoredProcedure [dbo].[insertPostCategory]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[insertPostCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[insertPostCategory]
GO
/****** Object:  StoredProcedure [dbo].[insertPostalCode]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[insertPostalCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[insertPostalCode]
GO
/****** Object:  StoredProcedure [dbo].[insertPost]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[insertPost]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[insertPost]
GO
/****** Object:  StoredProcedure [dbo].[insertLabelText]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[insertLabelText]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[insertLabelText]
GO
/****** Object:  StoredProcedure [dbo].[ExpirePosts]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExpirePosts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ExpirePosts]
GO
/****** Object:  StoredProcedure [dbo].[deleteUser]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[deleteUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[deleteUser]
GO
/****** Object:  StoredProcedure [dbo].[deletePostCategory]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[deletePostCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[deletePostCategory]
GO
/****** Object:  StoredProcedure [dbo].[deletePost]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[deletePost]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[deletePost]
GO
/****** Object:  StoredProcedure [dbo].[deleteLabelText]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[deleteLabelText]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[deleteLabelText]
GO
/****** Object:  StoredProcedure [dbo].[CheckForDeleteablePosts]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CheckForDeleteablePosts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CheckForDeleteablePosts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsers_tblPostalCodes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsers]'))
ALTER TABLE [dbo].[tblUsers] DROP CONSTRAINT [FK_tblUsers_tblPostalCodes]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblPosts_tblUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPosts]'))
ALTER TABLE [dbo].[tblPosts] DROP CONSTRAINT [FK_tblPosts_tblUsers]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblPosts_tblPostCategories]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPosts]'))
ALTER TABLE [dbo].[tblPosts] DROP CONSTRAINT [FK_tblPosts_tblPostCategories]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__tblSiteLo__fldLo__2D27B809]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblSiteLog] DROP CONSTRAINT [DF__tblSiteLo__fldLo__2D27B809]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__tblSiteLo__fldLo__2C3393D0]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblSiteLog] DROP CONSTRAINT [DF__tblSiteLo__fldLo__2C3393D0]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__tblPosts__fldPos__36B12243]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblPosts] DROP CONSTRAINT [DF__tblPosts__fldPos__36B12243]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__tblPosts__fldPos__35BCFE0A]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblPosts] DROP CONSTRAINT [DF__tblPosts__fldPos__35BCFE0A]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_Table_1_fldPOst_DeleteFlag]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblPosts] DROP CONSTRAINT [DF_Table_1_fldPOst_DeleteFlag]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_fldPosts_fldPost_Expiry]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblPosts] DROP CONSTRAINT [DF_fldPosts_fldPost_Expiry]
END

GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUsers]') AND type in (N'U'))
DROP TABLE [dbo].[tblUsers]
GO
/****** Object:  Table [dbo].[tblSiteLog]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSiteLog]') AND type in (N'U'))
DROP TABLE [dbo].[tblSiteLog]
GO
/****** Object:  Table [dbo].[tblPosts]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPosts]') AND type in (N'U'))
DROP TABLE [dbo].[tblPosts]
GO
/****** Object:  Table [dbo].[tblPostCategories]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPostCategories]') AND type in (N'U'))
DROP TABLE [dbo].[tblPostCategories]
GO
/****** Object:  Table [dbo].[tblPostalCodes]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPostalCodes]') AND type in (N'U'))
DROP TABLE [dbo].[tblPostalCodes]
GO
/****** Object:  Table [dbo].[tblLabelText]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLabelText]') AND type in (N'U'))
DROP TABLE [dbo].[tblLabelText]
GO

DECLARE @RoleName sysname
set @RoleName = N'gd_execprocs'
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = @RoleName AND type = 'R')
IF @RoleName <> N'public' and (select is_fixed_role from sys.database_principals where name = @RoleName) = 0
BEGIN
	DECLARE @RoleMemberName sysname
	DECLARE Member_Cursor CURSOR FOR
	select [name]
	from sys.database_principals 
	where principal_id in ( 
		select member_principal_id
		from sys.database_role_members
		where role_principal_id in (
			select principal_id
			FROM sys.database_principals where [name] = @RoleName AND type = 'R'))

	OPEN Member_Cursor;

	FETCH NEXT FROM Member_Cursor
	into @RoleMemberName
	
	DECLARE @SQL NVARCHAR(4000)

	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		SET @SQL = 'ALTER ROLE '+ QUOTENAME(@RoleName,'[') +' DROP MEMBER '+ QUOTENAME(@RoleMemberName,'[')
		EXEC(@SQL)
		
		FETCH NEXT FROM Member_Cursor
		into @RoleMemberName
	END;

	CLOSE Member_Cursor;
	DEALLOCATE Member_Cursor;
END
/****** Object:  DatabaseRole [gd_execprocs]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'gd_execprocs' AND type = 'R')
DROP ROLE [gd_execprocs]
GO
/****** Object:  User [NSW]    Script Date: 6/18/2017 1:25:31 PM ******/
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'NSW')
DROP USER [NSW]
GO
/****** Object:  User [NSW]    Script Date: 6/18/2017 1:25:31 PM ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'NSW')
CREATE USER [NSW] FOR LOGIN [NSW] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  DatabaseRole [gd_execprocs]    Script Date: 6/18/2017 1:25:31 PM ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'gd_execprocs' AND type = 'R')
CREATE ROLE [gd_execprocs]
GO
ALTER ROLE [db_owner] ADD MEMBER [NSW]
GO
/****** Object:  Table [dbo].[tblLabelText]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLabelText]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblLabelText](
	[fldLabel_ID] [varchar](50) NOT NULL,
	[fldLabel_English] [varchar](max) NULL,
	[fldLabel_Japanese] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblPostalCodes]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPostalCodes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblPostalCodes](
	[fldPostal_Code] [varchar](15) NOT NULL,
	[fldPostal_Longitude] [decimal](8, 5) NOT NULL,
	[fldPostal_Latitude] [decimal](8, 5) NOT NULL,
 CONSTRAINT [PK_tblPostalCodes] PRIMARY KEY CLUSTERED 
(
	[fldPostal_Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblPostCategories]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPostCategories]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblPostCategories](
	[fldPostCategory_English] [varchar](50) NOT NULL,
	[fldPostCategory_id] [int] IDENTITY(1,1) NOT NULL,
	[fldPostCategory_DescEnglish] [varchar](max) NULL,
	[fldPostCategory_DescJapanese] [nvarchar](max) NULL,
	[fldPostCategory_Japanese] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblPostCategories] PRIMARY KEY CLUSTERED 
(
	[fldPostCategory_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblPosts]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPosts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblPosts](
	[fldPost_CategoryID] [int] NOT NULL,
	[fldPost_Title] [nvarchar](50) NULL,
	[fldPost_Description] [nvarchar](max) NULL,
	[fldPost_Price] [money] NOT NULL,
	[fldPost_Expiry] [date] NOT NULL,
	[fldPost_UserID] [int] NOT NULL,
	[fldPost_Status] [varchar](15) NOT NULL,
	[fldPost_DeleteFlag] [bit] NOT NULL,
	[fldPost_IntakeDate] [datetime] NOT NULL,
	[fldPost_ChangeDate] [datetime] NOT NULL,
	[fldPost_id] [int] IDENTITY(1,1) NOT NULL,
	[fldPost_emailSent] [bit] NOT NULL,
	[fldPost_Renewed] [bit] NOT NULL,
 CONSTRAINT [PK_tblPosts] PRIMARY KEY CLUSTERED 
(
	[fldPost_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSiteLog]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSiteLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblSiteLog](
	[fldLog_id] [int] IDENTITY(1,1) NOT NULL,
	[fldLog_date] [smalldatetime] NOT NULL,
	[fldLog_caller] [varchar](50) NOT NULL,
	[fldLog_message] [nvarchar](4000) NOT NULL,
	[fldLog_importance] [varchar](50) NOT NULL,
	[fldLog_intakedate] [datetime] NOT NULL,
	[fldLog_chgdate] [datetime] NOT NULL,
 CONSTRAINT [PK_tblSiteLog] PRIMARY KEY CLUSTERED 
(
	[fldLog_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblUsers](
	[fldUser_Name] [varchar](50) NOT NULL,
	[fldUser_Password] [varchar](100) NOT NULL,
	[fldUser_Email] [varchar](50) NOT NULL,
	[fldUser_Phone] [varchar](20) NULL,
	[fldUser_PostalCode] [varchar](15) NOT NULL,
	[fldUser_Role] [varchar](15) NOT NULL,
	[fldUser_IntakeDate] [datetime] NOT NULL,
	[fldUser_ChangeDate] [datetime] NOT NULL,
	[fldUser_id] [int] IDENTITY(1,1) NOT NULL,
	[fldUser_langPref] [int] NOT NULL,
 CONSTRAINT [PK_tblUsers] PRIMARY KEY CLUSTERED 
(
	[fldUser_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_fldPosts_fldPost_Expiry]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblPosts] ADD  CONSTRAINT [DF_fldPosts_fldPost_Expiry]  DEFAULT (getdate()+(30)) FOR [fldPost_Expiry]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_Table_1_fldPOst_DeleteFlag]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblPosts] ADD  CONSTRAINT [DF_Table_1_fldPOst_DeleteFlag]  DEFAULT ((0)) FOR [fldPost_DeleteFlag]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__tblPosts__fldPos__35BCFE0A]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblPosts] ADD  DEFAULT ((0)) FOR [fldPost_emailSent]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__tblPosts__fldPos__36B12243]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblPosts] ADD  DEFAULT ((0)) FOR [fldPost_Renewed]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__tblSiteLo__fldLo__2C3393D0]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblSiteLog] ADD  DEFAULT (getdate()) FOR [fldLog_intakedate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__tblSiteLo__fldLo__2D27B809]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblSiteLog] ADD  DEFAULT (getdate()) FOR [fldLog_chgdate]
END

GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblPosts_tblPostCategories]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPosts]'))
ALTER TABLE [dbo].[tblPosts]  WITH CHECK ADD  CONSTRAINT [FK_tblPosts_tblPostCategories] FOREIGN KEY([fldPost_CategoryID])
REFERENCES [dbo].[tblPostCategories] ([fldPostCategory_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblPosts_tblPostCategories]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPosts]'))
ALTER TABLE [dbo].[tblPosts] CHECK CONSTRAINT [FK_tblPosts_tblPostCategories]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblPosts_tblUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPosts]'))
ALTER TABLE [dbo].[tblPosts]  WITH CHECK ADD  CONSTRAINT [FK_tblPosts_tblUsers] FOREIGN KEY([fldPost_UserID])
REFERENCES [dbo].[tblUsers] ([fldUser_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblPosts_tblUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPosts]'))
ALTER TABLE [dbo].[tblPosts] CHECK CONSTRAINT [FK_tblPosts_tblUsers]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsers_tblPostalCodes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsers]'))
ALTER TABLE [dbo].[tblUsers]  WITH CHECK ADD  CONSTRAINT [FK_tblUsers_tblPostalCodes] FOREIGN KEY([fldUser_PostalCode])
REFERENCES [dbo].[tblPostalCodes] ([fldPostal_Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsers_tblPostalCodes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsers]'))
ALTER TABLE [dbo].[tblUsers] CHECK CONSTRAINT [FK_tblUsers_tblPostalCodes]
GO
/****** Object:  StoredProcedure [dbo].[CheckForDeleteablePosts]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CheckForDeleteablePosts]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CheckForDeleteablePosts] AS' 
END
GO
-- =============================================
-- Author:		ac nicholls
-- Create date: 6/6/2014
-- Description:	sets posts for deletion depending on criteria
-- =============================================
ALTER PROCEDURE [dbo].[CheckForDeleteablePosts]

AS
BEGIN

update tblPosts
set fldPost_DeleteFlag = 1,
fldPost_ChangeDate = GETDATE()
where
(fldPost_Status = 'SOLD' and 
DATEADD(day, 14,fldPost_ChangeDate) < GETDATE() )
or 
(dateadd(day, 60, fldPost_Expiry) < CAST(GETDATE() as DATE)
and fldPost_Status = 'EXPIRED')

insert into tblsitelog (fldLog_Date, fldLog_caller, fldLog_message, fldLog_importance)
VALUES
(getdate(),'CheckForDeleteablePosts', 'Completed', 1)
END

GO
/****** Object:  StoredProcedure [dbo].[deleteLabelText]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[deleteLabelText]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[deleteLabelText] AS' 
END
GO
ALTER procedure [dbo].[deleteLabelText]
@id varchar(50)
as
BEGIN

DELETE FROM tblLabelText 
WHERE
fldLabel_ID = @id

END

GO
/****** Object:  StoredProcedure [dbo].[deletePost]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[deletePost]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[deletePost] AS' 
END
GO
ALTER procedure [dbo].[deletePost]
@ID int
as
BEGIN

Delete from tblPosts
where
fldPost_ID = @ID

END

GO
/****** Object:  StoredProcedure [dbo].[deletePostCategory]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[deletePostCategory]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[deletePostCategory] AS' 
END
GO
ALTER procedure [dbo].[deletePostCategory]
@id int
as
BEGIN

DELETE FROM tblPostCategories
WHERE
fldPostCategory_ID = @id

END

GO
/****** Object:  StoredProcedure [dbo].[deleteUser]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[deleteUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[deleteUser] AS' 
END
GO
ALTER procedure [dbo].[deleteUser]
@ID int
as
BEGIN

DELETE FROM tblUsers 
where
fldUser_ID = @ID

END

GO
/****** Object:  StoredProcedure [dbo].[ExpirePosts]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExpirePosts]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ExpirePosts] AS' 
END
GO

ALTER PROCEDURE [dbo].[ExpirePosts]

AS
BEGIN

update tblPosts
set 
fldPost_Status = 'EXPIRED',
fldPost_Renewed = 0,
fldPost_ChangeDate = GETDATE()
where
fldPost_Expiry < GETDATE()
and fldPost_Status = 'ACTIVE'

insert into tblsitelog (fldLog_Date, fldLog_caller, fldLog_message, fldLog_importance)
VALUES
(getdate(),'ExpirePosts', 'Completed', 1)
END

GO
/****** Object:  StoredProcedure [dbo].[insertLabelText]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[insertLabelText]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[insertLabelText] AS' 
END
GO
ALTER procedure [dbo].[insertLabelText]
@ID varchar(50), @english varchar(max), @japanese nvarchar(max)
as
BEGIN

insert into tblLabelText (fldLabel_ID, fldLabel_English, fldLabel_Japanese)
VALUES
(@ID, @english, N'' + @japanese + '')

END

GO
/****** Object:  StoredProcedure [dbo].[insertPost]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[insertPost]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[insertPost] AS' 
END
GO
ALTER procedure [dbo].[insertPost]
@catID int, @title nvarchar(50), @desc nvarchar(max), @price money, @userID int, @ID int output
as
BEGIN

insert into tblPosts
(fldPost_CategoryID, fldPost_Title, fldPost_Description, fldPost_Price, fldPost_Expiry, fldPost_UserID, fldPost_Status, fldPost_DeleteFlag, fldPost_IntakeDate, fldPost_ChangeDate)
VALUES
(@catID, @title, @desc, @price, GETDATE() + 30, @userID, 'ACTIVE', 0, GETDATE(), GETDATE())

set @ID = @@IDENTITY


END

GO
/****** Object:  StoredProcedure [dbo].[insertPostalCode]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[insertPostalCode]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[insertPostalCode] AS' 
END
GO
ALTER procedure [dbo].[insertPostalCode]
@code varchar(15), @lat decimal(8,5), @long decimal(8,5)
as
BEGIN

insert into tblPostalCodes
(fldPostal_Code, fldPostal_Longitude, fldPostal_Latitude)
VALUES
(@code, @long, @lat)

END

GO
/****** Object:  StoredProcedure [dbo].[insertPostCategory]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[insertPostCategory]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[insertPostCategory] AS' 
END
GO
ALTER procedure [dbo].[insertPostCategory]
@english varchar(50), @japanese nvarchar(50), @descEnglish varchar(max), @descJapanese nvarchar(max)
as
BEGIN

INSERT INTO tblPostCategories (fldPostCategory_English, fldPostCategory_Japanese,
fldPostCategory_DescEnglish, fldPostCategory_DescJapanese)
VALUES
(@english, @japanese, @descEnglish, @descJapanese)

END

GO
/****** Object:  StoredProcedure [dbo].[insertSiteLog]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[insertSiteLog]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[insertSiteLog] AS' 
END
GO
-- =============================================
-- Author:		AC Nicholls
-- Create date: 15/11/2010
-- Description:	This procedure inserts a new row into the site Log table
-- =============================================
ALTER PROCEDURE [dbo].[insertSiteLog]
	-- Add the parameters for the stored procedure here
	@caller varchar(50),@message varchar(4000),@import varchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into tblSiteLog (fldLog_date, fldLog_caller, fldLog_message, fldLog_importance, fldLog_intakedate, fldLog_chgdate)
	VALUES (getdate(), @caller, @message, @import, getdate(), getdate())
END

GO
/****** Object:  StoredProcedure [dbo].[insertUser]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[insertUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[insertUser] AS' 
END
GO

ALTER procedure [dbo].[insertUser]
@name varchar(50), @pass varchar(100), @email varchar(50), @phone varchar(15), @postalcode varchar(15), @langPref int
as
BEGIN

insert into tblUsers 
(fldUser_Name, fldUser_Password, fldUser_Email, fldUser_Phone, fldUser_PostalCode, fldUser_Role, fldUser_langPref, fldUser_IntakeDate, fldUser_ChangeDate)
VALUES
(@name, @pass, @email, @phone, @postalcode, 'MEMBER', @langPref, GETDATE(), GETDATE())

END

GO
/****** Object:  StoredProcedure [dbo].[modifyLabelText]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[modifyLabelText]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[modifyLabelText] AS' 
END
GO
ALTER procedure [dbo].[modifyLabelText]
@id varchar(50), @english varchar(max), @japanese nvarchar(max)
as
BEGIN

UPDATE tblLabelText 
set
fldLabel_English = @english,
fldLabel_Japanese = N'' + @japanese + ''
WHERE
fldLabel_ID = @id

END

GO
/****** Object:  StoredProcedure [dbo].[modifyPost]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[modifyPost]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[modifyPost] AS' 
END
GO
ALTER procedure [dbo].[modifyPost]
@ID int, @title nvarchar(50), @desc nvarchar(max), @price money, @status varchar(15)
as
BEGIN

UPDATE tblPosts
SET
fldPost_Title = @title,
fldPost_Description = @desc,
fldPost_Price = @price,
fldPost_Status = @status,
fldPost_ChangeDate = GETDATE()
WHERE
fldPost_id = @ID

END

GO
/****** Object:  StoredProcedure [dbo].[modifyPostCategory]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[modifyPostCategory]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[modifyPostCategory] AS' 
END
GO
ALTER procedure [dbo].[modifyPostCategory]
@id int, @english varchar(50), @japanese nvarchar(50), @descEnglish varchar(max), @descJapanese nvarchar(max)
as
BEGIN

UPDATE tblPostCategories
SET
fldPostCategory_English = @english,
fldPostCategory_Japanese = @japanese,
fldPostCategory_DescEnglish = @descEnglish,
fldPostCategory_DescJapanese = @descJapanese
WHERE
fldPostCategory_id = @id

END

GO
/****** Object:  StoredProcedure [dbo].[modifyUser]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[modifyUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[modifyUser] AS' 
END
GO

ALTER procedure [dbo].[modifyUser]
@ID int, @name varchar(50), @pass varchar(100), @email varchar(50), @phone varchar(15), @postalcode varchar(15), @langPref int
as
BEGIN

update tblUsers 
SET
fldUser_Name = @name,
fldUser_Password = @pass, 
fldUser_Email = @email,
fldUser_Phone = @phone, 
fldUser_PostalCode = @postalcode, 
fldUser_langPref = @langPref,
fldUser_ChangeDate = GETDATE()
where
fldUser_ID = @ID

END

GO
/****** Object:  StoredProcedure [dbo].[modifyUserPassword]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[modifyUserPassword]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[modifyUserPassword] AS' 
END
GO
ALTER procedure [dbo].[modifyUserPassword]
@ID int, @newPass varchar(100)
as
BEGIN

update tblUsers 
SET
fldUser_Password = @newPass 
where
fldUser_ID = @ID

END

GO
/****** Object:  StoredProcedure [dbo].[RenewPost]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RenewPost]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[RenewPost] AS' 
END
GO
/* stored procedures that were altered */

ALTER PROCEDURE [dbo].[RenewPost] 
@id int
AS
BEGIN

update tblPosts
set 
fldPost_Status = 'ACTIVE',
fldPost_Expiry = DATEADD(day, 30, fldPost_Expiry),
fldPost_emailSent=0,
fldPost_Renewed=1
where
fldPost_id = @id

insert into tblsitelog (fldLog_Date, fldLog_caller, fldLog_message, fldLog_importance)
VALUES
(getdate(),'RenewPost', 'Post ' + CAST(@id as varchar) + ' renewed', 1)
END

GO
/****** Object:  StoredProcedure [dbo].[searchPost]    Script Date: 6/18/2017 1:25:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[searchPost]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[searchPost] AS' 
END
GO

ALTER procedure [dbo].[searchPost]
@term nvarchar(50), @func char, @limiter char, @catid int, @userid int, @searchCats varchar(50),
@trunc bit, @start int, @finish int, @order varchar(25)
as
BEGIN
declare @sql nvarchar(max)

set @sql = N'Select * from (SELECT ROW_NUMBER() over (ORDER by ' + @order + ') as RowNum, * from tblPosts'

if ( @func = 'l')  -- list
begin
	if (@limiter = 'c')
	begin
		set @sql = @sql + N' where fldPost_CategoryID=' + CAST(@catid as NVARCHAR)
	end
	if (@limiter = 'u')
	begin
		set @sql = @sql + N' where fldPost_UserID=' + CAST(@userid as NVARCHAR)
	end
	set @sql = @sql + N' AND fldPost_Status not in (''SOLD'', ''EXPIRED'')'
end
if( @func = 's' ) -- search
begin
  set @sql = @sql + N' where fldPost_Status not in ( ''SOLD'', ''EXPIRED'')'
  set @sql = @sql + N' and (fldPost_Title like ''%' + @term + '%'' or fldPost_Description like ''%' + @term + '%'')'
  if(@searchCats != '')
  begin
	set @sql = @sql + N' and fldPost_CategoryID in (' + @searchCats + ')'
  end
end
set @sql = @sql + ') as Results '
if (@trunc = 1)
begin
	insert into tblsitelog (fldLog_Date, fldLog_caller, fldLog_message, fldLog_importance)
	VALUES (getdate(),'searchPost', @sql, 1)
	exec sp_executesql @sql
end
else
begin
	set @sql = @sql + ' WHERE RowNum >=' + CAST(@start as NVARCHAR) + ' and RowNum < ' + CAST(@finish as NVARCHAR)
	set @sql = @sql + ' ORDER BY ' + @order
	insert into tblsitelog (fldLog_Date, fldLog_caller, fldLog_message, fldLog_importance)
	VALUES (getdate(),'searchPost', @sql, 1)
	exec sp_executesql @sql
end
	

END

GO
