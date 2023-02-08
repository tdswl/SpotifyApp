using System.Collections.ObjectModel;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Api.Client.Users;
using SpotifyApp.Api.Contracts.Users.Enums;
using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.Services;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class ProfileViewModel : ObservableRecipient
{
    private readonly IAuthService _authService;
    private readonly IUsersClient _usersClient;
    private readonly IImageCache _imageCache;
    private readonly IMapper _mapper;
    
    [ObservableProperty]
    private ItemWithPreviewViewModel? _currentUser;

    [ObservableProperty] 
    private ObservableCollection<ItemWithPreviewViewModel> _topArtists = new();

    public ProfileViewModel()
    {
        //Designer constructor
    }
    
    public ProfileViewModel(IAuthService authService,
        IUsersClient usersClient,
        IImageCache imageCache,
        IMapper mapper)
    {
        _authService = authService;
        _usersClient = usersClient;
        _imageCache = imageCache;
        _mapper = mapper;

        GetUserInfoCommand.Execute(null);
        GetArtistsCommand.Execute(null);
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetUserInfoAsync(CancellationToken token)
    {
        var authInfo = await _authService.Login(token);
        var userInfo = await _usersClient.GetCurrentUserProfile(authInfo.AccessToken, token);
        CurrentUser = new ItemWithPreviewViewModel(_imageCache)
        {
            Item = _mapper.Map<ItemWithImages>(userInfo),
        };
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetArtistsAsync(CancellationToken token)
    {
        var authInfo = await _authService.Login(token);
        var artistsResponse = await _usersClient.GetUsersTopItems(ItemsType.Artists, authInfo.AccessToken, token);

        TopArtists.Clear();
        foreach (var artist in artistsResponse.Items)
        {
            TopArtists.Add(new ItemWithPreviewViewModel(_imageCache) { Item = _mapper.Map<ItemWithImages>(artist), });
        }
    }
}