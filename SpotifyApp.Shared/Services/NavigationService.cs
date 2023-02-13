using Avalonia.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Models.NavigateParams;
using SpotifyApp.Shared.ViewModels;
using SpotifyApp.Shared.Views;
using ProfileView = SpotifyApp.Shared.Views.ProfileView;
using ProfileViewModel = SpotifyApp.Shared.ViewModels.ProfileViewModel;

namespace SpotifyApp.Shared.Services;

internal sealed class NavigationService : INavigationService
{
    public UserControl NavigateTo(PageType pageType, INavigateParams? navigateParams = null)
    {
        switch (pageType)
        {
            case PageType.Profile:
                return new ProfileView { DataContext = Ioc.Default.GetRequiredService<ProfileViewModel>(), };
            case PageType.LikedSongs:
                return new LikedSongsView { DataContext = Ioc.Default.GetRequiredService<LikedSongsViewModel>(), };
            case PageType.Artist:
                var vm = Ioc.Default.GetRequiredService<ArtistScreenViewModel>();
                if (navigateParams is ArtistParams artistParams)
                {
                    vm.GetArtistCommand.ExecuteAsync(artistParams.Id);
                }
                return new ArtistScreenView { DataContext = vm, };
            default:
                throw new ArgumentOutOfRangeException(nameof(pageType), pageType, null);
        }
    }
}