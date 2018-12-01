using BMT.Customer.Web.Dtos;
using BMT.Customer.Web.Models;
using BMT.Customer.Web.ServiceContracts;
using BMT.Customer.Web.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BMT.Customer.Web.Controllers
{
    public class AcceptanceController : Controller
    {
        private readonly IProposalService _proposalService = new ProposalService();

        public ActionResult Index()
        {
            return View();
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
                TimeToLive = DateTime.Now.AddDays(20),
                Status = "PROPOSED"
            };
        }
    }
}