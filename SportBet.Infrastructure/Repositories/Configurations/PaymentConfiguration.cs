using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBet.Domain.Model;

namespace SportBet.Infrastructure.Repositories.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> entity)
    {
        entity.ToTable("Payment");
        entity.HasKey(e => e.PaymentId).HasName("PK_Payment");
        entity.HasIndex(e => e.BetCardId, "IX_Payment_BetCardId");
        entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
        entity.Property(e => e.PaymentDate).HasColumnType("datetime");
        entity.HasOne(d => d.BetCard).WithMany(p => p.Payments)
            .HasForeignKey(d => d.BetCardId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Payment_BetCard");
    }
}
