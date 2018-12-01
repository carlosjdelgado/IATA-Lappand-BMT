using BMT.Airline.Web.Models;
using BMT.Airline.Web.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMT.Airline.Web.Mappers
{
    public class OfferViewModelMapper
    {
        public static OfferViewModel Map(OfferDto offer)
        {
            return new OfferViewModel
            {
                AirlineName = offer.AirlineName,
                CreationTime = offer.CreationTime,
                DepartureDate = offer.OutboundDate,
                ReturnDate = offer.InboundDate,
                Price = offer.Price
            };
        }
    }
}