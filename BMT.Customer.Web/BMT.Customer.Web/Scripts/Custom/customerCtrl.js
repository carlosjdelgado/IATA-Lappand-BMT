$(document).ready(function () {
    $('#customerForm').ajaxForm(function () {
        var firstName = $('#firstName').val();
        var secondName = $('#secondName').val();
        var passengerType = $('#passengerType').val();
        var departureCity = $('#departureCity').val();
        var arrivalCity = $('#arrivalCity').val();
        var departureDatetime = $('#departureDatetime').val();
        var arrivalDatetime = $('#arrivalDatetime').val();
        var price = $('#price').val();

        var customerFormDto = {
            FirstName: firstName,
            SecondName: secondName,
            PassengerType: passengerType,
            DepartureCity: departureCity,
            ArrivalCity: arrivalCity,
            DepartureDatetime: departureDatetime,
            ArrivalDatetime: arrivalDatetime,
            Price: price
        };

        $.ajax({
            type: 'POST',
            url: '/Customer/SentProposal',
            data: { CustomerFormDto: customerFormDto },
            cache: false,
            success: function (result) {

            }
        });
    });
}); 