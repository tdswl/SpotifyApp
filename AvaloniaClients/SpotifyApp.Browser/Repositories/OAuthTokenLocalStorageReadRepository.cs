using System;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using SpotifyApp.Repositories.Contracts;
using SpotifyApp.Storage.Contracts.Models;

namespace SpotifyApp.Browser.Repositories;

public sealed class OAuthTokenLocalStorageReadRepository : IOAuthTokenReadRepository
{
    async Task<OAuthToken?> IOAuthTokenReadRepository.GetLatestUserToken(CancellationToken cancellationToken)
    {
        using (await JSHost.ImportAsync("localStorage.js", "../localStorage.js", cancellationToken))
        {
            try
            { 
                var read = Interop.LocalStorageInterop.Read("AuthToken");
                return JsonSerializer.Deserialize<OAuthToken>(read);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}