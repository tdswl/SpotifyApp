using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.OidcClient.Browser;

namespace SpotifyApp.Desktop.Browser;

/// <summary>
/// Browser for oAuth2 redirect
/// Code from example
/// https://github.com/IdentityModel/IdentityModel.OidcClient.Samples/blob/main/NetCoreConsoleClient/src/NetCoreConsoleClient/SystemBrowser.cs
/// </summary>
internal sealed class SystemBrowser : IBrowser
{
    private const int DefaultPort = 7890;
    private readonly string? _path;

    private int Port { get; }

    public SystemBrowser(int? port = DefaultPort, string? path = null)
    {
        _path = path;

        Port = port ?? GetRandomUnusedPort();
    }

    private static int GetRandomUnusedPort()
    {
        var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var port = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();
        return port;
    }

    async Task<BrowserResult> IBrowser.InvokeAsync(BrowserOptions options, CancellationToken cancellationToken)
    {
        using (var listener = new LoopbackHttpListener(Port, _path))
        {
            OpenBrowser(options.StartUrl);

            try
            {
                var result = await listener.WaitForCallbackAsync();
                if (string.IsNullOrWhiteSpace(result))
                {
                    return new BrowserResult { ResultType = BrowserResultType.UnknownError, Error = "Empty response." };
                }

                return new BrowserResult { Response = result, ResultType = BrowserResultType.Success };
            }
            catch (TaskCanceledException ex)
            {
                return new BrowserResult { ResultType = BrowserResultType.Timeout, Error = ex.Message };
            }
            catch (Exception ex)
            {
                return new BrowserResult { ResultType = BrowserResultType.UnknownError, Error = ex.Message };
            }
        }
    }

    private static void OpenBrowser(string url)
    {
        try
        {
            Process.Start(url);
        }
        catch
        {
            // hack because of this: https://github.com/dotnet/corefx/issues/10361
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                throw;
            }
        }
    }
}