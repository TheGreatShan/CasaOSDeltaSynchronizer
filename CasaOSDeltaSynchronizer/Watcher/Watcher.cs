namespace CasaOSDeltaSynchronizer.Watcher;

internal class Watcher : IDisposable
{
    private FileSystemWatcher _fileSystemWatcher;
    public string ChangedFilePaths { get; private set; } = "";
    public Watcher(string path)
    {
        _fileSystemWatcher = new FileSystemWatcher(path);
        _fileSystemWatcher.Created += OnCreated;
        
        _fileSystemWatcher.IncludeSubdirectories = true;
        _fileSystemWatcher.EnableRaisingEvents = true;
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        ChangedFilePaths = e.FullPath;
    }

    public void Dispose()
    {
        _fileSystemWatcher.Dispose();
    }
}