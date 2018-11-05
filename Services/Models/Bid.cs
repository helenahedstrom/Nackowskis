using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskis.Services.Models
{
    public class Bid
    {
        [JsonProperty("BudID")]
        public int BidId { get; set; }

        [JsonProperty("Summa")]
        public int SumOfBid { get; set; }

        [JsonProperty("AuktionID")]
        public int AuctionId { get; set; }

        [JsonProperty("Budgivare")]
        public string Bidder { get; set; }
        
    }
}
