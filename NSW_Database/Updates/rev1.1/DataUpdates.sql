
/* update two no longer used rows to have new values */
update tblLabelText
set
fldLabel_ID = 'Japanese'
where 
fldLabel_ID = 'Master.chkJanapese'
go

update tblLabelText
set
fldLabel_ID = 'English'
where 
fldLabel_ID = 'Master.chkEnglish'
go

/* add rows to tbllabelText for new changes */
insert into tblLabelText(fldLabel_ID, fldLabel_English)
values
('Profile.LangRequired', 'You must select a preferred language.')
go
insert into tblLabelText(fldLabel_ID, fldLabel_English)
values
('Profile.LanguagePreferenceLabel', 'Choose your preferred display language:')
go
insert into tblLabelText(fldLabel_ID, fldLabel_English)
values
('Profile.ValidPhone', 'Please enter a valid phone number.')
go
insert into tblLabelText(fldLabel_ID, fldLabel_English)
values
('Profile.EmailUsed', 'The email you entered is already in use. Try recovering the password.')
go
insert into tblLabelText(fldLabel_ID, fldLabel_English)
values
('Register.LanguagePreferenceLabel', 'Select the language you would like to receive emails in:')
go
insert into tblLabelText(fldLabel_ID, fldLabel_English)
values
('Register.LangRequired', 'You must select a preferred language.')
go
insert into tblLabelText(fldLabel_ID, fldLabel_English)
values
('Search.SearchCategoryPickerLabel1', '(Leave un-selected to search all categories.)')
go
insert into tblLabelText(fldLabel_ID, fldLabel_English)
values
('Search.SearchTermLabel', 'Enter your search term:')
go
delete from tblLabelText where fldLabel_ID='Search.SearchPostDescriptionLabel'
go
delete from tblLabelText where fldLabel_ID='Search.SearchPostTitleLabel'
go

/* update tblUsers for new email language preference  */

alter table tblUsers
 add fldUser_langPref int null
 go


 update tblUsers
 set fldUser_langPref=1
 go

alter table tblUsers
  alter column fldUser_langPref int not null
go


/* addition so the program doesn't fill people'e email inboxes  */
alter table tblPosts
 add fldPost_emailSent bit not null default ((0))
 go

 alter table tblPosts
add fldPost_Renewed bit not null default((0))
go
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
go

ALTER procedure [dbo].[insertUser]
@name varchar(50), @pass varchar(100), @email varchar(50), @phone varchar(15), @postalcode varchar(15), @langPref int
as
BEGIN

insert into tblUsers 
(fldUser_Name, fldUser_Password, fldUser_Email, fldUser_Phone, fldUser_PostalCode, fldUser_Role, fldUser_langPref, fldUser_IntakeDate, fldUser_ChangeDate)
VALUES
(@name, @pass, @email, @phone, @postalcode, 'MEMBER', @langPref, GETDATE(), GETDATE())

END
go

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
go

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
go
