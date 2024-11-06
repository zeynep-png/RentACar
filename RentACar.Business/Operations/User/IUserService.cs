using RentACar.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.Operations.User.Dtos
{
    public interface IUserService
    {
        // Asynchronous method to add a new user
        Task<ServiceMessage> AddUser(AddUserDto user);

        // Method to log in a user and return user information along with a service message
        ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user);
    }
}
