using System;

namespace Bidmytrip.Core.Api.Dtos
{
    public class OfferDto
    {
        public OfferDto()
        {
        }

        public string AirlineName { get; set; }

        public string OfferId { get; set; }
        public string ProposalId { get; set; }        

        public DateTime OutboundDate { get; set; }
        public DateTime? InboundDate { get; set; }
        public decimal Price { get; set; }       
        public string OfferCode { get; set; }
        public bool Selected { get; set; }

        public DateTime CreationTime { get; set; }        
    }
}