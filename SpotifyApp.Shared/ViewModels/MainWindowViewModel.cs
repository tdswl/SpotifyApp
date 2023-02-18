using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Messages;
using SpotifyApp.Shared.Models.NavigateParams;
using SpotifyApp.Shared.Services;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class MainWindowViewModel : ObservableRecipient, 
    IRecipient<NavigateMessage>
{
    private readonly INavigationService _navigationService;
    private readonly IAuthService _authService;

    [ObservableProperty]
    private UserControl? _content;
    
    [ObservableProperty]
    private PlayerViewModel? _player;
    
    public MainWindowViewModel()
    {
        //Designer constructor
    }

    public MainWindowViewModel(INavigationService navigationService,
        IAuthService authService)
    {
        _navigationService = navigationService;
        _authService = authService;
        IsActive = true;
    }

    protected override void OnActivated()
    {
        base.OnActivated();

        LoginCommand.ExecuteAsync(null);
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task LoginAsync(CancellationToken token)
    {
        var loginInfo = await _authService.Login(token);
        _player = Ioc.Default.GetRequiredService<PlayerViewModel>();
        NavigateToCommand.Execute(PageType.Profile);
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

    public void Receive(NavigateMessage message)
    {
        NavigateWithParams(message.Type, message.NavigateParams);
    }
}