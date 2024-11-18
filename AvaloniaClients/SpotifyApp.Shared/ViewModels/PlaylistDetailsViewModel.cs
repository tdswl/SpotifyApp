using CommunityToolkit.Mvvm.ComponentModel;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class PlaylistDetailsViewModel : ViewModelWithInitialization
{
    [ObservableProperty]
    private string _id;
}