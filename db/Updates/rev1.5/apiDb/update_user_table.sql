
alter table tblUsers drop column fldUser_Password;

alter table tblPosts drop constraint FK_tblposts_tblusers;

alter table tblUsers drop constraint PK_tblUsers;

CREATE TABLE [dbo].[tblUsers2](
	[fldUser_Name] [varchar](50) NOT NULL,
	[fldUser_Email] [varchar](50) NOT NULL,
	[fldUser_Phone] [varchar](20) NULL,
	[fldUser_PostalCode] [varchar](15) NOT NULL,
	[fldUser_Role] [varchar](15) NOT NULL,
	[fldUser_IntakeDate] [datetime] NOT NULL,
	[fldUser_ChangeDate] [datetime] NOT NULL,
	[fldUser_id] [int] NOT NULL,
	[fldUser_langPref] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblUsers2]  WITH CHECK ADD  CONSTRAINT [FK_tblUsers2_tblPostalCodes] FOREIGN KEY([fldUser_PostalCode])
REFERENCES [dbo].[tblPostalCodes] ([fldPostal_Code])
GO

ALTER TABLE [dbo].[tblUsers2] CHECK CONSTRAINT [FK_tblUsers2_tblPostalCodes]
GO


alter table tblUsers switch to tblusers2;
GO

if not exists (select * from tblUsers) drop table tblUsers;
exec sp_rename 'FK_tblUsers2_tblPostalCodes', 'FK_tblUsers_tblPostalCodes', 'OBJECT';
exec sp_rename 'tblUsers2', 'tblUsers', 'OBJECT';
GO

alter table tblUsers add constraint PK_tblUsers PRIMARY KEY CLUSTERED (fldUser_id ASC);
GO

alter table tblPosts WITH CHECK add constraint FK_tblPosts_tblUsers FOREIGN KEY (fldPost_userID)
REFERENCES dbo.tblUsers (fldUser_id);
GO

alter table dbo.tblPosts CHECK CONSTRAINT [FK_tblPosts_tblUsers];
GO

exec sp_rename 'tblUsers.fldUser_Name', 'fldUser_UserName', 'COLUMN';
GO
