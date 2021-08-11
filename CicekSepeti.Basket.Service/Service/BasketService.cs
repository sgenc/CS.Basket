using CicekSepeti.Basket.Caching;
using CicekSepeti.Basket.Core;
using CicekSepeti.BasketCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CicekSepeti.Basket.Service
{
    public class BasketService : IBasketService
    {
        private readonly ICacheService<BasketServiceModel> _cacheService;
        private readonly IProductService _productService;

        public BasketService(IProductService productService, ICacheService<BasketServiceModel> cacheService)
        {
            _productService = productService;
            _cacheService = cacheService;
        }

        public async Task<BasketApiResponseModel> AddItemsToBasket(BasketApiRequestModel basketRequestModel)
        {
            bool isStockAvailable = await _productService.CheckStockAvailability(basketRequestModel.ProductId, basketRequestModel.Quantity);

            if (!isStockAvailable)
            {
                return new BasketApiResponseModel()
                {
                    ExceptionMessage = "Stok yetersiz !!!",
                    HttpStatusCode = HttpStatusCode.BadRequest.GetHashCode()
                };
            }

            var basket = await GetBasket(basketRequestModel);

            if (basket == null)
                return await AddToEmptyBasket(basketRequestModel);
            else
                return await AddToBasket(basketRequestModel, basket);
        }

        private async Task<BasketServiceModel> GetBasket(BasketApiRequestModel basketRequestModel)
        {
            return  await _cacheService.GetAsync(basketRequestModel.CustomerId.ToString());
        }

        private async Task<BasketApiResponseModel> AddToBasket(BasketApiRequestModel basketRequestModel, BasketServiceModel basket)
        {
            if (basket.Products.Any(s => s.ProductId == basketRequestModel.ProductId))
            {
                basket.Products.FirstOrDefault(s => s.ProductId == basketRequestModel.ProductId).Quantity += basketRequestModel.Quantity;

                await _cacheService.AddAsync(basketRequestModel.CustomerId.ToString(), new BasketServiceModel()
                {
                    CustomerId = basketRequestModel.CustomerId,
                    Products = basket.Products
                });

                return new BasketApiResponseModel()
                {
                    Data = basket.Products,
                    HttpStatusCode = HttpStatusCode.OK.GetHashCode()
                };
            }
            else
            {
                basket.Products.Add(new ProductServiceModel() { ProductId = basketRequestModel.ProductId, Quantity = basketRequestModel.Quantity });

                await _cacheService.AddAsync(basketRequestModel.CustomerId.ToString(), new BasketServiceModel()
                {
                    CustomerId = basketRequestModel.CustomerId,
                    Products = basket.Products
                });

                return new BasketApiResponseModel()
                {
                    Data = basket.Products,
                    HttpStatusCode = HttpStatusCode.OK.GetHashCode()
                };
            }
        }

        private async Task<BasketApiResponseModel> AddToEmptyBasket(BasketApiRequestModel basketRequestModel)
        {
            var products = new List<ProductServiceModel>() {
                        new ProductServiceModel () {
                             ProductId = basketRequestModel.ProductId,
                             Quantity = basketRequestModel.Quantity
                        }
                };

            await _cacheService.AddAsync(basketRequestModel.CustomerId.ToString(), new BasketServiceModel()
            {
                CustomerId = basketRequestModel.CustomerId,
                Products = products
            });

            return new BasketApiResponseModel()
            {
                Data = products,
                HttpStatusCode = HttpStatusCode.Created.GetHashCode()
            };
        }
    }
}
