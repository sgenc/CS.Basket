using CicekSepeti.Basket.Core;
using System.Threading.Tasks;

namespace CicekSepeti.Basket.Service
{
    public interface IBasketService
    {
        Task<BasketApiResponseModel> AddItemsToBasket(BasketApiRequestModel basketRequestModel);
    }
}
