using CasaOSDeltaSynchronizer.Services;

namespace CasaOSDeltaSynchronizer.Test.Services;

public class FileCheckerTest
{
    [Fact]
    void check_if_file_is_same()
    {
        using var directory = DisposableFileSystem.DisposableDirectory.Create();
        var filePath = directory.RandomFileName();
        
        File.WriteAllText(filePath, "test");
        
        var fileChecker = new FileChecker();
        
        var isSame = fileChecker.IsSame(filePath, filePath);
        Assert.True(isSame);
    }
}