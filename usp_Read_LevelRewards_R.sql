USE PS_GameDefs

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_Read_LevelRewards_R
AS
BEGIN
	
	SELECT [Level]
      ,[Faction]
      ,[Family]
      ,[Job]
      ,[Type]
      ,[TypeID]
      ,[Count]
  FROM [PS_GameDefs].[dbo].[LevelRewards] order by level desc
	
END