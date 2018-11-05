using Nackowskis.Services;
using Nackowskis.Services.Models;
using Nackowskis.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskis.BusinessLayer
{
    public class AuctionBusiness : IAuctionBusiness
    {
        private INackowskisService _apiRepo;
        public AuctionBusiness(INackowskisService apiRepo)
        {
            _apiRepo = apiRepo;
        }

        public List<Auction> GetAuctions(string userId)
        {
            var result = _apiRepo.GetObject<List<Auction>>();
            var model = result.Where(a => a.CreatedBy == userId).ToList();
            return model;
        }

        public bool DeleteAuctions(int AuctionId)
        {
            return _apiRepo.DeleteAuction(AuctionId);
        }

        public bool UpdateAuction(Auction model, int auctionId)
        {
            return _apiRepo.UpdateAuction(model, auctionId);
        }

        public Auction GetSelectedAuction(int AuctionId)
        {
            var auctions = _apiRepo.GetObject<List<Auction>>();
            return auctions.Where(a => a.Id == AuctionId).FirstOrDefault();

        }

        public List<Bid> GetBidsForAuction(int auctionId)
        {
            return _apiRepo.GetBidObjects<List<Bid>>(auctionId);
        }

        public bool SaveBid(BidViewModel model)
        {
            var bidModel = new Bid();
            bidModel.AuctionId = model.AuctionId;
            bidModel.Bidder = model.CreatedBy;
            bidModel.SumOfBid = model.SumOfBid;
            return _apiRepo.SaveBidToApi<Bid>(bidModel);

        }
    }
}
