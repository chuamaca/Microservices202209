CREATE TABLE [dbo].[MatchEvents] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [MatchId]   NVARCHAR (10) NOT NULL,
    [Action]    NVARCHAR (50) NOT NULL,
    [TeamId]    INT           NULL,
    [PlayerId]  INT           NULL,
    [TimeStamp] DATETIME      NOT NULL,
    [Team1]     NVARCHAR (50) NULL,
    [Team2]     NVARCHAR (50) NULL,
    CONSTRAINT [PK_MatchEvents] PRIMARY KEY CLUSTERED ([Id] ASC)
);

