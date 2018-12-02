using System;

namespace BMT.Customer.Web.Dtos
{
    public class ProposalDto
    {
        public string ProposalId { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime OutboundDate { get; set; }
        public DateTime InboundDate { get; set; }
        public decimal Price { get; set; }
        public bool HasOffers { get; set; }
        public string Status { get; set; }
    }
}