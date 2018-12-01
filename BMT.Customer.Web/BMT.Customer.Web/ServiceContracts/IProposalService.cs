using BMT.Customer.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BMT.Customer.Web.ServiceContracts
{
    public interface IProposalService
    {
        Task<IEnumerable<ProposalModel>> GetProposals();
        Task<HttpResponseMessage> SendProposal(ProposalModel proposalModel);
    }
}
