using System.Collections.ObjectModel;
using AutoMapper;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.ViewModels.Items;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class SearchViewModel : ObservableRecipient
{
    private readonly DispatcherTimer _searchTimer = new();
    
    private readonly ISpotifyClient _spotifyClient;
    private readonly IMapper _mapper;
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(SearchCommand))]
    private string? _searchString;
    
    [ObservableProperty] 
    private ObservableCollection<AlbumViewModel> _albums = new();

    partial void OnSearchStringChanged(string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            StartTimer();
        }
    }

    public SearchViewModel()
    {
        //Designer constructor
    }
    
    public SearchViewModel(ISpotifyClient spotifyClient,
        IMapper mapper)
    {
        _spotifyClient = spotifyClient;
        _mapper = mapper;
        
        _searchTimer.Tick += SearchOnTick;
        IsActive = true;
    }
    
    [RelayCommand(IncludeCancelCommand = true, CanExecute = nameof(SearchCanExecute))]
    private async Task SearchAsync(CancellationToken token)
    {
        CleanLastSearch();
        
        if (string.IsNullOrWhiteSpace(SearchString))
        {
            return;
        }
        
        var searchedAlbums = await _spotifyClient.SearchAsync(SearchString, 
            new List<Anonymous> { Anonymous.Album }, 
            null, 
            null, 
            null, 
            null, 
            token);
        
        if (token.IsCancellationRequested)
        {
            return;
        }
        
        foreach (var searchedAlbum in searchedAlbums.Albums.Items)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }
            
            var albumVm = Ioc.Default.GetRequiredService<AlbumViewModel>();
            Albums.Add(albumVm);
            var album = _mapper.Map<AlbumModel>(searchedAlbum);
            albumVm.Item = album;
        }
    }
    
    private bool SearchCanExecute()
    {
        return !string.IsNullOrWhiteSpace(SearchString);
    }

    private void CleanLastSearch()
    {
        foreach (var album in Albums)
        {
            album.Dispose();
        }
        Albums.Clear();
    }
    
    private void StartTimer()
    {
        // if (SearchCommand.IsRunning)
        // {
        //     SearchCommand.Cancel();
        // }
        
        if (_searchTimer.IsEnabled)
        {
            _searchTimer.Stop();
        }

        _searchTimer.Interval = TimeSpan.FromMilliseconds(400);
        _searchTimer.Start();
    }
    
    private void SearchOnTick(object? sender, EventArgs e)
    {
        _searchTimer.Stop();
        SearchCommand.Execute(null);
    }

    protected override void OnDeactivated()
    {
        _searchTimer.Stop();
        _searchTimer.Tick -= SearchOnTick;
        SearchCommand.Cancel();
        CleanLastSearch();
        base.OnDeactivated();
    }
}