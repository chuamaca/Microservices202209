CREATE TABLE [dbo].[MatchEvents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Action] [nvarchar](50) NOT NULL,
	[TeamId] [int] NULL,
	[TimeStamp] [datetime] NOT NULL,
	[MatchId] [nvarchar](10) NOT NULL,
	[Team1] [nvarchar](50) NULL,
	[Team2] [nvarchar](50) NULL,
	[PlayerId] [int] NULL,
 CONSTRAINT [PK_MatchEvents] PRIMARY KEY CLUSTERED ([Id] ASC) 
)