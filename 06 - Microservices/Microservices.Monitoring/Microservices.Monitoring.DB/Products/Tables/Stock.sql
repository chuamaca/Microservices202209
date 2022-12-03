CREATE TABLE [Products].[Stock] (
    [StoreId]   INT NOT NULL,
    [ProductId] INT NOT NULL,
    [Quantity]   INT NULL,
    PRIMARY KEY CLUSTERED ([StoreId] ASC, [ProductId] ASC),
    FOREIGN KEY ([ProductId]) REFERENCES [Products].[Product] ([ProductId]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([StoreId]) REFERENCES [Sales].[Store] ([StoreId]) ON DELETE CASCADE ON UPDATE CASCADE
);

