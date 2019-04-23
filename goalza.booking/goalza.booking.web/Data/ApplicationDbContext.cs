using System;
using System.Collections.Generic;
using System.Text;
using goalza.booking.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace goalza.booking.web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .ToTable("Users")
                .Property(e => e.Id)
                .HasColumnName("UserId");           

            builder.Entity<ApplicationUser>()
                .Property(e => e.Address)
                .HasMaxLength(100);

            builder.Entity<IdentityRole>()
                .ToTable("Roles")
                .Property(p => p.Id)
                .HasColumnName("RoleId");

            builder.Entity<ApplicationUser>()
                .Property(e => e.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
