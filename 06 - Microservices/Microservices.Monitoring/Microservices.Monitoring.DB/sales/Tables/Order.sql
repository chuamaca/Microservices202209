CREATE TABLE [Sales].[Order] (
    [OrderId]      INT     IDENTITY (1, 1) NOT NULL,
    [CustomerId]   INT     NULL,
    [OrderStatus]  TINYINT NOT NULL,
    [OrderDate]    DATE    NOT NULL,
    [RequiredDate] DATE    NOT NULL,
    [ShippedDate]  DATE    NULL,
    [StoreId]      INT     NOT NULL,
    [StaffId]      INT     NOT NULL,
    PRIMARY KEY CLUSTERED ([OrderId] ASC),
    FOREIGN KEY ([CustomerId]) REFERENCES [Sales].[Customer] ([CustomerId]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([StaffId]) REFERENCES [Sales].[Staff] ([StaffId]),
    FOREIGN KEY ([StoreId]) REFERENCES [Sales].[Store] ([StoreId]) ON DELETE CASCADE ON UPDATE CASCADE
);

