CREATE TABLE [dbo].[RSO]
(
	[RSOID] int IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [RSOName] NVARCHAR(64) NOT NULL, 
    [Approved] BIT NOT NULL DEFAULT 0, 
    [Description] NVARCHAR(256) NOT NULL, 
    [CreatedDate] DATETIME NOT NULL,   
    [AdminID] UNIQUEIDENTIFIER NOT NULL,  
    [UniversityID] INT NOT NULL, 
	[Picture] VARBINARY(MAX) NULL, 
    CONSTRAINT [FK_RSO_User] FOREIGN KEY ([AdminID]) REFERENCES [User]([UserID]), 
    CONSTRAINT [FK_RSO_UniversityID] FOREIGN KEY ([UniversityID]) REFERENCES [University]([UniversityID])
)
