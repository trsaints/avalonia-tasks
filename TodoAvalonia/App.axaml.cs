using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using TodoAvalonia.Models;
using TodoAvalonia.Services;
using TodoAvalonia.ViewModels;
using TodoAvalonia.Views;

namespace TodoAvalonia;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private readonly MainWindowViewModel _mainWindowViewModel = new();
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);
            desktop.MainWindow = new MainWindow
            {
                DataContext = _mainWindowViewModel
            };

            desktop.ShutdownRequested += DesktopOnShutdownRequested;
        }

        base.OnFrameworkInitializationCompleted();
    }
    
    private bool _isReadyToClose;

    private async void DesktopOnShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
    {
        e.Cancel = !_isReadyToClose;

        if (!_isReadyToClose)
        {
            IEnumerable<TodoItem> itemsToSave = _mainWindowViewModel.TodoItems.Select(item => item.Get());
            await FileService.SaveAsync(itemsToSave);

            _isReadyToClose = true;

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Shutdown();
            }
        }
    }
}