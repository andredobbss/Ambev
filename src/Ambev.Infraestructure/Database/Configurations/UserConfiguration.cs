using Ambev.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.Infraestructure.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserDomain>
{
    public void Configure(EntityTypeBuilder<UserDomain> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(255).HasColumnName("Email");
        builder.Property(u => u.Username).IsRequired().HasMaxLength(255).HasColumnName("Username");
        builder.Property(u => u.Password).IsRequired().HasMaxLength(500).HasColumnName("Password");
        builder.Property(u => u.Status).IsRequired().HasMaxLength(20).HasColumnName("Status");
        builder.Property(u => u.Role).IsRequired().HasMaxLength(20).HasColumnName("Role");

        builder.OwnsOne(u => u.Name, n =>
        {
            n.Property(nm => nm.FirstName).HasMaxLength(100).HasColumnName("FirstName");
            n.Property(nm => nm.LastName).HasMaxLength(100).HasColumnName("LastName");
        });

        builder.OwnsOne(u => u.Address, a =>
        {
            a.Property(ad => ad.City).HasMaxLength(100).HasColumnName("City");
            a.Property(ad => ad.Street).HasMaxLength(255).HasColumnName("Street");
            a.Property(ad => ad.Number).HasColumnName("Number");
            a.Property(ad => ad.ZipCode).HasMaxLength(20).HasColumnName("ZipCode");

            a.OwnsOne(ad => ad.Geolocation, g =>
            {
                g.Property(gl => gl.Lat).HasMaxLength(50).HasColumnName("Lat");
                g.Property(gl => gl.Long).HasMaxLength(50).HasColumnName("Long");
            });
        });

        builder.OwnsOne(u => u.ExternalIdentity, n =>
        {
            n.Property(ex => ex.ExternalId).HasMaxLength(255).HasColumnName("ExternalId");
            n.Property(ex => ex.Provider).HasMaxLength(255).HasColumnName("Provider");
        });

        builder.HasIndex(x => x.Email, "IX_Users_Email");
        builder.HasIndex(x => x.Username, "IX_Users_Username");
        builder.HasIndex(x => x.Status, "IX_Users_Status");
        builder.HasIndex(x => x.Role, "IX_Users_Role");
    }
}
