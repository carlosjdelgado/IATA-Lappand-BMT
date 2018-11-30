namespace Bidmytrip.Core.Api.Dtos
{
    public class PassengerDto
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public PassengerTypeDto Type { get; set; }
    }
}