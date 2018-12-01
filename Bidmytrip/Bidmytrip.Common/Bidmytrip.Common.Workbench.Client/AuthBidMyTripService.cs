using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;

namespace Bidmytrip.Common.Workbench.Client
{
    public class AuthBidMyTripService
    {
        public static async Task<string> Auth()
        {
            var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IndVTG1ZZnNxZFF1V3RWXy1oeFZ0REpKWk00USIsImtpZCI6IndVTG1ZZnNxZFF1V3RWXy1oeFZ0REpKWk00USJ9.eyJhdWQiOiIwYWFjNTA1MC00OTY5LTRkNTctYjkyZC0xNDUyNWRjOGZiNmYiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC80ZDEzZTBmMy1hOTllLTQwNjItYTEyNS03NzY3N2RiOTYxMzAvIiwiaWF0IjoxNTQzNjc3NjQ5LCJuYmYiOjE1NDM2Nzc2NDksImV4cCI6MTU0MzY4MTU0OSwiYWlvIjoiQVVRQXUvOEpBQUFBenl2NW1kTWxlVDVEUHc1VysxbEtkTk9CSG4yL0RvVHdhYzRaZENud0NzWVBpOTJkUkQ5ZmtIUjdSRlIxeS9lTG41VGVTSVhOcEJrQXE4TkR3cTRzb2c9PSIsImFtciI6WyJwd2QiXSwiZW1haWwiOiJndWlsbGVybW8uZmxlbWluZ0BiaXJjaG1hbmdyb3VwLmNvbSIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0L2UwNjM1YmI0LTBiZDgtNGU0Yy1hYjUxLTI3N2ExMTYzZWQ5ZS8iLCJpcGFkZHIiOiIxOTQuMTExLjExOS40NSIsIm5hbWUiOiJHdWlsbGVybW8gRmxlbWluZyIsIm5vbmNlIjoiNDI3MWM4ZTktYjY0NC00NTNjLThlNDAtOTYxNDE5OGE5MjNkIiwib2lkIjoiMmZjOTY2NTItODFmOC00ZjE1LTg1YWUtYTg2YTVhZTMwZGViIiwicm9sZXMiOlsiQWRtaW5pc3RyYXRvciJdLCJzdWIiOiJhNUVucW9ua0F3YUhOaEZ5SE9STnZQNVJldlliZWdWVmxkbi1GNkxKbTdJIiwidGlkIjoiNGQxM2UwZjMtYTk5ZS00MDYyLWExMjUtNzc2NzdkYjk2MTMwIiwidW5pcXVlX25hbWUiOiJndWlsbGVybW8uZmxlbWluZ0BiaXJjaG1hbmdyb3VwLmNvbSIsInV0aSI6IkFFNFFVemNPclVDb3BZSFlDQVBCQVEiLCJ2ZXIiOiIxLjAifQ.Zjp4WdGyxbeqqaGmRjljoOZ9vMse3UhUoPBv0HvgqjxocQ-R7NK_lWnhVtjcSNNoDdV35Fy9boda53xG6Fux44Uddrd_HdYPJ7AleZcJMS6Z9QuJGDFJhLwSGdJfqYm3Pst9NR3dcXTqHB7EFzgG8-zRZIPHGrsimVdnOY4qtm3YVULvrfxHflLObtKMKOk_4Rv08pyyiepHF2RF3Wi2W_umv2I21ekvuh6cW4_v_56mOHWD_jnUHnMyP2B4zjZMxfiBzT8IdziiIIhGShqM1et2cT0hMRzIRtls3K1FLIYEk8Un23k02h9nwr3MWwfHeZE1cs8LSsXhAAwcLyB3wA";
            return token;
            /*
            // Blockchain API: 7816681b-95f7-459d-9182-7ab496a17dfa

            string tenantId = "4d13e0f3-a99e-4062-a125-77677db96130";
            string clientApplicationId = "795c7380-5dcd-42ca-9751-9f30665c1312";
            string clientSecret = "%{=^@[+&{:]==*B^h.?=!)4+@#W|.+:-#?}]h&+|!-?>=&=*&/=d:C@?:";
            string workBenchApiAppId = "c337ecea-efb3-404e-bc38-ff679788d24e";

            AuthenticationResult result = null;
            AuthenticationContext authenticationContext = new AuthenticationContext("https://login.microsoftonline.com/" + tenantId);

            var clientCredential = new ClientCredential(clientApplicationId, clientSecret);

            result = await authenticationContext.AcquireTokenAsync(workBenchApiAppId, clientCredential).ConfigureAwait(false);            

            return result.AccessToken; // Use this token to make API calls to workbench

            */


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