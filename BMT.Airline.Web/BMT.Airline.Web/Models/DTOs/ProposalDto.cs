using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMT.Airline.Web.Models.DTOs
{
    public class ProposalDto
    {
        public string TravellerName { get; set; }
        public string ProposalId { get; set; }
        public string Origin { get; set; }
        public string Destiny { get; set; }
        public DateTime OutboundDate { get; set; }
        public DateTime InboundDate { get; set; }
        public decimal Price { get; set; }
        public PassengerDto Passenger1 { get; set; }
        public PassengerDto Passenger2 { get; set; }
        public PassengerDto Passenger3 { get; set; }
        public PassengerDto Passenger4 { get; set; }
        public IEnumerable<OfferDto> Offers { get; set; }
        public DateTime TimeToLive { get; set; }
        public string Status { get; set; }
    }
}