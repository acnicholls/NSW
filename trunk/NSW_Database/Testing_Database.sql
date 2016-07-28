USE [master]
GO
/****** Object:  Database [NSW]    Script Date: 2016-07-28 12:59:03 AM ******/
CREATE DATABASE [NSW]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NSW', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLSERVER\MSSQL\DATA\KIMWebsite.mdf' , SIZE = 118016KB , MAXSIZE = 204800KB , FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'NSW_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLSERVER\MSSQL\DATA\KIMWebsite_log.ldf' , SIZE = 102208KB , MAXSIZE = 102400KB , FILEGROWTH = 1024KB )
GO
ALTER DATABASE [NSW] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NSW].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NSW] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NSW] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NSW] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NSW] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NSW] SET ARITHABORT OFF 
GO
ALTER DATABASE [NSW] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NSW] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NSW] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NSW] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NSW] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NSW] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NSW] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NSW] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NSW] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NSW] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NSW] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NSW] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NSW] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NSW] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NSW] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NSW] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NSW] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NSW] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [NSW] SET  MULTI_USER 
GO
ALTER DATABASE [NSW] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NSW] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NSW] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NSW] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [NSW] SET DELAYED_DURABILITY = DISABLED 
GO
/****** Object:  Login [XPSM1530\Anthony]    Script Date: 2016-07-28 12:59:03 AM ******/
CREATE LOGIN [XPSM1530\Anthony] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [terrariaBlog]    Script Date: 2016-07-28 12:59:03 AM ******/
CREATE LOGIN [terrariaBlog] WITH PASSWORD=N'4EMdXdPhtUTG34fqMbOGlpW0s/54aeDFe0wJ8pQO0yk=', DEFAULT_DATABASE=[terrariaBlog], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [terrariaBlog] DISABLE
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [Portfolio]    Script Date: 2016-07-28 12:59:03 AM ******/
CREATE LOGIN [Portfolio] WITH PASSWORD=N'PuZPhN05BkwyAn14smgcgofSW0Ho+JbfHG+3fJpjsKo=', DEFAULT_DATABASE=[portfolioLogins], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
ALTER LOGIN [Portfolio] DISABLE
GO
/****** Object:  Login [NT SERVICE\Winmgmt]    Script Date: 2016-07-28 12:59:03 AM ******/
CREATE LOGIN [NT SERVICE\Winmgmt] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT SERVICE\SQLWriter]    Script Date: 2016-07-28 12:59:03 AM ******/
CREATE LOGIN [NT SERVICE\SQLWriter] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT SERVICE\SQLAgent$SQLSERVER]    Script Date: 2016-07-28 12:59:03 AM ******/
CREATE LOGIN [NT SERVICE\SQLAgent$SQLSERVER] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT SERVICE\ReportServer$SQLSERVER]    Script Date: 2016-07-28 12:59:03 AM ******/
CREATE LOGIN [NT SERVICE\ReportServer$SQLSERVER] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT Service\MSSQL$SQLSERVER]    Script Date: 2016-07-28 12:59:03 AM ******/
CREATE LOGIN [NT Service\MSSQL$SQLSERVER] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT AUTHORITY\SYSTEM]    Script Date: 2016-07-28 12:59:03 AM ******/
CREATE LOGIN [NT AUTHORITY\SYSTEM] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [NSW]    Script Date: 2016-07-28 12:59:03 AM ******/
CREATE LOGIN [NSW] WITH PASSWORD=N'AyHROS652ItMKY/5coNlsbGEzjxcsVL/QE32PbT5EiQ=', DEFAULT_DATABASE=[NSW], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [NSW] DISABLE
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [KIMDevTest]    Script Date: 2016-07-28 12:59:03 AM ******/
CREATE LOGIN [KIMDevTest] WITH PASSWORD=N'NfFCtUUmFebwxSm5vhIVdvzKiGHjYwWgeCF92WzsUKU=', DEFAULT_DATABASE=[KIMv1_5], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
ALTER LOGIN [KIMDevTest] DISABLE
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [FSC_Web_User]    Script Date: 2016-07-28 12:59:03 AM ******/
CREATE LOGIN [FSC_Web_User] WITH PASSWORD=N'e+SjUhXMnJ3nnw0Hw+Zx89+sPDLOzAalRbZlrfmt04M=', DEFAULT_DATABASE=[FSCServices], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
ALTER LOGIN [FSC_Web_User] DISABLE
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [DBA]    Script Date: 2016-07-28 12:59:03 AM ******/
CREATE LOGIN [DBA] WITH PASSWORD=N'YvM1HVbfGHFO4wg5jz+l2WaM3Mob+C6WVYnCjrqJMkM=', DEFAULT_DATABASE=[NSW], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
ALTER LOGIN [DBA] DISABLE
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [##MS_PolicyTsqlExecutionLogin##]    Script Date: 2016-07-28 12:59:03 AM ******/
CREATE LOGIN [##MS_PolicyTsqlExecutionLogin##] WITH PASSWORD=N'ypsSZxehfrdQb8re9xwiNcJxIsdm2B2cTNuXY4Zh9lY=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [##MS_PolicyTsqlExecutionLogin##] DISABLE
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [##MS_PolicyEventProcessingLogin##]    Script Date: 2016-07-28 12:59:03 AM ******/
CREATE LOGIN [##MS_PolicyEventProcessingLogin##] WITH PASSWORD=N'u1qUFP3xBILoiSG2jhyB7JQzZOjU/2GoyL4obM7DcFE=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [##MS_PolicyEventProcessingLogin##] DISABLE
GO
ALTER AUTHORIZATION ON DATABASE::[NSW] TO [NT SERVICE\SQLAgent$SQLSERVER]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [XPSM1530\Anthony]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\Winmgmt]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\SQLWriter]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\SQLAgent$SQLSERVER]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT Service\MSSQL$SQLSERVER]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [DBA]
GO
ALTER SERVER ROLE [serveradmin] ADD MEMBER [DBA]
GO
ALTER SERVER ROLE [dbcreator] ADD MEMBER [DBA]
GO
USE [NSW]
GO
/****** Object:  User [NSW]    Script Date: 2016-07-28 12:59:04 AM ******/
CREATE USER [NSW] FOR LOGIN [NSW] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  DatabaseRole [gd_execprocs]    Script Date: 2016-07-28 12:59:04 AM ******/
CREATE ROLE [gd_execprocs]
GO
ALTER AUTHORIZATION ON ROLE::[gd_execprocs] TO [dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [NSW]
GO
GRANT CONNECT TO [NSW] AS [dbo]
GO
/****** Object:  Table [dbo].[tblLabelText]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblLabelText](
	[fldLabel_ID] [varchar](50) NOT NULL,
	[fldLabel_English] [varchar](max) NULL,
	[fldLabel_Japanese] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[tblLabelText] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[tblPostalCodes]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblPostalCodes](
	[fldPostal_Code] [varchar](15) NOT NULL,
	[fldPostal_Longitude] [decimal](8, 5) NOT NULL,
	[fldPostal_Latitude] [decimal](8, 5) NOT NULL,
 CONSTRAINT [PK_tblPostalCodes] PRIMARY KEY CLUSTERED 
(
	[fldPostal_Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[tblPostalCodes] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[tblPostCategories]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[tblPostCategories] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[tblPosts]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblPosts](
	[fldPost_CategoryID] [int] NOT NULL,
	[fldPost_Title] [nvarchar](50) NULL,
	[fldPost_Description] [nvarchar](max) NULL,
	[fldPost_Price] [money] NOT NULL,
	[fldPost_Expiry] [date] NOT NULL CONSTRAINT [DF_fldPosts_fldPost_Expiry]  DEFAULT (getdate()+(30)),
	[fldPost_UserID] [int] NOT NULL,
	[fldPost_Status] [varchar](15) NOT NULL,
	[fldPost_DeleteFlag] [bit] NOT NULL CONSTRAINT [DF_Table_1_fldPOst_DeleteFlag]  DEFAULT ((0)),
	[fldPost_IntakeDate] [datetime] NOT NULL,
	[fldPost_ChangeDate] [datetime] NOT NULL,
	[fldPost_id] [int] IDENTITY(1,1) NOT NULL,
	[fldPost_emailSent] [bit] NOT NULL DEFAULT ((0)),
	[fldPost_Renewed] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_tblPosts] PRIMARY KEY CLUSTERED 
(
	[fldPost_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[tblPosts] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[tblSiteLog]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSiteLog](
	[fldLog_id] [int] IDENTITY(1,1) NOT NULL,
	[fldLog_date] [smalldatetime] NOT NULL,
	[fldLog_caller] [varchar](50) NOT NULL,
	[fldLog_message] [nvarchar](4000) NOT NULL,
	[fldLog_importance] [varchar](50) NOT NULL,
	[fldLog_intakedate] [datetime] NOT NULL DEFAULT (getdate()),
	[fldLog_chgdate] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_tblSiteLog] PRIMARY KEY CLUSTERED 
(
	[fldLog_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[tblSiteLog] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[tblUsers] TO  SCHEMA OWNER 
GO
ALTER TABLE [dbo].[tblPosts]  WITH CHECK ADD  CONSTRAINT [FK_tblPosts_tblPostCategories] FOREIGN KEY([fldPost_CategoryID])
REFERENCES [dbo].[tblPostCategories] ([fldPostCategory_id])
GO
ALTER TABLE [dbo].[tblPosts] CHECK CONSTRAINT [FK_tblPosts_tblPostCategories]
GO
ALTER TABLE [dbo].[tblPosts]  WITH CHECK ADD  CONSTRAINT [FK_tblPosts_tblUsers] FOREIGN KEY([fldPost_UserID])
REFERENCES [dbo].[tblUsers] ([fldUser_id])
GO
ALTER TABLE [dbo].[tblPosts] CHECK CONSTRAINT [FK_tblPosts_tblUsers]
GO
ALTER TABLE [dbo].[tblUsers]  WITH CHECK ADD  CONSTRAINT [FK_tblUsers_tblPostalCodes] FOREIGN KEY([fldUser_PostalCode])
REFERENCES [dbo].[tblPostalCodes] ([fldPostal_Code])
GO
ALTER TABLE [dbo].[tblUsers] CHECK CONSTRAINT [FK_tblUsers_tblPostalCodes]
GO
/****** Object:  StoredProcedure [dbo].[CheckForDeleteablePosts]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ac nicholls
-- Create date: 6/6/2014
-- Description:	sets posts for deletion depending on criteria
-- =============================================
CREATE PROCEDURE [dbo].[CheckForDeleteablePosts]

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
ALTER AUTHORIZATION ON [dbo].[CheckForDeleteablePosts] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[deleteLabelText]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[deleteLabelText]
@id varchar(50)
as
BEGIN

DELETE FROM tblLabelText 
WHERE
fldLabel_ID = @id

END

GO
ALTER AUTHORIZATION ON [dbo].[deleteLabelText] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[deletePost]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deletePost]
@ID int
as
BEGIN

Delete from tblPosts
where
fldPost_ID = @ID

END

GO
ALTER AUTHORIZATION ON [dbo].[deletePost] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[deletePostCategory]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deletePostCategory]
@id int
as
BEGIN

DELETE FROM tblPostCategories
WHERE
fldPostCategory_ID = @id

END

GO
ALTER AUTHORIZATION ON [dbo].[deletePostCategory] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[deleteUser]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteUser]
@ID int
as
BEGIN

DELETE FROM tblUsers 
where
fldUser_ID = @ID

END

GO
ALTER AUTHORIZATION ON [dbo].[deleteUser] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[ExpirePosts]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ExpirePosts]

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
ALTER AUTHORIZATION ON [dbo].[ExpirePosts] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[insertLabelText]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[insertLabelText]
@ID varchar(50), @english varchar(max), @japanese nvarchar(max)
as
BEGIN

insert into tblLabelText (fldLabel_ID, fldLabel_English, fldLabel_Japanese)
VALUES
(@ID, @english, N'' + @japanese + '')

END

GO
ALTER AUTHORIZATION ON [dbo].[insertLabelText] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[insertPost]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[insertPost]
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
ALTER AUTHORIZATION ON [dbo].[insertPost] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[insertPostalCode]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[insertPostalCode]
@code varchar(15), @lat decimal(8,5), @long decimal(8,5)
as
BEGIN

insert into tblPostalCodes
(fldPostal_Code, fldPostal_Longitude, fldPostal_Latitude)
VALUES
(@code, @long, @lat)

END

GO
ALTER AUTHORIZATION ON [dbo].[insertPostalCode] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[insertPostCategory]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[insertPostCategory]
@english varchar(50), @japanese nvarchar(50), @descEnglish varchar(max), @descJapanese nvarchar(max)
as
BEGIN

INSERT INTO tblPostCategories (fldPostCategory_English, fldPostCategory_Japanese,
fldPostCategory_DescEnglish, fldPostCategory_DescJapanese)
VALUES
(@english, @japanese, @descEnglish, @descJapanese)

END

GO
ALTER AUTHORIZATION ON [dbo].[insertPostCategory] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[insertSiteLog]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		AC Nicholls
-- Create date: 15/11/2010
-- Description:	This procedure inserts a new row into the site Log table
-- =============================================
CREATE PROCEDURE [dbo].[insertSiteLog]
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
ALTER AUTHORIZATION ON [dbo].[insertSiteLog] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[insertUser]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[insertUser]
@name varchar(50), @pass varchar(100), @email varchar(50), @phone varchar(15), @postalcode varchar(15), @langPref int
as
BEGIN

insert into tblUsers 
(fldUser_Name, fldUser_Password, fldUser_Email, fldUser_Phone, fldUser_PostalCode, fldUser_Role, fldUser_langPref, fldUser_IntakeDate, fldUser_ChangeDate)
VALUES
(@name, @pass, @email, @phone, @postalcode, 'MEMBER', @langPref, GETDATE(), GETDATE())

END

GO
ALTER AUTHORIZATION ON [dbo].[insertUser] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[modifyLabelText]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[modifyLabelText]
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
ALTER AUTHORIZATION ON [dbo].[modifyLabelText] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[modifyPost]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[modifyPost]
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
ALTER AUTHORIZATION ON [dbo].[modifyPost] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[modifyPostCategory]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[modifyPostCategory]
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
ALTER AUTHORIZATION ON [dbo].[modifyPostCategory] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[modifyUser]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[modifyUser]
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
ALTER AUTHORIZATION ON [dbo].[modifyUser] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[modifyUserPassword]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[modifyUserPassword]
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
ALTER AUTHORIZATION ON [dbo].[modifyUserPassword] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[RenewPost]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* stored procedures that were altered */

CREATE PROCEDURE [dbo].[RenewPost] 
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
ALTER AUTHORIZATION ON [dbo].[RenewPost] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[searchPost]    Script Date: 2016-07-28 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[searchPost]
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
ALTER AUTHORIZATION ON [dbo].[searchPost] TO  SCHEMA OWNER 
GO
USE [master]
GO
ALTER DATABASE [NSW] SET  READ_WRITE 
GO
