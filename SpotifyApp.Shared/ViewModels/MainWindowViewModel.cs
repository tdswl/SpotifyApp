using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Services;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class MainWindowViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly IAuthService _authService;

    [ObservableProperty]
    private UserControl? _content;
    
    public MainWindowViewModel()
    {
        //Designer constructor
    }

    public MainWindowViewModel(INavigationService navigationService,
        IAuthService authService)
    {
        _navigationService = navigationService;
        _authService = authService;

        LoginCommand.ExecuteAsync(null);
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task LoginAsync(CancellationToken token)
    {
        var loginInfo = await _authService.Login(token);
        NavigateToCommand.Execute(PageType.Profile);
    }
    
    [RelayCommand]
    private void NavigateTo(PageType type)
    {
        Content = _navigationService.NavigateTo(type);
    }
}