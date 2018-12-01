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
    public static class Functions
    {
        [FunctionName("PostProposal")]
        public static IActionResult PostProposal(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "proposals")] ProposalDto proposal,
            HttpRequest req, TraceWriter log)
        {
            try
            {
                if (!proposal.IsValid())
                {
                    return new BadRequestResult();
                }

                var service = new BlockChainService(log);

                service.PostProposal(proposal);

                return (ActionResult)new OkResult();
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);

                throw;
            }            
        }

        [FunctionName("GetProposals")]
        public static IActionResult GetProposals(
             [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "proposals")] HttpRequest req, 
             TraceWriter log)
        {
            try
            {
                var service = new BlockChainService(log);

                var proposal = service.GetProposals();

                return (ActionResult)new OkObjectResult(proposal);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);

                throw;
            }
        }

        [FunctionName("PostOffer")]
        public static IActionResult PostOffer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "proposals/offers")] OfferDto offer,
            HttpRequest req, TraceWriter log)
        {
            try
            {
                var service = new BlockChainService(log);

                service.PostOffer(offer);

                return (ActionResult)new OkResult();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);

                throw;
            }
        }
    }
}
