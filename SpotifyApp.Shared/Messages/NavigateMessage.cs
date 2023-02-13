using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Models.NavigateParams;

namespace SpotifyApp.Shared.Messages;

public sealed class NavigateMessage
{
    public PageType Type { get; set; }
    
    public INavigateParams? NavigateParams { get; set; }
}