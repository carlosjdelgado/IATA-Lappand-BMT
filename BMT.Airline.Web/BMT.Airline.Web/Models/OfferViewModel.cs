using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMT.Airline.Web.Models
{
    public class OfferViewModel
    {
        public DateTime CreationTime { get; set; }
        public string AirlineName { get; set; }
        public string AirlineLogo { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal Price { get; set; }
        public bool Selected { get; set; }
    }
}