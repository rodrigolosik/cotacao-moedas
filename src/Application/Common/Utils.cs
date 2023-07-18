namespace Application.Common;

public static class Utils
{
    public static string ConverterTimestampParaData(string timestamp)
    {
        return DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(timestamp)).ToString("dd/MM/yyyy HH:mm:ss");
    }
}
