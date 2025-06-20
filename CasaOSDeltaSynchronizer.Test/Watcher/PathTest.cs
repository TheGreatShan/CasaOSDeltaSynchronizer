using Path = CasaOSDeltaSynchronizer.Watcher.Path;

namespace CasaOSDeltaSynchronizer.Test.Watcher;

public class PathTest
{
    [Fact]
    void should_throw_error_when_path_does_not_exist()
    {
        var exception = Assert.Throws<InvalidDataException>(() => new Path("IDoNotExist.txt"));
        Assert.Equal("Path 'IDoNotExist.txt' does not exist.", exception.Message);
    }

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