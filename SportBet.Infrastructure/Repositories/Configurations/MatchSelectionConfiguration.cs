using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBet.Domain.Model;
namespace SportBet.Infrastructure.Repositories.Configurations;

// MatchSelection
public class MatchSelectionConfiguration : IEntityTypeConfiguration<MatchSelection>
{
    public void Configure(EntityTypeBuilder<MatchSelection> entity)
    {
        entity.ToTable("MatchSelection");
        entity.HasKey(e => e.MatchSelectionId).HasName("PK_MatchSelection");
        entity.Property(e => e.Name).HasMaxLength(50);
        entity.Property(e => e.Description).HasMaxLength(150);
        entity.Property(e => e.IsEnabled).IsRequired();
        entity.Property(e => e.CreatedAt).IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("getdate()");
        entity.Property(e => e.ActiveUntil).IsRequired()
            .HasColumnType("datetime");
    }
}
