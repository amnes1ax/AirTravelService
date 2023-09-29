using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirTravelService.ReadModel.PostgresSql.Configurations;

internal sealed class DocumentFieldConfiguration : IEntityTypeConfiguration<DocumentFieldItem>
{
    public void Configure(EntityTypeBuilder<DocumentFieldItem> builder)
    {
        builder.ToTable("document_fields");
        builder.HasKey("DocumentId", "Name");
    }
}