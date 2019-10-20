using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Entities;
using Identity.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<StoreUser> _userManager;
        private readonly SignInManager<StoreUser> _signInManager;
        public UserService(UserManager<StoreUser> userManager, SignInManager<StoreUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;        
        }

        public async Task<bool> LoginUser(LoginViewModel model)
        {
            if (model == null) return false;
            var user = await _userManager.FindByNameAsync(model.Username)
                                          .ConfigureAwait(true);
            if (user == null) return false;
            var signIn = await _signInManager.PasswordSignInAsync(user, model.Password, model.IsRememberMe, false)
                                             .ConfigureAwait(true);
            if (signIn.Succeeded) return true;

            return false;
        }

        public async Task LogoutUser()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(true);
        }

        public async Task<bool> RegisterUser(RegisterViewModel model)
        {
            if (model == null) return false;
            var storeUser = new StoreUser
            {
                FirstName = model?.FirstName,
                LastName = model?.LastName,
                Email = model?.Email,
                UserName = model?.Email,
                PhoneNumber = model?.PhoneNumber
            };

            var isCreated = await _userManager.CreateAsync(storeUser, model.Password).ConfigureAwait(true);
            if (isCreated.Succeeded) return true;
            return false;
        }
    }
}
