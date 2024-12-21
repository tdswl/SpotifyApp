using System.Runtime.InteropServices.JavaScript;

namespace SpotifyApp.Browser.Interop;

internal static partial class CacheStorageInterop
{
    [JSImport("cacheData", "cacheStorage.js")]
    public static partial byte[] CacheData(string url);
}