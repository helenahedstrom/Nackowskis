using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nackowskis.BusinessLayer;
using Nackowskis.Models;
using Nackowskis.Services;
using Nackowskis.Services.Models;
using Nackowskis.ViewModels;

namespace Nackowskis.Controllers
{
    public class HomeController : Controller
    {
        private readonly INackowskisService _INackowskiService;
        private IAuctionBusiness _auctionBusiness;
        public HomeController(INackowskisService nackowskisService, IAuctionBusiness auctionBusiness)
        {
            _INackowskiService = nackowskisService;
            _auctionBusiness = auctionBusiness;
        }
        public IActionResult Index()
        {
            var model = new List<Auction>();
            model = _INackowskiService.GetObject<List<Auction>>();

            var ongoingAuctions = model.Where(a => a.EndDate > DateTime.Now && a.StartDate < DateTime.Now).ToList();
            return View(ongoingAuctions);
        }


        public IActionResult ViewBids(int auctionId, int price, string title, string description, DateTime endDate /*Auction auction*/)
        {
            var model = new BidViewModel();
            model.AuctionId = auctionId;
            model.StartPrice = price;
            model.AuctionTitle = title;
            model.AuctionDescription = description;
            model.endDate = endDate;
            model.ListOfBids = _auctionBusiness.GetBidsForAuction(auctionId);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult MakeBid(BidViewModel model, int highestBid)
        {
            var isSuccess = new SuccessViewModel();
            if (model.SumOfBid < model.StartPrice || model.SumOfBid < highestBid)
            {
                isSuccess.isSuccess = false;
            }
            else
            {
                string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                model.CreatedBy = userId;
                isSuccess.isSuccess = _auctionBusiness.SaveBid(model);

            }
            return PartialView("_ResultBidsPartial", isSuccess);

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
