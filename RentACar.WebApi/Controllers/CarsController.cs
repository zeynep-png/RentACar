using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Business.Operations.Car;
using RentACar.Business.Operations.Car.Dtos;
using RentACar.WebApi.Filters;
using RentACar.WebApi.Models;

namespace RentACar.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCar(int id)
        {
            var car = await _carService.GetCar(id);

            if (car is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(car);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _carService.GetAllCars();

            return Ok(cars);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddCar(AddCarRequest request)
        {
            var addCarDto = new AddCarDto
            {
                Make = request.Make,
                Model = request.Model,
                VehicleType = request.VehicleType,
                PricePerDay = request.PricePerDay,
                FeatureIds = request.FeatureIds,
                Year = (int)request.Year,
                StockQuantity = request.StockQuantity


            };
            var result = await _carService.AddCar(addCarDto);

          
            if (!result.IsSucceed)
            {
                return BadRequest(result.Message); // Return error message if not successful
            }
            else
            {
                return Ok(); // Return OK response if successful
            }
        }

        

        
        [HttpDelete("{id}")] 
        [Authorize(Roles = "Admin")] // 
        public async Task<IActionResult> DeleteCar(int id)
        {
            var result = await _carService.DeleteCar(id); 

           
            if (!result.IsSucceed)
                return NotFound(result.Message); 
            else
                return Ok(); 
        }

       
        [HttpPut("{id}")] // This method will respond to PUT requests for updates
        [Authorize(Roles = "Admin")] // Only users with the Admin role can access this endpoint
        [TimeControlFilter] // Apply a custom time control filter for this action
        public async Task<IActionResult> UpdateCar(int id, UpdateCarRequest request)
        {
            
            var updateCarDto = new UpdateCarDto
            {
                Id = id, 
                Make = request.Make,
                Model = request.Model,
                VehicleType = request.VehicleType,
                PricePerDay = request.PricePerDay,
                FeatureIds = request.FeatureIds,
                Year = (int)request.Year,
                StockQuantity = request.StockQuantity
            };

            var result = await _carService.UpdateCar(updateCarDto); 

            // Check if the update succeeded
            if (!result.IsSucceed)
                return NotFound(result.Message); // Return 404 if not found
            else
                return await GetCar(id); 
        }
    }
}