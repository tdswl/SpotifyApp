namespace SpotifyApp.Api.Client.OpenApiClient
{
    public interface ITokenService
    {
        Task<string> GetAccessToken(CancellationToken token);
    }
}
