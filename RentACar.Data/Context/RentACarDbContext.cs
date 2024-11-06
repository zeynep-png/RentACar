using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RentACar.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Data.Context
{
    public class RentACarDbContext :DbContext
    {
        // Constructor that accepts DbContextOptions and passes them to the base DbContext
        public RentACarDbContext(DbContextOptions<RentACarDbContext> options) : base(options)
        {
        }

        // Configures the model with Fluent API and seeds initial data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Applying configurations for various entities using Fluent API
            modelBuilder.ApplyConfiguration(new FeatureConfiguration());
            modelBuilder.ApplyConfiguration(new CarConfiguration());
            modelBuilder.ApplyConfiguration(new CarFeatureConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            // Seeding initial data for the SettingEntity
            modelBuilder.Entity<SettingEntity>().HasData(
                new SettingEntity
                {
                    Id = 1,
                    MaintenanceMode = false // Initial setting for maintenance mode
                });

            
            base.OnModelCreating(modelBuilder);
        }

        // DbSet properties representing tables in the database
        public DbSet<UserEntity> Users => Set<UserEntity>(); 
        public DbSet<FeatureEntity> Features => Set<FeatureEntity>(); 
        public DbSet<CarEntity> Cars => Set<CarEntity>(); 
        public DbSet<CarFeatureEntity> CarFeatures => Set<CarFeatureEntity>(); 
        public DbSet<ReservationEntity> Reservations => Set<ReservationEntity>(); 
        public DbSet<SettingEntity> Settings => Set<SettingEntity>(); 
        public DbSet<PaymentEntity> Payments => Set<PaymentEntity>(); 
    }
}