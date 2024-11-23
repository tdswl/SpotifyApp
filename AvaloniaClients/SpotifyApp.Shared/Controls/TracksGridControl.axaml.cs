using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;

namespace SpotifyApp.Shared.Controls;

public class TracksGridControl : ItemsControl
{
    private DataGrid? _tracksGrid;
    
    private ICommand? _loadMoreCommand;

    /// <summary>
    /// Defines the <see cref="LoadMoreCommand"/> property.
    /// </summary>
    public static readonly DirectProperty<TracksGridControl, ICommand?> LoadMoreCommandProperty =
        AvaloniaProperty.RegisterDirect<TracksGridControl, ICommand?>(nameof(LoadMoreCommand),
            control => control.LoadMoreCommand, 
            (control, command) => control.LoadMoreCommand = command,
            enableDataValidation: true);
    
    /// <summary>
    /// Gets or sets an <see cref="ICommand"/> to be invoked when the button is clicked.
    /// </summary>
    public ICommand? LoadMoreCommand
    {
        get => _loadMoreCommand;
        set => SetAndRaise(LoadMoreCommandProperty, ref _loadMoreCommand, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        _tracksGrid = e.NameScope.Find<DataGrid>("TracksGrid");
        if (_tracksGrid != null)
        {
            //_tracksGrid.VerticalScroll += TracksGridOnVerticalScroll;
        }
        
        base.OnApplyTemplate(e);
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        if (_tracksGrid != null)
        {
            //_tracksGrid.VerticalScroll -= TracksGridOnVerticalScroll;
        }
        
        base.OnUnloaded(e);
    }

    // Worked only when drag thumb by mouse
    // TODO: same code for mouse wheel scroll
    private void TracksGridOnVerticalScroll(object? sender, ScrollEventArgs e)
    {
        if (sender is not ScrollBar sb)
        {
            return;
        }

        if (e.ScrollEventType == ScrollEventType.EndScroll)
        {
            var delta = Math.Abs(sb.Maximum - e.NewValue);
            if (delta <= double.Epsilon)
            {
                LoadMoreCommand?.Execute(null);
            }
        }
    }
}