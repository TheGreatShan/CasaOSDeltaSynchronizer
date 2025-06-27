using CasaOSDeltaSynchronizer.Services;

namespace CasaOSDeltaSynchronizer; 
public class Program
{
    public static void Main(string[] args)
    {
        var appSettings = AppSettingsReader.Read("./appsettings.json");
        var watcher = new Watcher.Watcher(appSettings.ClientLocation);

        while (true)
        {
            watcher.ChangedFilePaths.ForEach(Console.WriteLine);
        }
    }
}