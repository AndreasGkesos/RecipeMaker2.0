CREATE TABLE [dbo].[Meals]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialID(),
    [Name] NVARCHAR(50) NULL,
	[Deleted] BIT NULL, 
    [DateDeleted] DATETIME NULL, 
    [DateCreated] DATETIME NULL, 
    CONSTRAINT [PK_Meals] PRIMARY KEY CLUSTERED ([Id] ASC)
)
