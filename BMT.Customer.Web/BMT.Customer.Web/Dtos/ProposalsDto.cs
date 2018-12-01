using System.Collections.Generic;

namespace BMT.Customer.Web.Dtos
{
    public class ProposalsDto
    {
        public IEnumerable<ProposalDto> Proposals { get; set; } = new List<ProposalDto>();
    }
}