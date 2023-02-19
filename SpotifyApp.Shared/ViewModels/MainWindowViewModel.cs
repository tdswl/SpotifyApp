using AutoMapper;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Messages;
using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.Models.NavigateParams;
using SpotifyApp.Shared.Services;
using SpotifyApp.Shared.ViewModels.Items;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class MainWindowViewModel : ObservableRecipient, 
    IRecipient<NavigateMessage>
{
    private readonly ISpotifyClient _spotifyClient;
    private readonly IMapper _mapper;
    private readonly INavigationService _navigationService;
        
    [ObservableProperty]
    private UserViewModel? _currentUser;
    
    [ObservableProperty]
    private UserControl? _content;
    
    [ObservableProperty]
    private PlayerViewModel? _player;
    
    public MainWindowViewModel()
    {
        //Designer constructor
    }

    public MainWindowViewModel(INavigationService navigationService,
        ISpotifyClient spotifyClient,
        IMapper mapper)
    {
        _navigationService = navigationService;
        _spotifyClient = spotifyClient;
        _mapper = mapper;
        IsActive = true;
    }

    protected override void OnActivated()
    {
        base.OnActivated();

        GetUserInfoCommand.ExecuteAsync(null);
    }
    
    [RelayCommand]
    private void NavigateTo(PageType type)
    {
        NavigateWithParams(type, null);
    }
    
    private void NavigateWithParams(PageType pageType, INavigateParams? navigateParams)
    {
        if (Content?.DataContext is ObservableRecipient previous)
        {
            previous.IsActive = false;
        }
        Content = _navigationService.NavigateTo(pageType, navigateParams);
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetUserInfoAsync(CancellationToken token)
    {
        var userInfo = await _spotifyClient.GetCurrentUsersProfileAsync(token);
        
        if (token.IsCancellationRequested)
        {
            return;
        }
        
        var userVm = Ioc.Default.GetRequiredService<UserViewModel>();
        userVm.Item = _mapper.Map<UserModel>(userInfo);
        CurrentUser = userVm;
        NavigateToCommand.Execute(PageType.Profile);
    }

    public void Receive(NavigateMessage message)
    {
        NavigateWithParams(message.Type, message.NavigateParams);
    }
}