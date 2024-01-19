using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBet.Domain.Model;

namespace SportBet.Infrastructure.Repositories.Configurations;

public class RewardConfiguration : IEntityTypeConfiguration<Reward>
{
    public void Configure(EntityTypeBuilder<Reward> entity)
    {
        entity.ToTable("Reward");
        entity.HasKey(e => e.RewardId).HasName("PK_Reward");
        entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
        entity.Property(e => e.RewardDate).HasColumnType("datetime");
        entity.HasOne(d => d.BetCard).WithMany(p => p.Rewards)
            .HasForeignKey(d => d.BetCardId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Reward_Person");
    }
}