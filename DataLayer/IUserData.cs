using Microsoft.AspNetCore.Mvc;
using Nackowskis.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskis.DataLayer
{
    public interface IUserData
    {
        void Register(UserViewModel user);

        List<UpgradeUserViewModel> ViewUsers();

        UpgradeUserViewModel UserUpgrade(string userId);

        void UserAuthentication(UpgradeUserViewModel model);
    }
}
