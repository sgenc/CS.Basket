using CicekSepeti.Basket.Core;
using CicekSepeti.Basket.Service;
using CicekSepeti.Basket.Test.TestModels;
using CicekSepeti.BasketApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Moq;


namespace CicekSepeti.Basket.Test.ControllerTests
{
    public class BasketControllerTests
    {
        [Fact]
        public async Task When_AddToBasket_Endpoint_Called_Then_It_Should_Return_OkResult() 
        {
            // Arrange
            var serviceMock = new Mock<IBasketService>(MockBehavior.Strict);

            serviceMock.Setup(s => s.AddItemsToBasket(It.IsAny<BasketApiRequestModel>())).ReturnsAsync(ModelGenerator.CreateDummyBasketResponseModel()).Verifiable();

            var controller = new BasketController(serviceMock.Object);

            // Act
            var result = await controller.AddToBasket(ModelGenerator.CreateDummyBasketRequestModel());

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var basketResponseModel = okResult.Value as BasketApiResponseModel;

            serviceMock.Verify();
            basketResponseModel.HttpStatusCode.Should().Be(200);
            basketResponseModel.ExceptionMessage.Should().BeEmpty();
            basketResponseModel.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task When_AddToBasket_Endpoint_Called_Then_It_Should_Return_OkResult_With_Response_Data_Field_Should_Be_Null()
        {
            // Arrange
            var serviceMock = new Mock<IBasketService>(MockBehavior.Strict);

            serviceMock.Setup(s => s.AddItemsToBasket(It.IsAny<BasketApiRequestModel>())).ReturnsAsync(ModelGenerator.CreateDummyFaultBasketResponseModel()).Verifiable();

            var controller = new BasketController(serviceMock.Object);

            // Act
            var result = await controller.AddToBasket(ModelGenerator.CreateDummyFaultBasketRequestModel());

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var basketResponseModel = okResult.Value as BasketApiResponseModel;

            serviceMock.Verify();
            basketResponseModel.HttpStatusCode.Should().Be(400);
            basketResponseModel.ExceptionMessage.Should().NotBeEmpty();
            basketResponseModel.Data.Should().BeNull();
        }
    }
}
