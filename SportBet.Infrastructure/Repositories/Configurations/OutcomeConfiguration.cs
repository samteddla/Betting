using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBet.Domain.Model;
namespace SportBet.Infrastructure.Repositories.Configurations;

// Outcome
public class OutcomeConfiguration : IEntityTypeConfiguration<Outcome>
{
    public void Configure(EntityTypeBuilder<Outcome> entity)
    {
        entity.ToTable("Outcome");
        entity.HasKey(e => e.Id).HasName("PK_Outcome");
        entity.Property(e => e.Name).HasMaxLength(50);
        entity.Property(e => e.IsEnabled).IsRequired();
    }
}
