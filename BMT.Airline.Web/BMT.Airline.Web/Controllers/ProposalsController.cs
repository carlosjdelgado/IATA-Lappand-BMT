using BMT.Airline.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Index()
        {
            var proposals = _proposalsService.GetActiveProposals();
            return View(proposals);
        }
    }
}