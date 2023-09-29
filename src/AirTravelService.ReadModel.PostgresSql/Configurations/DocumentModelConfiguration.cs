using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirTravelService.ReadModel.PostgresSql.Configurations
{
    internal sealed class DocumentModelConfiguration : IEntityTypeConfiguration<DocumentModelItem>
    {
        public void Configure(EntityTypeBuilder<DocumentModelItem> builder)
        {
            builder.ToTable("documents");
            builder.HasKey(m => m.DocumentId);
        }
    }
}