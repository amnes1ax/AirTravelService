using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirTravelService.ReadModel.PostgresSql.Configurations;

internal sealed class TicketModelConfiguration : IEntityTypeConfiguration<TicketModelItem>
{
    public void Configure(EntityTypeBuilder<TicketModelItem> builder)
    {
        builder.ToTable("tickets");
        builder.HasKey(m => m.TicketId);
    }
}