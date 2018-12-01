using System;

namespace BMT.Customer.Web.Dtos
{
    public class CustomerFormDto
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PassengerType { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime DepartureDatetime { get; set; }
        public DateTime ArrivalDatetime { get; set; }
        public int Price { get; set; }
    }
}