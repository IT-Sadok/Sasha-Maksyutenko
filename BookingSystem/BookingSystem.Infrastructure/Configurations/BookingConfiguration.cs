
using BookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingSystem.Infrastructure.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.TotalPrice)
                .HasPrecision(18, 2);

            builder.HasOne(b => b.Apartment)
                .WithMany()
                .HasForeignKey(b => b.ApartmentId);

            builder.HasIndex(b => b.UserId);
            builder.HasIndex(b => b.ApartmentId);
        }
    }
}