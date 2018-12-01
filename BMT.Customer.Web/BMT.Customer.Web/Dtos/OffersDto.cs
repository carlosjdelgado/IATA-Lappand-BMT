using System;
using System.Collections.Generic;

namespace BMT.Customer.Web.Dtos
{
    public class OffersDto
    {
        public string ProposalId { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime OutboundDate { get; set; }
        public DateTime InboundDate { get; set; }
        public IEnumerable<OfferDto> Offers { get; set; } = new List<OfferDto>();
        public decimal Price { get; set; }
    }
}