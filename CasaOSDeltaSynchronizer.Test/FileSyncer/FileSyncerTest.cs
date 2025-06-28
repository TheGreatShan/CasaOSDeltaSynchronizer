using CasaOSDeltaSynchronizer.Watcher;
using Path = System.IO.Path;

namespace CasaOSDeltaSynchronizer.Test.FileSyncer;

public class FileSyncerTest
{
    [Fact]
    void should_copy_new_file()
    {
        var source = DisposableFileSystem.DisposableDirectory.Create();
        var target = DisposableFileSystem.DisposableDirectory.Create();

        var filePath = Path.Combine(source.Path, "test.txt");
        File.WriteAllText(filePath, "test");

        CasaOSDeltaSynchronizer.FileSyncer.FileSyncer.Sync(
            new Change(new CasaOSDeltaSynchronizer.Watcher.Path(filePath), ChangeType.Created),
            new CasaOSDeltaSynchronizer.Watcher.Path(target.Path));

        Assert.True(File.Exists(Path.Combine(target.Path, "test.txt")));
    }
}