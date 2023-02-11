using Avalonia.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.ViewModels;
using SpotifyApp.Shared.Views;
using ProfileView = SpotifyApp.Shared.Views.ProfileView;
using ProfileViewModel = SpotifyApp.Shared.ViewModels.ProfileViewModel;

namespace SpotifyApp.Shared.Services;

internal sealed class NavigationService : INavigationService
{
    public UserControl NavigateTo(PageType pageType, object? navigateParams = null)
    {
        switch (pageType)
        {
            case PageType.Profile:
                return new ProfileView { DataContext = Ioc.Default.GetRequiredService<ProfileViewModel>(), };
            case PageType.LikedSongs:
                return new LikedSongsView { DataContext = Ioc.Default.GetRequiredService<LikedSongsViewModel>(), };
            default:
                throw new ArgumentOutOfRangeException(nameof(pageType), pageType, null);
        }
    }
}