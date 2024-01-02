insert into AspNetRoles (Name, NormalizedName, ConcurrencyStamp)
VALUES
('Member', 'MEMBER', NEWID()),
('Admin', 'ADMIN', NEWID());


/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[Name]
      ,[NormalizedName]
      ,[ConcurrencyStamp]
  FROM [IdSrvConfig].[dbo].[AspNetRoles]