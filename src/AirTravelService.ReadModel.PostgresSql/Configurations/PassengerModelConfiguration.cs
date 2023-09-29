using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirTravelService.ReadModel.PostgresSql.Configurations;

internal sealed class PassengerModelConfiguration : IEntityTypeConfiguration<PassengerModelItem>
{
    public void Configure(EntityTypeBuilder<PassengerModelItem> builder)
    {
        builder.ToTable("passengers");
        builder.HasKey(m => m.PassengerId);
    }
}