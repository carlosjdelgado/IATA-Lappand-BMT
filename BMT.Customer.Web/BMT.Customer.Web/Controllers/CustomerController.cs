using BMT.Customer.Web.Dtos;
using BMT.Customer.Web.Models;
using BMT.Customer.Web.ServiceContracts;
using BMT.Customer.Web.Services;
using System;
using System.Collections.Generic;
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

        [HttpPost]
        public async Task<HttpResponseMessage> SentProposal(CustomerFormDto customerFormDto)
        {
            var proposalModel = MapProposalModel(customerFormDto);

            return await _proposalService.SendProposal(proposalModel);
        }

        private ProposalModel MapProposalModel(CustomerFormDto customerFormDto)
        {
            return new ProposalModel
            {
                ProposalId = Guid.NewGuid().ToString(),
                TravellerName = customerFormDto.FirstName,
                Origin = customerFormDto.DepartureCity,
                Destiny = customerFormDto.ArrivalCity,
                OutboundDate = customerFormDto.DepartureDatetime,
                InboundDate = customerFormDto.ArrivalDatetime,
                Passenger1 = new PassengerModel
                {
                    FirstName = customerFormDto.FirstName,
                    SecondName = customerFormDto.SecondName,
                    Type = customerFormDto.PassengerType
                },
                Price = customerFormDto.Price,
                TimeToLive = DateTime.Now.AddDays(20),
                Status = "PROPOSED"
            };
        }
    }
}