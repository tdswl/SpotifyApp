using System.Net;
using AutoMapper;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.ViewModels.Items;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class PlayerViewModel : ObservableRecipient
{
    private readonly DispatcherTimer? _updateTimer = new();
    
    private readonly ISpotifyClient _spotifyClient;
    private readonly IMapper _mapper;

    [ObservableProperty] 
    private bool _isShuffleEnabled;
    
    [ObservableProperty] 
    private bool _isPlaying;
    
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
        _updateTimer.Tick += UpdateTimerOnTick;
        IsActive = true;
    }

    partial void OnCurrentTrackChanging(TrackViewModel? value)
    {
        CurrentTrack?.Dispose();
    }

    protected override void OnActivated()
    {
        base.OnActivated();
        
        StartTimer();
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

            IsShuffleEnabled = playbackResponse.Shuffle_state;
            IsPlaying = playbackResponse.Is_playing;

            if (CurrentTrack?.Item?.Id != playbackResponse.Item.Id)
            {
                var trackVm = Ioc.Default.GetRequiredService<TrackViewModel>();
                var track = _mapper.Map<TrackModel>(playbackResponse.Item);
                trackVm.Item = track;
                CurrentTrack = trackVm;
            }
        }
        catch (ApiException e) when (e.StatusCode == (int)HttpStatusCode.NoContent)
        {
            // Nothing - 204 is good code, but Spotify openapi yml say it's not 
            CurrentTrack = null;
        }
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task ShuffleAsync(CancellationToken token)
    {
        await _spotifyClient.ToggleShuffleForUsersPlaybackAsync(!IsShuffleEnabled, null, token);
        IsShuffleEnabled = !IsShuffleEnabled;
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task PreviousAsync(CancellationToken token)
    {
        await _spotifyClient.SkipUsersPlaybackToPreviousTrackAsync(null, token);
        await GetCurrentPlaybackCommand.ExecuteAsync(null);
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task PlayPauseAsync(CancellationToken token)
    {
        if (IsPlaying)
        {
            await _spotifyClient.PauseAUsersPlaybackAsync(null, token);
        }
        else
        {
            await _spotifyClient.StartAUsersPlaybackAsync(null, null, token);
        }

        IsPlaying = !IsPlaying;
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task NextAsync(CancellationToken token)
    {
        await _spotifyClient.SkipUsersPlaybackToNextTrackAsync(null, token);
        await GetCurrentPlaybackCommand.ExecuteAsync(null);
    }
    
    private void StartTimer()
    {
        if (_updateTimer.IsEnabled)
        {
            return;
        }

        _updateTimer.Interval = TimeSpan.FromSeconds(5);
        _updateTimer.Start();
    }

    private void UpdateTimerOnTick(object? sender, EventArgs e)
    {
        GetCurrentPlaybackCommand.ExecuteAsync(null);
    }

    protected override void OnDeactivated()
    {
        if (_updateTimer != null)
        {
            _updateTimer.Stop();
            _updateTimer.Tick -= UpdateTimerOnTick;
        }
        GetCurrentPlaybackCommand.Cancel();
        CurrentTrack?.Dispose();
        CurrentTrack = null;
        base.OnDeactivated();
    }
}