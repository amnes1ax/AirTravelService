using AirTravelService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirTravelService.DataAccess.PostgresSql.Repositories.Passengers.Context.Configurations;

public class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
{
    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        builder.ToTable("passengers");
        builder.HasKey(m => m.AggregateRootId);
        
        builder.Property(m => m.AggregateRootId)
            .HasColumnName("passenger_id");
        
        builder.Property(m => m.FirstName)
            .HasMaxLength(64)
            .IsRequired();
        
        builder.Property(m => m.LastName)
            .HasMaxLength(64)
            .IsRequired();
        
        builder.Property(m => m.Patronymic)
            .HasMaxLength(64);
    }
}