GO

CREATE TYPE dbo.GUIDs AS TABLE(GUID UNIQUEIDENTIFIER PRIMARY KEY);

GO 

CREATE PROCEDURE [dbo].[GetMealsData]
	@GUIDs [dbo].GUIDs READONLY
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	m.Id AS MealId, pq.Id AS ProductQuantityId, pq.ProductId AS ProductId, s.Id AS SupplierId,
			pq.Quantity AS PQQuantity, p.Name AS ProductName, p.Quantity AS ProductQuanyity,
			s.Name AS SupplierName, s.Preference, s.DaysBefore
	FROM dbo.Meals AS m
	INNER JOIN @GUIDs AS g
	ON m.Id = g.[GUID]
	INNER JOIN dbo.ProductQuantities AS pq
	ON m.Id = pq.MealId
	INNER JOIN dbo.Products AS p
	ON pq.ProductId = p.Id
	INNER JOIN dbo.Suppliers AS s
	ON p.SupplierId = s.Id
	order by m.Id

END
GO