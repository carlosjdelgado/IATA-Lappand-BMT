using BMT.Customer.Web.Dtos;
using BMT.Customer.Web.Mappers;
using BMT.Customer.Web.ServiceContracts;
using BMT.Customer.Web.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BMT.Customer.Web.Controllers
{
    public class OfferController : Controller
    {
        private IOfferService _offerService = new OfferService();
        
        public async Task<ActionResult> Index(string proposalId)
        {
            var proposalModel = await _offerService.GetOffers(proposalId);

            var offersDto = OffersDtoMapper.Map(proposalModel);

            SetBestPrice(offersDto);

            return View("~/Views/Customer/Offers.cshtml", offersDto);
        }

        private void SetBestPrice(OffersDto offersDto)
        {
            var offersList = offersDto.Offers.ToList().OrderBy(o => o.Price);

            offersList.FirstOrDefault().SetAsBestPrice();

            offersDto.Offers = offersList;
        }
    }
}