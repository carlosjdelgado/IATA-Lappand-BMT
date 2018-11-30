using System;
using System.Threading.Tasks;
using Bidmytrip.Common.Workbench.Client;
using Bidmytrip.Core.Api.Dtos;
using Microsoft.Azure.WebJobs.Host;

namespace Bidmytrip.Core.Api.Services
{
    public class BlockChainService
    {
        private readonly TraceWriter _log;

        public BlockChainService(TraceWriter log)
        {
            _log = log;
        }

        internal Task PostProposal(ProposalDto proposal)
        {
            _log.Info("Post proposal");

            //GatewayApi.Instance.

            return Task.CompletedTask;
        }
    }
}
