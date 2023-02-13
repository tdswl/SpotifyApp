using Avalonia.Controls;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Models.NavigateParams;

namespace SpotifyApp.Shared.Services;

public interface INavigationService
{
    public UserControl NavigateTo(PageType pageType, INavigateParams? navigateParams = null);
}