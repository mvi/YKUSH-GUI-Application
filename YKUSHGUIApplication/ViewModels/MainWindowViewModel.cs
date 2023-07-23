using System.Collections.Generic;
using System.Collections.ObjectModel;
using YKUSHGUIApplication.Models;
using YKUSHGUIApplication.Services;

namespace YKUSHGUIApplication.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ITestService m_testService;

    public ObservableCollection<BoardInfo> Test { get; set; } = new();

    public MainWindowViewModel(ITestService testService)
    {
        Test = new ObservableCollection<BoardInfo> {new() {Serial = "Hello", Type = BoardType.ykush3}};
        m_testService = testService;
    }

    public MainWindowViewModel()
    {
        Test = new ObservableCollection<BoardInfo> {new() {Serial = "Hello", Type = BoardType.ykush3}};
        m_testService = new DummyService();
    }
}