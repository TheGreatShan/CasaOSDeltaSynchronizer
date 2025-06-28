using CasaOSDeltaSynchronizer.Watcher;
using Path = CasaOSDeltaSynchronizer.Watcher.Path;

namespace CasaOSDeltaSynchronizer.FileSyncer;

internal class FileSyncer
{
    internal static void Sync(Change change, Path target)
    {
        var combinedTargetPath = System.IO.Path.Combine(target.FullPath, change.Path.FullPath.Split("/").Last());
        switch (change.Type)
        {
            case ChangeType.Created:
                File.Copy(
                    change.Path.FullPath,
                    combinedTargetPath);
                break;
            case ChangeType.Changed:
                File.Replace(change.Path.FullPath, combinedTargetPath, null);
                break;
            case ChangeType.Removed:
                break;
            case ChangeType.Renamed:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}