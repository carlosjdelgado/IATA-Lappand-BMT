using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMT.Airline.Web.Models
{
    public class ProposalViewModel
    {
        public string ProposalId { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsAcceptable { get; set; }   
        public string Status { get; set; }
        public IEnumerable<PassengerViewModel> Passengers { get; set; }
        public IEnumerable<OfferViewModel> Offers { get; set; }
    }
}