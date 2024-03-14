using SportBet.Domain.Model;
using SportBet.Infrastructure;

namespace SportBet.Application;

public class Utils
{
    public  void CreateFirstTimeData(BetContext context)
    {
        AddUser(context);
        AddMatchTypes(context);
        AddOutCome(context);
        AddTeams(context);
        AddMatches(context);
    }
    public  void AddUser(BetContext context)
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

    public void AddMatchTypes(BetContext context)
    {
        var macheTypes = new SportBet.Domain.Model.BetMatchType[]
                    {
                new () { Name = "Full Time" },
                new () { Name = "Half Time" },
                new () { Name = "Both Time" }
                    };
        context.MatchTypes.AddRange(macheTypes);

        context.SaveChanges();
    }

    public void AddTeams(BetContext context)
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

    public void AddOutCome(BetContext context)
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

    public void AddMatches(BetContext context)
    {
        DateTime date = DateTime.Now.Date.AddDays(3);
        var maches = new Match[]
                   {
                    new() { HomeTeamId = 1, AwayTeamId = 20, MatchDate = date },
                    new() { HomeTeamId = 2, AwayTeamId = 19, MatchDate = date },
                    new() { HomeTeamId = 3, AwayTeamId = 18, MatchDate = date },
                    new() { HomeTeamId = 4, AwayTeamId = 17, MatchDate = date },
                    new() { HomeTeamId = 5, AwayTeamId = 16, MatchDate = date },
                    new() { HomeTeamId = 6, AwayTeamId = 15, MatchDate = date },
                    new() { HomeTeamId = 7, AwayTeamId = 14, MatchDate = date },
                    new() { HomeTeamId = 8, AwayTeamId = 13, MatchDate = date },
                    new() { HomeTeamId = 9, AwayTeamId = 12, MatchDate = date },
                    new() { HomeTeamId = 10, AwayTeamId = 11, MatchDate = date },
                    new() { HomeTeamId = 19, AwayTeamId = 1, MatchDate = date.AddDays(2) },
                    new() { HomeTeamId = 20, AwayTeamId = 2, MatchDate = date.AddDays(2) },
                    new() { HomeTeamId = 18, AwayTeamId = 3, MatchDate = date.AddDays(2) },
                    new() { HomeTeamId = 17, AwayTeamId = 4, MatchDate = date.AddDays(2) },
                    new() { HomeTeamId = 16, AwayTeamId = 5, MatchDate = date.AddDays(2) },
                    new() { HomeTeamId = 15, AwayTeamId = 6, MatchDate = date.AddDays(2) },
                    new() { HomeTeamId = 14, AwayTeamId = 7, MatchDate = date.AddDays(2) },
                    new() { HomeTeamId = 13, AwayTeamId = 8, MatchDate = date.AddDays(2) },
                    new() { HomeTeamId = 12, AwayTeamId = 9, MatchDate = date.AddDays(2) },
                    new() { HomeTeamId = 11, AwayTeamId = 10, MatchDate = date.AddDays(2) },                    
                   };

        context.Matches.AddRange(maches);
        context.SaveChanges();
    }

}