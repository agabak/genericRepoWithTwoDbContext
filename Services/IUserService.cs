using Identity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Services
{
    public interface IUserService
    {
        Task<bool> LoginUser(LoginViewModel model);
        Task LogoutUser();
        Task<bool> RegisterUser(RegisterViewModel model);
    }
}
