CREATE TABLE [dbo].[Attends]
(
	[UserID] UNIQUEIDENTIFIER NOT NULL , 
    [EventID] INT NOT NULL, 
    [AttendeeID] int IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    CONSTRAINT [FK_Attends_User] FOREIGN KEY ([UserID]) REFERENCES [User]([UserID]), 
    CONSTRAINT [FK_Attends_Event] FOREIGN KEY ([EventID]) REFERENCES [Event]([EventID])
)
