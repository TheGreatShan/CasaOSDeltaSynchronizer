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
        
        
        var isSame = FileChecker.IsSame(filePath, filePath);
        Assert.True(isSame);
    }

    [Fact]
    void check_if_file_is_different()
    {
        using var directory = DisposableFileSystem.DisposableDirectory.Create();
        var filePath = directory.RandomFileName();
        var filePath2 = directory.RandomFileName();

        File.WriteAllText(filePath, "test");
        File.WriteAllText(filePath2, "test2");

        var isSame = FileChecker.IsSame(filePath, filePath2);
        Assert.False(isSame);
    }
}