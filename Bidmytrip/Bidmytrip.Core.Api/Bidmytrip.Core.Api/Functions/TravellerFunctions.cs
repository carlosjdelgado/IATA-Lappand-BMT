using Bidmytrip.Core.Api.Dtos;
using Bidmytrip.Core.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Threading.Tasks;

namespace Bidmytrip.Core.Api
{
    public static class TravellerFunctions
    {
        [FunctionName("PostProposal")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "Proposals")] ProposalDto proposal,
            HttpRequest req, TraceWriter log)
        {
            try
            {
                if (!proposal.IsValid())
                {
                    return new BadRequestObjectResult("Please pass a name on the query string or in the request body");
                }

                var service = new BlockChainService(log);

                await service.PostProposal(proposal);

                return (ActionResult)new OkResult();
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);

                throw;
            }            
        }
    }
}
