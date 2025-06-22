namespace CasaOSDeltaSynchronizer.Watcher;

public record Change(Path Path, ChangeType Type);

public enum ChangeType
{
    Created,
    Change,
    Removed,
    Renamed
}