CREATE TABLE [dbo].[User]
(
	[UserID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserName] NVARCHAR(64) NOT NULL, 
    [FirstName] NVARCHAR(64) NOT NULL, 
    [LastName] NVARCHAR(64) NOT NULL, 
    [PasswordSalt] NVARCHAR(128) NOT NULL, 
    [PasswordHash] NVARCHAR(128) NOT NULL, 
    [Email] NVARCHAR(64) NOT NULL, 
    [IsSuperAdmin] BIT NOT NULL DEFAULT 0, 
    [Picture] VARBINARY(MAX) NULL,
	[UniversityID] INT NULL, 
    CONSTRAINT [FK_User_ToTable] FOREIGN KEY ([UniversityID]) REFERENCES [University]([UniversityID])

)
