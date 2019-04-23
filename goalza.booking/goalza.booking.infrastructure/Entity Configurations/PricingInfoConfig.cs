using goalza.booking.core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace goalza.booking.infrastructure.Entity_Configurations
{
    public class PricingInfoConfig : IEntityTypeConfiguration<PricingInfo>
    {
        public void Configure(EntityTypeBuilder<PricingInfo> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Day)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.Morning)
                .IsRequired()
                .HasColumnType("money");

            builder.Property(e => e.AfterNoon)
                .IsRequired()
                .HasColumnType("money");

            builder.Property(e => e.Evening)
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

            builder.HasOne(e => e.FutsalCourt)
                .WithOne(p => p.PricingInfo)
                .HasForeignKey<PricingInfo>(e => e.FutsalCourtId);
        }
    }
}
