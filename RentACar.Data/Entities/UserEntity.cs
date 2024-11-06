using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Data.Entities;
using RentACar.Data.Enums;
using System.Collections.Generic;

namespace RentACar.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public UserType UserType { get; set; }

        // Navigational properties
        public ICollection<ReservationEntity> Reservations { get; set; } 
    }

    public class UserConfiguration : BaseConfiguration<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
           

           
            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Password)
                   .IsRequired();
                 

            builder.Property(u => u.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.BirthDate)
                   .IsRequired();

            builder.Property(u => u.UserType)
                   .IsRequired();

           
            base.Configure(builder);
        }
    }
}
