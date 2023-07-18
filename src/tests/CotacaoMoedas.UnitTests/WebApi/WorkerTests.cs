using Application.Common.Interfaces;
using Domain.Configurations;
using Domain.Dtos;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;
using WebApi.Workers;

namespace CotacaoMoedas.UnitTests.WebApi;

public class WorkerTests
{
    private readonly AutoMocker _mock;
    private readonly Mock<IEconomiaService> _economiaServiceMock;
    private readonly Mock<ITelegramService> _telegramServiceMock;

    public WorkerTests()
    {
        _mock = new AutoMocker();
        _economiaServiceMock = _mock.GetMock<IEconomiaService>();
        _telegramServiceMock = _mock.GetMock<ITelegramService>();
    }

    [Fact]
    public async Task ExecuteAsync_Should_ReturnOk()
    {
        // Arrange
        var options = Options.Create(new WorkerConfiguration());
        CancellationTokenSource cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromSeconds(5));

        _economiaServiceMock.Setup(x => x.PegarCotacoesPorQuantidadeDeDiasAsync()).ReturnsAsync(new List<ResponseEconomiaDto>());
        _telegramServiceMock.Setup(x => x.SendMessageAsync(It.IsAny<IEnumerable<ResponseEconomiaDto>>()));

        var worker = new Worker(_economiaServiceMock.Object, _telegramServiceMock.Object, options);

        // Act
        await worker.StartAsync(cts.Token);
        await worker.StopAsync(new CancellationToken());

        // Assert
        _economiaServiceMock.Verify(v => v.PegarCotacoesPorQuantidadeDeDiasAsync(), Times.AtLeastOnce);
        _telegramServiceMock.Verify(v => v.SendMessageAsync(It.IsAny<IEnumerable<ResponseEconomiaDto>>()), Times.AtLeastOnce);
    }
}
