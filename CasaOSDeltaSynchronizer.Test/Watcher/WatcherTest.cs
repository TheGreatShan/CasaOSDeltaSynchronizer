using CasaOSDeltaSynchronizer.Services;
using CasaOSDeltaSynchronizer.Watcher;
using Path = System.IO.Path;
using File = System.IO.File;

namespace CasaOSDeltaSynchronizer.Test.Watcher;

[Collection("Disable parallel tests")]
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
        System.IO.File.WriteAllText(fullPath, fileName);
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
        System.IO.File.WriteAllText(fullPath, fileName);
        Thread.Sleep(100);

        System.IO.File.WriteAllText(fullPath, "new file content");
        Thread.Sleep(100);

        var expected = new List<Change>
        {
            new(new CasaOSDeltaSynchronizer.Watcher.Path(fullPath), ChangeType.Created),
            new(new CasaOSDeltaSynchronizer.Watcher.Path(fullPath), ChangeType.Changed)
        };

        Assert.Equivalent(expected, watcher.ChangedFilePaths);

        disposableDirectory.Dispose();
    }

    [Fact]
    void should_return_filename_if_file_was_deleted()
    {
        var disposableDirectory = DisposableFileSystem.DisposableDirectory.Create();
        var path = disposableDirectory.Path;

        using var watcher = new CasaOSDeltaSynchronizer.Watcher.Watcher(path);

        const string fileName = "test.txt";
        var fullPath = Path.Combine(path, fileName);
        System.IO.File.WriteAllText(fullPath, fileName);
        Thread.Sleep(100);

        System.IO.File.Delete(fullPath);
        Thread.Sleep(100);

        var expected = new List<Change>
        {
            new(new CasaOSDeltaSynchronizer.Watcher.Path(fullPath), ChangeType.Created),
            new(new CasaOSDeltaSynchronizer.Watcher.Path(fullPath), ChangeType.Removed)
        };

        Assert.Equivalent(expected, watcher.ChangedFilePaths);

        disposableDirectory.Dispose();
    }

    [Fact]
    void should_return_filename_if_file_was_renamed()
    {
        var disposableDirectory = DisposableFileSystem.DisposableDirectory.Create();
        var path = disposableDirectory.Path;

        using var watcher = new CasaOSDeltaSynchronizer.Watcher.Watcher(path);

        const string fileName = "test.txt";
        var fullPath = Path.Combine(path, fileName);
        System.IO.File.WriteAllText(fullPath, fileName);
        Thread.Sleep(100);

        var newFullPath = Path.Combine(path, "newFile.txt");
        System.IO.File.Move(fullPath, newFullPath);
        Thread.Sleep(100);

        var expected = new List<Change>
        {
            new(new CasaOSDeltaSynchronizer.Watcher.Path(fullPath), ChangeType.Created),
            new(new CasaOSDeltaSynchronizer.Watcher.Path(newFullPath), ChangeType.Renamed)
        };

        Assert.Equivalent(expected, watcher.ChangedFilePaths);

        disposableDirectory.Dispose();
    }
}