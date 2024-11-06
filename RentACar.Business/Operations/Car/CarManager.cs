using Microsoft.EntityFrameworkCore;
using RentACar.Business.Operations.Car.Dtos;
using RentACar.Business.Types;
using RentACar.Data.Entities;
using RentACar.Data.Repositories;
using RentACar.Data.UnifOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.Operations.Car
{
    public class CarManager : ICarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<CarEntity> _carRepository;
        private readonly IRepository<CarFeatureEntity> _carFeatureRepository;

        public CarManager(IUnitOfWork unitOfWork, IRepository<CarEntity> carRepository, IRepository<CarFeatureEntity> carFeatureRepository)
        {
            _unitOfWork = unitOfWork;
            _carRepository = carRepository;
            _carFeatureRepository = carFeatureRepository;
        }
        public async Task<ServiceMessage> AddCar(AddCarDto car)
        {
            var hasCar = _carRepository.GetAll(x => x.Make.ToLower() == car.Make.ToLower()).Any();

            if (hasCar)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "This Car already exist."
                };
            }
            await _unitOfWork.BeginTransaction();

            var carEntity = new CarEntity
            {
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                PricePerDay = car.PricePerDay,
                StockQuantity = car.StockQuantity,
                VehicleType = car.VehicleType,
                

            };
            _carRepository.Add(carEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ) {
                throw new Exception("Error due adding car");
            }


            foreach (var featureId in car.FeatureIds)
            {
                var carFeature = new CarFeatureEntity
                {
                    CarId = carEntity.Id,
                    FeatureId = featureId
                };

                
                _carFeatureRepository.Add(carFeature);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync(); 
                await _unitOfWork.CommitTransaction(); // Commit the transaction
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction(); // Roll back if an error occurs
                throw new Exception("Error due adding car features. Rolling back.");
            }

            return new ServiceMessage
            {
                IsSucceed = true, 
            };
        }

        public async Task<CarDto> GetCar(int id)
        {
            var car = await _carRepository.GetAll(x => x.Id == id)
                .Select(x => new CarDto
                {
                    Id = x.Id,
                    
                   
                    Make = x.Make,
                    Model = x.Model,
                    Year = x.Year,
                    PricePerDay = x.PricePerDay,
                    StockQuantity = x.StockQuantity,
                    VehicleType = x.VehicleType,
                    Features = x.CarFeatures.Select(f => new CarFeaturesDto
                    {
                        Id = f.Id,
                        Title = f.Feature.Title
                    }).ToList()
                }).FirstOrDefaultAsync();

            return car;
        }

        public async Task<List<CarDto>> GetAllCars()
        {
            var cars = await _carRepository.GetAll()
                 .Select(x => new CarDto
                 {
                     Make = x.Make,
                     Model = x.Model,
                     Year = x.Year,
                     PricePerDay = x.PricePerDay,
                     StockQuantity = x.StockQuantity,
                     VehicleType = x.VehicleType,
                     
                     Features = x.CarFeatures.Select(f => new CarFeaturesDto
                     {
                         Id = f.Id,
                         Title = f.Feature.Title
                     }).ToList()
                 }).ToListAsync();

            return cars; 
        }

        public async Task<ServiceMessage> DeleteCar(int id)
        {
            var car = _carRepository.GetById(id);
            if (car is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "The car you want to delete is not exist"
                };
            }
            _carRepository.Delete(id); 
            try
            {
                await _unitOfWork.SaveChangesAsync(); // Save changes
            }
            catch (Exception)
            {
                throw new Exception("Error due deleting the car");
            }

            return new ServiceMessage
            {
                IsSucceed = true // Indicate success
            };
        }

       
        public async Task<ServiceMessage> UpdateCar(UpdateCarDto car)
        {
            var carEntity = _carRepository.GetById(car.Id);

            if (carEntity is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "The car cannot found"
                };
            }

            await _unitOfWork.BeginTransaction(); // Begin a new transaction

            carEntity.StockQuantity = car.StockQuantity;
            carEntity.Make = car.Make;
            carEntity.PricePerDay = car.PricePerDay;
            carEntity.Year = car.Year;
            carEntity.VehicleType = car.VehicleType;
            carEntity.Model = car.Model;
            


            _carRepository.Update(carEntity); 

            try
            {
                await _unitOfWork.SaveChangesAsync(); // Save changes
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction(); // Roll back if an error occurs
                throw new Exception("Error due updating the car");
            }

            
            var carFeatures = _carFeatureRepository
                .GetAll(x => x.CarId == car.Id) 
                .ToList();

            // Delete existing features
            foreach (var carFeature in carFeatures)
            {
                _carFeatureRepository.Delete(carFeature, false);
            }

            // Add new features from the DTO
            foreach (var featureId in car.FeatureIds)
            {
                var carFeature = new CarFeatureEntity
                {
                    CarId = carEntity.Id,
                    FeatureId = featureId
                };
                _carFeatureRepository.Add(carFeature); // Add the feature relationship
            }

            try
            {
                await _unitOfWork.SaveChangesAsync(); // Save changes
                await _unitOfWork.CommitTransaction(); // Commit the transaction
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction(); // Roll back if an error occurs
                throw new Exception("Error due updating the car");
            }

            return new ServiceMessage
            {
                IsSucceed = true // Indicate success
            };
        }
    }
}
