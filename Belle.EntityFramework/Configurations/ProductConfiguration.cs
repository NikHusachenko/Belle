using Belle.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Belle.EntityFramework.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Products").HasKey(prod => prod.Id);

            builder.Property(prod => prod.Name).HasMaxLength(63);
            builder.Property(prod => prod.Description).HasMaxLength(511);
        }
    }
}