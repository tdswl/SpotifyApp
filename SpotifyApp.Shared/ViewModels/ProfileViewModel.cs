using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Api.Client.Users;
using SpotifyApp.Api.Contracts.Users.Enums;
using SpotifyApp.Api.Contracts.Users.Models;
using SpotifyApp.Api.Contracts.Users.Responses;
using SpotifyApp.Shared.Services;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class ProfileViewModel : ObservableRecipient
{
    private readonly IAuthService _authService;
    private readonly IUsersClient _usersClient;
    private readonly IImageCache _imageCache;
    
    [ObservableProperty]
    private GetCurrentUserProfileResponse? _userProfile;

    [ObservableProperty] 
    private ObservableCollection<ArtistViewModel> _topArtists;

    public ProfileViewModel()
    {
        //Designer constructor
    }
    
    public ProfileViewModel(IAuthService authService,
        IUsersClient usersClient,
        IImageCache imageCache)
    {
        _authService = authService;
        _usersClient = usersClient;
        _imageCache = imageCache;

        GetUserInfoCommand.Execute(null);
        GetArtistsCommand.Execute(null);
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetUserInfoAsync(CancellationToken token)
    {
        var authInfo = await _authService.Login(token);
        UserProfile = await _usersClient.GetCurrentUserProfile(authInfo.AccessToken, token);
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetArtistsAsync(CancellationToken token)
    {
        var authInfo = await _authService.Login(token);
        var artistsResponse = await _usersClient.GetUsersTopItems(ItemsType.Artists, authInfo.AccessToken, token);
        TopArtists = new ObservableCollection<ArtistViewModel>(
            artistsResponse.Items.Select(a => new ArtistViewModel(_imageCache)
            {
                Artist = a,
            }));
    }
}