using RentACar.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.Operations.Car.Dtos
{
    public class UpdateCarDto
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal PricePerDay { get; set; }
        public int StockQuantity { get; set; }

        public bool IsInStock => StockQuantity > 0;

        public VehicleType VehicleType { get; set; }
        public List<int> FeatureIds { get; set; }

    }
}
