using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace SpotifyApp.Browser.Interop;

[SupportedOSPlatform("browser")]
internal static partial class LocalStorageInterop
{
    [JSImport("read", "LocalStorageScript")]
    internal static partial string Read(string key);
    
    [JSImport("add", "LocalStorageScript")]
    internal static partial void Add(string key, string value);
    
    [JSImport("remove", "LocalStorageScript")]
    internal static partial void Remove(string key);
}