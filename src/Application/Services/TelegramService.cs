using Application.Common;
using Application.Common.Interfaces;
using Domain.Configurations;
using Domain.Dtos;
using Infra.Gateways;
using Microsoft.Extensions.Options;
using Serilog;
using System.Text;

namespace Application.Services;

public class TelegramService : ITelegramService
{
    private readonly ITelegramGateway _telegramGateway;
    private readonly TelegramConfiguration _telegramConfiguration;

    public TelegramService(ITelegramGateway telegramGateway, IOptions<TelegramConfiguration> options)
    {
        _telegramGateway = telegramGateway;
        _telegramConfiguration = options.Value;
    }

    public async Task SendMessageAsync(IEnumerable<ResponseEconomiaDto> dtos)
    {
        try
        {
            Log.Information("Preparando para enviar a mensagem para o canal");

            var mensagem = FormatarMensagemParaEnvio(dtos);

            Log.Information($"Mensagem gerada para envio: {mensagem}");

            await _telegramGateway.SendMessageAsync(_telegramConfiguration.ChatId, mensagem);

            Log.Information("Mensagem publicada no Canal do Telegram do sucesso");
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }

    private static string FormatarMensagemParaEnvio(IEnumerable<ResponseEconomiaDto> dtos)
    {
        StringBuilder sb = new();
        var cotacaoDia = dtos.FirstOrDefault();

        if (cotacaoDia is not null)
        {
            sb.AppendLine($"*{cotacaoDia.Name}*");
            sb.AppendLine($"*Oferta Máxima:* {cotacaoDia.Ask}");
            sb.AppendLine($"*Oferta Minima:* {cotacaoDia.Bid}");
            sb.AppendLine($"*Maior do Dia:* {cotacaoDia.High}");
            sb.AppendLine($"*Menor do Dia:* {cotacaoDia.Low}");
            sb.AppendLine($"*Variação da Oferta:* {cotacaoDia.VarBid}");
            sb.AppendLine($"*Percentual de Mudança:* {cotacaoDia.PctChange}%");
            sb.AppendLine($"*Última Atualização:* {Convert.ToDateTime(cotacaoDia.CreateDate):dd/MM/yyyy HH:mm:ss}");
            sb.AppendLine($"*Data/Hora Consulta:* {Utils.ConverterTimestampParaData(cotacaoDia.Timestamp):dd/MM/yyyy HH:mm:ss}");
        }

        var menorCotacaoDoPeriodo = dtos.FirstOrDefault(x => x.Bid == dtos.Min(x => x.Bid));
        if (menorCotacaoDoPeriodo is not null)
            sb.AppendLine($"*Menor Cotação do Período:* {menorCotacaoDoPeriodo.Bid} - {Utils.ConverterTimestampParaData(menorCotacaoDoPeriodo.Timestamp)}");

        var maiorCotacaoDoPeriodo = dtos.FirstOrDefault(x => x.Ask == dtos.Max(x => x.Ask));
        if (maiorCotacaoDoPeriodo is not null)
            sb.AppendLine($"*Maior Cotação do Período:* {maiorCotacaoDoPeriodo.Ask} - {Utils.ConverterTimestampParaData(maiorCotacaoDoPeriodo.Timestamp)}");

        return sb.ToString();
    }
}
