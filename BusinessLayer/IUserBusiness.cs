using Nackowskis.Services.Models;
using Nackowskis.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskis.BusinessLayer
{
    public interface IUserBusiness
    {
        //UserViewModel RegisterUser(UserViewModel user);
        List<UpgradeUserViewModel> ViewUsers();

        UpgradeUserViewModel UserUpgrade(string userId);

        void UserAuthentication(UpgradeUserViewModel model);

        List<Auction> GetSearchResult(string searchTerm);
    }
}
