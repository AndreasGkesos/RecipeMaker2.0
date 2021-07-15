CREATE TABLE [dbo].[Products]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialID(), 
    [Name] NVARCHAR(50) NULL,
    [Quantity] INT NULL, 
    [SupplierId] UNIQUEIDENTIFIER NOT NULL,
    [Deleted] BIT NULL, 
    [DateDeleted] DATETIME NULL, 
    [DateCreated] DATETIME NULL, 
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Product_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [dbo].[Suppliers]([Id])
)
