CREATE TABLE [dbo].[Event]
(
	[EventID] int IDENTITY(1,1)  NOT NULL PRIMARY KEY, 
    [EventName] NVARCHAR(64) NOT NULL, 
    [DatePublished] DATETIME NOT NULL, 
    [Approved] BIT NOT NULL DEFAULT 0, 
    [ContactPhone] NVARCHAR(16) NOT NULL, 
    [ContactEmail] NVARCHAR(128) NOT NULL, 
    [Description] NVARCHAR(256) NOT NULL, 
    [EventDate] DATETIME NOT NULL, 
    [LocationID] INT NOT NULL, 
    [EventTypeID] INT NOT NULL, 
    [EventCategoryID] INT NOT NULL, 
    [Picture] VARBINARY(MAX) NULL, 
    [AdminID] UNIQUEIDENTIFIER NOT NULL, 
    [UniversityID] INT NOT NULL, 
    [RSOID] INT NULL, 
    CONSTRAINT [FK_Event_Location] FOREIGN KEY ([LocationID]) REFERENCES [Location]([LocationID]), 
    CONSTRAINT [FK_Event_EventType] FOREIGN KEY ([EventTypeID]) REFERENCES [EventType]([EventTypeID]), 
    CONSTRAINT [FK_Event_EventCategory] FOREIGN KEY ([EventCategoryID]) REFERENCES [EventCategory]([EventCategoryID]),
	CONSTRAINT [FK_Event_User] FOREIGN KEY ([AdminID]) REFERENCES [User](UserID),
	CONSTRAINT [FK_Event_University] FOREIGN KEY ([UniversityID]) REFERENCES [University](UniversityID),
	CONSTRAINT [FK_Event_RSO] FOREIGN KEY ([RSOID]) REFERENCES [RSO](RSOID)
)
