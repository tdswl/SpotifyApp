using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SpotifyApp.Shared.Views;

public sealed partial class ProfileView : UserControl
{
    public ProfileView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}