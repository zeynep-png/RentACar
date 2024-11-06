﻿using RentACar.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace RentACar.WebApi.Models
{
    public class AddCarRequest
    {
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        public int? Year { get; set; }
        [Required]
        public decimal PricePerDay { get; set; }
        [Required]
        public int StockQuantity { get; set; }
        [Required]
        public bool IsInStock => StockQuantity > 0;
        [Required]
        public VehicleType VehicleType { get; set; }

        public List<int> FeatureIds { get; set; }
    }
}
