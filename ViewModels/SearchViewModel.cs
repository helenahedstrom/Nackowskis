using Nackowskis.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskis.ViewModels
{
    public class SearchViewModel
    {
        public SearchViewModel()
        {
            var ListOfAuctions = new List<Auction>();
        }
        public string SearchTerm { get; set; }
        public List<Auction> ListOfAuctions { get; set; }
    }
}
