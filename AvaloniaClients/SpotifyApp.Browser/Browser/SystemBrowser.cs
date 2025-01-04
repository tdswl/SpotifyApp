using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Duende.IdentityModel.OidcClient.Browser;
using SpotifyApp.Storage.Contracts.Models;

namespace SpotifyApp.Browser.Browser;

internal sealed class SystemBrowser : IBrowser
{
    public SystemBrowser()
    {
    }

    async Task<BrowserResult> IBrowser.InvokeAsync(BrowserOptions options, CancellationToken cancellationToken)
    {
        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("browser")))
            {
                using (await JSHost.ImportAsync("RedirectScript", "../redirect.js", cancellationToken))
                {
                    try
                    { 
                        Interop.RedirectInterop.Open("https://duckduckgo.com");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            
            return new BrowserResult { ResultType = BrowserResultType.Success };
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