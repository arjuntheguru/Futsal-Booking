using goalza.booking.core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace goalza.booking.infrastructure.Entity_Configurations
{
    public class FutsalCourtConfig : IEntityTypeConfiguration<FutsalCourt>
    {
        public void Configure(EntityTypeBuilder<FutsalCourt> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Dimension)
                .IsRequired()
                .HasMaxLength(100);

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

            builder.HasOne(e => e.FutsalCompany)
                .WithMany(p => p.FutsalCourts)
                .HasForeignKey(e => e.FutsalCompanyId);

            builder.HasMany(e => e.Gallery)
                .WithOne(p => p.FutsalCourt)
                .HasForeignKey(p => p.FustalCourtId);

            builder.HasMany(e => e.Bookings)
                .WithOne(p => p.FutsalCourt)
                .HasForeignKey(p => p.FutsalCourtId);            
        }
    }
}
