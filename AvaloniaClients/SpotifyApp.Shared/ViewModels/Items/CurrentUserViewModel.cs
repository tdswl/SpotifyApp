using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Services.Contracts;
using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.ViewModels.Items;

public sealed partial class CurrentUserViewModel : ImagePreviewViewModel
{
    private readonly ISpotifyClient _spotifyClient;
    private readonly IMapper _mapper;

    public CurrentUserViewModel(IImageCache imageCache,
        ISpotifyClient spotifyClient,
        IMapper mapper) : base(imageCache)
    {
        _spotifyClient = spotifyClient;
        _mapper = mapper;
        IsActive = true;
    }

    protected override void OnActivated()
    {
        base.OnActivated();

        GetUserInfoCommand.ExecuteAsync(null);
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetUserInfoAsync(CancellationToken token)
    {
        var userInfo = await _spotifyClient.GetCurrentUsersProfileAsync(token);
        
        if (token.IsCancellationRequested)
        {
            return;
        }
        
        Item = _mapper.Map<UserModel>(userInfo);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose();
        // The current user is shared across all application.
        // If it disposed - something is wrong
        throw new InvalidOperationException("The current user must be a singleton.");
    }
}