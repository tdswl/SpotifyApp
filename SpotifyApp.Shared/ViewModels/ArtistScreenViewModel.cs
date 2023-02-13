using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Api.Client.Artists;
using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.Services;
using SpotifyApp.Shared.ViewModels.Items;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class ArtistScreenViewModel : ObservableRecipient
{
    private readonly IAuthService _authService;
    private readonly IArtistsClient _artistsClient;
    private readonly IMapper _mapper;

    [ObservableProperty] 
    private ArtistViewModel? _artist;
    
    public ArtistScreenViewModel()
    {
        //Designer constructor
    }
    
    public ArtistScreenViewModel(IAuthService authService,
        IArtistsClient artistsClient,
        IMapper mapper)
    {
        _authService = authService;
        _artistsClient = artistsClient;
        _mapper = mapper;
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetArtistAsync(string id, CancellationToken token)
    {
        var authInfo = await _authService.Login(token);
        var artistResponse = await _artistsClient.GetArtist(id,
            authInfo.AccessToken,
            token);

        var artistVm = Ioc.Default.GetRequiredService<ArtistViewModel>();
        artistVm.TakeBiggestImage = true;
        var artist = _mapper.Map<ArtistModel>(artistResponse);
        artistVm.Item = artist;
        Artist = artistVm;
    }
}