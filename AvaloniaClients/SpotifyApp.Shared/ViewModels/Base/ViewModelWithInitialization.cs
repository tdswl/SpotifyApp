using CommunityToolkit.Mvvm.ComponentModel;

namespace SpotifyApp.Shared.ViewModels.Base;

public abstract partial class ViewModelWithInitialization : ObservableObject
{
    [ObservableProperty] 
    private bool _isLoading;

    public async Task Initialize(CancellationToken cancellationToken = default)
    {
        IsLoading = true;

        await InitializeInternal(cancellationToken);

        IsLoading = false;
    }

    protected virtual Task InitializeInternal(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}