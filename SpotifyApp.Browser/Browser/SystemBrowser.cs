using System;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.OidcClient.Browser;

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