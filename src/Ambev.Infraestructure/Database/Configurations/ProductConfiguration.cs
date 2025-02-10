using Ambev.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.Infraestructure.Database.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<ProductDomain>
{
    public void Configure(EntityTypeBuilder<ProductDomain> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Title).HasMaxLength(255).HasColumnName("Title");
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)").HasColumnName("Price");
        builder.Property(p => p.Description).HasMaxLength(500).HasColumnName("Description");
        builder.Property(p => p.Category).HasMaxLength(100).HasColumnName("Category");
        builder.Property(p => p.Image).HasColumnName("Image");

        builder.OwnsOne(p => p.Rating, r =>
        {
            r.Property(pr => pr.Rate).HasColumnName("Rate");
            r.Property(pr => pr.Count).HasColumnName("Count");
        });

        builder.HasIndex(x => x.Title, "IX_Products_Title");
        builder.HasIndex(x => x.Price, "IX_Products_Price");
        builder.HasIndex(x => x.Category, "IX_Products_Category");
    }
}
