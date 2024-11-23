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
                var vm = Ioc.Default.GetRequiredService<SpotifyItemDetailsViewModel>();
                if (navigateParams is SpotifyItemParams playlistParams)
                {
                    vm.Id = playlistParams.Item.Id;
                    vm.Type = playlistParams.Item.Type;
                }
                return new SpotifyItemDetailsView { DataContext = vm, };
            default:
                throw new ArgumentOutOfRangeException(nameof(pageType), pageType, null);
        }
    }
}