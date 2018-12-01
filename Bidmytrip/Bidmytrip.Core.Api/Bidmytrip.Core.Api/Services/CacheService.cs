using Bidmytrip.Core.Api.Dtos;
using System.Collections.Generic;

namespace Bidmytrip.Core.Api.Services
{
    public static class CacheService
    {
        public static IList<ProposalDto> Proposals = new List<ProposalDto>();
    }
}
