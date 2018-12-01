using System;

namespace BMT.Customer.Web.Models
{
    public class OfferModel
    {
        public string AirlineName { get; set; }
        public string OfferId { get; set; }
        public DateTime OutboundDate { get; set; }
        public DateTime InboundDate { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationTime { get; set; }
    }
}