using BMT.Customer.Web.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace BMT.Customer.Web.ServiceContracts
{
    public interface IProposalService
    {
        Task<HttpResponseMessage> SendProposal(ProposalModel proposalModel);
    }
}
