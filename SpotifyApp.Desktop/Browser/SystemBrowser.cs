using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.OidcClient.Browser;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace SpotifyApp.Desktop.Browser;

/// <summary>
/// Code from example
/// https://github.com/IdentityModel/IdentityModel.OidcClient.Samples/blob/main/NetCoreConsoleClient/src/NetCoreConsoleClient/SystemBrowser.cs
/// </summary>
public sealed class SystemBrowser : IBrowser
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

    public async Task<BrowserResult> InvokeAsync(BrowserOptions options,
        CancellationToken cancellationToken = default)
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

public class LoopbackHttpListener : IDisposable
{
    private const int DefaultTimeout = 60 * 5; // 5 mins (in seconds)

    private readonly IWebHost _host;
    private readonly TaskCompletionSource<string> _source = new();

    public LoopbackHttpListener(int port, string? path = null)
    {
        path ??= string.Empty;
        if (path.StartsWith("/"))
        {
            path = path[1..];
        }

        var url = $"http://127.0.0.1:{port}/{path}";

        _host = new WebHostBuilder()
            .UseKestrel()
            .UseUrls(url)
            .Configure(Configure)
            .Build();
        _host.Start();
    }

    public void Dispose()
    {
        Task.Run(async () =>
        {
            await Task.Delay(500);
            _host.Dispose();
        });
    }

    private void Configure(IApplicationBuilder app)
    {
        app.Run(async ctx =>
        {
            if (ctx.Request.Method == "GET")
            {
                await SetResultAsync(ctx.Request.QueryString.Value, ctx);
            }
            else if (ctx.Request.Method == "POST")
            {
                if (!ctx.Request.ContentType.Equals("application/x-www-form-urlencoded",
                        StringComparison.OrdinalIgnoreCase))
                {
                    ctx.Response.StatusCode = 415;
                }
                else
                {
                    using (var sr = new StreamReader(ctx.Request.Body, Encoding.UTF8))
                    {
                        var body = await sr.ReadToEndAsync();
                        await SetResultAsync(body, ctx);
                    }
                }
            }
            else
            {
                ctx.Response.StatusCode = 405;
            }
        });
    }

    private async Task SetResultAsync(string value, HttpContext ctx)
    {
        try
        {
            ctx.Response.StatusCode = 200;
            ctx.Response.ContentType = "text/html";
            await ctx.Response.WriteAsync("<h1>You can now return to the application.</h1>");
            await ctx.Response.Body.FlushAsync();

            _source.TrySetResult(value);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());

            ctx.Response.StatusCode = 400;
            ctx.Response.ContentType = "text/html";
            await ctx.Response.WriteAsync("<h1>Invalid request.</h1>");
            await ctx.Response.Body.FlushAsync();
        }
    }

    public Task<string> WaitForCallbackAsync(int timeoutInSeconds = DefaultTimeout)
    {
        Task.Run(async () =>
        {
            await Task.Delay(timeoutInSeconds * 1000);
            _source.TrySetCanceled();
        });

        return _source.Task;
    }
}