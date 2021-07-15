CREATE TABLE [dbo].[ProductQuantities]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialID(), 
    [Quantity] INT NULL, 
    [ProductId] UNIQUEIDENTIFIER NULL,
    [MealId] UNIQUEIDENTIFIER NULL,
    [Deleted] BIT NULL, 
    [DateDeleted] DATETIME NULL, 
    [DateCreated] DATETIME NULL, 
    CONSTRAINT [PK_ProductQuantity] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_ProductQuantity_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products]([Id]), 
    CONSTRAINT [FK_ProductQuantities_Meal] FOREIGN KEY ([MealId]) REFERENCES [dbo].[Meals]([Id])
)
