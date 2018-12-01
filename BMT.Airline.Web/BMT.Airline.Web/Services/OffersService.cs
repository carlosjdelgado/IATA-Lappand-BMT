using BMT.Airline.Web.Models;
using BMT.Airline.Web.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BMT.Airline.Web.Services
{
    public class OffersService
    {
        private ConfigurationProvider _configuration;
        private BMTCoreApiService _bmtCoreApiService;

        public OffersService()
        {
            _configuration = new ConfigurationProvider();
            _bmtCoreApiService = new BMTCoreApiService();
        }

        public async Task PostOffer(OfferRequest offerRequest)
        {
            var offerDto = new OfferDto
            {
                AirlineName = _configuration.AirlineName,
                CreationTime = DateTime.Now,
                OfferId = Guid.NewGuid().ToString(),
                Price = offerRequest.Price,
                InboundDate = offerRequest.ReturnDate,
                OutboundDate = offerRequest.DepartureDate,
                ProposalId = offerRequest.ProposalId
            };

            await _bmtCoreApiService.PostOffer(offerDto).ConfigureAwait(false);
        }
    }
}