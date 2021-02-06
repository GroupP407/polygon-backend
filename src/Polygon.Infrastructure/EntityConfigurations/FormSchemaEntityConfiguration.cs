using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Polygon.Domain.Entities;

namespace Polygon.Infrastructure.EntityConfigurations
{
    public class FormSchemaEntityConfiguration : IEntityTypeConfiguration<FormSchema>
    {
        public void Configure(EntityTypeBuilder<FormSchema> builder)
        {
            builder.HasKey(schema => schema.Id);
            builder.Property(schema => schema.Schema)
                .HasColumnType("jsonb")
                .HasJObjectConversion()
                .IsRequired();
            builder.HasQueryFilter(schema => !schema.IsDeleted);
        }
    }
}