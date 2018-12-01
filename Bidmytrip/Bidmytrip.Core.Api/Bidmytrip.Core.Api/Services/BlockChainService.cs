using Bidmytrip.Common.Workbench.Client;
using Bidmytrip.Common.Workbench.Client.Models;
using Bidmytrip.Core.Api.Dtos;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidmytrip.Core.Api.Services
{
    public class BlockChainService
    {
        private const string blockChainApiUrl = "https://iagndcblockchain-qqau77-api.azurewebsites.net";
        private const string defaultAuthToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IndVTG1ZZnNxZFF1V3RWXy1oeFZ0REpKWk00USIsImtpZCI6IndVTG1ZZnNxZFF1V3RWXy1oeFZ0REpKWk00USJ9.eyJhdWQiOiIwYWFjNTA1MC00OTY5LTRkNTctYjkyZC0xNDUyNWRjOGZiNmYiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC80ZDEzZTBmMy1hOTllLTQwNjItYTEyNS03NzY3N2RiOTYxMzAvIiwiaWF0IjoxNTQzNjg5NDYwLCJuYmYiOjE1NDM2ODk0NjAsImV4cCI6MTU0MzY5MzM2MCwiYWlvIjoiQVhRQWkvOEpBQUFBTnBDcTNFVmd3QVVpZVVlYytRT3NqMm5jRE82YTZPMmYrS1lGM0RQVElaZ0VqU1hNRE1SUDVHWmNmYXZhOTNtTURzaGM3TE9ta2VydkkwSFBmakdmMzBoenFJYkEvNExkeTF6Y3VKR294YzlReVBxb0d1cmFHU3dkM0tkdjVDMjc3dVBkVWRmZWN4TlVla0pvWnFFRGp3PT0iLCJhbXIiOlsicnNhIl0sImVtYWlsIjoiYWx2YXJvLm1vbnRlcm9AYmlyY2htYW5ncm91cC5jb20iLCJpZHAiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9lMDYzNWJiNC0wYmQ4LTRlNGMtYWI1MS0yNzdhMTE2M2VkOWUvIiwiaXBhZGRyIjoiMTk0LjExMS4xMTkuNDUiLCJuYW1lIjoiQWx2YXJvIE1vbnRlcm8iLCJub25jZSI6IjQ2ZTRlMjEzLWJmMzYtNGUzNy1iODI2LWZhNDNlZWNiMzQ5ZSIsIm9pZCI6IjdkNzk1MWNjLTYwZDUtNDBmOC05N2U4LTU2OGI1NGU3NzA2NCIsInN1YiI6Im1XOWxMM09vekgyWHNFUDU0M1ViR3pNTlM5SUl3cXRhek5HY3BSU2ZQckEiLCJ0aWQiOiI0ZDEzZTBmMy1hOTllLTQwNjItYTEyNS03NzY3N2RiOTYxMzAiLCJ1bmlxdWVfbmFtZSI6ImFsdmFyby5tb250ZXJvQGJpcmNobWFuZ3JvdXAuY29tIiwidXRpIjoiWUoyM0RxajZuVUtSMHY0N1lNeGtBQSIsInZlciI6IjEuMCJ9.DFVuQX9XFMZ-yC6AYGxmVNP6w1gRCtq_caKCqYiT_0qoyxUaR7RgxoMVgYbBQ-I6qwD-kGPBnAJ3Igac_LzdI4sbsOpGdqPg8XJOTE9RIVoYzGIXT7R0GqDA-BsDnK5H-0FdJVlBPNZ3QyZuCUvOw1jeoFq_hOxlP8zXoacbIJapAk37v-my_ZVbynnYshj64nx48LkKkLZSvZnL7JfAf7m2-4_at39TxGkmwIPwdsBefphpo3-RGKNUd9CcM0Z7Y1o1WqFCI63-zlNYeMt2UaUl5TtYzDSFPug3GBwcebUrGAShJZd5HKWFHGT8nCxdL70z7EM3n8REVEaZDVDlXg";
        
        private const string BidMyTripAppName = "BitMytripV1.1";
        private const string ProposalFlowName = "ProposalFlow";

        private const string Proposed = "PROPOSED";
        private const string Accepted = "ACCEPTED";

        private readonly TraceWriter _log;

        public BlockChainService(TraceWriter log)
        {
            _log = log;
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

            await RecordNewProposal(authToken, proposal);

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

        
        // RELEATED TO BLOCK CHAIN METHODS

        private async Task RecordNewProposal(string authToken, ProposalDto proposal)
        {
            InitializeGatewayInstance(authToken);

            var contract = await GetContract();
            var workFlow = await GetWorkFlow();

            var workFlowInfoId = await GetNextWorkflowInfoId(workFlow.Id.ToString());

            ActionInformation action = BuildNewProposalAction(workFlowInfoId, proposal);

            var result = await GatewayApi.Instance.CreateNewContractAsync(action,
                workFlow.Id.ToString(),
                contract.ContractCodeId.ToString(),
                contract.LedgerId.ToString());
        }

        private static ActionInformation BuildNewProposalAction(long workFlowInfoId, ProposalDto proposal)
        {
            return new ActionInformation()
            {
                WorkflowFunctionId = workFlowInfoId,
                WorkflowActionParameters = new List<WorkflowActionParameter>()
                {
                    new WorkflowActionParameter()
                    {
                        Name = "departureCity",
                        Value = proposal.Origin
                    },
                    new WorkflowActionParameter()
                    {
                        Name = "arrivalCity",
                        Value = proposal.Destiny
                    },
                    new WorkflowActionParameter()
                    {
                        Name = "departureDate",
                        Value = proposal.OutboundDate.ToString("MM/dd/yyyy")
                    },
                    new WorkflowActionParameter()
                    {
                        Name = "arrivalDate",
                        Value = proposal.InboundDate.HasValue ? string.Empty : proposal.InboundDate.Value.ToString("MM/dd/yyyy")
                    },
                    new WorkflowActionParameter()
                    {
                        Name = "price",
                        Value = proposal.Price.ToString()
                    },
                    new WorkflowActionParameter()
                    {
                        Name = "timeToLive",
                        Value = ((int)Math.Ceiling((proposal.TimeToLive - DateTime.UtcNow).TotalHours)).ToString()
                    }
                }
            };
        }

        private void InitializeGatewayInstance(string authToken)
        {
            if (string.IsNullOrEmpty(authToken))
            {
                authToken = defaultAuthToken;
            }

            GatewayApi.Instance.SetAuthToken(authToken);
            GatewayApi.SiteUrl = blockChainApiUrl;
        }

        private async Task<long> GetNextWorkflowInfoId(string workFlowInstanceId)
        {
            var workFlowInstances = await GatewayApi.Instance.GetWorkflowInstancesAsync(workFlowInstanceId).ConfigureAwait(false);

            return workFlowInstances.OrderBy(w => w.Id).Last().Id + 1;
        }

        private async Task<Workflow> GetWorkFlow()
        {
            var applications = await GatewayApi.Instance.GetApplicationsAsync();
            var applicationId = applications.Single(app => app.Name == BidMyTripAppName).Id.ToString();

            var workflows = await GatewayApi.Instance.GetWorkflowsByApplicationIdAsync(applicationId).ConfigureAwait(false);

            return workflows.Single(app => app.Name == ProposalFlowName);
        }

        private async Task<ContractCodes> GetContract()
        {
            var applications = await GatewayApi.Instance.GetApplicationsAsync();
            var applicationId = applications.Single(app => app.Name == BidMyTripAppName).Id.ToString();

            var contracts = await GatewayApi.Instance.GetContractCodesByApplicationAsync(applicationId).ConfigureAwait(false);

            return contracts.First();
        }
    }
}
