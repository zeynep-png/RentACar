using System.ComponentModel.DataAnnotations;

namespace RentACar.WebApi.Models
{
    public class AddFeatureRequest
    {
        [Required]
        [Length(5, 30)]
        public string Title { get; set; }
    }
}
