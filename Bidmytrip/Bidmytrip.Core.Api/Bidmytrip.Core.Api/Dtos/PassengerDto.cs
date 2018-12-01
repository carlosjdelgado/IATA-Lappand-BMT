namespace Bidmytrip.Core.Api.Dtos
{
    public class PassengerDto
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PassengerType { get; set; } // ADULT / CHILD / INFANT

        internal bool IsValid()
        {
            return
                !string.IsNullOrEmpty(FirstName)
                && !string.IsNullOrEmpty(SecondName)
                && !string.IsNullOrEmpty(PassengerType);
        }
    }
}