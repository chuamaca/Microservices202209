CREATE TABLE [dbo].[LoggedEvents] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Action]      NVARCHAR (50)  NOT NULL,
    [AggregateId] INT            NOT NULL,
    [Cargo]       NVARCHAR (MAX) NULL,
    [TimeStamp]   DATETIME       NULL,
    CONSTRAINT [PK_LoggedEvents] PRIMARY KEY CLUSTERED ([Id] ASC)
);

