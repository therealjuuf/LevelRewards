USE [PS_GameDefs]
GO

/****** Object:  Table [dbo].[LevelRewards]    Script Date: 04/13/2018 19:12:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LevelRewards](
	[Level] [int] NOT NULL,
	[Faction] [int] NOT NULL,
	[Family] [int] NOT NULL,
	[Job] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[TypeID] [int] NOT NULL,
	[Count] [int] NOT NULL
) ON [PRIMARY]

GO


