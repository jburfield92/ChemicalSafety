CREATE TABLE [dbo].[EventComment]
(
	[CommentID] INT NOT NULL PRIMARY KEY, 
    [EventID] INT NOT NULL , 
    [UserID] UNIQUEIDENTIFIER NOT NULL ,  
    [Comment] NVARCHAR(256) NULL, 
    [CommentDate] DATETIME NOT NULL, 
    CONSTRAINT [FK_EventComment_Event] FOREIGN KEY ([EventID]) REFERENCES [Event]([EventID]), 
    CONSTRAINT [FK_EventComment_User] FOREIGN KEY ([UserID]) REFERENCES [User]([UserID])
)
