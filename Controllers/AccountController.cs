using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Entities;
using Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<StoreUser> _userManager;
        private readonly SignInManager<StoreUser> _signInManager;
        public AccountController(UserManager<StoreUser> userManager, 
                                 SignInManager<StoreUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            
                var user = await _userManager.FindByNameAsync(model?.Username).ConfigureAwait(true);
                if (user == null)
                 {
                    ModelState.AddModelError("", "User doesn't exist"); 
                    return View(model);
                 }
           
              if(!await _userManager.CheckPasswordAsync(user, model.Password).ConfigureAwait(true))
                {
                    ModelState.AddModelError("", "enable to login");
                    return View(model);
                }

            var signIn = await _signInManager.PasswordSignInAsync(user, model.Password, model.IsRememberMe, false)
                                               .ConfigureAwait(true);

            if (signIn.Succeeded)
               {
                  if (Request.Query.ContainsKey("ReturnUrl")) return Redirect(Request.Query.Keys.FirstOrDefault());

                  return RedirectToAction("Index", "Home");
               }
               
            ModelState.AddModelError("", "Something went wrong, you can't login");

            return View(model);
        }

        public IActionResult Logout()
        {
            if(this.User.Identity.IsAuthenticated)
            {
                _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var storeUser = new StoreUser
                            {
                                FirstName = model?.FirstName,
                                LastName = model?.LastName,
                                Email = model?.Email,
                                UserName = model?.Email,
                                PhoneNumber = model?.PhoneNumber
                            };
          var isCreated = await _userManager.CreateAsync(storeUser, model.Password).ConfigureAwait(true);
            if (isCreated.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("", "Enable to register a user");
            return View(model);
        }
    }
}