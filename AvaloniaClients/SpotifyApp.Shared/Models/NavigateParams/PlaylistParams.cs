namespace SpotifyApp.Shared.Models.NavigateParams;

public sealed class PlaylistParams: INavigateParams
{
    public required string Id { get; set; }
}