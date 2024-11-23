using SpotifyApp.Shared.ViewModels.SpotifyItems;

namespace SpotifyApp.Shared.Models.NavigateParams;

public sealed class SpotifyItemParams: INavigateParams
{
    public required SpotifyItemBaseViewModel Item { get; set; }
}