using System.Collections.Generic;
using System.Collections.ObjectModel;
using YKUSHGUI.Models;
using YKUSHGUI.Services;

namespace YKUSHGUI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IBoardStatusService boardStatusService;

    public ObservableCollection<BoardInfo> Boards { get; }

    public MainWindowViewModel(IBoardStatusService boardStatusService)
    {
        Boards = new ObservableCollection<BoardInfo>();
        this.boardStatusService = boardStatusService;
        this.boardStatusService.Start(Boards);
    }

    public MainWindowViewModel()
    {
        Boards = new ObservableCollection<BoardInfo> {new() {Serial = "Hello", Type = BoardType.ykush3}};
        boardStatusService = new DummyService();
    }
}