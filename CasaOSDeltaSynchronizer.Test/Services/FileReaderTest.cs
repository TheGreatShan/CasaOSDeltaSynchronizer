using CasaOSDeltaSynchronizer.Services;

namespace CasaOSDeltaSynchronizer.Test.Services;

public class FileReaderTest
{
    [Fact]
    public void read_app_settings()
    {
        var readSettings = AppSettingsReader.Read("Services/appsettingsTest.json");
        
        Assert.Equal("Client", readSettings.ClientLocation);
        Assert.Equal("Server", readSettings.ServerLocation);
    }
}

