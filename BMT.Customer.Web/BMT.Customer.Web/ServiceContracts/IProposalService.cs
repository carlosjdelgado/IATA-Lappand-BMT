using BMT.Customer.Web.Models;
using System.Threading.Tasks;

namespace BMT.Customer.Web.ServiceContracts
{
    public interface IProposalService
    {
        Task SendProposal(ProposalModel proposalModel);
    }
}
