CREATE TABLE [dbo].[RoomRules] (
    [Id]                 INT  NOT NULL,
    [RoomId]             INT  NULL,
    [Slot]               INT  NOT NULL,
    [StartTimeOfDayHour] INT  NOT NULL,
    [StartTimeOfDayMin]  INT  NOT NULL,
    [ValidSince]         DATE NULL,
    CONSTRAINT [PK_RoomRules] PRIMARY KEY CLUSTERED ([Id] ASC)
);

