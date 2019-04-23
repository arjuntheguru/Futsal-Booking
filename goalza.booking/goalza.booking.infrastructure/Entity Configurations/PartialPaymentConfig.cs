using goalza.booking.core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace goalza.booking.infrastructure.Entity_Configurations
{
    public class PartialPaymentConfig : IEntityTypeConfiguration<PartialPayment>
    {
        public void Configure(EntityTypeBuilder<PartialPayment> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Price)
                .IsRequired()
                .HasColumnType("money");

            builder.Property(e => e.PaidAmount)
                .IsRequired()
                .HasColumnType("money");

            builder.Property(e => e.RemainingAmount)
               .IsRequired()
               .HasColumnType("money");               

            builder.Property(e => e.CreatedBy)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode(false);

            builder.Property(e => e.CreatedDate)
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime");

            builder.HasOne(e => e.Booking)
                .WithOne(p => p.PartialPayment)
                .HasForeignKey<PartialPayment>(e => e.BookingId);
                
        }
    }
}
