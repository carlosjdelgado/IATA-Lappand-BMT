﻿using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BMT.Customer.Web.Models;
using BMT.Customer.Web.ServiceContracts;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using System;

namespace BMT.Customer.Web.Services
{
    public class ProposalService : IProposalService
    {
        private const string BidMyTripProposalUrl = "https://bidmytripcoreapiv1.azurewebsites.net/api/Proposals/";

        private ConfigurationProvider _configuration = new ConfigurationProvider();
        private static readonly HttpClient _client = new HttpClient();

        public async Task<IEnumerable<ProposalModel>> GetProposals()
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Headers.Add("X-Authorization", _configuration.AuthorizationKey);
            httpRequestMessage.RequestUri = new Uri(BidMyTripProposalUrl);
            httpRequestMessage.Method = HttpMethod.Get;

            var httpResponse = await _client.SendAsync(httpRequestMessage).ConfigureAwait(false);
            var responseAsJson = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IEnumerable<ProposalModel>>(responseAsJson);
        }

        public async Task<HttpResponseMessage> SendProposal(ProposalModel proposalModel)
        {
            var proposalModelserialized = JsonConvert.SerializeObject(proposalModel);

            var buffer = Encoding.UTF8.GetBytes(proposalModelserialized);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.Add("X-Authorization", _configuration.AuthorizationKey);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await _client.PostAsync(BidMyTripProposalUrl, byteContent).ConfigureAwait(false);
        }
    }
}