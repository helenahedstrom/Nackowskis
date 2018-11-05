using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nackowskis.BusinessLayer;
using Nackowskis.Services;
using Nackowskis.Services.Models;
using Nackowskis.ViewModels;

namespace Nackowskis.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IUserBusiness _businessLayer;
        private INackowskisService _nackowski;
        private IAuctionBusiness _auctionBusiness;
        public AdminController(INackowskisService nackowski, IUserBusiness businesslayer, IAuctionBusiness auctionBusiness)
        {
            _nackowski = nackowski;
            _businessLayer = businesslayer;
            _auctionBusiness = auctionBusiness;
        }

        public IActionResult HandleAuction()
        {
            return View();
        }

        public IActionResult AddAuction()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAuction(Auction auctionVm)
        {
            //string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //auctionVm.GroupCode = 1130;
            //auctionVm.CreatedBy = userId;

            //var isSuccess = _nackowski.SaveAuctionToApi(auctionVm).Result;

            //if (isSuccess)
            //{
            //    return PartialView();
            //}
            //return View();

            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            auctionVm.GroupCode = 1130;
            auctionVm.CreatedBy = userId;
            var isSuccess = new SuccessViewModel();
            isSuccess.isSuccess = _nackowski.SaveAuctionToApi(auctionVm);

            return PartialView("_AddedAuctionPartial", isSuccess);
        }


        public IActionResult UpdateDeleteAuction()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = _auctionBusiness.GetAuctions(userId);
            return View(model);
        }

        public IActionResult UpdateAuction(int auctionId)
        {
            var model = _auctionBusiness.GetSelectedAuction(auctionId);
            model.Id = auctionId;
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateSelectedAuction(Auction model)
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.GroupCode = 1130;
            model.CreatedBy = userId;
            var auctionId = model.Id;
            var isSuccess = new SuccessViewModel();
            isSuccess.isSuccess = _auctionBusiness.UpdateAuction(model, auctionId);
            return PartialView("_UpdatedAuctionPartial", isSuccess);
        }


        public IActionResult DeleteSelectedAuction(int auctionId)
        {
            var isSuccess = new SuccessViewModel();
            isSuccess.isSuccess = _auctionBusiness.DeleteAuctions(auctionId);
            return View(isSuccess);
        }


        public IActionResult ViewUsers()
        {
            List<UpgradeUserViewModel> model = _businessLayer.ViewUsers();
            return View(model);
        }

        public IActionResult UserUpgrade(string userId)
        {
            UpgradeUserViewModel model = _businessLayer.UserUpgrade(userId);
            return View(model);
        }

        [HttpPost]
        public IActionResult UserUpgrade(UpgradeUserViewModel model)
        {
            _businessLayer.UserAuthentication(model);
            return PartialView("_UpgradedUserPartial");
        }
    }
}