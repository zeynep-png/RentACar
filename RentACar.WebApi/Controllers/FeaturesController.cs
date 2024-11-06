using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Business.Operations.Feature;
using RentACar.Business.Operations.Feature.Dtos;
using RentACar.WebApi.Models;

namespace RentACar.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureService _featureService; // Feature service for handling feature operations

        // Constructor accepting the feature service via dependency injection
        public FeaturesController(IFeatureService featureService)
        {
            _featureService = featureService; // Initialize the feature service
        }

        // Endpoint for adding a new feature
        [HttpPost] // This method will respond to POST requests
        [Authorize(Roles = "Admin")] // Only users with the Admin role can access this endpoint
        public async Task<IActionResult> AddFeature(AddFeatureRequest request)
        {
            // Create DTO for adding feature
            var addFeatureDto = new AddFeatureDto
            {
                Title = request.Title, // Set the title from the request
            };

            // Attempt to add the feature using the feature service
            var result = await _featureService.AddFeature(addFeatureDto);

            // Check if feature addition succeeded
            if (result.IsSucceed)
                return Ok(); // Return OK response if successful
            else
                return BadRequest(result.Message); // Return error message if not successful
        }
    }
}