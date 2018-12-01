using BMT.Airline.Web.Models.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BMT.Airline.Web.Services
{
    public class BMTCoreApiService
    {
        private const string BmtGetProposalsUrl = "https://bidmytripcoreapiv1.azurewebsites.net/api/Proposals";
        private const string BmtPostOffersApiUrl = "https://bidmytripcoreapiv1.azurewebsites.net/api/Proposals/Offers";

        private HttpClient _httpClient;
        private ConfigurationProvider _configurationProvider;

        public BMTCoreApiService()
        {
            _httpClient = new HttpClient();
            _configurationProvider = new ConfigurationProvider();
        }

        public async Task<IEnumerable<ProposalDto>> GetProposals()
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Headers.Add("X-Authorization", _configurationProvider.AuthorizationKey);
            httpRequestMessage.RequestUri = new Uri(BmtGetProposalsUrl);
            httpRequestMessage.Method = HttpMethod.Get;

            var httpResponse = await _httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
            var responseAsJson = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IEnumerable<ProposalDto>>(responseAsJson);
        } 

        public async Task PostOffer(OfferDto offer)
        {
            var offerSerialized = JsonConvert.SerializeObject(offer);

            var buffer = Encoding.UTF8.GetBytes(offerSerialized);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            byteContent.Headers.Add("X-Authorization", _configurationProvider.AuthorizationKey);

            await _httpClient.PostAsync(BmtPostOffersApiUrl, byteContent);
        }
    }
}