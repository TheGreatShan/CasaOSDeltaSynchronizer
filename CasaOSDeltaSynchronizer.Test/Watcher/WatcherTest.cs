using CasaOSDeltaSynchronizer.Services;

namespace CasaOSDeltaSynchronizer.Test.Watcher;

public class WatcherTest
{
    [Fact]
    void should_return_filename_if_file_was_created()
    {
        var disposableDirectory = DisposableFileSystem.DisposableDirectory.Create();
        var path = disposableDirectory.Path;

        using var watcher = new CasaOSDeltaSynchronizer.Watcher.Watcher(path);
        
        const string fileName = "test.txt";
        var fullPath = Path.Combine(path, fileName);
        File.WriteAllText(fullPath, fileName);
        Thread.Sleep(100);
        
        Assert.Equal(fullPath, watcher.ChangedFilePaths);
        
        disposableDirectory.Dispose();
    }
}