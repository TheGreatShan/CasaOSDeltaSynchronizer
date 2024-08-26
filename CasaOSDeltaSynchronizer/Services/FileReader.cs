using System.Text.Json;

namespace CasaOSDeltaSynchronizer.Services;

internal class FileReader
{
    internal static T Read<T>(string path) =>
        JsonSerializer.Deserialize<T>(File.ReadAllText(path))!;
}

internal static class AppSettingsReader
{
    public static AppSettings Read(string path) =>
        FileReader.Read<AppSettings>(path);
}

internal record AppSettings(string ServerLocation, string ClientLocation);