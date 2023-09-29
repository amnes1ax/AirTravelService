using AirTravelService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirTravelService.DataAccess.PostgresSql.Repositories.Tickets.Contexts.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("tickets");
        builder.HasKey(m => m.AggregateRootId);
        builder.Property(m => m.AggregateRootId)
            .HasColumnName("ticket_id");
        
        builder.Property(m => m.OrderNumber)
            .HasMaxLength(32)
            .IsRequired();
        
        builder.Property(m => m.ServiceProvider)
            .HasMaxLength(128);
        
        builder.Property(m => m.DeparturePoint)
            .HasMaxLength(128)
            .IsRequired();
        
        builder.Property(m => m.DestinationPoint)
            .HasMaxLength(128)
            .IsRequired();
    }
}