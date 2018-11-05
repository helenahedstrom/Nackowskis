using Nackowskis.Models;
using Nackowskis.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskis.Services
{
    public interface IHttpClientService
    {
        Uri BaseUrl { get; }
        string ApiKey { get; }

        List<Auction> GetObject<List>();

        bool GetAuction(int auctionId);

        List<Bid> GetBidObjects<List>(int auctionId);

        Task<T> GetObjectAsync<T>(string queryString);
    }
}
