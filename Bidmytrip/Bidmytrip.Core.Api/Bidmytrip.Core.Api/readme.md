POST PROPOSAL -- https://bidmytripcoreapiv1.azurewebsites.net/api/Proposals/
{
  "travellerName": "Manolo",
  "proposalId": "f10244aa-5a2e-4437-a080-727f4eb8a271",
  "origin": "origin",
  "destiny": "destiny",
  "outboundDate": "2018-10-12",
  "inboundDate": "2018-12-12",
  "price": "100.0",
  "Passenger1": {
		  "firstName": "Manolo",
		  "secondName": "Escobar",
		  "passengerType": "ADULT"
	  },
  "Passenger2": {
		  "firstName": "Manola",
		  "secondName": "Escobar",
		  "passengerType": "ADULT"
	  },
  "Passenger3": {
		"firstName": "Manolo",
		"secondName": "Hijo",
		"passengerType": "CHILD"
	},
  "Passenger4": {
		"firstName": "Manola",
		"secondName": "Hija",
		"passengerType": "INFANT"
	},
  "TimeToLive": "2018-09-12T00:00:00",
  "Status": "PROPOSED"
}



POST PROPOSAL CONFIRM -- https://bidmytripcoreapiv1.azurewebsites.net/api/Proposals/Confirm
{
	"ProposalId": "ProposalId",
	"OfferId": "OfferId"
}



GET PROPOSAL -- https://bidmytripcoreapiv1.azurewebsites.net/api/Proposals/

CLEAN ALL PROPOSAL -- https://bidmytripcoreapiv1.azurewebsites.net/api/Proposals/CleanAll

POST OFFER -- https://bidmytripcoreapiv1.azurewebsites.net/api/Proposals/Offers
{
  "AirlineName": "Manolo",
  "OfferId": "2a4aae30-b777-44e8-b1a7-b1f308d41239",
  "ProposalId": "f10244aa-5a2e-4437-a080-727f4eb8a271", 
  "outboundDate": "2018-10-12",
  "inboundDate": "2018-12-12",
  "Price": "100.0"
}

************************************************************

Passenger Type: // ADULT / CHILD / INFANT
Proposal Status: // PROPOSED / ACCEPTED / CONFIRMED / CLOSED

