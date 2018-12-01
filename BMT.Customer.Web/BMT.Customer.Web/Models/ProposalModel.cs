using System;
using System.Collections.Generic;

namespace BMT.Customer.Web.Models
{
    public class ProposalModel
    {
        public string TravellerName { get; set; }
        public string ProposalId { get; set; }
        public string Origin { get; set; }
        public string Destiny { get; set; }
        public DateTime OutboundDate { get; set; }
        public DateTime InboundDate { get; set; }
        public decimal Price { get; set; }
        public PassengerModel Passenger1 { get; set; } = new PassengerModel();
        public PassengerModel Passenger2 { get; set; } = new PassengerModel();
        public PassengerModel Passenger3 { get; set; } = new PassengerModel();
        public PassengerModel Passenger4 { get; set; } = new PassengerModel();
        public DateTime TimeToLive { get; set; }
        public string Status { get; set; }
        public IEnumerable<OfferModel> Offers { get; set; } = new List<OfferModel>();
    }
}