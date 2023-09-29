using AirTravelService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirTravelService.DataAccess.PostgresSql.Repositories.Documents.Context.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("documents");
        builder.HasKey(m=>m.AggregateRootId);
        
        builder.Property(m => m.AggregateRootId)
            .HasColumnName("document_id");

        builder.Property(m => m.Type)
            .HasMaxLength(128)
            .IsRequired();

        builder.HasMany(m => m.Fields)
            .WithOne()
            .HasForeignKey("DocumentId")
            .HasPrincipalKey(m => m.AggregateRootId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}