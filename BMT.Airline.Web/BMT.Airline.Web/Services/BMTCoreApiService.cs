using BMT.Airline.Web.Models.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace BMT.Airline.Web.Services
{
    public class BMTCoreApiService
    {
        private const string BmtCoreApiUrl = "https://bidmytripcoreapiv1.azurewebsites.net/api/Proposals/";

        private HttpClient _httpClient;

        public BMTCoreApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<ProposalDto>> GetProposals()
        {
            var httpResponse = await _httpClient.GetAsync(BmtCoreApiUrl).ConfigureAwait(false);
            var responseAsJson = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IEnumerable<ProposalDto>>(responseAsJson);
        } 
    }
}