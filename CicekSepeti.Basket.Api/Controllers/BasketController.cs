using CicekSepeti.Basket.Core;
using CicekSepeti.Basket.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CicekSepeti.BasketApi.Controllers
{
    [ApiController]
    [Route("api/basket")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToBasket([FromBody] BasketApiRequestModel basketRequestModel)
        {
            var basketResponse = await _basketService.AddItemsToBasket(basketRequestModel);

            return Ok(basketResponse);
        }
    }
}
