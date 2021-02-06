using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Polygon.Domain.Entities;

namespace Polygon.Infrastructure.EntityConfigurations
{
    public class FormDataEntityConfiguration : IEntityTypeConfiguration<FormData>
    {
        public void Configure(EntityTypeBuilder<FormData> builder)
        {
            builder.HasKey(data => data.Id);
            builder.Property(data => data.JsonData)
                .HasColumnType("jsonb")
                .HasJObjectConversion()
                .IsRequired();
        }
    }
}