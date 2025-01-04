using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace SpotifyApp.Browser.Interop;

[SupportedOSPlatform("browser")]
internal static partial class RedirectInterop
{
    [JSImport("open", "RedirectScript")]
    internal static partial void Open(string url);
}