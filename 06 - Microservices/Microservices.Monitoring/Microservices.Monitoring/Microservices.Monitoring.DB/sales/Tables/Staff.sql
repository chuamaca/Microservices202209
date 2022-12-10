CREATE TABLE [Sales].[Staff] (
    [StaffId]   INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] VARCHAR (50)  NOT NULL,
    [LastName]  VARCHAR (50)  NOT NULL,
    [Email]      VARCHAR (255) NOT NULL,
    [Phone]      VARCHAR (25)  NULL,
    [Active]     TINYINT       NOT NULL,
    [StoreId]   INT           NOT NULL,
    [ManagerId] INT           NULL,
    PRIMARY KEY CLUSTERED ([StaffId] ASC),
    FOREIGN KEY ([ManagerId]) REFERENCES [Sales].[Staff] ([StaffId]),
    FOREIGN KEY ([StoreId]) REFERENCES [Sales].[Store] ([StoreId]) ON DELETE CASCADE ON UPDATE CASCADE,
    UNIQUE NONCLUSTERED ([email] ASC)
);

