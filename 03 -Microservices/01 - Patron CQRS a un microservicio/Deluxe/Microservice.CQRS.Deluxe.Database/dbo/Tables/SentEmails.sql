CREATE TABLE [dbo].[SentEmails] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Address] NVARCHAR (50)  NOT NULL,
    [Body]    NVARCHAR (200) NOT NULL,
    [Sent]    DATETIME       NOT NULL,
    CONSTRAINT [PK_SentEmails] PRIMARY KEY CLUSTERED ([Id] ASC)
);

