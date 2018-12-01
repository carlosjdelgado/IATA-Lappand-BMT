using BMT.Customer.Web.Dtos;
using BMT.Customer.Web.Models;
using BMT.Customer.Web.ServiceContracts;
using BMT.Customer.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BMT.Customer.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IProposalService _proposalService = new ProposalService();

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Proposals()
        {
            var proposalModel = await _proposalService.GetProposal();

            var proposalsDto = MapProposalDto(proposalModel);

            return View("~/Views/Customer/Proposals.cshtml", proposalsDto);
        }

        private ProposalsDto MapProposalDto(IEnumerable<ProposalModel> proposalModel)
        {
            var proposalDtoList = new List<ProposalDto>();

            foreach (var proposal in proposalModel)
            {
                foreach (var offer in proposal.Offers)
                {
                    proposalDtoList.Add(
                        new ProposalDto
                        {
                            ProposalId = proposal.ProposalId,
                            OfferId = offer.OfferId,
                            AirlineName = offer.AirlineName,
                            OutboundDate = offer.OutboundDate,
                            InboundDate = offer.InboundDate,
                            DepartureCity = proposal.Origin,
                            ArrivalCity = proposal.Destiny,
                            Price = offer.Price
                        }
                    );
                }
            }

            SetBestPrice(proposalDtoList);

            return new ProposalsDto
            {
                Proposals = proposalDtoList
            };
        }

        private void SetBestPrice(List<ProposalDto> proposalDtoList)
        {
            var disctinctProposals = proposalDtoList.GroupBy(o => o.ProposalId).Select(g => g.First());

            foreach (var disctinctProposal in disctinctProposals)
            {
                proposalDtoList.Where(p => p.ProposalId == disctinctProposal.ProposalId).OrderBy(p => p.Price).First().IsAcceptable = true;
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> SentProposal(CustomerFormDto CustomerFormRequestDto)
        {
            var proposalModel = MapProposalModel(CustomerFormRequestDto);

            return await _proposalService.SendProposal(proposalModel);
        }

        private ProposalModel MapProposalModel(CustomerFormDto CustomerFormRequestDto)
        {
            return new ProposalModel
            {
                ProposalId = Guid.NewGuid().ToString(),
                TravellerName = CustomerFormRequestDto.FirstName,
                Origin = CustomerFormRequestDto.DepartureCity,
                Destiny = CustomerFormRequestDto.ArrivalCity,
                OutboundDate = CustomerFormRequestDto.DepartureDatetime,
                InboundDate = CustomerFormRequestDto.ArrivalDatetime,
                Passenger1 = new PassengerModel
                {
                    FirstName = CustomerFormRequestDto.FirstName,
                    SecondName = CustomerFormRequestDto.SecondName,
                    Type = CustomerFormRequestDto.PassengerType
                },
                Price = CustomerFormRequestDto.Price,
                TimeToLive = DateTime.Now.AddDays(20)
            };
        }
    }
}