CREATE TABLE [dbo].[Courts] (
    [Id]        INT           NOT NULL,
    [Name]      NVARCHAR (50) NULL,
    [FirstSlot] INT           CONSTRAINT [DF_Table_1_StartingAt] DEFAULT ((8)) NOT NULL,
    [LastSlot]  INT           CONSTRAINT [DF_Courts_LastSlot] DEFAULT ((20)) NOT NULL,
    CONSTRAINT [PK_Courts] PRIMARY KEY CLUSTERED ([Id] ASC)
);

