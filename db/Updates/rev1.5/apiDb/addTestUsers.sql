
insert into tblUsers (fldUser_id, fldUser_username, fldUser_email, fldUser_phone, fldUser_postalCode, fldUser_role, fldUser_langPref, fldUser_intakeDate, fldUser_changeDate)
values(1, 'alice','AliceSmith@email.com', '', '386-2204', 'MEMBER', 1, getdate(), getdate());

insert into tblUsers (fldUser_id, fldUser_username, fldUser_Email, fldUser_phone, fldUser_postalCode, fldUser_role, fldUser_langPref, fldUser_intakeDate, fldUser_changeDate)
values(2, 'bob', 'BobSmith@email.com', '', '386-2204', 'MEMBER', 1, getdate(), getdate());