using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.DependencyInjection;
using MainWindowViewModel = SpotifyApp.Shared.ViewModels.MainWindowViewModel;

namespace SpotifyApp.Shared.Views;

public sealed partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);

        DataContext = Ioc.Default.GetRequiredService<MainWindowViewModel>();
    }
}