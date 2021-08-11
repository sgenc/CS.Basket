using CicekSepeti.Basket.Caching;
using CicekSepeti.Basket.Core.DataModel;
using CicekSepeti.Basket.Data.Repositories;
using CicekSepeti.Basket.Service;
using CicekSepeti.Basket.Test.TestModels;
using CicekSepeti.BasketCore;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CicekSepeti.Basket.Test.ServiceTests
{
    public class BasketServiceTests
    {
        [Fact]
        public async Task When_AddItemToEmptyBasket_Called_Then_Should_Be_Succesfull()
        {
            var basketRequestModel = ModelGenerator.CreateDummyBasketRequestModel();

            var basketServiceModel = new BasketServiceModel()
            {
                CustomerId = basketRequestModel.CustomerId,
                Products = new List<ProductServiceModel>() {
                                    new ProductServiceModel () {
                                         ProductId = basketRequestModel.ProductId,
                                         Quantity = basketRequestModel.Quantity
                                    }
                }
            };

            var expectedStockAvailability = true;
            var expectedBasketServiceModel = ModelGenerator.CreateDummyBasketServiceModel();
            var expectedProductModel = ModelGenerator.CreateDummyProductModel();


            var cacheServiceMock = new Mock<ICacheService<BasketServiceModel>>();
            cacheServiceMock.Setup(s => s.GetAsync(It.IsAny<string>())).ReturnsAsync((BasketServiceModel)null);
            cacheServiceMock.Setup(s => s.AddAsync(It.IsAny<string>(), basketServiceModel));

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(s => s.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(expectedProductModel).Verifiable();

            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(s => s.CheckStockAvailability(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedStockAvailability).Verifiable();


            var basketService = new BasketService(productServiceMock.Object, cacheServiceMock.Object);
            var basketResponseModel = await basketService.AddItemsToBasket(basketRequestModel);


            basketResponseModel.Data.Should().NotBeNull();
            basketResponseModel.HttpStatusCode.Should().Be(HttpStatusCode.Created.GetHashCode());

        }

        [Fact]
        public async Task When_AddItemToBasket_Called_Then_Should_Be_Successful()
        {
            var basketRequestModel = ModelGenerator.CreateDummyBasketRequestModel();

            var basketServiceModel = new BasketServiceModel()
            {
                CustomerId = basketRequestModel.CustomerId,
                Products = new List<ProductServiceModel>() {
                                    new ProductServiceModel () {
                                         ProductId = basketRequestModel.ProductId,
                                         Quantity = basketRequestModel.Quantity
                                    }
                }
            };

            var expectedStockAvailability = true;
            var expectedBasketServiceModel = ModelGenerator.CreateDummyBasketServiceModel();
            var expectedProductModel = ModelGenerator.CreateDummyProductModel();


            var cacheServiceMock = new Mock<ICacheService<BasketServiceModel>>();
            cacheServiceMock.Setup(s => s.GetAsync(It.IsAny<string>())).ReturnsAsync(expectedBasketServiceModel);
            cacheServiceMock.Setup(s => s.AddAsync(It.IsAny<string>(), basketServiceModel));

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(s => s.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(expectedProductModel).Verifiable();

            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(s => s.CheckStockAvailability(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedStockAvailability).Verifiable();
            
            
            var basketService = new BasketService(productServiceMock.Object, cacheServiceMock.Object);
            var basketResponseModel = await basketService.AddItemsToBasket(basketRequestModel);


            basketResponseModel.Data.Should().NotBeNull();
            basketResponseModel.HttpStatusCode.Should().Be(HttpStatusCode.OK.GetHashCode());

        }

        [Fact]
        public async Task When_AddItemToBasket_Called_While_Insufficient_Stock_Then_Should_Be_UnSuccessful()
        {
            var basketRequestModel = ModelGenerator.CreateDummyBasketRequestModel();

            var basketServiceModel = new BasketServiceModel()
            {
                CustomerId = basketRequestModel.CustomerId,
                Products = new List<ProductServiceModel>() {
                                    new ProductServiceModel () {
                                         ProductId = basketRequestModel.ProductId,
                                         Quantity = basketRequestModel.Quantity
                                    }
                }
            };

            var expectedStockAvailability = false;
            var expectedBasketServiceModel = ModelGenerator.CreateDummyBasketServiceModel();
            var expectedProductModel = ModelGenerator.CreateDummyProductModel();


            var cacheServiceMock = new Mock<ICacheService<BasketServiceModel>>();
            cacheServiceMock.Setup(s => s.GetAsync(It.IsAny<string>())).ReturnsAsync(expectedBasketServiceModel);
            cacheServiceMock.Setup(s => s.AddAsync(It.IsAny<string>(), basketServiceModel));

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(s => s.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(expectedProductModel).Verifiable();

            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(s => s.CheckStockAvailability(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedStockAvailability).Verifiable();


            var basketService = new BasketService(productServiceMock.Object, cacheServiceMock.Object);
            var basketResponseModel = await basketService.AddItemsToBasket(basketRequestModel);


            basketResponseModel.Data.Should().BeNull();
            basketResponseModel.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest.GetHashCode());

        }
    }
}
