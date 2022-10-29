CREATE TABLE [dbo].[Matches] (
    [Id]        NVARCHAR (10) NOT NULL,
    [Team1]     NVARCHAR (50) NOT NULL,
    [Team2]     NVARCHAR (50) NOT NULL,
    [State]     INT           CONSTRAINT [DF_Matches_State] DEFAULT ((0)) NOT NULL,
    [Score1]    INT           CONSTRAINT [DF_Matches_Score1] DEFAULT ((0)) NOT NULL,
    [Score2]    INT           CONSTRAINT [DF_Matches_Score2] DEFAULT ((0)) NOT NULL,
    [Period]    INT           CONSTRAINT [DF_Matches_Period] DEFAULT ((0)) NOT NULL,
    [Timeouts1] NVARCHAR (10) NULL,
    [Timeouts2] NVARCHAR (10) NULL,
    CONSTRAINT [PK_Matches] PRIMARY KEY CLUSTERED ([Id] ASC)
);

