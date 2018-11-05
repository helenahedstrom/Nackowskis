using Newtonsoft.Json;
using System;

namespace Nackowskis.Services.Models
{
    public class Auction
    {
        [JsonProperty("AuktionID")]
        public int Id { get; set; }

        [JsonProperty("Titel")]
        public string Title { get; set; }

        [JsonProperty("Beskrivning")]
        public string Description { get; set; }

        [JsonProperty("StartDatum")]
        public DateTime StartDate { get; set; }

        [JsonProperty("SlutDatum")]
        public DateTime EndDate { get; set; }

        [JsonProperty("Gruppkod")]
        public int GroupCode { get; set; }

        [JsonProperty("Utropspris")]
        public int StartPrice { get; set; }

        [JsonProperty("SkapadAv")]
        public string CreatedBy { get; set; }
    }
}
