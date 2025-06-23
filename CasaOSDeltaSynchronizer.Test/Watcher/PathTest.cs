using Path = CasaOSDeltaSynchronizer.Watcher.Path;

namespace CasaOSDeltaSynchronizer.Test.Watcher;

public class PathTest
{
    [Fact]
    void should_create_path_object_when_valid()
    {
        var disposableDir = DisposableFileSystem.DisposableDirectory.Create();
        var path = disposableDir.Path;
        
        var actual = new Path(path);
        
        Assert.Equal(path, actual.FullPath);
        disposableDir.Dispose();
    }
}