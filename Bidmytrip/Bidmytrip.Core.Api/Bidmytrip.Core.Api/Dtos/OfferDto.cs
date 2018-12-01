﻿using System;

namespace Bidmytrip.Core.Api.Dtos
{
    public class OfferDto
    {
        public OfferDto()
        {
            CreationTime = DateTime.UtcNow;
        }

        public string AirlineName { get; set; }

        public string OfferId { get; set; }
        public string ProposalId { get; set; }        

        public DateTime OutboundDate { get; set; }
        public DateTime? InboundDate { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationTime { get; }
    }
}