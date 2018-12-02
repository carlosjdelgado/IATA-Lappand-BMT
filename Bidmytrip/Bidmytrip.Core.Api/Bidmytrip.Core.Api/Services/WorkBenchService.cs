using Bidmytrip.Common.Workbench.Client;
using Bidmytrip.Common.Workbench.Client.Models;
using Bidmytrip.Core.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidmytrip.Core.Api.Services
{
    public class WorkBenchService
    {
        private const string blockChainApiUrl = "https://iagndcblockchain-qqau77-api.azurewebsites.net";
        private const string defaultAuthToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IndVTG1ZZnNxZFF1V3RWXy1oeFZ0REpKWk00USIsImtpZCI6IndVTG1ZZnNxZFF1V3RWXy1oeFZ0REpKWk00USJ9.eyJhdWQiOiIwYWFjNTA1MC00OTY5LTRkNTctYjkyZC0xNDUyNWRjOGZiNmYiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC80ZDEzZTBmMy1hOTllLTQwNjItYTEyNS03NzY3N2RiOTYxMzAvIiwiaWF0IjoxNTQzNzM2NTAwLCJuYmYiOjE1NDM3MzY1MDAsImV4cCI6MTU0Mzc0MDQwMCwiYWlvIjoiNDJSZ1lPaUlDK1JkNzJmRTlqbWxmZTZKMVdiOEY3VWVWbnZYSFN6L3VOZjZ1a2pveGxNQSIsImFtciI6WyJwd2QiXSwiZ2l2ZW5fbmFtZSI6IkZJTk5BSVIiLCJpcGFkZHIiOiIxOTQuMTExLjExOS40NSIsIm5hbWUiOiJGSU5OQUlSIiwibm9uY2UiOiI0YjRiYjMwZS1lNGFhLTQwZjEtODhkNi0zMDE1MGE1MWNiNDIiLCJvaWQiOiIwZmUyY2ZjMi1lYzJiLTRiNjEtOGFmMC1mOTE1YWFjMDczZGMiLCJzdWIiOiJMWV9tMG5fTmo4ZEF1bjM4ckFSX3Jrb3lHRlJNcWNwNzdNQnBxVjR6ZWlNIiwidGlkIjoiNGQxM2UwZjMtYTk5ZS00MDYyLWExMjUtNzc2NzdkYjk2MTMwIiwidW5pcXVlX25hbWUiOiJmaW5uYWlyQGliaXNtYWRyaWQuY29tIiwidXBuIjoiZmlubmFpckBpYmlzbWFkcmlkLmNvbSIsInV0aSI6ImVLbHVIRmVnTlVLZ2tSa0F2V1lDQUEiLCJ2ZXIiOiIxLjAifQ.FBH68lojwLeLsoTlfrsrIyB7qlRh7a_-CH7lNbpUn_aOaJPyT2di-36fp2vAqcsiQe0lfZii3YRgG_PcAWKHzA56aNSQvApZuappYcgjSMRuT0-FGHAlcRSY_8sVmPwbPUulKcoonSjN4Z2NdSzVCG9-tHCKuqYsyOyo7xnpLCOpbJUu65CLxzXRdFOBKhw-3IIxrrPxw1DhFfOq2l-0d95M9O86gWipjqRFshIb9NLrPTvEHH3GFiu4KMSvpkpeOkAzuJqcvG2QpRej1OH3GaeE_eHO3PGhH0iChpubPAlT1i9Xt4TYknaCx_bnaFLX_KXqAW3Aw_pU1eWT_tvJOw";

        private const string BidMyTripAppName = "BidMytripV1.2";
        private const string ProposalFlowName = "ProposalFlow";

        private GatewayApi _apiGateway;

        public WorkBenchService()
        {
            _apiGateway = new GatewayApi
            {
                SiteUrl = blockChainApiUrl
            };
        }

        internal async Task<long> RecordNewProposal(string authToken, ProposalDto proposal)
        {
            InitializeGatewayInstance(authToken);

            var contract = await GetContract();
            var workFlow = await GetWorkFlow();

            var workFlowInfoId = await GetNextWorkflowInfoId(workFlow.Id.ToString());
            var action = BuildNewProposalAction(workFlowInfoId, proposal);

            var result = await _apiGateway.CreateNewContractAsync(action,
                workFlow.Id.ToString(),
                contract.ContractCodeId.ToString(),
                contract.LedgerId.ToString());

            return workFlowInfoId;
        }

        internal async Task RecordAcceptance(string authToken, ProposalDto proposal)
        {
            InitializeGatewayInstance(authToken);

            var contract = await GetContract();
            var workFlow = await GetWorkFlow();

            //var workFlowInfoId = await GetNextWorkflowInfoId(workFlow.Id.ToString());
            //var action = BuildAcceptanceAction(workFlowInfoId, proposal);
            var action = BuildAcceptanceAction(proposal.WorkFlowInfoId, proposal);

            var result = await _apiGateway.PostWorkflowActionAsync(action,
                contract.ContractCodeId.ToString());
        }

        internal async Task RecordConfirm(string authToken, ProposalDto proposal)
        {
            InitializeGatewayInstance(authToken);

            var contract = await GetContract();
            var workFlow = await GetWorkFlow();

            var workFlowInfoId = await GetNextWorkflowInfoId(workFlow.Id.ToString());
            var action = BuildConfirmationAction(workFlowInfoId, proposal);

            var result = await _apiGateway.PostWorkflowActionAsync(action,
                contract.ContractCodeId.ToString());
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

        private static ActionInformation BuildAcceptanceAction(long workFlowInfoId, ProposalDto proposal)
        {
            return new ActionInformation()
            {
                WorkflowFunctionId = workFlowInfoId,
                WorkflowActionParameters = new List<WorkflowActionParameter>()
                {
                    new WorkflowActionParameter()
                    {
                        Name = "offerCode",
                        Value = proposal.Offers.First().OfferCode
                    }
                }
            };
        }

        private static ActionInformation BuildConfirmationAction(long workFlowInfoId, ProposalDto proposal)
        {
            return new ActionInformation()
            {
                WorkflowFunctionId = workFlowInfoId,
                WorkflowActionParameters = new List<WorkflowActionParameter>()
                {
                }
            };
        }

        private void InitializeGatewayInstance(string authToken)
        {
            if (string.IsNullOrEmpty(authToken))
            {
                authToken = defaultAuthToken;
            }

            _apiGateway.SetAuthToken(authToken);
        }

        private async Task<long> GetNextWorkflowInfoId(string workFlowInstanceId)
        {
            var workFlowInstances = await _apiGateway.GetWorkflowInstancesAsync(workFlowInstanceId).ConfigureAwait(false);

            return workFlowInstances.OrderBy(w => w.Id).Last().Id + 1;
        }

        private async Task<Workflow> GetWorkFlow()
        {
            var applications = await _apiGateway.GetApplicationsAsync();
            var applicationId = applications.First(app => app.Name == BidMyTripAppName).Id.ToString();

            var workflows = await _apiGateway.GetWorkflowsByApplicationIdAsync(applicationId).ConfigureAwait(false);

            return workflows.Single(app => app.Name == ProposalFlowName);
        }

        private async Task<ContractCodes> GetContract()
        {
            var applications = await _apiGateway.GetApplicationsAsync();
            var applicationId = applications.First(app => app.Name == BidMyTripAppName).Id.ToString();

            var contracts = await _apiGateway.GetContractCodesByApplicationAsync(applicationId).ConfigureAwait(false);

            return contracts.First();
        }
    }
}
