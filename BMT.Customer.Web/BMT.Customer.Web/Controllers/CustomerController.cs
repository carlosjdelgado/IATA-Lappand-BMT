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

        [HttpGet]
        public async Task<ActionResult> Proposals()
        {
            var proposalsModel = await _proposalService.GetProposals();

            var disctinctProposals = GetDistinctProposals(proposalsModel);

            var proposalsDto = new ProposalsDto
            {
                Proposals = MapProposalDto(disctinctProposals)
            };

            return View("~/Views/Customer/Proposals.cshtml", proposalsDto);
        }

        [HttpPost]
        public async Task<ActionResult> SentProposal(CustomerFormDto CustomerFormRequestDto)
        {
            var proposalModel = MapProposalModel(CustomerFormRequestDto);

            await _proposalService.SendProposal(proposalModel);

            return RedirectToAction("Proposals", "Customer");
        }

        private IEnumerable<ProposalDto> MapProposalDto(IEnumerable<ProposalModel> distintictProposals)
        {
            var proposalDtoList = new List<ProposalDto>();

            foreach (var distintictProposal in distintictProposals)
            {
                proposalDtoList.Add(new ProposalDto
                {
                    ProposalId = distintictProposal.ProposalId,
                    OutboundDate = distintictProposal.OutboundDate,
                    InboundDate = distintictProposal.InboundDate,
                    DepartureCity = distintictProposal.Origin,
                    ArrivalCity = distintictProposal.Destiny,
                    Price = distintictProposal.Price
                });
            }

            return proposalDtoList;
        }

        private IEnumerable<ProposalModel> GetDistinctProposals(IEnumerable<ProposalModel> proposalsModel)
        {
            return proposalsModel.GroupBy(o => o.ProposalId).Select(g => g.First());
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
                    PassengerType = CustomerFormRequestDto.PassengerType
                },
                Price = CustomerFormRequestDto.Price,
                TimeToLive = DateTime.Now.AddDays(20)
            };
        }
    }
}