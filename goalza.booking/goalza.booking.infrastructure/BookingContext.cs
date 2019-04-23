using goalza.booking.infrastructure.Entity_Configurations;
using Microsoft.EntityFrameworkCore;
using System;

namespace goalza.booking.infrastructure
{
    public class BookingContext : DbContext
    {
        public BookingContext()
        {

        }

        public BookingContext(DbContextOptions<BookingContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FutsalCompanyConfig());
            modelBuilder.ApplyConfiguration(new BookingConfig());
            modelBuilder.ApplyConfiguration(new FutsalCourtConfig());
            modelBuilder.ApplyConfiguration(new GalleryConfig());
            modelBuilder.ApplyConfiguration(new PartialPaymentConfig());
            modelBuilder.ApplyConfiguration(new PricingInfoConfig());
            modelBuilder.ApplyConfiguration(new SocialMediaConfig());
            modelBuilder.ApplyConfiguration(new TeamConfig());
            modelBuilder.ApplyConfiguration(new TimeSetttingsConfig());

        }
    }
}
