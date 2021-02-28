using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.Context
{
    public partial class Logistics : DbContext
    {
        public Logistics()
            : base("name=LogisticsContext")
        {
        }

        public virtual DbSet<Applications> Applications { get; set; }
        public virtual DbSet<ApplicationStatus> ApplicationStatus { get; set; }
        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Fuel> Fuel { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationStatus>()
                .HasMany(e => e.Applications)
                .WithOptional(e => e.ApplicationStatus)
                .HasForeignKey(e => e.ApplicationStatusId);

            modelBuilder.Entity<Cars>()
                .Property(e => e.FuelConsumption)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Roles)
                .HasForeignKey(e => e.UserRoleId);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Cars)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.DriverId);
        }
    }
}
