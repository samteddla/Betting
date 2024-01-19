
CREATE TABLE [BetMatchType] (
    [MatchTypeId] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [IsEnabled] bit NOT NULL,
    CONSTRAINT [PK__BetMatchType] PRIMARY KEY ([MatchTypeId])
);

CREATE TABLE [MatchSelection] (
    [MatchSelectionId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(150) NOT NULL,
    [IsEnabled] bit NOT NULL,
    CONSTRAINT [PK_MatchSelection] PRIMARY KEY ([MatchSelectionId])
);

CREATE TABLE [Outcome] (
    [Id] int NOT NULL IDENTITY,
    [OutcomeId] int NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [IsEnabled] bit NOT NULL,
    CONSTRAINT [PK_Outcome] PRIMARY KEY ([Id])
);

CREATE TABLE [Person] (
    [PersonId] int NOT NULL IDENTITY,
    [UserName] nvarchar(50) NOT NULL,
    [FirstName] nvarchar(50) NOT NULL,
    [MiddleName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [PhoneNumber] nvarchar(50) NOT NULL,
    [Email] nvarchar(50) NOT NULL,
    [Password] nvarchar(50) NOT NULL,
    [Roles] nvarchar(150) NOT NULL,
    [Balance] decimal(10,2) NOT NULL,
    [IsEnabled] bit NOT NULL,
    [RegistrationDate] datetime NOT NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY ([PersonId])
);

CREATE TABLE [Team] (
    [TeamId] int NOT NULL IDENTITY,
    [TeamName] nvarchar(50) NOT NULL,
    [ShortName] nvarchar(150) NOT NULL,
    [IsEnabled] bit NOT NULL,
    CONSTRAINT [PK_Team] PRIMARY KEY ([TeamId])
);


CREATE TABLE [Match] (
    [MatchId] int NOT NULL IDENTITY,
    [HomeTeamId] int NOT NULL,
    [AwayTeamId] int  NULL,
    [HomeScore] int  NULL,
    [AwayScore] int  NULL,
    [MatchDate] datetime NOT NULL,
    CONSTRAINT [PK_Match] PRIMARY KEY ([MatchId]),
    FOREIGN KEY ([HomeTeamId]) REFERENCES [Team] ([TeamId]),
);

