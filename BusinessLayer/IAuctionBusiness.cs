using Nackowskis.Services.Models;
using Nackowskis.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskis.BusinessLayer
{
    public interface IAuctionBusiness
    {
        List<Auction> GetAuctions(string userId);
        bool DeleteAuctions(int AuctionId);
        bool UpdateAuction(Auction model, int auctionId);
        Auction GetSelectedAuction(int AuctionId);
        List<Bid> GetBidsForAuction(int auctionId);
        bool SaveBid(BidViewModel model);
    }
}
