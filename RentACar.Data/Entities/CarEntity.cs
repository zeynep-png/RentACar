using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using RentACar.Data.Entities;
using RentACar.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Data.Entities
{
    //araba ne
    public class CarEntity : BaseEntity
    {

        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal PricePerDay { get; set; }
        public int StockQuantity { get; set; }

        public bool IsInStock => StockQuantity > 0;

        public VehicleType VehicleType { get; set; }

        public ICollection<CarFeatureEntity> CarFeatures { get; set; }
        public ICollection<ReservationEntity> Reservations { get; set; }
    }
}
public class CarConfiguration : BaseConfiguration<CarEntity>
{
    public override void Configure(EntityTypeBuilder<CarEntity> builder)
    {



        builder.Property(x => x.Model)
            .IsRequired()
            .HasMaxLength(80);

     



        base.Configure(builder);
    }

}
