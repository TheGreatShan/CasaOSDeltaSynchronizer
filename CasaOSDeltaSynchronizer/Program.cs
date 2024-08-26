using CasaOSDeltaSynchronizer.Services;

namespace CasaOSDeltaSynchronizer;

public class Program
{
    public static void Main(string[] args)
    {
        var appSettings = AppSettingsReader.Read("appsettings.json");

        var files = Directory.GetFiles(appSettings.ClientLocation);
        foreach (var fromPath in files)
        {
            if (fromPath == null)
                return;

            var toPath = $"{appSettings.ServerLocation}/{fromPath.Trim(fromPath.ToCharArray())}";
            var exists = Directory.Exists(toPath);
            var message = exists && FileChecker.IsSame(fromPath, toPath) ? "No update needed" : "Update needed";
            Console.WriteLine(message);
        }
    }
}