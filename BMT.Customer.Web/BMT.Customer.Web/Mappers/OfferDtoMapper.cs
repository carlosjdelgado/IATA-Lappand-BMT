using BMT.Customer.Web.Dtos;
using BMT.Customer.Web.Models;

namespace BMT.Customer.Web.Mappers
{
    public static class OfferDtoMapper
    {
        public static OfferDto Map(OfferModel offerModel)
        {
            return new OfferDto
            {
                OfferId = offerModel.OfferId,
                OutboundDate = offerModel.OutboundDate,
                InboundDate = offerModel.InboundDate,
                Price = offerModel.Price
            };
        }
    }
}