using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Data.Entities
{
    //hangi arabanın hangi özellikleri var
    public class CarFeatureEntity :BaseEntity
    {
        public int CarId { get; set; }
        public CarEntity Car { get; set; }

        public int FeatureId { get; set; }
        public FeatureEntity Feature { get; set; }
    }
    public class CarFeatureConfiguration : BaseConfiguration<CarFeatureEntity>
    {
        public void Configure(EntityTypeBuilder<CarFeatureEntity> builder)
        {
            builder.Ignore(x => x.Id);

            builder.HasKey("CarId", "FeatureId");


            base.Configure(builder);
        }
    }
}
