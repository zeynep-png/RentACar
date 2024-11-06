using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Data.Enums;
using System;

namespace RentACar.Data.Entities
{
    
    public class PaymentEntity : BaseEntity
    {
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentType PaymentMethod { get; set; }

        // Foreign Key for Reservation
        public int ReservationId { get; set; }
        public ReservationEntity Reservation { get; set; }





    }

    public class PaymentConfiguration : BaseConfiguration<PaymentEntity>
    {
        public override void Configure(EntityTypeBuilder<PaymentEntity> builder)
        {
            
            builder.Property(p => p.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            
            builder.Property(p => p.PaymentDate)
                   .IsRequired();

           




            base.Configure(builder);
        }
    }
}
