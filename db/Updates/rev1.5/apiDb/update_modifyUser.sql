
ALTER procedure [dbo].[modifyUser]
@ID int, @username varchar(50), @email varchar(50), @phone varchar(15), @postalcode varchar(15), @langPref int
as
BEGIN

update tblUsers 
SET
fldUser_UserName = @username,
fldUser_Email = @email,
fldUser_Phone = @phone, 
fldUser_PostalCode = @postalcode, 
fldUser_langPref = @langPref,
fldUser_ChangeDate = GETDATE()
where
fldUser_ID = @ID

END
