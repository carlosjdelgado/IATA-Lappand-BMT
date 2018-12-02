using BMT.Airline.Web.Mappers;
using BMT.Airline.Web.Models;
using BMT.Airline.Web.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BMT.Airline.Web.Services
{
    public class ProposalsService
    {
        private ConfigurationProvider _configuration;
        private BMTCoreApiService _bmtCoreApiService;

        public ProposalsService()
        {
            _configuration = new ConfigurationProvider();
            _bmtCoreApiService = new BMTCoreApiService();
        }

        public async Task<ProposalsReportViewModel> GetProposalsReport()
        {
            var proposals = await _bmtCoreApiService.GetProposals().ConfigureAwait(false);

            return new ProposalsReportViewModel
            {
                AirlineName = _configuration.AirlineName,
                AirlineLogo = _configuration.AirlineLogo,
                Proposals = BuildProposals(proposals)
            };
        }

        private IEnumerable<ProposalViewModel> BuildProposals(IEnumerable<ProposalDto> proposals)
        {
            var filteredProposals = proposals
                .Where(p => !_configuration.UnnaceptableOrigins.Contains(p.Origin))
                .Where(p => !_configuration.UnnaceptableDestinations.Contains(p.Destiny));

            return filteredProposals.Select(p => ProposalViewModelMapper.Map(p, _configuration));
        }    

        public async Task<ProposalViewModel> GetProposal(string proposalId)
        {
            var proposals = await _bmtCoreApiService.GetProposals().ConfigureAwait(false);
            var proposal = proposals.FirstOrDefault(p => p.ProposalId == proposalId);

            if (proposal == null)
                return null;

            return ProposalViewModelMapper.Map(proposal, _configuration);
        }        
    }
}