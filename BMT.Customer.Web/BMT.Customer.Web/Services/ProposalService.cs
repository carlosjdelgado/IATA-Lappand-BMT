using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BMT.Customer.Web.Models;
using BMT.Customer.Web.ServiceContracts;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;

namespace BMT.Customer.Web.Services
{
    public class ProposalService : IProposalService
    {
        private const string BidMyTripProposalUrl = "https://bidmytripcoreapiv1.azurewebsites.net/api/Proposals/";

        private static readonly HttpClient client = new HttpClient();

        public async Task<IEnumerable<ProposalModel>> GetProposal()
        {
            var response = await client.GetAsync(BidMyTripProposalUrl).ConfigureAwait(false);

            var responseAsJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IEnumerable<ProposalModel>>(responseAsJson);
        }

        public async Task<HttpResponseMessage> SendProposal(ProposalModel proposalModel)
        {
            var proposalModelserialized = JsonConvert.SerializeObject(proposalModel);

            var buffer = Encoding.UTF8.GetBytes(proposalModelserialized);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await client.PostAsync(BidMyTripProposalUrl, byteContent);
        }
    }
}