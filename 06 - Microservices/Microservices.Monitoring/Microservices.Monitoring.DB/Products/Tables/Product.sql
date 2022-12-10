CREATE TABLE [Products].[Product] (
    [ProductId]   INT             IDENTITY (1, 1) NOT NULL,
    [ProductName] VARCHAR (255)   NOT NULL,
    [BrandId]     INT             NOT NULL,
    [CategoryId]  INT             NOT NULL,
    [ModelYear]   SMALLINT        NOT NULL,
    [ListPrice]   DECIMAL (10, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductId] ASC),
    FOREIGN KEY ([BrandId]) REFERENCES [Products].[Brand] ([BrandId]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([CategoryId]) REFERENCES [Products].[Category] ([CategoryId]) ON DELETE CASCADE ON UPDATE CASCADE
);

