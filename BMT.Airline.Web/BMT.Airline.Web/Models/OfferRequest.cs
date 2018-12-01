using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMT.Airline.Web.Models
{
    public class OfferRequest
    {
        public string ProposalId { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal Price { get; set; }
        public string OfferCode { get; set; }
    }
}