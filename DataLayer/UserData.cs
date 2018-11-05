using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nackowskis.Data;
using Nackowskis.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Nackowskis.DataLayer
{
    public class UserData : IUserData
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ApplicationDbContext _context;
        public UserData(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async void Register(UserViewModel vm)
        {
            var user = new ApplicationUser { UserName = vm.Email, Email = vm.Email };

            var result = await _userManager.CreateAsync(user, vm.Password);

            var role = await _userManager.AddToRoleAsync(user, "RegularUser");

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

            }
        }

        public void UserAuthentication(UpgradeUserViewModel model)
        {
            var userRole = (from b in _context.UserRoles
                            where b.UserId == model.UserId
                            select b).FirstOrDefault();


            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();

            userRole.RoleId = model.RoleId;
            userRole.UserId = model.UserId;
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();

        }

        public UpgradeUserViewModel UserUpgrade(string userId)
        {
            var users = (
                        from a in _context.Users
                        join b in _context.UserRoles
                        on a.Id equals b.UserId
                        join c in _context.Roles
                        on b.RoleId equals c.Id
                        where a.Id == userId
                        select new UpgradeUserViewModel
                        {
                            UserId = a.Id,
                            UserName = a.UserName,
                            RoleId = b.RoleId,
                            RoleName = c.Name
                        }).FirstOrDefault();


            users.Roles = _context.Roles
      .Select(a => new SelectListItem()
      {
          Value = a.Id.ToString(),
          Text = a.Name
      }).ToList();

            return users;
        }

        public List<UpgradeUserViewModel> ViewUsers()
        {
            var users = (
                         from a in _context.Users
                         join b in _context.UserRoles
                         on a.Id equals b.UserId
                         join c in _context.Roles
                         on b.RoleId equals c.Id
                         select new UpgradeUserViewModel
                         {
                             UserId = a.Id,
                             UserName = a.UserName,
                             RoleId = b.RoleId,
                             RoleName = c.Name
                         }).ToList();
            return users;
        }
    }
}
