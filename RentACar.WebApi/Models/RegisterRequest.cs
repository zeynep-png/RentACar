﻿using System.ComponentModel.DataAnnotations;

namespace RentACar.WebApi.Models
{
    public class RegisterRequest
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
