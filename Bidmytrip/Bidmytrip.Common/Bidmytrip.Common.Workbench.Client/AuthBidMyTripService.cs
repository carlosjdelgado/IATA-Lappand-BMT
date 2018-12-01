using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;

namespace Bidmytrip.Common.Workbench.Client
{
    public class AuthBidMyTripService
    {
        public static async Task<string> Auth()
        {
            string tenantId = "4d13e0f3-a99e-4062-a125-77677db96130";
            string clientApplicationId = "795c7380-5dcd-42ca-9751-9f30665c1312";
            string clientSecret = "%{=^@[+&{:]==*B^h.?=!)4+@#W|.+:-#?}]h&+|!-?>=&=*&/=d:C@?:";
            string workBenchApiAppId = "c337ecea-efb3-404e-bc38-ff679788d24e";

            AuthenticationResult result = null;
            AuthenticationContext authenticationContext = new AuthenticationContext("https://login.microsoftonline.com/" + tenantId);

            var clientCredential = new ClientCredential(clientApplicationId, clientSecret);

            result = await authenticationContext.AcquireTokenAsync(workBenchApiAppId, clientCredential).ConfigureAwait(false);

            return result.AccessToken; // Use this token to make API calls to workbench


             /* 
            // Sample API Call
            HttpClient client = new HttpClient();
            HttpResponseMessage response = null;
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                response = await client.GetAsync("{workbench-api-url}/api/v1/users");
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}", e);
            }

            Console.WriteLine("{0}", response.Content);
            */
        }

    }
}