namespace CasaOSDeltaSynchronizer.Watcher;

public record Change(Path Path, ChangeType Type);

public enum ChangeType
{
    Created,
    Changed,
    Removed,
    Renamed
}