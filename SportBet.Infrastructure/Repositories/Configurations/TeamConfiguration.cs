using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBet.Domain.Model;
namespace SportBet.Infrastructure.Repositories.Configurations;

// Team
public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> entity)
    {
        entity.ToTable("Team");
        entity.HasKey(e => e.TeamId).HasName("PK_Team");
        entity.Property(e => e.ShortName).HasMaxLength(150);
        entity.Property(e => e.TeamName).HasMaxLength(50);
        entity.Property(e => e.IsEnabled).IsRequired();
    }
}
