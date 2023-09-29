using AirTravelService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirTravelService.DataAccess.PostgresSql.Repositories.Documents.Context.Configurations;

internal sealed class DocumentFieldConfigurations : IEntityTypeConfiguration<DocumentField>
{
    public void Configure(EntityTypeBuilder<DocumentField> builder)
    {
        builder.ToTable("document_fields");
        builder.HasKey("DocumentId", "Name");
        builder.Property<Guid>("DocumentId")
            .IsRequired();
        builder.Property(m => m.Value)
            .HasMaxLength(1024)
            .IsRequired();
    }
}