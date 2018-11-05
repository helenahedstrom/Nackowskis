using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskis.Services
{
    public interface INackowskisService : IHttpClientService
    {
        string BuildQueryString(string query);

        bool SaveAuctionToApi<T>(T vm);

        bool SaveBidToApi<Bid>(Models.Bid vm);

        bool DeleteAuction(int vm);

        bool UpdateAuction<T>(T vm, int auctionId);
    }
}
