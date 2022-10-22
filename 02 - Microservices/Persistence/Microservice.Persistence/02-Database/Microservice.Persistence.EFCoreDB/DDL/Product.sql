CREATE TABLE [dbo].[Product]
(
	ProductId INT NOT NULL IDENTITY PRIMARY KEY,
	[Description] VARCHAR(250),
	Price DECIMAL(30,6),
	Unit VARCHAR(3)
)