using System;

namespace Bidmytrip.Core.Api.Dtos
{
    public class OfferDto
    {
        public OfferDto()
        {
            CreationTime = DateTime.UtcNow;
            Selected = false;
        }

        public string AirlineName { get; set; }

        public string OfferId { get; set; }
        public string ProposalId { get; set; }        

        public DateTime OutboundDate { get; set; }
        public DateTime? InboundDate { get; set; }
        public decimal Price { get; set; }       
        public string OfferCode { get; set; }
        public bool Selected { get; internal set; }

        public DateTime CreationTime { get; }        
    }
}