using CasaOSDeltaSynchronizer.Watcher;
using Path = CasaOSDeltaSynchronizer.Watcher.Path;

namespace CasaOSDeltaSynchronizer.FileSyncer;

internal class FileSyncer
{
    internal static void Sync(Change change, Path target)
    {
        if (change.Type == ChangeType.Created)
        {
            File.Copy(
                change.Path.FullPath,
                System.IO.Path.Combine(target.FullPath, change.Path.FullPath.Split("/").Last()));
        }
    }
}