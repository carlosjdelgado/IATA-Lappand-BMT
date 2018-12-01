using BMT.Airline.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BMT.Airline.Web.Controllers
{
    public class ProposalsController : Controller
    {
        private ProposalsService _proposalsService;

        public ProposalsController()
        {
            _proposalsService = new ProposalsService();
        }

        // GET: Proposals
        public async Task<ActionResult> Index()
        {
            var proposals = await _proposalsService.GetProposalsReport();
            return View(proposals);
        }
    }
}