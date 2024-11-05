using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SpotifyApp.Shared.Views;

public partial class SearchView : UserControl
{
    public SearchView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}