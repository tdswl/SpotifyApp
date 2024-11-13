using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] 
    private NavigateViewModel? _navigate;

    [ObservableProperty] 
    private ProfileViewModel? _profile;

    public MainWindowViewModel()
    {
        Navigate = Ioc.Default.GetRequiredService<NavigateViewModel>();
        Profile = Ioc.Default.GetRequiredService<ProfileViewModel>();
    }
}