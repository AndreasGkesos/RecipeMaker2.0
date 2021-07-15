/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


insert into dbo.Suppliers ([Id], [Name], [Preference], [DaysBefore], [DateCreated], [Deleted]) values ('dcdfa9b9-c2ef-4e1e-8aa6-37e65da2146f', 'Supplier1', 1, 2, GETUTCDATE(), 0)
insert into dbo.Suppliers ([Id], [Name], [Preference], [DaysBefore], [DateCreated], [Deleted]) values ('853b9a53-cab4-4d12-b228-d92c5efeaa6d', 'Supplier2', 2, 1, GETUTCDATE(), 0)
insert into dbo.Suppliers ([Id], [Name], [Preference], [DaysBefore], [DateCreated], [Deleted]) values ('8369773d-cfff-4751-9abe-b5b9882d8f09', 'Supplier3', 2, 1, GETUTCDATE(), 0)
insert into dbo.Suppliers ([Id], [Name], [Preference], [DaysBefore], [DateCreated], [Deleted]) values ('b794be71-b7a4-49dd-8190-eda11ffb707b', 'Supplier4', 1, 1, GETUTCDATE(), 0)

insert into dbo.Products ([Id], [Name], [Quantity], [SupplierId], [DateCreated], [Deleted]) values ('14f74c88-9ee3-4813-ab8d-a6bd3241f393', 'alati', 1000, 'dcdfa9b9-c2ef-4e1e-8aa6-37e65da2146f', GETUTCDATE(), 0)
insert into dbo.Products ([Id], [Name], [Quantity], [SupplierId], [DateCreated], [Deleted]) values ('a97c9493-4e7b-4b5c-8f17-5b1e7e3c3887', 'patates', 10000, '853b9a53-cab4-4d12-b228-d92c5efeaa6d', GETUTCDATE(), 0)
insert into dbo.Products ([Id], [Name], [Quantity], [SupplierId], [DateCreated], [Deleted]) values ('5b9caacd-dd6e-4256-8bc2-d50de0276d25', 'kitrino turi', 3000, '8369773d-cfff-4751-9abe-b5b9882d8f09', GETUTCDATE(), 0)
insert into dbo.Products ([Id], [Name], [Quantity], [SupplierId], [DateCreated], [Deleted]) values ('a65c1846-5a05-4a31-908c-ec8638d4f7fb', 'banana', 10000, 'b794be71-b7a4-49dd-8190-eda11ffb707b', GETUTCDATE(), 0)
insert into dbo.Products ([Id], [Name], [Quantity], [SupplierId], [DateCreated], [Deleted]) values ('11897914-39fa-4bc3-83ce-3624823c2abf', 'fraoula', 10000, 'b794be71-b7a4-49dd-8190-eda11ffb707b', GETUTCDATE(), 0)
insert into dbo.Products ([Id], [Name], [Quantity], [SupplierId], [DateCreated], [Deleted]) values ('4ab5dfd1-f0ba-4664-bed5-c6ba32e98249', 'milo', 10000, 'b794be71-b7a4-49dd-8190-eda11ffb707b', GETUTCDATE(), 0)

insert into dbo.Meals([Id], [Name], [DateCreated], [Deleted]) values ('0e41c283-556c-463f-9948-3f702abac832', 'patates tiganites', GETUTCDATE(), 0)
insert into dbo.Meals([Id], [Name], [DateCreated], [Deleted]) values ('2522588b-8279-4356-aa1b-b40eb5b4979f', 'froutosalata', GETUTCDATE(), 0)
insert into dbo.Meals([Id], [Name], [DateCreated], [Deleted]) values ('c2292deb-f4c0-48ad-b8f3-b6537969d121', 'mikrifroutosalata', GETUTCDATE(), 0)

insert into dbo.ProductQuantities([Id], [Quantity], [MealId], [ProductId], [DateCreated], [Deleted]) values ('44527392-80d9-4311-aa27-111923fb1536', 10, '0e41c283-556c-463f-9948-3f702abac832', '14f74c88-9ee3-4813-ab8d-a6bd3241f393', GETUTCDATE(), 0)
insert into dbo.ProductQuantities([Id], [Quantity], [MealId], [ProductId], [DateCreated], [Deleted]) values ('9ac294f2-98ef-4c22-960d-b1742b8f10f8', 300, '0e41c283-556c-463f-9948-3f702abac832', 'a97c9493-4e7b-4b5c-8f17-5b1e7e3c3887', GETUTCDATE(), 0)
insert into dbo.ProductQuantities([Id], [Quantity], [MealId], [ProductId], [DateCreated], [Deleted]) values ('ca21ebdb-7cee-41f8-a02d-06a148690fd0', 100, '0e41c283-556c-463f-9948-3f702abac832', '5b9caacd-dd6e-4256-8bc2-d50de0276d25', GETUTCDATE(), 0)

insert into dbo.ProductQuantities([Id], [Quantity], [MealId], [ProductId], [DateCreated], [Deleted]) values ('39bdb4f3-3bd1-44be-9b4e-019e45a18f53', 150, '2522588b-8279-4356-aa1b-b40eb5b4979f', 'a65c1846-5a05-4a31-908c-ec8638d4f7fb', GETUTCDATE(), 0)
insert into dbo.ProductQuantities([Id], [Quantity], [MealId], [ProductId], [DateCreated], [Deleted]) values ('e909ea10-be05-4f0d-817e-7a66a2e5a281', 100, '2522588b-8279-4356-aa1b-b40eb5b4979f', '11897914-39fa-4bc3-83ce-3624823c2abf', GETUTCDATE(), 0)
insert into dbo.ProductQuantities([Id], [Quantity], [MealId], [ProductId], [DateCreated], [Deleted]) values ('0f88a4d6-0e2c-48e9-9191-454ea2505935', 200, '2522588b-8279-4356-aa1b-b40eb5b4979f', '4ab5dfd1-f0ba-4664-bed5-c6ba32e98249', GETUTCDATE(), 0)

insert into dbo.ProductQuantities([Id], [Quantity], [MealId], [ProductId], [DateCreated], [Deleted]) values ('675bcf81-93a0-4ebd-aa11-ffb071933521', 150, 'c2292deb-f4c0-48ad-b8f3-b6537969d121', 'a65c1846-5a05-4a31-908c-ec8638d4f7fb', GETUTCDATE(), 0)
insert into dbo.ProductQuantities([Id], [Quantity], [MealId], [ProductId], [DateCreated], [Deleted]) values ('ba847eb6-ff05-40f3-a027-f2a98d9ca418', 100, 'c2292deb-f4c0-48ad-b8f3-b6537969d121', '11897914-39fa-4bc3-83ce-3624823c2abf', GETUTCDATE(), 0)