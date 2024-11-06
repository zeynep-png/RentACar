using RentACar.Business.Operations.Car.Dtos;
using RentACar.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.Operations.Car
{
    public interface ICarService
    {
        Task<ServiceMessage> AddCar(AddCarDto car);
        Task<CarDto> GetCar(int id);
        Task<List<CarDto>> GetAllCars();
        Task<ServiceMessage> UpdateCar(UpdateCarDto car);
        Task<ServiceMessage> DeleteCar(int id);
    }
}
