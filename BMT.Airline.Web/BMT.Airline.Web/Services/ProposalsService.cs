using BMT.Airline.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMT.Airline.Web.Services
{
    public class ProposalsService
    {
        public ProposalsService()
        {
        }

        public IEnumerable<ProposalViewModel> GetActiveProposals()
        {
            return new List<ProposalViewModel>
            {
                new ProposalViewModel
                {
                    DepartureCity = "MAD",
                    ArrivalCity = "AMS",
                    DepartureDate = new DateTime(2018, 12, 10),
                    ReturnDate = new DateTime(2018,12,20),
                    Price = 100,
                    IsAcceptable = true
                },
                new ProposalViewModel
                {
                    DepartureCity = "MAD",
                    ArrivalCity = "AMS",
                    DepartureDate = new DateTime(2018, 12, 12),
                    ReturnDate = new DateTime(2018,12,18),
                    Price = 80,
                    IsAcceptable = false
                },
                new ProposalViewModel
                {
                    DepartureCity = "MAD",
                    ArrivalCity = "LON",
                    DepartureDate = new DateTime(2019, 1, 12),
                    ReturnDate = new DateTime(2019,1,18),
                    Price = 300,
                    IsAcceptable = true
                }
            };
        } 
    }
}