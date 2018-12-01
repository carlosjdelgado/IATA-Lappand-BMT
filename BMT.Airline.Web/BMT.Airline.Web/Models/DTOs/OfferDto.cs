using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMT.Airline.Web.Models.DTOs
{
    public class OfferDto
    {
        public string ProposalId { get; set; }
        public DateTime CreationTime { get; set; }
        public string AirlineName { get; set; }
        public string OfferId { get; set; }
        public DateTime OutboundDate { get; set; }
        public DateTime InboundDate { get; set; }
        public decimal Price { get; set; }
    }
}