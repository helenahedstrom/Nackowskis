using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nackowskis.Data;
using Nackowskis.DataLayer;
using Nackowskis.Models;
using Nackowskis.Services;
using Nackowskis.Services.Models;
using Nackowskis.ViewModels;

namespace Nackowskis.BusinessLayer
{
    public class UserBusiness : IUserBusiness
    {
        private IUserData _repo;
        private INackowskisService _apiRepo;
        public UserBusiness(IUserData repo, INackowskisService apiRepo)
        {
            _repo = repo;
            _apiRepo = apiRepo;
        }

        public List<Auction> GetSearchResult(string searchTerm)
        {
            var model = new List<Auction>();
            model = _apiRepo.GetObject<List<Auction>>();


            return model.Where(a => a.Title.ToUpper().Contains(searchTerm.ToUpper()) || a.Description.ToUpper().Contains(searchTerm.ToUpper())).ToList(); ;
        }

        public void UserAuthentication(UpgradeUserViewModel model)
        {
            _repo.UserAuthentication(model);
        }

        public UpgradeUserViewModel UserUpgrade(string userId)
        {
            return _repo.UserUpgrade(userId);
        }

        List<UpgradeUserViewModel> IUserBusiness.ViewUsers()
        {
            return _repo.ViewUsers();
        }

        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        //public UserBusiness(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //}
        //public UserViewModel RegisterUser(UserViewModel user)
        //{
        //    //var user = new ApplicationUser { UserName = vm.UserName, Email = vm.Email };

        //    //var result = await _userManager.CreateAsync(user, vm.Password);

        //    //var role = await _userManager.AddToRoleAsync(user, "RegularUser");

        //    //if (result.Succeeded)
        //    //{
        //    //    await _signInManager.SignInAsync(user, false);

        //    //    return RedirectToAction("Index", "Home");
        //    //}

        //    //return user = _repo.Register(user);


        //}
    }
}
