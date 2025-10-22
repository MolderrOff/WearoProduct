using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wearo.Domain.Entities;

namespace Wearo.Infrastructure.Persistens.EntityConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(p => p.Description, description =>
        {
            description.Property(d => d.Brand)
                .IsRequired()
                .HasMaxLength(50);

            description.Property(d => d.Title)
                .IsRequired()
                .HasMaxLength(150);

            description.Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(500);

            description.Property(d => d.ArticleNumber)
                .IsRequired()
                .HasMaxLength(50);

            description.ToTable("Products");
        });

        builder.OwnsMany(p => p.AvailableSizes, size =>
        {
            size.ToTable("ProductSizes");

            size.WithOwner().HasForeignKey("ProductId");

            size.Property<Guid>("Id")
                .ValueGeneratedOnAdd();

            size.HasKey("Id");

            size.Property(s => s.Value)
                .IsRequired()
                .HasMaxLength(10);
        });
    }
}
