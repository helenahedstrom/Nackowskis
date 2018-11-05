using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskis.ViewModels
{
    public class UpgradeUserViewModel
    {
        public UpgradeUserViewModel()
        {
            Roles = new List<SelectListItem>();
        }

        public List<SelectListItem> Roles { get; set; }

        public string RoleId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string RoleName { get; set; }
    }
}
