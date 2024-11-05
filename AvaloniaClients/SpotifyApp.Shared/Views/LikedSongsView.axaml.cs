using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SpotifyApp.Shared.Views;

public partial class LikedSongsView : UserControl
{
    public LikedSongsView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}