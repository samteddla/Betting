using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SportBet.Razar.Model;

public partial class BetOnContext : DbContext
{
    public BetOnContext()
    {
    }

    public BetOnContext(DbContextOptions<BetOnContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BetCard> BetCards { get; set; }

    public virtual DbSet<BetMatchType> BetMatchTypes { get; set; }

    public virtual DbSet<BetResult> BetResults { get; set; }

    public virtual DbSet<BetSelection> BetSelections { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<MatchSelection> MatchSelections { get; set; }

    public virtual DbSet<MatchSelectionMatch> MatchSelectionMatches { get; set; }

    public virtual DbSet<Outcome> Outcomes { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Reward> Rewards { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=WPW04SVBX\\SQLEXPRESS;Initial Catalog=BetBackup;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BetCard>(entity =>
        {
            entity.ToTable("BetCard");

            entity.HasIndex(e => e.MatchSelectionId, "IX_BetCard_MatchSelectionId");

            entity.HasIndex(e => e.MatchTypeId, "IX_BetCard_MatchType");

            entity.HasIndex(e => e.PersonId, "IX_BetCard_PersonId");

            entity.Property(e => e.BetAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BetDate).HasColumnType("datetime");
            entity.Property(e => e.WonAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.MatchSelection).WithMany(p => p.BetCards)
                .HasForeignKey(d => d.MatchSelectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BetCard_MatchSelection");

            entity.HasOne(d => d.MatchType).WithMany(p => p.BetCards)
                .HasForeignKey(d => d.MatchTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BetCard_MatchType");

            entity.HasOne(d => d.Person).WithMany(p => p.BetCards)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BetCard_Person");
        });

        modelBuilder.Entity<BetMatchType>(entity =>
        {
            entity.HasKey(e => e.MatchTypeId).HasName("PK__BetMatchType");

            entity.ToTable("BetMatchType");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<BetResult>(entity =>
        {
            entity.ToTable("BetResult");

            entity.HasIndex(e => e.MatchId, "IX_BetResult_MatchId");

            entity.HasIndex(e => e.MatchSelectionId, "IX_BetResult_MatchSelectionId");

            entity.HasIndex(e => e.MatchTypeId, "IX_BetResult_MatchType");

            entity.HasOne(d => d.Match).WithMany(p => p.BetResults)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BetResult_Match");

            entity.HasOne(d => d.MatchSelection).WithMany(p => p.BetResults)
                .HasForeignKey(d => d.MatchSelectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BetResult_MatchSelection");

            entity.HasOne(d => d.MatchType).WithMany(p => p.BetResults)
                .HasForeignKey(d => d.MatchTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BetResult_MatchType");
        });

        modelBuilder.Entity<BetSelection>(entity =>
        {
            entity.ToTable("BetSelection");

            entity.HasIndex(e => e.BetCardId, "IX_BetSelection_BetCardId");

            entity.HasIndex(e => e.MatchId, "IX_BetSelection_MatchId");

            entity.HasOne(d => d.BetCard).WithMany(p => p.BetSelections)
                .HasForeignKey(d => d.BetCardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BetSelection_BetCard");

            entity.HasOne(d => d.Match).WithMany(p => p.BetSelections)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BetSelection_Match");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.ToTable("Match");

            entity.HasIndex(e => e.AwayTeamId, "IX_Match_AwayTeamId");

            entity.HasIndex(e => e.HomeTeamId, "IX_Match_HomeTeamId");

            entity.Property(e => e.MatchDate).HasColumnType("datetime");

            entity.HasOne(d => d.AwayTeam).WithMany(p => p.MatchAwayTeams)
                .HasForeignKey(d => d.AwayTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Match_AwayTeam");

            entity.HasOne(d => d.HomeTeam).WithMany(p => p.MatchHomeTeams)
                .HasForeignKey(d => d.HomeTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Match_HomeTeam");
        });

        modelBuilder.Entity<MatchSelection>(entity =>
        {
            entity.ToTable("MatchSelection");

            entity.Property(e => e.ActiveUntil).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<MatchSelectionMatch>(entity =>
        {
            entity.ToTable("MatchSelectionMatch");

            entity.HasIndex(e => e.MatchId, "IX_MatchSelectionMatch_MatchId");

            entity.HasIndex(e => e.SelectionId, "IX_MatchSelectionMatch_SelectionId");

            entity.HasOne(d => d.Match).WithMany(p => p.MatchSelectionMatches)
                .HasForeignKey(d => d.MatchId)
                .HasConstraintName("FK_MSM_Match");

            entity.HasOne(d => d.Selection).WithMany(p => p.MatchSelectionMatches)
                .HasForeignKey(d => d.SelectionId)
                .HasConstraintName("FK_MSM_Selection");
        });

        modelBuilder.Entity<Outcome>(entity =>
        {
            entity.ToTable("Outcome");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payment");

            entity.HasIndex(e => e.BetCardId, "IX_Payment_BetCardId");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");

            entity.HasOne(d => d.BetCard).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BetCardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_BetCard");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("Person");

            entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
            entity.Property(e => e.Roles).HasMaxLength(150);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<Reward>(entity =>
        {
            entity.ToTable("Reward");

            entity.HasIndex(e => e.BetCardId, "IX_Reward_BetCardId");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RewardDate).HasColumnType("datetime");

            entity.HasOne(d => d.BetCard).WithMany(p => p.Rewards)
                .HasForeignKey(d => d.BetCardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reward_Person");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.ToTable("Team");

            entity.Property(e => e.ShortName).HasMaxLength(150);
            entity.Property(e => e.TeamName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
