using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace SpotifyApp.Shared.Controls;

public class TracksGridControl : ItemsControl
{
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
        // Worked only when drag thumb by mouse
        var tracksGrid = e.NameScope.Find<DataGrid>("TracksGrid");
        if (tracksGrid != null)
        {
            tracksGrid.VerticalScroll += (sender, args) =>
            {
                if (sender is not ScrollBar sb)
                {
                    return;
                }

                if (args.ScrollEventType == ScrollEventType.EndScroll)
                {
                    var delta = Math.Abs(sb.Maximum - args.NewValue);
                    if (delta <= double.Epsilon)
                    {
                        LoadMoreCommand?.Execute(null);
                    }
                }
            };
        }
        
        base.OnApplyTemplate(e);
    }
}