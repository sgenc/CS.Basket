using CicekSepeti.Basket.Core.DataModel;
using CicekSepeti.Basket.Data.Repositories;
using CicekSepeti.Basket.Service;
using CicekSepeti.Basket.Test.TestModels;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentAssertions;
using System;
using Xunit;
using Moq;


namespace CicekSepeti.Basket.Test.ServiceTests
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task When_CheckStockAvailability_Called_Then_Should_Return_True()
        {
            var productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);

            var expectedProductResponse = ModelGenerator.CreateDummyProductModel();

            productRepositoryMock.Setup(s => s.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(expectedProductResponse).Verifiable();

            var productService = new ProductService(productRepositoryMock.Object);

            var isStockAvailable =  await productService.CheckStockAvailability(1, 2);

            productRepositoryMock.Verify();
            isStockAvailable.Should().Be(true);
        }

        [Fact]
        public async Task When_CheckStockAvailability_Called_Then_Should_Return_False()
        {
            var productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);

            var product = ModelGenerator.CreateDummyProductModel();

            productRepositoryMock.Setup(s => s.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(product).Verifiable();

            var productService = new ProductService(productRepositoryMock.Object);

            var isStockAvailable = await productService.CheckStockAvailability(1, 200);

            productRepositoryMock.Verify();
            isStockAvailable.Should().Be(false);
        }
    }
}
