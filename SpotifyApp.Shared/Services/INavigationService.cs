using Avalonia.Controls;
using SpotifyApp.Shared.Enums;

namespace SpotifyApp.Shared.Services;

public interface INavigationService
{
    public UserControl NavigateTo(PageType pageType, object? navigateParams = null);
}