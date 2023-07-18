using Application.Common.Interfaces;
using Application.Services;
using CotacaoMoedas.UnitTests.Utils;
using Domain.Configurations;
using Domain.Dtos;
using Infra.Gateways;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;

namespace CotacaoMoedas.UnitTests.Application
{
    public class TelegramServiceTests
    {
        private readonly AutoMocker _mock;
        private readonly Mock<ITelegramGateway> _telegramGatewayMock;
        private readonly Mock<ITelegramService> _telegramServiceMock;

        public TelegramServiceTests()
        {
            _mock = new AutoMocker();
            _telegramGatewayMock = _mock.GetMock<ITelegramGateway>();
            _telegramServiceMock = _mock.GetMock<ITelegramService>();
        }


        [Fact]
        public async Task PegarCotacaoAsync_Should_ReturnOk()
        {
            // Arrange
            var options = Options.Create(new TelegramConfiguration());
            var responseDtos = Fakedata.ResponseDtos();

            _telegramGatewayMock.Setup(x => x.SendMessageAsync(It.IsAny<string>(),It.IsAny<string>()));
            _telegramServiceMock.Setup(x => x.SendMessageAsync(It.IsAny<IEnumerable<ResponseEconomiaDto>>()));

            var economiaService = new TelegramService(_telegramGatewayMock.Object, options);

            // Act
            await economiaService.SendMessageAsync(responseDtos);

            // Assert
            _telegramGatewayMock.Verify(v => v.SendMessageAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task PegarCotacaoAsync_Should_ThrowAnException()
        {
            // Arrange
            var options = Options.Create(new TelegramConfiguration());
            List<ResponseEconomiaDto> responseDtos = new();

            _telegramGatewayMock.Setup(x => x.SendMessageAsync(It.IsAny<string>(), It.IsAny<string>())).Throws<Exception>();

            var economiaService = new TelegramService(_telegramGatewayMock.Object, options);

            // Act
            var result = economiaService.SendMessageAsync(responseDtos);

            // Assert
            await Assert.ThrowsAsync<Exception>(async () => await result);
        }
    }
}
