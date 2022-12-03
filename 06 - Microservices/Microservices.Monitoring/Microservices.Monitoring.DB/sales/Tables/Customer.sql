CREATE TABLE [Sales].[Customer] (
    [CustomerId] INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]  VARCHAR (255) NOT NULL,
    [LastName]   VARCHAR (255) NOT NULL,
    [Phone]       VARCHAR (25)  NULL,
    [Email]       VARCHAR (255) NOT NULL,
    [Street]      VARCHAR (255) NULL,
    [City]        VARCHAR (50)  NULL,
    [State]       VARCHAR (25)  NULL,
    [ZipCode]    VARCHAR (5)   NULL,
    PRIMARY KEY CLUSTERED ([CustomerId] ASC)
);

