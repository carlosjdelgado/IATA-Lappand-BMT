using BMT.Customer.Web.Dtos;
using BMT.Customer.Web.Models;
using System.Linq;

namespace BMT.Customer.Web.Mappers
{
    public static class OffersDtoMapper
    {
        public static OffersDto Map(ProposalModel proposalModel)
        {
            return new OffersDto
            {
                ProposalId = proposalModel.ProposalId,
                DepartureCity = proposalModel.Origin,
                ArrivalCity = proposalModel.Destiny,
                OutboundDate = proposalModel.OutboundDate,
                InboundDate = proposalModel.InboundDate,
                Price = proposalModel.Price,
                Offers = proposalModel.Offers?.Select(OfferDtoMapper.Map)
            };
        }
    }
}