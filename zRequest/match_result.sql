
SELECT r.*, b.TeamName as Home, a.TeamName As Away, t.Name
  FROM BetResult r
  left join Match m on m.MatchId = r.MatchId
  left join Team a on a.TeamId = m.AwayTeamId
  left join Team b on b.TeamId = m.HomeTeamId
  left join BetMatchType t on t.MatchTypeId = r.MatchTypeId
where r.MatchSelectionId = 1
and r.MatchTypeId = 3

select b.*, c.MatchSelectionId, c.MatchTypeId from BetSelection b
left join BetCard c on c.BetCardId = b.BetCardId
where b.BetCardId = 14

select * from BetResult
where MatchSelectionId = 1
and MatchTypeId = 3

select * from Match
