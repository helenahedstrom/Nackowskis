using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskis.ViewModels
{
    public class AuctionViewModel
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int GroupCode { get; set; }

        public int StartPrice { get; set; }

        public string CreatedBy { get; set; }
    }
}
