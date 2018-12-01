using BMT.Customer.Web.ServiceContracts;
using BMT.Customer.Web.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BMT.Customer.Web.Controllers
{
    public class AcceptanceController : Controller
    {
        private IProposalService _proposalService = new ProposalService();

        // GET: Acceptance
        //[HttpGet]
        //public async Task<ActionResult> Index(string proposalId)
        //{
        //    var p = await _proposalService.GetProposal(proposalId);
        //    return View(p);
        //}
    }
}