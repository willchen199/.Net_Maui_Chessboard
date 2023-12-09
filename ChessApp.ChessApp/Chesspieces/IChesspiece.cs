using ChessApp.Moving;

namespace ChessApp.Chesspieces;

public interface IChesspiece
{
    public ChesspieceName Name { get; set; }
    public string ImageSource { get; set; }
    public Color Color { get; }
    public int CurrentRow { get; set; }
    public int CurrentColumn { get; set; }
    public Position CurrentPosition => new(CurrentRow, CurrentColumn);

    public bool HasClearPath(ChessboardSquare oldSquare, ChessboardSquare newSquare,
        List<ChessboardSquare> currentSquares);

    public bool IsCaptured { get; set; }
    public bool IsInCheck { get; set; }
    public bool IsInCheckmate { get; set; }
    public bool IsInStalemate { get; set; }

    public void Move(ChessboardSquare newSquare);

    public bool CanMove(ChessboardSquare oldSquare, ChessboardSquare newSquare);
    public void Capture();
    public void Promote();
}