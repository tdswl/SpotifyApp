namespace SpotifyApp.Shared.Models.NavigateParams;

public sealed class ArtistParams : INavigateParams
{
    public required string Id { get; set; }
}