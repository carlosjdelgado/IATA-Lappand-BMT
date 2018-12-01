using Bidmytrip.Core.Api.Dtos;
using Microsoft.Azure.WebJobs.Host;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidmytrip.Core.Api.Services
{
    public class BidMyTripService
    {
        private const string Proposed = "PROPOSED";
        private const string Accepted = "ACCEPTED";
        private const string Confirmed = "CONFIRMED";

        private readonly TraceWriter _log;
        private readonly WorkBenchService _workBenchService;

        public BidMyTripService(TraceWriter log, WorkBenchService workBenchService)
        {
            _workBenchService = workBenchService;
        }

        internal void PostProposals(IEnumerable<ProposalDto> proposals)
        {
            var proposalList = proposals.ToList();

            foreach(var p in proposalList)
            {
                p.Status = Proposed;
            }

            foreach (var p in proposalList)
            {
                CacheService.Proposals.Add(p);
            }
        }

        internal async Task<ProposalDto> PostProposal(string authToken, ProposalDto proposal)
        {
            proposal.Status = Proposed;

            CacheService.Proposals.Add(proposal);

            //await _workBenchService.RecordNewProposal(authToken, proposal);

            return proposal;
        }

        internal ProposalDto ConfirmProposal(string authToken, ProposalConfirmedDto proposalConfirmedDto)
        {
            var proposal = CacheService.Proposals
                .First(p => p.ProposalId == proposalConfirmedDto.ProposalId);

            proposal.Status = Confirmed;

            var offer = proposal.Offers.First(o => o.OfferId == proposalConfirmedDto.OfferId);

            offer.Selected = true;

            proposal.Offers.Add(offer);

            return proposal;
        }

        internal IEnumerable<ProposalDto> GetProposals(string authToken)
        {
            return CacheService.Proposals;
        }

        internal ProposalDto PostOffer(string authToken, OfferDto offer)
        {
            var proposal = CacheService.Proposals.First(p => p.ProposalId == offer.ProposalId);

            if (proposal.Status == Proposed && IsEqualOrBetter(proposal, offer))
            {
                proposal.Status = Accepted;
            }

            proposal.Offers.Add(offer);

            return proposal;
        }

        private bool IsEqualOrBetter(ProposalDto proposal, OfferDto offer)
        {
            return offer.OutboundDate == proposal.OutboundDate
                && offer.InboundDate == proposal.InboundDate
                && offer.Price <= proposal.Price;
        }       
    }
}
