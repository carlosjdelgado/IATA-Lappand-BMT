using BMT.Airline.Web.Models;
using BMT.Airline.Web.Models.DTOs;
using BMT.Airline.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMT.Airline.Web.Mappers
{
    public static class ProposalViewModelMapper
    {
        private const string AdultType = "ADT";
        private const string ChildType = "CHD";
        private const string InfantType = "INF";
        private const string Adult = "Adult";
        private const string Child = "Child";
        private const string Infant = "Infant";


        public static ProposalViewModel Map(ProposalDto proposalDto, ConfigurationProvider configuration)
        {
            return new ProposalViewModel
            {
                ProposalId = proposalDto.ProposalId,
                ArrivalCity = proposalDto.Destiny,
                DepartureCity = proposalDto.Origin,
                DepartureDate = proposalDto.OutboundDate,
                ReturnDate = proposalDto.InboundDate,
                ExpirationDate = proposalDto.TimeToLive,
                Price = proposalDto.Price,                
                IsAcceptable = BuildIsAcceptable(proposalDto, configuration),
                Passengers = BuildPassengers(proposalDto),
                Offers = proposalDto.Offers.Select(OfferViewModelMapper.Map)
            };
        }

        private static IEnumerable<PassengerViewModel> BuildPassengers(ProposalDto proposalDto)
        {
            var passengers = new List<PassengerViewModel>();

            if (proposalDto.Passenger1 != null)
                passengers.Add(BuildPassenger(proposalDto.Passenger1));

            if (proposalDto.Passenger2 != null)
                passengers.Add(BuildPassenger(proposalDto.Passenger2));

            if (proposalDto.Passenger3 != null)
                passengers.Add(BuildPassenger(proposalDto.Passenger3));

            if (proposalDto.Passenger4 != null)
                passengers.Add(BuildPassenger(proposalDto.Passenger4));

            return passengers;
        }

        private static PassengerViewModel BuildPassenger(PassengerDto passenger)
        {
            return new PassengerViewModel
            {
                FirstName = passenger.FirstName,
                SecondName = passenger.SecondName,
                PassengerType = BuilPassengerType(passenger.PassengerType)
            };
        }

        private static string BuilPassengerType(string type)
        {
            switch(type)
            {
                case AdultType:
                    return Adult;

                case ChildType:
                    return Child;

                case InfantType:
                    return Infant;

                default:
                    return Adult;
            }
        }

        private static bool BuildIsAcceptable(ProposalDto proposalDto, ConfigurationProvider configuration)
        {
            if (proposalDto.Price > configuration.MinimumAcceptablePrice)
                return false;

            if (configuration.UnnaceptableOrigins.Any(uo => uo == proposalDto.Origin))
                return false;

            if (configuration.UnnaceptableDestinations.Any(ud => ud == proposalDto.Destiny))
                return false;

            return true;
        }
    }
}