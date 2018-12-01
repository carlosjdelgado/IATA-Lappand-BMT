using BMT.Customer.Web.Dtos;
using BMT.Customer.Web.Models;
using BMT.Customer.Web.ServiceContracts;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Customer.Web.Services
{
    public class OfferService : IOfferService
    {
        private const string BidMyTripConfirmProposalUrl = "https://bidmytripcoreapiv1.azurewebsites.net/api/Proposals/Confirm";

        private ConfigurationProvider _configuration = new ConfigurationProvider();
        private readonly IProposalService _proposalService = new ProposalService();
        private static readonly HttpClient _client = new HttpClient();

        private const string BidMyTripProposalUrl = "https://bidmytripcoreapiv1.azurewebsites.net/api/Proposals/";

        public async Task<ProposalModel> GetOffers(string proposalId)
        {
            var proposalsModel = await _proposalService.GetProposals().ConfigureAwait(false);

            return proposalsModel.Where(p => p.ProposalId == proposalId).FirstOrDefault();
        }

        public async Task ConfirmOffer(ConfirmOfferDto confirmOfferDto)
        {
            var proposalModelserialized = JsonConvert.SerializeObject(confirmOfferDto);

            var buffer = Encoding.UTF8.GetBytes(proposalModelserialized);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync(BidMyTripProposalUrl, byteContent).ConfigureAwait(false);
        }
    }
}