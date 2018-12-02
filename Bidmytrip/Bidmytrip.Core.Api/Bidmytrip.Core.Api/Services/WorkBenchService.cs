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
        private const string defaultAuthToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IndVTG1ZZnNxZFF1V3RWXy1oeFZ0REpKWk00USIsImtpZCI6IndVTG1ZZnNxZFF1V3RWXy1oeFZ0REpKWk00USJ9.eyJhdWQiOiIwYWFjNTA1MC00OTY5LTRkNTctYjkyZC0xNDUyNWRjOGZiNmYiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC80ZDEzZTBmMy1hOTllLTQwNjItYTEyNS03NzY3N2RiOTYxMzAvIiwiaWF0IjoxNTQzNjk0MTA2LCJuYmYiOjE1NDM2OTQxMDYsImV4cCI6MTU0MzY5ODAwNiwiYWlvIjoiQVhRQWkvOEpBQUFBWnFMT2lJNFNwTTkvNElHUzFleFd0Rk1tdnhkSzRHQkJ3RVdscm5SZE83ZW9XQ2hXUm93TmluTXQwZUNvLytKSjMzeis1c0xWalJxTzF2QWVmZVliRzBKNGdtdzRyQmQ0MWxSeTMvQWVQa0FJcTExZFRBdHJubk1Xa1ZPenhGRDlJYUZWczBBa2hHTzQwNm4xU2svd3BRPT0iLCJhbXIiOlsicnNhIl0sImNfaGFzaCI6ImdOb0JtZ2JuM0RYRFU0LTBvUnZHUmciLCJlbWFpbCI6ImFsdmFyby5tb250ZXJvQGJpcmNobWFuZ3JvdXAuY29tIiwiaWRwIjoiaHR0cHM6Ly9zdHMud2luZG93cy5uZXQvZTA2MzViYjQtMGJkOC00ZTRjLWFiNTEtMjc3YTExNjNlZDllLyIsImlwYWRkciI6IjE5NC4xMTEuMTE5LjQ1IiwibmFtZSI6IkFsdmFybyBNb250ZXJvIiwibm9uY2UiOiI3ODRmMGZkMy1mMTMyLTQ0MTgtYjY4Yy1kMjg2NTRhMGNjZDQiLCJvaWQiOiI3ZDc5NTFjYy02MGQ1LTQwZjgtOTdlOC01NjhiNTRlNzcwNjQiLCJzdWIiOiJtVzlsTDNPb3pIMlhzRVA1NDNVYkd6TU5TOUlJd3F0YXpOR2NwUlNmUHJBIiwidGlkIjoiNGQxM2UwZjMtYTk5ZS00MDYyLWExMjUtNzc2NzdkYjk2MTMwIiwidW5pcXVlX25hbWUiOiJhbHZhcm8ubW9udGVyb0BiaXJjaG1hbmdyb3VwLmNvbSIsInV0aSI6IkRJY3UtYjdmVkV5bE1Mc3Z1N2R0QUEiLCJ2ZXIiOiIxLjAifQ.NGi_AUp2Xmw8Nee7FKGaSnbht0dtf_pOsvALYvkZJXfY9fDyOw_rOS38hfJ5Y5Mw3AtM6uHCCKQ1WyNCOrtTcwi4N7zuEDsmocG9Ekm5R02ZXP1F720TDVPhCotCtTQB4JI6KJ8DuCay3_42tvIbH5XIzwkq7hfnfmkscSDKjKnLFs2-dYHnr3I8Y63x5A7rY-qKw_yeCBAnOOcLexohUC6fb36OouDHkzGoCcFyrOAGmSqgyRIeAUdrwJUJ0KvQsIkfeNCJkKRt97pVFeGsrlisjRtIFo8MRuFn9m-23LifnScSp5xKcxpT7TAdGCOpKAfonV1cFHVidGpPxOOX3g";

        private const string BidMyTripAppName = "BitMytripV1.1";
        private const string ProposalFlowName = "ProposalFlow";

        private GatewayApi _apiGateway;

        public WorkBenchService()
        {
            _apiGateway = new GatewayApi
            {
                SiteUrl = blockChainApiUrl
            };
        }

        internal async Task RecordNewProposal(string authToken, ProposalDto proposal)
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
        }

        internal async Task RecordAcceptance(string authToken, ProposalDto proposal)
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
        }

        internal async Task RecordConfirm(string authToken, ProposalDto proposal)
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
