CREATE TABLE [dbo].[Bookings] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [RequestId]  VARCHAR (50)   CONSTRAINT [DF_Bookings_Guid] DEFAULT (newid()) NOT NULL,
    [Name]       NVARCHAR (50)  NOT NULL,
    [CourtId]    INT            NOT NULL,
    [StartingAt] INT            NOT NULL,
    [Length]     INT            CONSTRAINT [DF_Bookings_Length] DEFAULT ((1)) NOT NULL,
    [Notes]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Bookings_Courts] FOREIGN KEY ([CourtId]) REFERENCES [dbo].[Courts] ([Id])
);

