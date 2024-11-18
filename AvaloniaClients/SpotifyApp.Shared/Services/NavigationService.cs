using Avalonia.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Models.NavigateParams;
using SpotifyApp.Shared.ViewModels;
using SpotifyApp.Shared.Views;

namespace SpotifyApp.Shared.Services;

internal sealed class NavigationService : INavigationService
{
    UserControl INavigationService.NavigateTo(PageType pageType, INavigateParams? navigateParams)
    {
        switch (pageType)
        {
            case PageType.Home:
                return null;
            case PageType.PlaylistDetails:
                var vm = Ioc.Default.GetRequiredService<PlaylistDetailsViewModel>();
                if (navigateParams is PlaylistParams playlistParams)
                {
                    vm.Id = playlistParams.Id;
                }
                return new PlaylistDetailsView { DataContext = vm, };
            default:
                throw new ArgumentOutOfRangeException(nameof(pageType), pageType, null);
        }
    }
}