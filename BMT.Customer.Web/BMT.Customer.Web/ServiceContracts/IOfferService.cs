using BMT.Customer.Web.Dtos;
using BMT.Customer.Web.Models;
using System.Threading.Tasks;

namespace BMT.Customer.Web.ServiceContracts
{
    public interface IOfferService
    {
        Task<ProposalModel> GetOffers(string proposalId);
        Task ConfirmOffer(ConfirmOfferDto confirmOfferDto);
    }
}
