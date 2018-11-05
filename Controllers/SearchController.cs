using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nackowskis.BusinessLayer;
using Nackowskis.Services;
using Nackowskis.Services.Models;
using Nackowskis.ViewModels;

namespace Nackowskis.Controllers
{
    public class SearchController : Controller
    {
        //private readonly INackowskisService _INackowskiService;
        //public SearchController(INackowskisService nackowskisService)
        //{
        //    _INackowskiService = nackowskisService;
        //}

        private IUserBusiness _businessLayer;
        private IAuctionBusiness _auctionBusiness;

        public SearchController(IUserBusiness businesslayer, IAuctionBusiness auctionBusiness)
        {
            _businessLayer = businesslayer;
            _auctionBusiness = auctionBusiness;
        }

        //public IActionResult Search()
        //{
        //    var model = new SearchViewModel();
        //    return View(model);

        //    //return View(model);
        //}

        public IActionResult Search(string searchTerm)
        {
            var model = new SearchViewModel();
            model.SearchTerm = searchTerm;
            model.ListOfAuctions = _businessLayer.GetSearchResult(searchTerm);
            return View(model);

        }


        public IActionResult SearchResult(int auctionId, int price, string title, string description, DateTime endDate, string searchTerm)
        {
            var model = new BidViewModel();
            model.AuctionId = auctionId;
            model.StartPrice = price;
            model.AuctionTitle = title;
            model.AuctionDescription = description;
            model.endDate = endDate;
            model.ListOfBids = _auctionBusiness.GetBidsForAuction(auctionId);
            model.serchTerm = searchTerm;
            return View(model);
        }
    }
}