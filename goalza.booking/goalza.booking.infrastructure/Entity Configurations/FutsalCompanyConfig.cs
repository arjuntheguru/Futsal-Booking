using goalza.booking.core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace goalza.booking.infrastructure.Entity_Configurations
{
    public class FutsalCompanyConfig : IEntityTypeConfiguration<FutsalCompany>
    {
        public void Configure(EntityTypeBuilder<FutsalCompany> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .UseSqlServerIdentityColumn();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.RegistrationNo)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.Address)
               .IsRequired()
               .HasMaxLength(50);

            builder.Property(e => e.ContactNo)
               .IsRequired()
               .HasMaxLength(20);

            builder.Property(e => e.Email)
               .IsRequired();

            builder.Property(e => e.OpeningTime)
               .IsRequired()
               .HasColumnType("time");

            builder.Property(e => e.ClosingTime)
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

            builder.HasOne(e => e.TimeSettings)
                .WithOne(p => p.FutsalCompany)
                .HasForeignKey<TimeSettings>(p => p.FutsalCompanyId);

            builder.HasMany(e => e.FutsalCourts)
                .WithOne(p => p.FutsalCompany)
                .HasForeignKey(e => e.FutsalCompanyId);

            builder.HasOne(e => e.SocialMedia)
                .WithOne(p => p.FutsalCompany)
                .HasForeignKey<SocialMedia>(e => e.FutsalCompanyId);
        }
    }
}
