using BMT.Customer.Web.Models;
using BMT.Customer.Web.ServiceContracts;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BMT.Customer.Web.Services
{
    public class OfferService : IOfferService
    {
        private ConfigurationProvider _configuration = new ConfigurationProvider();
        private readonly IProposalService _proposalService = new ProposalService();
        private static readonly HttpClient _client = new HttpClient();

        private const string BidMyTripProposalUrl = "https://bidmytripcoreapiv1.azurewebsites.net/api/Proposals/";

        public async Task<ProposalModel> GetOffers(string proposalId)
        {
            var proposalsModel = await _proposalService.GetProposals().ConfigureAwait(false);

            return proposalsModel.Where(p => p.ProposalId == proposalId).FirstOrDefault();
        }
    }
}