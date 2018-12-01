using System.Collections.Generic;

namespace Bidmytrip.Core.Api.Dtos
{
    public class ProposalList
    {
        public IEnumerable<ProposalDto> Proposals { get; set; }
    }
}
