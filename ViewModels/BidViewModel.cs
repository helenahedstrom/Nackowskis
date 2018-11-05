using Nackowskis.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskis.ViewModels
{
    public class BidViewModel
    {
        public BidViewModel()
        {
            ListOfBids = new List<Bid>();
        }
        public int AuctionId { get; set; }
        public int SumOfBid { get; set; }
        public int StartPrice { get; set; }
        public string CreatedBy { get; set; }
        public string AuctionTitle { get; set; }
        public string AuctionDescription { get; set; }
        public DateTime endDate { get; set; }
        public string serchTerm { get; set; }

        public List<Bid> ListOfBids { get; set; }
    }
}
