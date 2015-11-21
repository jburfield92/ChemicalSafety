CREATE TABLE [dbo].[EventRating]
(
	[EventRatingID] int IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [UserID] UNIQUEIDENTIFIER NOT NULL, 
    [EventID] INT NOT NULL, 
    [Rating] INT NOT NULL, 
    CONSTRAINT [FK_EventRating_User] FOREIGN KEY ([UserID]) REFERENCES [User]([UserID]), 
    CONSTRAINT [FK_EventRating_Event] FOREIGN KEY ([EventID]) REFERENCES [Event]([EventID])
)
