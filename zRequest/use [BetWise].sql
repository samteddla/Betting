use [BetWise];

select * from BetCard where BetCardId = 7;
select * from BetSelection where BetCardId = 7;
select * from BetResult where MatchTypeId = 3 and MatchSelectionId = 1 and MatchId = 6

select * from BetCard 
select * from BetSelection
select * from BetResult
select * from [Match];
select * from BetMatchType;
select * from MatchSelection;
select * from MatchSelectionMatch;
select * from BetResult;
select * from Outcome;
SELECT * FROM Team;

select * from BetCard b
left join BetSelection s on b.BetCardId = s.BetCardId
left join BetMatchType m on b.MatchTypeId = m.MatchTypeId
left join MatchSelection ms on b.MatchSelectionId = ms.MatchSelectionId
where b.PersonId = 1 and b.BetCardId = 1;

select * from BetCard b
left join MatchSelection ms on b.MatchSelectionId = ms.MatchSelectionId
left join BetMatchType m on b.MatchTypeId = m.MatchTypeId
where b.PersonId = 1;

select * from BetResult r 
where r.MatchId = 10
and r.MatchSelectionId = 9
and r.MatchTypeId = 2;

update BetResult set Outcome = 1 
where MatchId = 10
and MatchSelectionId = 9
and MatchTypeId = 2;

delete BetResult 
where BetResultId = 13


DECLARE @__request_MatchSelectionId_0 int = 4;

DECLARE @__request_MatchId_0 int = 10;
DECLARE @__request_MatchTypeId_0 int = 10;
DECLARE @__request_MatchSelectionId_1 int = 9;
DECLARE @__request_MatchTypeId_2 int = 10;
declare @__request_CardId_0 int = 1;
declare @___userContext_UserId_1 int = 1;


SELECT CASE
    WHEN [t].[BetResultId] IS NOT NULL THEN [t].[Outcome]
    ELSE 0
END AS [MatchResultId], [b0].[Outcome] AS [BetSelectionOutcome], [b0].[BetSelectionId], [b0].[MatchId], [b].[BetCardId], [b].[BetAmount], [b].[WonAmount], [b].[MatchTypeId], CASE
    WHEN [t].[BetResultId] IS NULL THEN 0
    WHEN [t].[MatchTypeId] = [b].[MatchTypeId] THEN [b].[MatchTypeId]
    ELSE 0
END AS [ReultMatchTypeId], CASE
    WHEN ([t].[BetResultId] IS NULL) OR [t].[Outcome] = 0 THEN N'NotPlayed'
    WHEN [t].[Outcome] = [b0].[Outcome] THEN N'Win'
    ELSE N'Loss'
END AS [MatchResult], CASE
    WHEN ([t].[BetResultId] IS NULL) OR [t].[Outcome] = 0 THEN 0
    WHEN [t].[Outcome] = [b0].[Outcome] THEN 1
    ELSE 0
END AS [WinCount]
FROM [BetCard] AS [b]
INNER JOIN [BetSelection] AS [b0] ON [b].[BetCardId] = [b0].[BetCardId]
INNER JOIN [BetMatchType] AS [b1] ON [b].[MatchTypeId] = [b1].[MatchTypeId]
LEFT JOIN (
    SELECT [b2].[BetResultId], [b2].[MatchId], [b2].[MatchSelectionId], [b2].[MatchTypeId], [b2].[Outcome], [b3].[MatchTypeId] AS [MatchTypeId0]
    FROM [BetResult] AS [b2]
    INNER JOIN [BetMatchType] AS [b3] ON [b2].[MatchTypeId] = [b3].[MatchTypeId]
) AS [t] ON [b0].[MatchId] = [t].[MatchId] AND [b].[MatchSelectionId] = [t].[MatchSelectionId] AND [b1].[MatchTypeId] = [t].[MatchTypeId0]
WHERE [b].[BetCardId] = @__request_CardId_0 AND [b].[PersonId] = @___userContext_UserId_1

SELECT CASE
    WHEN EXISTS (
        SELECT 1
        FROM [BetResult] AS [b]
        WHERE [b].[MatchId] = @__request_MatchId_0 AND [b].[MatchSelectionId] = @__request_MatchSelectionId_1 AND [b].[MatchTypeId] = @__request_MatchTypeId_2) THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END

SELECT [m].[MatchSelectionId], [m].[ActiveUntil], [m].[CreatedAt], [m].[Description], [m].[IsEnabled], [m].[Name], [t].[Id], [t].[MatchId], [t].[SelectionId], [t].[MatchId0], [t].[AwayScore], [t].[AwayTeamId], [t].[HomeScore], [t].[HomeTeamId], [t].[MatchDate]
FROM [MatchSelection] AS [m]
LEFT JOIN (
    SELECT [m0].[Id], [m0].[MatchId], [m0].[SelectionId], [m1].[MatchId] AS [MatchId0], [m1].[AwayScore], [m1].[AwayTeamId], [m1].[HomeScore], [m1].[HomeTeamId], [m1].[MatchDate]
    FROM [MatchSelectionMatch] AS [m0]
    INNER JOIN [Match] AS [m1] ON [m0].[MatchId] = [m1].[MatchId]
) AS [t] ON [m].[MatchSelectionId] = [t].[SelectionId]
WHERE [m].[MatchSelectionId] = @__request_MatchSelectionId_0
ORDER BY [m].[MatchSelectionId], [t].[Id]

SELECT [m].[MatchSelectionId], [m].[Name], [m].[Description], [m].[ActiveUntil], [m0].[SelectionId], [m0].[MatchId], [t].[TeamId] AS [HomeId], [t].[TeamName] AS [Home], [t].[ShortName] AS [HomeTeam], [t0].[TeamId] AS [AwayId], [t0].[TeamName] AS [AwayTeam], [t0].[ShortName] AS [Away], [m1].[MatchDate]
FROM [MatchSelection] AS [m]
INNER JOIN [MatchSelectionMatch] AS [m0] ON [m].[MatchSelectionId] = [m0].[SelectionId]
INNER JOIN [Match] AS [m1] ON [m0].[MatchId] = [m1].[MatchId]
INNER JOIN [Team] AS [t] ON [m1].[HomeTeamId] = [t].[TeamId]
INNER JOIN [Team] AS [t0] ON [m1].[AwayTeamId] = [t0].[TeamId]
WHERE [m].[MatchSelectionId] = @__request_MatchSelectionId_0

select * from MatchSelection m
inner join MatchSelectionMatch ms on m.MatchSelectionId = ms.SelectionId
inner join [Match] ma on ms.MatchId = ma.MatchId
inner join Team t on ma.HomeTeamId = t.TeamId
inner join Team t2 on ma.AwayTeamId = t2.TeamId
where m.MatchSelectionId = 4;
;



DECLARE @request_MatchId_0 int = 8;
DECLARE @request_MatchSelectionId_1 int = 1;
DECLARE @request_MatchTypeId_2 int = 2;

SELECT [b].[BetResultId], [b].[MatchId], [b].[MatchSelectionId], [b].[MatchTypeId], [b].[Outcome], [b].[ResultDate]
FROM [BetResult] AS [b]
WHERE [b].[MatchId] = @request_MatchId_0 
AND [b].[MatchSelectionId] = @request_MatchSelectionId_1 
AND [b].[MatchTypeId] = @request_MatchTypeId_2