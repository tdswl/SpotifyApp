using System.Net.Http.Headers;
using System.Text;

namespace SpotifyApp.Api.Client.OpenApiClient
{
    public partial class SpotifyClient
    {
        private readonly ITokenService _tokenService;

        public SpotifyClient(ITokenService tokenService,
            IHttpClientFactory httpClientFactory) : this(httpClientFactory.CreateClient())
        {
            _tokenService = tokenService;
        }

        private async Task PrepareRequestAsync(HttpClient client,
            HttpRequestMessage request,
            StringBuilder url,
            CancellationToken cancellationToken)
        {
            var authToken = await _tokenService.GetAccessToken(cancellationToken);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }

        private async Task PrepareRequestAsync(HttpClient client,
            HttpRequestMessage request,
            string url,
            CancellationToken cancellationToken)
        {
            var authToken = await _tokenService.GetAccessToken(cancellationToken);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }

        private static Task ProcessResponseAsync(HttpClient client,
            HttpResponseMessage request,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
