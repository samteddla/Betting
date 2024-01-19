using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BetMatchType = SportBet.Domain.Model.BetMatchType;
namespace SportBet.Infrastructure.Repositories.Configurations;

// BetMatchType
public class BetMatchTypeConfiguration : IEntityTypeConfiguration<BetMatchType>
{
    public void Configure(EntityTypeBuilder<BetMatchType> entity)
    {
        entity.ToTable("BetMatchType");
        entity.HasKey(e => e.MatchTypeId).HasName("PK__BetMatchType");
        entity.Property(e => e.Name).HasMaxLength(50);
        entity.Property(e => e.IsEnabled).IsRequired();
    }
}
