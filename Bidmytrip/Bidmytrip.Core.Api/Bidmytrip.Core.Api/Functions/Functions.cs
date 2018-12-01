using Bidmytrip.Core.Api.Dtos;
using Bidmytrip.Core.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bidmytrip.Core.Api
{
    public static class Functions
    {
        private static string AuthTokenHeader = "X-Authorization";

        [FunctionName("PostProposals")]
        public static async Task<IActionResult> PostProposals(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "proposals/Bulk")] HttpRequest req, TraceWriter log)
        {
            try
            {
                var content = await req.ReadAsStringAsync();
                var deserializedContent = JsonConvert.DeserializeObject<ProposalList>(content);

                if (deserializedContent.Proposals.Any(p => !p.IsValid()))
                {
                    return new BadRequestResult();
                }

                var service = new BlockChainService(log);

                service.PostProposals(deserializedContent.Proposals);

                return new OkResult();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);

                throw;
            }
        }

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

                var authToken = req.Headers.ContainsKey(AuthTokenHeader) ? req.Headers[AuthTokenHeader][0] : string.Empty;

                var service = new BlockChainService(log);

                var newProposal = service.PostProposal(authToken, proposal);

                return (ActionResult)new OkObjectResult(newProposal);
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
                var authToken = req.Headers.ContainsKey(AuthTokenHeader) ? req.Headers[AuthTokenHeader][0] : string.Empty;

                var service = new BlockChainService(log);

                var proposal = service.GetProposals(authToken);

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
                var authToken = req.Headers.ContainsKey(AuthTokenHeader) ? req.Headers[AuthTokenHeader][0] : string.Empty;

                var service = new BlockChainService(log);

                var modifiedProposal = service.PostOffer(authToken, offer);

                return new OkObjectResult(modifiedProposal);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);

                throw;
            }
        }
    }
}
