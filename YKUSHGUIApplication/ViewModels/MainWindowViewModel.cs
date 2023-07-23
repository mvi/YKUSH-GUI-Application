using YKUSHGUIApplication.Services;

namespace YKUSHGUIApplication.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ITestService m_testService;
    public string Greeting => "Welcome to Avalonia!";

    public MainWindowViewModel(ITestService testService)
    {
        m_testService = testService;
    }
}