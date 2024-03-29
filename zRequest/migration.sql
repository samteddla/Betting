use betWise;


CREATE TABLE [__EFMigrationsHistory] (
    [MigrationId] nvarchar(150) NOT NULL,
    [ProductVersion] nvarchar(32) NOT NULL,
    CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
);


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


CREATE TABLE [BetCard] (
    [BetCardId] int NOT NULL IDENTITY,
    [PersonId] int NOT NULL,
    [MatchSelectionId] int NOT NULL,
    [BetAmount] decimal(10,2) NOT NULL,
    [WonAmount] decimal(10,2) NOT NULL,
    [BetDate] datetime NOT NULL,
    [MatchTypeId] int NOT NULL,
    CONSTRAINT [PK_BetCard] PRIMARY KEY ([BetCardId]),
    CONSTRAINT [FK_BetCard_MatchSelection] FOREIGN KEY ([MatchSelectionId]) REFERENCES [MatchSelection] ([MatchSelectionId]),
    CONSTRAINT [FK_BetCard_MatchType] FOREIGN KEY ([MatchTypeId]) REFERENCES [BetMatchType] ([MatchTypeId]),
    CONSTRAINT [FK_BetCard_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person] ([PersonId])
);


CREATE TABLE [Match] (
    [MatchId] int NOT NULL IDENTITY,
    [HomeTeamId] int NOT NULL,
    [AwayTeamId] int NOT NULL,
    [HomeScore] int NOT NULL,
    [AwayScore] int NOT NULL,
    [MatchDate] datetime NOT NULL,
    CONSTRAINT [PK_Match] PRIMARY KEY ([MatchId]),
    CONSTRAINT [FK_Match_AwayTeam] FOREIGN KEY ([AwayTeamId]) REFERENCES [Team] ([TeamId]),
    CONSTRAINT [FK_Match_HomeTeam] FOREIGN KEY ([HomeTeamId]) REFERENCES [Team] ([TeamId])
);


CREATE TABLE [BetResult] (
    [BetResultId] int NOT NULL IDENTITY,
    [MatchId] int NOT NULL,
    [Outcome] int NOT NULL,
    [ResultDate] datetime2 NOT NULL,
    [MatchTypeId] int NOT NULL,
    CONSTRAINT [PK_BetResult] PRIMARY KEY ([BetResultId]),
    CONSTRAINT [FK_BetResult_Match] FOREIGN KEY ([MatchId]) REFERENCES [Match] ([MatchId]),
    CONSTRAINT [FK_BetResult_MatchType] FOREIGN KEY ([MatchTypeId]) REFERENCES [BetMatchType] ([MatchTypeId])
);


CREATE TABLE [BetSelection] (
    [BetSelectionId] int NOT NULL IDENTITY,
    [BetCardId] int NOT NULL,
    [MatchId] int NOT NULL,
    [Outcome] int NOT NULL,
    CONSTRAINT [PK_BetSelection] PRIMARY KEY ([BetSelectionId]),
    CONSTRAINT [FK_BetSelection_BetCard] FOREIGN KEY ([BetCardId]) REFERENCES [BetCard] ([BetCardId]),
    CONSTRAINT [FK_BetSelection_Match] FOREIGN KEY ([MatchId]) REFERENCES [Match] ([MatchId])
);


CREATE TABLE [MatchSelectionMatch] (
    [Id] int NOT NULL IDENTITY,
    [SelectionId] int NOT NULL,
    [MatchId] int NOT NULL,
    CONSTRAINT [PK_MatchSelectionMatch] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MSM_Match] FOREIGN KEY ([MatchId]) REFERENCES [Match] ([MatchId]) ON DELETE CASCADE,
    CONSTRAINT [FK_MSM_Selection] FOREIGN KEY ([SelectionId]) REFERENCES [MatchSelection] ([MatchSelectionId]) ON DELETE CASCADE
);


CREATE INDEX [IX_BetCard_MatchSelectionId] ON [BetCard] ([MatchSelectionId]);


CREATE INDEX [IX_BetCard_MatchType] ON [BetCard] ([MatchTypeId]);


CREATE INDEX [IX_BetCard_PersonId] ON [BetCard] ([PersonId]);


CREATE INDEX [IX_BetResult_MatchId] ON [BetResult] ([MatchId]);


CREATE INDEX [IX_BetResult_MatchType] ON [BetResult] ([MatchTypeId]);


CREATE INDEX [IX_BetSelection_BetCardId] ON [BetSelection] ([BetCardId]);


CREATE INDEX [IX_BetSelection_MatchId] ON [BetSelection] ([MatchId]);


CREATE INDEX [IX_Match_AwayTeamId] ON [Match] ([AwayTeamId]);


CREATE INDEX [IX_Match_HomeTeamId] ON [Match] ([HomeTeamId]);


CREATE INDEX [IX_MatchSelectionMatch_MatchId] ON [MatchSelectionMatch] ([MatchId]);


CREATE INDEX [IX_MatchSelectionMatch_SelectionId] ON [MatchSelectionMatch] ([SelectionId]);


INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231214040228_InitialChanges', N'7.0.14');

CREATE TABLE Payment (
  PaymentId INT PRIMARY KEY,
  BetCardId INT,
  Amount DECIMAL(10, 2) NOT NULL,
  PaymentDate datetime2 NOT NULL,
  CONSTRAINT [PK_Payment] PRIMARY KEY ([PaymentId]),
  FOREIGN KEY (BetCardId) REFERENCES BetCard(BetCardId)
);

CREATE TABLE Reward (
  RewardId INT PRIMARY KEY,
  BetCardId INT,
  Amount DECIMAL(10, 2) NOT NULL,
  RewardDate DATE NOT NULL,
  Constraint [PK_Reward] PRIMARY KEY ([RewardId]),
  FOREIGN KEY (BetCardId) REFERENCES BetCard(BetCardId)
);