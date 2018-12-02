using System.Collections.Generic;

namespace Bidmytrip.Core.Api.Dtos
{
    public class ProposalListDto
    {
        public IEnumerable<ProposalDto> Proposals { get; set; }
    }
}
