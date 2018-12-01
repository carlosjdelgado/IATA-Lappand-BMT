using System;
using System.Collections.Generic;

namespace BMT.Customer.Web.Dtos
{
    public class ProposalAcceptanceDto
    {
        public string ProposalId { get; set; }
        public string TravellerName { get; set; }
        public DateTime OutboundDate { get; set; }
        public DateTime InboundDate { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<PassengerDto> Passengers { get; set; } = new List<PassengerDto>();
    }
}