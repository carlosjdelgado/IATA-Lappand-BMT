using BMT.Airline.Web.Models;
using BMT.Airline.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BMT.Airline.Web.Controllers
{
    public class AcceptanceController : Controller
    {
        private ProposalsService _proposalsService;
        private OffersService _offersService;
        
        public AcceptanceController()
        {
            _proposalsService = new ProposalsService();
            _offersService = new OffersService();
        }

        // GET: Acceptance
        [HttpGet]
        public async Task<ActionResult> Index(string proposalId)
        {
            var p = await _proposalsService.GetProposal(proposalId);
            return View(p);
        }

        [HttpPost]
        public async Task<ActionResult> PostOffer(OfferRequest request)
        {
            await _offersService.PostOffer(request);
            return RedirectToAction("Index", new { ProposalId = request.ProposalId });
        } 
    }
}