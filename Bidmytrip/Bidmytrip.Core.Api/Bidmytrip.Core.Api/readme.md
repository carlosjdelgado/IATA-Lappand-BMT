POST PROPOSAL -- http://localhost:7071/api/Proposals

{
  "travellerName": "Manolo",
  "proposalId": "f10244aa-5a2e-4437-a080-727f4eb8a271",
  "origin": "origin",
  "destiny": "destiny",
  "outboundDate": "2018-10-12",
  "inboundDate": "2018-12-12",
  "price": "100.0",
  "passengers": [
	  {
	  "firstName": "first name",
	  "secondName": "second name",
	  "type": 0
	}
  ],
  "TimeToLive": "2018-09-12T00:00:00",
  "Status": "PROPOSED"
}


GET PROPOSAL -- http://localhost:7071/api/Proposals



POST OFFER -- http://localhost:7071/api/Proposals/Offers

{
  "AirlineName": "Manolo",
  "OfferId": "2a4aae30-b777-44e8-b1a7-b1f308d41239",
  "ProposalId": "f10244aa-5a2e-4437-a080-727f4eb8a271", 
  "outboundDate": "10/12/2018",
  "inboundDate": "12/12/2018"
}