using BMT.Customer.Web.Models;
using BMT.Customer.Web.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMT.Customer.Web.Services
{
    public class OfferService : IOfferService
    {
        private const string BidMyTripProposalUrl = "https://bidmytripcoreapiv1.azurewebsites.net/api/Proposals/";
        private readonly IProposalService _proposalService = new ProposalService();

        public async Task<ProposalModel> GetOffers(string proposalId)
        {
            var proposalsModel = await _proposalService.GetProposals().ConfigureAwait(false);

            return proposalsModel.Where(p => p.ProposalId == proposalId).FirstOrDefault();
        }
    }
}