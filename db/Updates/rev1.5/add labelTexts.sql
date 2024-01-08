use nsw
go
declare @search varchar(12) = '%master%';

select * from tblLabelText
where fldLabel_ID like @search or fldLabel_English like @search

declare @insert bit = 0;
if(@insert = 1)
BEGIN
	insert into tblLabelText (fldLabel_id, fldLabel_English, fldLabel_Japanese)
	VALUES ('Master.btnRegister','Register',N'登録');
	insert into tblLabelText (fldLabel_id, fldLabel_English, fldLabel_Japanese)
	VALUES ('Master.btnPosts','Posts',N'投稿');
END;