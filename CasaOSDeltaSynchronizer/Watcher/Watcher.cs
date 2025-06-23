namespace CasaOSDeltaSynchronizer.Watcher;

internal class Watcher : IDisposable
{
    private readonly FileSystemWatcher _fileSystemWatcher;
    public List<Change> ChangedFilePaths { get; } = [];

    public Watcher(string path)
    {
        _fileSystemWatcher = new FileSystemWatcher(path);
        _fileSystemWatcher.Created += OnCreated;
        _fileSystemWatcher.Changed += OnChanged;
        _fileSystemWatcher.Deleted += OnDeleted;
        
        
        _fileSystemWatcher.IncludeSubdirectories = true;
        _fileSystemWatcher.EnableRaisingEvents = true;
    }

    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        ChangedFilePaths.Add(new Change(new Path(e.FullPath), ChangeType.Removed));
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        ChangedFilePaths.Add(new Change(new Path(e.FullPath), ChangeType.Changed));
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        ChangedFilePaths.Add(new Change(new Path(e.FullPath), ChangeType.Created));
    }

    public void Dispose()
    {
        _fileSystemWatcher.Dispose();
    }
}