using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SpotifyApp.Shared.ViewModels.Base;

public abstract partial class ViewModelWithInitialization : ObservableObject
{
    private bool _isInitialized;
    
    [ObservableProperty] 
    private bool _isLoading;
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task InitializeAsync(CancellationToken token)
    {
        if (!_isInitialized)
        {
            IsLoading = true;
            
            await Initialize(token);
            _isInitialized = true;
            
            IsLoading = false;
        }
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task DeactivateAsync(CancellationToken token)
    {
        if (_isInitialized)
        {
            IsLoading = true;
            
            await Deactivate(token);
            _isInitialized = false;
            
            IsLoading = false;
        }
    }

    protected virtual Task Initialize(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    protected virtual Task Deactivate(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}