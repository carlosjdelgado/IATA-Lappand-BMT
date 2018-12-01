using System.Configuration;

namespace BMT.Customer.Web.Services
{
    public class ConfigurationProvider
    {
        public string AirlineName => ConfigurationManager.AppSettings["AirlineName"];
        public string AuthorizationKey => ConfigurationManager.AppSettings["AuthorizationKey"];
    }
}