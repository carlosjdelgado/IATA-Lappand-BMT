using System;

namespace Bidmytrip.Core.Api.Dtos
{
    public class ProposalDto
    {
        public string Origin { get; set; }
        public string Destiny { get; set; }
        public DateTime OutboundDate { get; set; }
        public DateTime? InboundDate { get; set; }
        public PassengerDto[] Passengers { get; set; }
        public DateTime TimeToLive { get; set; }

        internal bool IsValid()
        {
            return true;
        }
    }
}
