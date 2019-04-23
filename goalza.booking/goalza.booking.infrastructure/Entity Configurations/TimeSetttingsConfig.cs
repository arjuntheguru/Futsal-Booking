using goalza.booking.core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace goalza.booking.infrastructure.Entity_Configurations
{
    public class TimeSetttingsConfig : IEntityTypeConfiguration<TimeSettings>
    {
        public void Configure(EntityTypeBuilder<TimeSettings> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.MorningStartTime)
                .IsRequired()
                .HasColumnType("time");

            builder.Property(e => e.MorningEndTime)
                .IsRequired()
                .HasColumnType("time");

            builder.Property(e => e.AfternoonStartTime)
                .IsRequired()
                .HasColumnType("time");

            builder.Property(e => e.AfternoonEndTime)
                .IsRequired()
                .HasColumnType("time");

            builder.Property(e => e.EveningStartTime)
                .IsRequired()
                .HasColumnType("time");

            builder.Property(e => e.EveningEndTime)
                .IsRequired()
                .HasColumnType("time");

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

        }
    }
}
