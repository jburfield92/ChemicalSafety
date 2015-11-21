CREATE TABLE [dbo].[University]
(
	[UniversityID] int IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [UniversityName] NVARCHAR(64) NOT NULL, 
	[Description] NVARCHAR(256) NOT NULL, 
    [CreatedDate] DATETIME NOT NULL,      
    [SuperAdminID] UNIQUEIDENTIFIER NOT NULL, 
    [LocationID] INT NOT NULL, 
	[Picture] VARBINARY(MAX) NULL,
    CONSTRAINT [FK_University_ToTable] FOREIGN KEY ([SuperAdminID]) REFERENCES [User]([UserID]), 
    CONSTRAINT [FK_University_ToTable_1] FOREIGN KEY ([LocationID]) REFERENCES [Location]([LocationID]), 
)
