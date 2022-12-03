CREATE TABLE [sales].[OrderItem] (
    [OrderItemId]    INT             NOT NULL,
    [OrderId]   INT             NOT NULL,    
    [ProductId] INT             NOT NULL,
    [Quantity]   INT             NOT NULL,
    [ListPrice] DECIMAL (10, 2) NOT NULL,
    [Discount]   DECIMAL (4, 2)  DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([OrderId] ASC, [OrderItemId] ASC),
    FOREIGN KEY ([OrderId]) REFERENCES [Sales].[Order] ([OrderId]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([ProductId]) REFERENCES [Products].[Product] ([ProductId]) ON DELETE CASCADE ON UPDATE CASCADE
);

