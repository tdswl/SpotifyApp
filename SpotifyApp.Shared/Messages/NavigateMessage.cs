using SpotifyApp.Shared.Enums;

namespace SpotifyApp.Shared.Messages;

public sealed class NavigateMessage
{
    public PageType Type { get; set; }
    
    public object? NavigateParams { get; set; }
}