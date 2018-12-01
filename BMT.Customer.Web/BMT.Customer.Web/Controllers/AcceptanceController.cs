using BMT.Customer.Web.Dtos;
using BMT.Customer.Web.Models;
using BMT.Customer.Web.ServiceContracts;
using BMT.Customer.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BMT.Customer.Web.Controllers
{
    public class AcceptanceController : Controller
    {
        private IProposalService _proposalService = new ProposalService();
        private IOfferService _offerService = new OfferService();

        [HttpGet]
        public async Task<ActionResult> Index(string proposalId, string offerId)
        {
            IEnumerable<ProposalModel> proposals = await _proposalService.GetProposals();

            var proposalAcceptance = GetProposalAcceptance(proposals, proposalId);

            var proposalAcceptanceDto = MapProposalAcceptanceDto(proposalAcceptance);

            return View("~/Views/Acceptance/Index.cshtml", proposalAcceptanceDto);
        }
        
        public async Task<ActionResult> ConfirmOffer(string proposalId, string offerId)
        {
            var confirmOfferDto = new ConfirmOfferDto
            {
                ProposalId = proposalId,
                OfferId = offerId
            };

            await _offerService.ConfirmOffer(confirmOfferDto);
            return RedirectToAction("Proposals", "CustomerProposals");
        }

        private ProposalAcceptanceDto MapProposalAcceptanceDto(ProposalModel proposalAcceptance)
        {
            return new ProposalAcceptanceDto
            {
                ProposalId = proposalAcceptance.ProposalId,
                OfferId = BuildOfferId(proposalAcceptance.Offers),
                TravellerName = proposalAcceptance.TravellerName,
                OutboundDate = proposalAcceptance.OutboundDate,
                InboundDate = proposalAcceptance.InboundDate,
                Price = BuildBestOfferPrice(proposalAcceptance.Offers),
                Passengers = BuildPassengers(proposalAcceptance)
            };
        }

        private string BuildOfferId(IEnumerable<OfferModel> offers)
        {
            return offers.OrderBy(o => o.Price).FirstOrDefault()?.OfferId;
        }

        private decimal BuildBestOfferPrice(IEnumerable<OfferModel> offers)
        {
            return offers.OrderBy(o => o.Price).FirstOrDefault().Price;
        }

        private ProposalModel GetProposalAcceptance(IEnumerable<ProposalModel> proposals, string proposalId)
        {
            return proposals.Where(p => p.ProposalId == proposalId).FirstOrDefault();
        }

        private static IEnumerable<PassengerDto> BuildPassengers(ProposalModel proposalDto)
        {
            var passengers = new List<PassengerDto>();

            if (proposalDto.Passenger1 != null)
                passengers.Add(BuildPassenger(proposalDto.Passenger1));

            if (proposalDto.Passenger2 != null)
                passengers.Add(BuildPassenger(proposalDto.Passenger2));

            if (proposalDto.Passenger3 != null)
                passengers.Add(BuildPassenger(proposalDto.Passenger3));

            if (proposalDto.Passenger4 != null)
                passengers.Add(BuildPassenger(proposalDto.Passenger4));

            return passengers;
        }

        private static PassengerDto BuildPassenger(PassengerModel passenger)
        {
            return new PassengerDto
            {
                FirstName = passenger.FirstName,
                SecondName = passenger.SecondName,
                PassengerType = passenger.Type
            };
        }
    }
}