using System;

namespace BMT.Customer.Web.Dtos
{
    public class OfferDto
    {
        public string OfferId { get; set; }
        public DateTime OutboundDate { get; set; }
        public DateTime InboundDate { get; set; }
        public decimal Price { get; set; }
        public bool IsBestPrice { get; set; }
        public bool Selected { get; set; }
        public void SetAsBestPrice()
        {
            IsBestPrice = true;
        }
    }
}