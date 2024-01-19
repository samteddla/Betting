
using Microsoft.EntityFrameworkCore;
using psamson.Context;
using psamson.Model;
using Sqids;
class Program
{
    static void Main()
    {
        //DeleteDb();
        // CreateDBandAddTestData(true);
        CreateDBandAddTestData();
    }

    private static void CreateDBandAddTestData(bool test = false)
    {
        using (var context = new BetContext())
        {
            context.Database.EnsureCreated();
            if (test)
            {
                // create first time data
                CreateFirstTimeData(context);
            }
            else
            {
                // Show played matches
                GetBetSelection(context, 1);
                // Show played matches with win/loss
                GetBetSelectionWinLoss(context, 1);
            }
        }
    }

    private static void CreateFirstTimeData(BetContext context)
    {
        AddUser(context); // 1 Time only
        AddMatchTypes(context); // 1 Time only, only in db
        AddOutCome(context); // 1 Time only, only in db
        AddTeams(context);
        AddMatches(context);
        CreateLottery(context);
        CreateMachesForLottery(context);
        BetOn(context, 1, 1);
        CreateResults(context);
    }

    private static void DeleteDb()
    {
        using (var context = new BetContext())
        {
            context.Database.EnsureDeleted();
        }
    }

    public static void BetOn(BetContext _context, int matchSelectionId, List<BetSelection> betSelection)
    {
        var card = new BetCard
        {
            PersonId = personId,
            BetDate = DateTime.Now,
            MatchTypeId = matchTypeId,
            MatchSelectionId = matchSelectionId,
        };
        
        betSelection.ForEach(x => x.BetCard = card);

        _context.BetSelections.AddRange(betSelection);
        _context.SaveChanges();
    }
    public static void CreateLotteryWithSelection(BetContext _context, list<int> maches)
    {
        var matchSelection = new MatchSelection
        {
            Name = "Sunday Lotto",
            Description = "Suunday Week 5 Lotto",
            IsEnabled = true
        };

        var query = from b in _context.Matches
                    where b.MatchId in maches
                    select b;

        var MatchSelectionMatchType = query.Select(x => new MatchSelectionMatch
        {
            MatchSelection = matchSelection,
            Match = x
        }).ToList();

        _context.MatchSelectionMatches.AddRange(MatchSelectionMatchType);
        _context.SaveChanges();
    }
    private static void CreateLottery(BetContext context)
    {
        var matchSelection = new MatchSelection
        {
            Name = "Sunday Lotto",
            Description = "Suunday Week 5 Lotto",
        };
        context.MatchSelections.Add(matchSelection);
        context.SaveChanges();
    }
  
    private static void CreateResults(BetContext context)
    {
        DateTime date = DateTime.Now.Date.AddDays(3);
        var betResults = new List<BetResult>
            {
                new () { MatchId = 1, Outcome = 1, ResultDate = date, MatchTypeId = 1 },
                new () { MatchId = 2, Outcome = 2, ResultDate = date, MatchTypeId = 1 },
                new () { MatchId = 3, Outcome = 3, ResultDate = date, MatchTypeId = 1 },
                new () { MatchId = 4, Outcome = 4, ResultDate = date, MatchTypeId = 1 },
                new () { MatchId = 5, Outcome = 5, ResultDate = date, MatchTypeId = 1 },
                new () { MatchId = 6, Outcome = 6, ResultDate = date, MatchTypeId = 1 },
                new () { MatchId = 7, Outcome = 7, ResultDate = date, MatchTypeId = 1 },
                new () { MatchId = 8, Outcome = 1, ResultDate = date, MatchTypeId = 1 },
                new () { MatchId = 9, Outcome = 2, ResultDate = date, MatchTypeId = 1 },
                new () { MatchId = 10, Outcome = 4, ResultDate = date, MatchTypeId = 1 },
                new () { MatchId = 11, Outcome = 5, ResultDate = date, MatchTypeId = 1 },
                new () { MatchId = 12, Outcome = 6, ResultDate = date, MatchTypeId = 1 }
            };

        context.BetResults.AddRange(betResults);
        context.SaveChanges();
    }

    //TODO : List of BetSelection also should be paramter
    private static void BetOn(BetContext context, int personId, int matchTypeId, List<BetSelection> betSelections = null)
    {
        
        var card = new BetCard
        {
            PersonId = personId,
            BetDate = DateTime.Now,
            MatchTypeId = matchTypeId,
            MatchSelectionId = 1,
        };

        // var card = context.BetCards.Where(x => x.BetCardId == 1).FirstOrDefault();

        var my12BetSelections = new List<BetSelection>
            {
                new() { BetCard = card, MatchId = 1, Outcome = 1 },
                new() { BetCard = card, MatchId = 2, Outcome = 2 },
                new() { BetCard = card, MatchId = 3, Outcome = 3 },
                new() { BetCard = card, MatchId = 4, Outcome = 4 },
                new() { BetCard = card, MatchId = 5, Outcome = 5 },
                new() { BetCard = card, MatchId = 6, Outcome = 6 },
                new() { BetCard = card, MatchId = 7, Outcome = 7 },
                new() { BetCard = card, MatchId = 8, Outcome = 1 },
                new() { BetCard = card, MatchId = 9, Outcome = 2 },
                new() { BetCard = card, MatchId = 10, Outcome = 4 },
                new() { BetCard = card, MatchId = 11, Outcome = 5 },
                new() { BetCard = card, MatchId = 12, Outcome = 5 }
            };

        context.BetSelections.AddRange(my12BetSelections);

        context.SaveChanges();
    }

    private static void AddMatches(BetContext context)
    {
        DateTime date = DateTime.Now.Date.AddDays(3);
        var maches = new Match[]
                   {
                new() { HomeTeamId = 1, AwayTeamId = 2, MatchDate = date },
                new() { HomeTeamId = 3, AwayTeamId = 4, MatchDate = date },
                new() { HomeTeamId = 5, AwayTeamId = 6, MatchDate = date },
                new() { HomeTeamId = 7, AwayTeamId = 8, MatchDate = date },
                new() { HomeTeamId = 9, AwayTeamId = 10, MatchDate = date },
                new() { HomeTeamId = 11, AwayTeamId = 12, MatchDate = date },
                new() { HomeTeamId = 13, AwayTeamId = 14, MatchDate = date },
                new() { HomeTeamId = 15, AwayTeamId = 16, MatchDate = date },
                new() { HomeTeamId = 17, AwayTeamId = 18, MatchDate = date },
                new() { HomeTeamId = 19, AwayTeamId = 20, MatchDate = date },
                new() { HomeTeamId = 1, AwayTeamId = 3, MatchDate = date },
                new() { HomeTeamId = 2, AwayTeamId = 4, MatchDate = date }
                   };

        context.Matches.AddRange(maches);
        context.SaveChanges();
    }

    private static void CreateMachesForLottery(BetContext context)
    {
        var maches = new MatchSelectionMatch[]
                   {
                        new() { SelectionId = 1, MatchId = 2 },
                        new() { SelectionId = 1, MatchId = 3 },
                        new() { SelectionId = 1, MatchId = 4 },
                        new() { SelectionId = 1, MatchId = 5 },
                        new() { SelectionId = 1, MatchId = 6 },
                        new() { SelectionId = 1, MatchId = 7 },
                        new() { SelectionId = 1, MatchId = 8 },
                        new() { SelectionId = 1, MatchId = 9 },
                        new() { SelectionId = 1, MatchId = 10 },
                        new() { SelectionId = 1, MatchId = 11 },
                        new() { SelectionId = 1, MatchId = 12 },
                    };
        context.MatchSelectionMatches.AddRange(maches);
        context.SaveChanges();
    }
    private static void AddOutCome(BetContext context)
    {
        var outcomes = new List<Outcome>
            {
                new () { OutcomeId = 1, Name = "Win home"},
                new () { OutcomeId = 2, Name = "Win away"},
                new () { OutcomeId = 4, Name = "Draw"},
                new () { OutcomeId = 5, Name = "Win home or draw"},
                new () { OutcomeId = 3, Name = "Win home or away"},
                new () { OutcomeId = 6, Name = "Win away or draw"},
                new () { OutcomeId = 7, Name = "Win home or away or draw"}
            };
        context.Outcomes.AddRange(outcomes);
        context.SaveChanges();
    }

    private static void AddTeams(BetContext context)
    {
        var temas = new Team[]
                    {
                new () { TeamName = "Liverpool", ShortName = "LIV" },
                new () { TeamName = "Manchester City", ShortName = "MCI" },
                new () { TeamName = "Chelsea", ShortName = "CHE" },
                new () { TeamName = "Arsenal", ShortName = "ARS" },
                new () { TeamName = "Manchester United", ShortName = "MUN" },
                new () { TeamName = "Tottenham", ShortName = "TOT" },
                new () { TeamName = "Everton", ShortName = "EVE" },
                new () { TeamName = "Leicester City", ShortName = "LEI" },
                new () { TeamName = "Wolverhampton Wanderers", ShortName = "WOL" },
                new () { TeamName = "West Ham United", ShortName = "WHU" },
                new () { TeamName = "Watford", ShortName = "WAT" },
                new () { TeamName = "Crystal Palace", ShortName = "CRY" },
                new () { TeamName = "Newcastle United", ShortName = "NEW" },
                new () { TeamName = "Bournemouth", ShortName = "BOU" },
                new () { TeamName = "Burnley", ShortName = "BUR" },
                new () { TeamName = "Southampton", ShortName = "SOU" },
                new () { TeamName = "Brighton & Hove Albion", ShortName = "BHA" },
                new () { TeamName = "Cardiff City", ShortName = "CAR" },
                new () { TeamName = "Fulham", ShortName = "FUL" },
                new () { TeamName = "Huddersfield Town", ShortName = "HUD" }
                    };

        context.Teams.AddRange(temas);
        context.SaveChanges();
    }

    private static void AddMatchTypes(BetContext context)
    {
        var macheTypes = new psamson.Model.BetMatchType[]
                    {
                new () { Name = "Full Time" },
                new () { Name = "Half Time" },
                new () { Name = "Both Time" },
                new () { Name = "System" },
                    };
        context.MatchTypes.AddRange(macheTypes);

        context.SaveChanges();
    }
    private static void AddUser(BetContext context)
    {
        var user = new Person
        {
            UserName = "JohnDoe",
            FirstName = "John",
            MiddleName = "S.",
            LastName = "Doe",
            PhoneNumber = "123456789",
            Email = "bet1@Sportbet.com",
            Password = "123456",
            Roles = "Admin,User",
            Balance = 10,
            RegistrationDate = DateTime.Now
        };
        context.People.Add(user);

        context.SaveChanges();
    }

    private static void GetBetSelection(BetContext context, int betCardId)
    {
        var sql = $@"select m.MatchId, t.TeamName, t2.TeamName,b.BetCardId, b.Outcome, o.Name, p.PersonName, mt.Name from BetSelection b
            left join Match m on m.MatchId = b.MatchId
            inner join team t on t.TeamId= m.AwayTeamId
            inner join team t2 on t2.TeamId = m.HomeTeamId
            inner join Outcome o on o.OutcomeId = b.Outcome
            inner join BetCard c on c.BetCardId = b.BetCardId
            inner join Person p on p.PersonId = c.PersonId
            inner join BetMatchType  mt on mt.MatchTypeId = c.BetMatchType
            where b.BetCardId = 5";

        var query = from b in context.BetSelections
                    join m in context.Matches on b.MatchId equals m.MatchId into bm
                    from m in bm.DefaultIfEmpty()
                    join t in context.Teams on m.AwayTeamId equals t.TeamId
                    join t2 in context.Teams on m.HomeTeamId equals t2.TeamId
                    join o in context.Outcomes on b.Outcome equals o.OutcomeId
                    join c in context.BetCards on b.BetCardId equals c.BetCardId
                    join p in context.People on c.PersonId equals p.PersonId
                    join mt in context.MatchTypes on c.MatchTypeId equals mt.MatchTypeId
                    where b.BetCardId == betCardId
                    select new
                    {
                        MatchId = m.MatchId,
                        BetMatchType = c.BetMatchType,
                        AwayTeamName = t.TeamName,
                        HomeTeamName = t2.TeamName,
                        BetCardId = b.BetCardId,
                        Outcome = b.Outcome,
                        OutcomeName = o.Name,
                        PersonName = p.FirstName,
                        MatchTypeName = mt.Name,
                        PersonId = p.PersonId
                    };

        foreach (var betselection in query)
        {
            Console.WriteLine(@$"MatchId: {betselection.MatchId}, BetMatchType :{betselection.BetMatchType}, AwayTeam: {betselection.AwayTeamName}, HomeTeam: {betselection.HomeTeamName}, OutcomeName: {betselection.OutcomeName}, PersonName: {betselection.PersonName}");
        }

        // SendToSMSWithSqidsEncoder(context, betCardId);
    }

    private static void GetBetSelectionWinLoss(BetContext context, int betCardId)
    {
        var sql = $@"select r.Outcome, b.Outcome, m.MatchId,mt.MatchTypeId, t.TeamName, t2.TeamName,b.BetCardId, b.Outcome, o.Name, p.PersonName, mt.Name from BetSelection b
                    inner join Match m on m.MatchId = b.MatchId
                    inner join team t on t.TeamId= m.AwayTeamId
                    inner join team t2 on t2.TeamId = m.HomeTeamId
                    inner join Outcome o on o.OutcomeId = b.Outcome
                    inner join BetCard c on c.BetCardId = b.BetCardId
                    inner join Person p on p.PersonId = c.PersonId
                    inner join BetMatchType  mt on mt.MatchTypeId = c.BetMatchType
                    left join BetResult r on r.MatchId = b.MatchId
                    where b.BetCardId = 5";

        var query = from b in context.BetSelections
                    join m in context.Matches on b.MatchId equals m.MatchId
                    join t in context.Teams on m.AwayTeamId equals t.TeamId
                    join t2 in context.Teams on m.HomeTeamId equals t2.TeamId
                    join o in context.Outcomes on b.Outcome equals o.OutcomeId
                    join c in context.BetCards on b.BetCardId equals c.BetCardId
                    join p in context.People on c.PersonId equals p.PersonId
                    join mt in context.MatchTypes on c.MatchTypeId equals mt.MatchTypeId
                    join r in context.BetResults on b.MatchId equals r.MatchId into br
                    from r in br.DefaultIfEmpty()
                    where b.BetCardId == betCardId
                    select new
                    {
                        BetResultOutcome = r != null ? r.Outcome : 0,
                        BetSelectionOutcome = b.Outcome,
                        MatchId = m.MatchId,
                        MatchTypeId = mt.MatchTypeId,
                        AwayTeamName = t.TeamName,
                        HomeTeamName = t2.TeamName,
                        BetCardId = b.BetCardId,
                        OutcomeName = o.Name,
                        PersonName = p.FirstName,
                        MatchTypeName = mt.Name,
                        MatchResult = r == null ? "NotPlayed" : r.Outcome == b.Outcome ? "Win" : "Loss"
                    };

        // Works 11 lines var res2 = query.Where(x => x.AnOutcome != null).ToList();

        foreach (var betselection in query)
        {
            Console.WriteLine(@$"MatchId: {betselection.MatchId}, MatchResult: {betselection.MatchResult}");
            // Console.WriteLine(@$"MatchId: {betselection.MatchId}, BetMatchType :{betselection.MatchTypeId}, AwayTeam: {betselection.AwayTeamName}, HomeTeam: {betselection.HomeTeamName}, OutcomeName: {betselection.OutcomeName}, PersonName: {betselection.PersonName}, BetMatchType: {betselection.MatchTypeName}, MatchResult: {betselection.MatchResult}");  
        }
    }
    private static void SendToSMSWithSqidsEncoder(BetContext context, int betCardId)
    {
        var query = from b in context.BetSelections
                    join m in context.Matches on b.MatchId equals m.MatchId into bm
                    from m in bm.DefaultIfEmpty()
                    join t in context.Teams on m.AwayTeamId equals t.TeamId
                    join t2 in context.Teams on m.HomeTeamId equals t2.TeamId
                    join o in context.Outcomes on b.Outcome equals o.OutcomeId
                    join c in context.BetCards on b.BetCardId equals c.BetCardId
                    join p in context.People on c.PersonId equals p.PersonId
                    join mt in context.MatchTypes on c.MatchTypeId equals mt.MatchTypeId
                    where b.BetCardId == betCardId
                    select new
                    {
                        MatchId = m.MatchId,
                        MatchTypeId = c.MatchTypeId,
                        AwayTeamName = t.TeamName,
                        HomeTeamName = t2.TeamName,
                        BetCardId = b.BetCardId,
                        Outcome = b.Outcome,
                        OutcomeName = o.Name,
                        PersonName = p.FirstName,
                        MatchTypeName = mt.Name,
                        PersonId = p.PersonId
                    };

        var PeopleId = 0;
        var betCardsId = 0;
        var matchTypesId = 0;
        var matchIdsOutcome = new List<long>();
        foreach (var betselection in query)
        {
            PeopleId = betselection.PersonId;
            betCardsId = betselection.BetCardId;
            matchTypesId = betselection.MatchTypeId;
            matchIdsOutcome.Add(betselection.MatchId);
            matchIdsOutcome.Add(betselection.Outcome);

            Console.WriteLine(@$"MatchId: {betselection.MatchId},  Outcome: {betselection.Outcome},BetMatchType :{betselection.MatchTypeId}, BetCardId: {betselection.BetCardId}, PersonId: {betselection.PersonId}");
        }

        var send = new List<long>
        {
            PeopleId,
            betCardsId,
            matchTypesId
        };
        send.AddRange(matchIdsOutcome);

        var sqids = new SqidsEncoder<long>(new()
        {
            Alphabet = "ExnXM1kBN6cuhsAvjW3Co7l2RePyY8DwaU04Tzt9fHQrqSVKdpimLGIJOgb5ZF",
        });
        var id = sqids.Encode(send.ToArray());
        Console.WriteLine(id);

        var numbers = sqids.Decode(id);

        Console.WriteLine($@"PersonId: {numbers[0]}, BetCardId: {numbers[1]}, BetMatchType: {numbers[2]}");


        int ids = 3;
        do
        {
            var matchIds = (double)numbers[ids];
            var outcomeIds = (double)numbers[ids + 1];
            Console.WriteLine($@"MatchId: {matchIds}, Outcome: {outcomeIds}");
            ids += 2;
        } while (numbers.Count >= ids + 1);
    }
}
