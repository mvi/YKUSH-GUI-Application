using System.Collections.ObjectModel;
using YKUSHGUI.Models;

namespace YKUSHGUI.Services;

public class BoardStatusService : IBoardStatusService
{
    public void Start(ObservableCollection<BoardInfo> boards)
    {
        void UpdateBoards(BoardType boardType)
        {
            foreach (var serial in YKUSHCommand.ListAttachedBoards(boardType))
            {
                boards.Add(new BoardInfo
                {
                    Serial = serial,
                    Type = boardType
                });
            }
        }

        UpdateBoards(BoardType.ykush);
        UpdateBoards(BoardType.ykush3);
        UpdateBoards(BoardType.ykushxs);
    }
}