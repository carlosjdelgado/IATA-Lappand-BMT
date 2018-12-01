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
        public int Price { get; set; }
        public IEnumerable<PassengerModel> Passengers { get; set; }
        public DateTime TimeToLive { get; set; }
        public string Status { get; set; }
    }
}