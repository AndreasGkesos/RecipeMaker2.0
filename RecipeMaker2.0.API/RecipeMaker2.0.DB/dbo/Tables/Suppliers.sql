CREATE TABLE [dbo].[Suppliers]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialID(), 
    [Name] NVARCHAR(50) NULL, 
    [DaysBefore] INT NULL, 
    [Preference] INT NULL, 
    [Deleted] BIT NULL, 
    [DateDeleted] DATETIME NULL, 
    [DateCreated] DATETIME NOT NULL, 
    CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED ([Id] ASC)
)
