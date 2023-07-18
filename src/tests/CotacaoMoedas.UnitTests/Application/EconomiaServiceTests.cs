using Application.Common.Interfaces;
using Application.Services;
using Domain.Configurations;
using Domain.Dtos;
using Infra.Gateways;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;

namespace CotacaoMoedas.UnitTests.Application;

public class EconomiaServiceTests
{
    private readonly AutoMocker _mock;
    private readonly Mock<IEconomiaService> _economiaServiceMock;
    private readonly Mock<IEconomiaGateway> _economiaGatewayMock;

    public EconomiaServiceTests()
    {
        _mock = new AutoMocker();
        _economiaGatewayMock = _mock.GetMock<IEconomiaGateway>();
        _economiaServiceMock = _mock.GetMock<IEconomiaService>();
    }

    [Fact]
    public async Task PegarCotacoesPorQuantidadeDeDiasAsync_Should_ReturnOk()
    {
        // Arrange
        var options = Options.Create(new EconomiaConfiguration());

        _economiaServiceMock.Setup(x => x.PegarCotacoesPorQuantidadeDeDiasAsync()).ReturnsAsync(new List<ResponseEconomiaDto>());
        _economiaGatewayMock.Setup(x => x.PegarCotacoesPorQuantidadeDeDiasAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(new List<ResponseEconomiaDto>());

        var economiaService = new EconomiaService(_economiaGatewayMock.Object, options);

        // Act
        var result = await economiaService.PegarCotacoesPorQuantidadeDeDiasAsync();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task PegarCotacoesPorQuantidadeDeDiasAsync_Should_ThrowAnException()
    {
        // Arrange
        var options = Options.Create(new EconomiaConfiguration());

        _economiaGatewayMock.Setup(x => x.PegarCotacoesPorQuantidadeDeDiasAsync(It.IsAny<string>(), It.IsAny<int>())).Throws<Exception>();

        var economiaService = new EconomiaService(_economiaGatewayMock.Object, options);

        // Act
        var result = economiaService.PegarCotacoesPorQuantidadeDeDiasAsync();

        // Assert
        await Assert.ThrowsAsync<Exception>(async () => await result);
    }

}
