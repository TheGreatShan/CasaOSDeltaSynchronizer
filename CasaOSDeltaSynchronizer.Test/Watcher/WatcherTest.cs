using CasaOSDeltaSynchronizer.Services;
using CasaOSDeltaSynchronizer.Watcher;
using Path = System.IO.Path;

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

        var expected = new List<Change> { new(new CasaOSDeltaSynchronizer.Watcher.Path(fullPath), ChangeType.Created) };
        Assert.Equivalent(
            expected,
            watcher.ChangedFilePaths);

        disposableDirectory.Dispose();
    }    
    
    [Fact]
    void should_return_filename_if_file_was_changed()
    {
        var disposableDirectory = DisposableFileSystem.DisposableDirectory.Create();
        var path = disposableDirectory.Path;

        using var watcher = new CasaOSDeltaSynchronizer.Watcher.Watcher(path);

        const string fileName = "test.txt";
        var fullPath = Path.Combine(path, fileName);
        File.WriteAllText(fullPath, fileName);
        Thread.Sleep(100);
        
        File.WriteAllText(fullPath, "new file content");
        Thread.Sleep(100);
        
        var expected = new List<Change> { new(new CasaOSDeltaSynchronizer.Watcher.Path(fullPath), ChangeType.Created), new(new CasaOSDeltaSynchronizer.Watcher.Path(fullPath), ChangeType.Changed) };

        Assert.Equivalent(expected, watcher.ChangedFilePaths);
        
        disposableDirectory.Dispose();
    }
}