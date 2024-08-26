using System.Security.Cryptography;

namespace CasaOSDeltaSynchronizer.Services;

internal class FileChecker
{
    public static bool IsSame(string localPath, string serverPath)
    {
        var localHash = CalculateSHA256(new FileInfo(localPath));
        var serverHash = CalculateSHA256(new FileInfo(serverPath));   
        
        return localHash == serverHash;
    }

    internal static string CalculateSHA256(FileInfo fileInfo)
    {
        using var sha256 = SHA256.Create();
        using var stream = fileInfo.OpenRead();
        
        var hash = sha256.ComputeHash(stream);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }
}