using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SpotifyApp.Shared.Views;

public partial class ArtistScreenView : UserControl
{
    public ArtistScreenView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}