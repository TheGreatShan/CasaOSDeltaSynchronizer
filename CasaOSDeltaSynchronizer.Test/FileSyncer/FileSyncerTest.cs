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
        var content = "test";
        File.WriteAllText(filePath, content);

        CasaOSDeltaSynchronizer.FileSyncer.FileSyncer.Sync(
            new Change(new CasaOSDeltaSynchronizer.Watcher.Path(filePath), ChangeType.Created),
            new CasaOSDeltaSynchronizer.Watcher.Path(target.Path));

        Assert.True(File.Exists(Path.Combine(target.Path, "test.txt")));
        Assert.Equal(content, File.ReadAllText(Path.Combine(target.Path, "test.txt")));
    }   
    
    [Fact]
    void should_copy_updated_file()
    {
        var source = DisposableFileSystem.DisposableDirectory.Create();
        var target = DisposableFileSystem.DisposableDirectory.Create();

        var filePath = Path.Combine(source.Path, "test.txt");
        var content = "test";
        File.WriteAllText(filePath, content);

        CasaOSDeltaSynchronizer.FileSyncer.FileSyncer.Sync(
            new Change(new CasaOSDeltaSynchronizer.Watcher.Path(filePath), ChangeType.Created),
            new CasaOSDeltaSynchronizer.Watcher.Path(target.Path));

        Assert.True(File.Exists(Path.Combine(target.Path, "test.txt")));
        Assert.Equal(content, File.ReadAllText(Path.Combine(target.Path, "test.txt")));

        
        content = "new content";
        File.WriteAllText(filePath, content);
        
        CasaOSDeltaSynchronizer.FileSyncer.FileSyncer.Sync(
            new Change(new CasaOSDeltaSynchronizer.Watcher.Path(filePath), ChangeType.Changed),
            new CasaOSDeltaSynchronizer.Watcher.Path(target.Path));
        
        Assert.True(File.Exists(Path.Combine(target.Path, "test.txt")));
        Assert.Equal(content, File.ReadAllText(Path.Combine(target.Path, "test.txt")));
    }
    
    [Fact]
    void should_delete_removed_file()
    {
        var source = DisposableFileSystem.DisposableDirectory.Create();
        var target = DisposableFileSystem.DisposableDirectory.Create();

        var filePath = Path.Combine(source.Path, "test.txt");
        var content = "test";
        File.WriteAllText(filePath, content);

        CasaOSDeltaSynchronizer.FileSyncer.FileSyncer.Sync(
            new Change(new CasaOSDeltaSynchronizer.Watcher.Path(filePath), ChangeType.Created),
            new CasaOSDeltaSynchronizer.Watcher.Path(target.Path));

        Assert.True(File.Exists(Path.Combine(target.Path, "test.txt")));
        Assert.Equal(content, File.ReadAllText(Path.Combine(target.Path, "test.txt")));

        
        File.Delete(filePath);
        
        CasaOSDeltaSynchronizer.FileSyncer.FileSyncer.Sync(
            new Change(new CasaOSDeltaSynchronizer.Watcher.Path(filePath), ChangeType.Removed),
            new CasaOSDeltaSynchronizer.Watcher.Path(target.Path));
        
        Assert.False(File.Exists(Path.Combine(target.Path, "test.txt")));
    }
}