using System.Collections.ObjectModel;
using YKUSHGUI.Models;

namespace YKUSHGUI.Services;

public interface IBoardStatusService
{
    void Start(ObservableCollection<BoardInfo> test);
}