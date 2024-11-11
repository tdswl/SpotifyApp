using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Messages;
using SpotifyApp.Shared.Models.NavigateParams;
using SpotifyApp.Shared.Services;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class NavigateViewModel : ObservableRecipient, 
    IRecipient<NavigateMessage>
{
    private readonly INavigationService _navigationService;
    
    [ObservableProperty]
    private UserControl? _content;
    
    public NavigateViewModel()
    {
        //Designer constructor
    }

    public NavigateViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        IsActive = true;
    }

    protected override void OnActivated()
    {
        base.OnActivated();

        NavigateToCommand.Execute(PageType.Home);
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