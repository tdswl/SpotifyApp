using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class MainWindowViewModel : ObservableRecipient
{
    [ObservableProperty] 
    private NavigateViewModel? _navigate;
   
    [ObservableProperty]
    private SearchViewModel? _search;
    
    [ObservableProperty]
    private PlayerViewModel? _player;
    
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
        Search = Ioc.Default.GetRequiredService<SearchViewModel>();
    }
}