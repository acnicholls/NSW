
ALTER procedure [dbo].[insertUser]
@id int,
@username varchar(50), 
@email varchar(50), 
@phone varchar(15), 
@postalcode varchar(15), 
@langPref int
as
BEGIN

insert into tblUsers 
(fldUser_id, fldUser_UserName, fldUser_Email, fldUser_Phone, fldUser_PostalCode, fldUser_Role, fldUser_langPref, fldUser_IntakeDate, fldUser_ChangeDate)
VALUES
(@id, @username, @email, @phone, @postalcode, 'MEMBER', @langPref, GETDATE(), GETDATE())

END
