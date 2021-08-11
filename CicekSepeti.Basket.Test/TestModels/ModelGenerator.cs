using CicekSepeti.Basket.Core;
using CicekSepeti.Basket.Core.DataModel;
using CicekSepeti.BasketCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CicekSepeti.Basket.Test.TestModels
{
    public static class ModelGenerator
    {
        public static BasketApiRequestModel CreateDummyBasketRequestModel()
        {
            return new BasketApiRequestModel()
            {
                CustomerId = 1,
                ProductId = 1,
                Quantity = 5
            };
        }

        public static BasketApiRequestModel CreateDummyFaultBasketRequestModel()
        {
            return new BasketApiRequestModel()
            {
                CustomerId = 1,
                ProductId = 0,
                Quantity = 5
            };
        }

        public static BasketApiResponseModel CreateDummyBasketResponseModel()
        {
            return new BasketApiResponseModel()
            {
                Data = new List<ProductServiceModel>() {
                        new ProductServiceModel () {
                             ProductId = 1,
                             Quantity = 5
                        } },
                ExceptionMessage = string.Empty,
                HttpStatusCode = HttpStatusCode.OK.GetHashCode()
            };
        }

        public static BasketApiResponseModel CreateDummyFaultBasketResponseModel()
        {
            return new BasketApiResponseModel()
            {
                Data = null,
                ExceptionMessage = "ProductId, 0 değerinden büyük olmalıdır",
                HttpStatusCode = HttpStatusCode.BadRequest.GetHashCode()
            };
        }

        public static Product CreateDummyProductModel()
        {
            return new Product()
            {
                ProductId = 1,
                Name = "Flower",
                Quantity = 5
            };
        }

        public static BasketServiceModel CreateDummyBasketServiceModel()
        {
            return new BasketServiceModel()
            {
                CustomerId = 1,
                Products = new List<ProductServiceModel>() {
                        new ProductServiceModel () {
                             ProductId = 1,
                             Quantity = 5
                        } }
            };
        }
    }
}
