namespace CasaOSDeltaSynchronizer.Watcher;

public class Path
{
    public string FullPath { get; }

    public Path(string path)
    {
        if (!System.IO.Path.Exists(path)) 
            throw new InvalidDataException($"Path '{path}' does not exist.");
        FullPath = path;
    }
};