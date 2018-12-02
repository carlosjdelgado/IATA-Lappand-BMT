using Bidmytrip.Core.Api.Dtos;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidmytrip.Core.Api.Services
{
    public class BidMyTripService
    {
        private const bool useWorkBench = false;

        private const string Proposed = "PROPOSED";
        private const string Accepted = "ACCEPTED";
        private const string Confirmed = "CONFIRMED";

        private readonly TraceWriter _log;
        private readonly WorkBenchService _workBenchService;

        public BidMyTripService(TraceWriter log, WorkBenchService workBenchService)
        {
            _workBenchService = workBenchService;
        }

        internal async Task PostProposals(IEnumerable<ProposalDto> proposals)
        {
            var proposalList = proposals.ToList();

            var fullDb = await CacheService.GetFullDb();

            foreach (var p in proposalList)
            {
                p.Status = Proposed;
            }            
            foreach (var p in proposalList)
            {
                fullDb.Add(p);
            }

            await CacheService.UpdataDb(fullDb);
        }

        internal async Task<ProposalDto> PostProposal(string authToken, ProposalDto proposal)
        {
            proposal.CreationDate = DateTime.UtcNow;
            proposal.Status = Proposed;

            var fullDb = await CacheService.GetFullDb();

            fullDb.Add(proposal);

            await CacheService.UpdataDb(fullDb);

            if (useWorkBench)
            {
                proposal.WorkFlowInfoId = await _workBenchService.RecordNewProposal(authToken, proposal);
            }

            await CacheService.UpdataDb(fullDb);

            return proposal;
        }

        internal async Task<ProposalDto> ConfirmProposal(string authToken, ProposalConfirmedDto proposalConfirmedDto)
        {
            var fullDb = await CacheService.GetFullDb();

            var proposal = fullDb.First(p => p.ProposalId == proposalConfirmedDto.ProposalId);
            proposal.Status = Confirmed;
            var offer = proposal.Offers.First(o => o.OfferId == proposalConfirmedDto.OfferId);
            offer.Selected = true;

            await CacheService.UpdataDb(fullDb);

            if (useWorkBench)
            {
                await _workBenchService.RecordConfirm(authToken, proposal);
            }

            return proposal;
        }

        internal async Task<IEnumerable<ProposalDto>> GetProposals(string authToken)
        {
            return await CacheService.GetFullDb();
        }

        internal async Task<ProposalDto> PostOffer(string authToken, OfferDto offer)
        {
            var hasBeenAccepted = false;

            var fullDb = await CacheService.GetFullDb();

            var proposal = fullDb.First(p => p.ProposalId == offer.ProposalId);
            if (proposal.Status == Proposed && IsEqualOrBetter(proposal, offer))
            {
                proposal.Status = Accepted;
                hasBeenAccepted = true;
            }

            offer.Selected = false;
            offer.CreationTime = DateTime.UtcNow;

            proposal.Offers.Add(offer);

            await CacheService.UpdataDb(fullDb);  
            
            if (hasBeenAccepted && useWorkBench)
            {
                await _workBenchService.RecordAcceptance(authToken, proposal);
            }

            return proposal;
        }

        internal async Task CleanAllProposals(string authToken)
        {
            await CacheService.UpdataDb(new List<ProposalDto>());
        }

        private bool IsEqualOrBetter(ProposalDto proposal, OfferDto offer)
        {
            return offer.OutboundDate == proposal.OutboundDate
                && offer.InboundDate == proposal.InboundDate
                && offer.Price <= proposal.Price;
        }        
    }
}
