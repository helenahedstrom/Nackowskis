using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Configuration;
using Nackowskis.Models;
using Nackowskis.Services.Models;
using Newtonsoft.Json;

namespace Nackowskis.Services
{
    public class NackowskiService : INackowskisService
    {
        public NackowskiService(IConfiguration config)
        {
            ApiKey = config["ApiKey:Nackowskis"];
            BaseUrl = new Uri(config["ApiBaseUrls:Nackowskis"]);
        }

        public Uri BaseUrl { get; }

        public string ApiKey { get; }

        public string BuildQueryString(string query)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAuction(int vm)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = BaseUrl;
                var path = "Auktion/1130/" + vm;
                var response = client.DeleteAsync(path).Result;
                var isSuccess = GetAuction(vm);
                if (response.IsSuccessStatusCode && isSuccess)
                {
                    return true;
                }

            }
            return false;
        }

        public bool GetAuction(int auctionId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = BaseUrl;
                var reader = client.GetAsync("Auktion/1130").Result;

                var serializedObj = reader.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<List<Auction>>(serializedObj);
                var result = model.Where(a => a.Id == auctionId).FirstOrDefault();

                if (result == null)
                {
                    return true;
                }
                return false;
            }
        }

        public List<Bid> GetBidObjects<List>(int auctionId)
        {
            using (HttpClient client = new HttpClient())
            {
                var path = "Bud/1130/" + auctionId;
                client.BaseAddress = BaseUrl;
                var reader = client.GetAsync(path).Result;

                var serializedObj = reader.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<List<Bid>>(serializedObj);
                return model;
            }
        }

        public List<Auction> GetObject<List>()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = BaseUrl;
                var reader = client.GetAsync("Auktion/1130").Result;

                var serializedObj = reader.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<List<Auction>>(serializedObj);
                return model;
            }
        }

        public async Task<T> GetObjectAsync<T>(string queryString)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = BaseUrl;
                var reader = await client.GetAsync(queryString);

                var serializedObj = await reader.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<T>(serializedObj);
                return model;
            }
        }

        public bool SaveAuctionToApi<T>(T vm)
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = BaseUrl;
                var json = JsonConvert.SerializeObject(vm);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                var response = client.PostAsync("Auktion/1130", stringContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

            }
            return false;
        }

        public bool SaveBidToApi<Bid>(Models.Bid vm)
        {
            using (HttpClient client = new HttpClient())
            {
                var path = "Bud/1130/" + vm.AuctionId;
                client.BaseAddress = BaseUrl;
                var json = JsonConvert.SerializeObject(vm);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                var response = client.PostAsync(path, stringContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }


        public bool UpdateAuction<T>(T vm, int auctionId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = BaseUrl;
                var path = "Auktion/1130/" + auctionId;
                var json = JsonConvert.SerializeObject(vm);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                var response = client.PutAsync(path, stringContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

            }
            return false;
        }
    }
}
