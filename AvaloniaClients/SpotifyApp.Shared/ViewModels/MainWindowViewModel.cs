using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class MainWindowViewModel : ObservableRecipient
{
    [ObservableProperty] 
    private NavigateViewModel? _navigate;

    public MainWindowViewModel()
    {
        //Designer constructor
        IsActive = true;
    }

    protected override void OnActivated()
    {
        base.OnActivated();

        Navigate = Ioc.Default.GetRequiredService<NavigateViewModel>();
    }
}