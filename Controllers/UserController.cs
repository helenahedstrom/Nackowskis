using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nackowskis.BusinessLayer;
using Nackowskis.Data;
using Nackowskis.Models;
using Nackowskis.ViewModels;

namespace Nackowskis.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        //private IUserBusiness _businessLayer;

        ////Injecta business skiktet för att kunna göra anrop
        ////Controllern är helt isolerad från datalayer 
        //public UserController(IUserBusiness businesslayer)
        //{
        //    _businessLayer = businesslayer;
        //}

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private IUserBusiness _businessLayer;

        public UserController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IUserBusiness businesslayer)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _businessLayer = businesslayer;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(vm);

        }

        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }



        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // var model = _businessLayer.RegisterUser(vm);
                var user = new ApplicationUser { UserName = vm.Email, Email = vm.Email };

                var result = await _userManager.CreateAsync(user, vm.Password);

                var role = await _userManager.AddToRoleAsync(user, "RegularUser");

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
