using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBet.Domain.Model;
namespace SportBet.Infrastructure.Repositories.Configurations;

// Person
public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> entity)
    {
        entity.ToTable("Person");
        entity.HasKey(e => e.PersonId).HasName("PK_Person");
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
        entity.Property(e => e.IsEnabled).IsRequired();
    }
}
