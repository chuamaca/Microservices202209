CREATE TABLE [dbo].[Matches](
	[Id] [nvarchar](128) NOT NULL,
	[Team1] [nvarchar](max) NULL,
	[Team2] [nvarchar](max) NULL,
	[State] [int] NOT NULL,
	[Score1] [int] NOT NULL,
	[Score2] [int] NOT NULL,
	[Period] [int] NOT NULL,
	[Timeouts1] [nvarchar](max) NULL,
	[Timeouts2] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Matches] PRIMARY KEY CLUSTERED ([Id] ASC)
)


