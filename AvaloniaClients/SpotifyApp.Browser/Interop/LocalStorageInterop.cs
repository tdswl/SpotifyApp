using System.Runtime.InteropServices.JavaScript;

namespace SpotifyApp.Browser.Interop;

internal static partial class LocalStorageInterop
{
    [JSImport("read", "localStorage.js")]
    public static partial string Read(string key);
    
    [JSImport("add", "localStorage.js")]
    public static partial void Add(string key, string value);

    [JSImport("update", "localStorage.js")]
    public static partial void Update(string key, string value);
    
    [JSImport("delete", "localStorage.js")]
    public static partial void Delete(string key);
}