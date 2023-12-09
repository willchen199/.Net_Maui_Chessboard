using System.Collections.ObjectModel;
using ChessApp.Moving;

namespace ChessApp.Chesspieces;

public interface IChesspiece
{
    public List<ChessboardSquare> AvailableSquares(ChessboardSquare currentSquare,
        ObservableCollection<ChessboardSquare> chessboardSquares);
    public ChesspieceName Name { get; set; }
    public string ImageSource { get; set; }
    public Color Color { get; }
    public int CurrentRow { get; set; }
    public int CurrentColumn { get; set; }

    public bool IsCaptured { get; set; }
    public bool IsInCheck { get; set; }
    public bool IsInCheckmate { get; set; }
    public bool IsInStalemate { get; set; }

    public void Move(ChessboardSquare newSquare);

    public bool CanMove(ChessboardSquare oldSquare, ChessboardSquare newSquare, ObservableCollection<ChessboardSquare> chessboardSquares);
    public void Capture();
    public void Promote();
}