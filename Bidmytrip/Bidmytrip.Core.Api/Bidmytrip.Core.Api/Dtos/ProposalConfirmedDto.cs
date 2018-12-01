using System;

namespace Bidmytrip.Core.Api.Dtos
{
    public class ProposalConfirmedDto
    {
        public string ProposalId { get; set; }
        public string OfferId { get; set; }

        internal bool IsValid()
        {
            return
                !string.IsNullOrEmpty(ProposalId)
                && !string.IsNullOrEmpty(OfferId);
        }
    }
}
