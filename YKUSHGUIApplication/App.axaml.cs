using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using YKUSHGUIApplication.Services;
using YKUSHGUIApplication.ViewModels;

namespace YKUSHGUIApplication;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Initialize the dependencies
        var test = new DummyService();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(test),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}