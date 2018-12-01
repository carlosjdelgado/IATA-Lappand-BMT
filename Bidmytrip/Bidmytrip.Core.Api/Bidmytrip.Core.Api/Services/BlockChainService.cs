
using Bidmytrip.Core.Api.Dtos;
using Microsoft.Azure.WebJobs.Host;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidmytrip.Core.Api.Services
{
    public class BlockChainService
    {
        private readonly TraceWriter _log;

        public BlockChainService(TraceWriter log)
        {
            _log = log;
        }

        internal void PostProposal(ProposalDto proposal)
        {
            CacheService.Proposals.Add(proposal);

            //RecordNewProposal().Wait();
        }

        internal IEnumerable<ProposalDto> GetProposals()
        {
            return CacheService.Proposals;
        }

        internal void PostOffer(OfferDto offer)
        {
            var proposal = CacheService.Proposals.First(p => p.ProposalId == offer.ProposalId);

            proposal.Offers.Add(offer);
        }
        /*
        private async Task RecordNewProposal()
        {
            var authToken = await AuthBidMyTripService.Auth();
            GatewayApi.Instance.SetAuthToken(authToken);
            GatewayApi.SiteUrl = "https://iagndcblockchain-qqau77-api.azurewebsites.net";
            var response = await GatewayApi.Instance.GetApplicationsAsync();
            _log.Info(response.ToString());
        }*/
    }
}
