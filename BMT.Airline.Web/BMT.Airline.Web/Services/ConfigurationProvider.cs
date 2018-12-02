using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace BMT.Airline.Web.Services
{
    public class ConfigurationProvider
    {
        public string AirlineName => ConfigurationManager.AppSettings["AirlineName"];
        public string AirlineLogo => ConfigurationManager.AppSettings["AirlineLogo"];
        public IEnumerable<string> UnnaceptableOrigins => (ConfigurationManager.AppSettings["UnacceptableOrigins"]).Split(',').ToList();
        public IEnumerable<string> UnnaceptableDestinations => (ConfigurationManager.AppSettings["UnnaceptableDestinations"]).Split(',').ToList();
        public decimal MinimumAcceptablePrice => decimal.Parse(ConfigurationManager.AppSettings["MinimumAcceptablePrice"]);
        public string AuthorizationKey => ConfigurationManager.AppSettings["AuthorizationKey"];
    }
}