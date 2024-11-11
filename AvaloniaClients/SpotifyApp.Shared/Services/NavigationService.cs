using Avalonia.Controls;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Models.NavigateParams;

namespace SpotifyApp.Shared.Services;

internal sealed class NavigationService : INavigationService
{
    UserControl INavigationService.NavigateTo(PageType pageType, INavigateParams? navigateParams)
    {
        switch (pageType)
        {
            case PageType.Home:
                return null;
            default:
                throw new ArgumentOutOfRangeException(nameof(pageType), pageType, null);
        }
    }
}