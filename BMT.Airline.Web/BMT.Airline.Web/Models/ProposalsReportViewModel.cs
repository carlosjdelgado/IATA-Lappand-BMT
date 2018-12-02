using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMT.Airline.Web.Models
{
    public class ProposalsReportViewModel
    {
        public string AirlineName { get; set; }
        public string AirlineLogo { get; set; }
        public IEnumerable<ProposalViewModel> Proposals { get; set; }
    }
}