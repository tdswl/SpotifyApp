using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class MainWindowViewModel : ObservableRecipient
{
    [ObservableProperty] 
    private NavigateViewModel? _navigate;
    
    [ObservableProperty]
    private PlayerViewModel? _player;
    
    [ObservableProperty]
    private LibraryViewModel? _library;

    public MainWindowViewModel()
    {
        //Designer constructor
        IsActive = true;
    }

    protected override void OnActivated()
    {
        base.OnActivated();

        Navigate = Ioc.Default.GetRequiredService<NavigateViewModel>();
        Player = Ioc.Default.GetRequiredService<PlayerViewModel>();
        Library = Ioc.Default.GetRequiredService<LibraryViewModel>();
    }
}