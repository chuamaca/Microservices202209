CREATE TABLE [Sales].[Store] (
    [StoreId]   INT           IDENTITY (1, 1) NOT NULL,
    [StoreName] VARCHAR (255) NOT NULL,
    [Phone]      VARCHAR (25)  NULL,
    [Email]      VARCHAR (255) NULL,
    [Street]     VARCHAR (255) NULL,
    [City]       VARCHAR (255) NULL,
    [State]      VARCHAR (10)  NULL,
    [ZipCode]   VARCHAR (5)   NULL,
    PRIMARY KEY CLUSTERED ([StoreId] ASC)
);

