﻿using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace SpotifyApp.Browser.Interop;

[SupportedOSPlatform("browser")]
internal static partial class CacheStorageInterop
{
    [JSImport("cacheData", "CacheStorageScript")]
    internal static partial byte[] CacheData(string url);
}