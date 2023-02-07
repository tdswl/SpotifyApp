using Avalonia.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using SpotifyApp.Shared.Enums;
using ProfileView = SpotifyApp.Shared.Views.ProfileView;
using ProfileViewModel = SpotifyApp.Shared.ViewModels.ProfileViewModel;

namespace SpotifyApp.Shared.Services;

public sealed class NavigationService : INavigationService
{
    public UserControl NavigateTo(PageType pageType, object? navigateParams = null)
    {
        switch (pageType)
        {
            case PageType.Profile:
                return new ProfileView { DataContext = Ioc.Default.GetRequiredService<ProfileViewModel>(), };
            default:
                throw new ArgumentOutOfRangeException(nameof(pageType), pageType, null);
        }
    }
}