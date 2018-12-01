using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BMT.Customer.Web.Models;
using BMT.Customer.Web.ServiceContracts;
using Newtonsoft.Json;
using System.Text;

namespace BMT.Customer.Web.Services
{
    public class ProposalService : IProposalService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<HttpResponseMessage> SendProposal(ProposalModel proposalModel)
        {
            var proposalModelserialized = JsonConvert.SerializeObject(proposalModel);

            var buffer = Encoding.UTF8.GetBytes(proposalModelserialized);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return client.PostAsync("https://bidmytripcoreapiv1.azurewebsites.net/api/Proposals/", byteContent).Result;
        }
    }
}