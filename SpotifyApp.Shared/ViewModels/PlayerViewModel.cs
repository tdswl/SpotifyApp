using System.Net;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.ViewModels.Items;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class PlayerViewModel : ObservableRecipient
{
    private readonly ISpotifyClient _spotifyClient;
    private readonly IMapper _mapper;

    [ObservableProperty] 
    private TrackViewModel? _currentTrack;
    
    public PlayerViewModel()
    {
        //Designer constructor
    }
    
    public PlayerViewModel(ISpotifyClient spotifyClient,
        IMapper mapper)
    {
        _spotifyClient = spotifyClient;
        _mapper = mapper;
        IsActive = true;
    }

    partial void OnCurrentTrackChanging(TrackViewModel? value)
    {
        CurrentTrack?.Dispose();
    }

    protected override void OnActivated()
    {
        base.OnActivated();

        GetCurrentPlaybackCommand.Execute(null);
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetCurrentPlaybackAsync(string id, CancellationToken token)
    {
        try
        {
            var playbackResponse = await _spotifyClient
                .GetInformationAboutTheUsersCurrentPlaybackAsync(null, null, token);

            if (token.IsCancellationRequested)
            {
                return;
            }
            
            var trackVm = Ioc.Default.GetRequiredService<TrackViewModel>();
            var track = _mapper.Map<TrackModel>(playbackResponse.Item);
            trackVm.Item = track;
            CurrentTrack = trackVm;
        }
        catch (ApiException e) when (e.StatusCode == (int)HttpStatusCode.NoContent)
        {
            // Nothing - 204 is good code, but Spotify openapi yml say it's mot 
            CurrentTrack = null;
        }
    }

    protected override void OnDeactivated()
    {
        GetCurrentPlaybackCommand.Cancel();
        CurrentTrack?.Dispose();
        CurrentTrack = null;
    }
}