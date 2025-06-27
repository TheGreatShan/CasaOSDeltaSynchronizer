using CasaOSDeltaSynchronizer.Services;

namespace CasaOSDeltaSynchronizer; 
public class Program
{
    public static void Main(string[] args)
    {
        var appSettings = AppSettingsReader.Read("./appsettings.json");
        var watcher = new Watcher.Watcher(appSettings.ClientLocation);

        var cancellationTokenSource = new CancellationTokenSource();
        Console.CancelKeyPress += (sender, e) =>
        {
            e.Cancel = true;
            cancellationTokenSource.Cancel();
        };
        
        while (!cancellationTokenSource.Token.IsCancellationRequested)
        {
            watcher.ChangedFilePaths.ForEach(Console.WriteLine);
            Thread.Sleep(1000); 
        }

        Console.WriteLine("File copying exited");
    }
}