using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wearo.Domain.Entities;

namespace Wearo.Infrastructure.Persistens.EntityConfigurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.CategoryAttributes)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany<Product>()
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);
    }
}
