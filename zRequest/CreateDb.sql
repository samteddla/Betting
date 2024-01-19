use master;

DECLARE @DatabaseName nvarchar(128) = N'$(DatabaseName)';
DECLARE @DataPath nvarchar(128) = N'$(DataPath)';
DECLARE @LogPath nvarchar(128) = N'$(LogPath)';

CREATE DATABASE [$(DatabaseName)]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'$(DatabaseName)', FILENAME = N'$(DataPath)$(DatabaseName).mdf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'$(DatabaseName)_log', FILENAME = N'$(LogPath)$(DatabaseName)_log.ldf' , SIZE = 8192KB , FILEGROWTH = 65536KB )

DECLARE @__request_MatchSelectionId_0 int = 4;

SELECT [m].[MatchSelectionId], [m].[ActiveUntil], [m].[CreatedAt], [m].[Description], [m].[IsEnabled], [m].[Name], [t].[Id], [t].[MatchId], [t].[SelectionId], [t].[MatchId0], [t].[AwayScore], [t].[AwayTeamId], [t].[HomeScore], [t].[HomeTeamId], [t].[MatchDate]
FROM [MatchSelection] AS [m]
LEFT JOIN (
    SELECT [m0].[Id], [m0].[MatchId], [m0].[SelectionId], [m1].[MatchId] AS [MatchId0], [m1].[AwayScore], [m1].[AwayTeamId], [m1].[HomeScore], [m1].[HomeTeamId], [m1].[MatchDate]
    FROM [MatchSelectionMatch] AS [m0]
    INNER JOIN [Match] AS [m1] ON [m0].[MatchId] = [m1].[MatchId]
) AS [t] ON [m].[MatchSelectionId] = [t].[SelectionId]
WHERE [m].[MatchSelectionId] = @__request_MatchSelectionId_0
ORDER BY [m].[MatchSelectionId], [t].[Id]