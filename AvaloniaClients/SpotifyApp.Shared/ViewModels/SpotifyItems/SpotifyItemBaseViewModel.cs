using CommunityToolkit.Mvvm.ComponentModel;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.ViewModels.SpotifyItems;

public abstract partial class SpotifyItemBaseViewModel : ObservableObject
{
    [ObservableProperty] 
    private string _id;
    
    [ObservableProperty] 
    private ImageViewModel _image;
    
    [ObservableProperty] 
    private SpotifyItemType _type;

    public SpotifyItemBaseViewModel()
    {
        // Designer constructor
    }
}