using Ambev.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.Infraestructure.Database.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<CartDomain>
{
    public void Configure(EntityTypeBuilder<CartDomain> builder)
    {
        builder.ToTable("Carts");

        builder.HasKey(c => c.Id);
      
        builder.Property(c => c.UserId).HasColumnName("UserId");
        builder.Property(c => c.Date).HasColumnName("Date");
        builder.Property(c => c.Cancel).HasColumnName("Cancel");
        builder.Property(c => c.TotalSold).HasColumnName("TotalSold");
               
        builder.OwnsMany(c => c.Products, p =>
        {
            p.WithOwner().HasForeignKey("CartId");
            p.Property(cp => cp.Title).HasMaxLength(255).HasColumnName("Title");
            p.Property(cp => cp.ProductId).HasColumnName("ProductId");
            p.Property(cp => cp.Quantity).HasColumnName("Quantity");
            p.Property(cp => cp.Price).HasColumnName("Price");
            p.Property(cp => cp.Category).HasMaxLength(255).HasColumnName("Category");
            p.Property(cp => cp.Subsidiary).HasMaxLength(100).HasColumnName("Subsidiary");
        });

        builder.OwnsMany(c => c.CartWithCalculators, p =>
        {
            p.Property(cp => cp.PriceWithDiscount).HasColumnType("decimal(18,2)").HasColumnName("PriceWithDiscount"); 
            p.Property(cp => cp.Discount).HasColumnType("decimal(18,2)").HasColumnName("Discount"); 
            p.Property(cp => cp.DiscountMessage).HasMaxLength(255).HasColumnName("DiscountMessage");
        });


        builder.HasIndex(x => x.UserId, "IX_Carts_UserId");
        builder.HasIndex(x => x.Date, "IX_Cars_Date");
        builder.HasIndex(x => x.Cancel, "IX_Carts_Cancel");
    }
}
