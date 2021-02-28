using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.Context
{
    public partial class LogisticsContext : DbContext
    {
        public LogisticsContext()
            : base("name=LogisticsContext")
        {
        }

        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<Fuel> Fuel { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .Property(e => e.FuelConsumption)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OrderStatus>()
                .HasMany(e => e.Order)
                .WithOptional(e => e.OrderStatus)
                .HasForeignKey(e => e.ApplicationStatusId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Car)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.DriverId);
        }
    }
}
