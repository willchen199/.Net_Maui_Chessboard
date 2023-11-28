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
    public bool IsCaptured { get; set; }
    public bool IsInCheck { get; set; }
    public bool IsInCheckmate { get; set; }
    public bool IsInStalemate { get; set; }
    public bool IsInCheckmateAndStalemate { get; set; }
    public bool IsInCheckAndCheckmateAndStalemate { get; set; }
    public bool IsInCheckOrCheckmateOrStalemate { get; set; }
    public bool IsInCheckAndCheckmateOrStalemate { get; set; }
    public bool IsInCheckOrCheckmateAndStalemate { get; set; }
    public bool IsInCheckOrCheckmateOrStalemateOrNone { get; set; }
    public bool IsInCheckAndCheckmateOrStalemateOrNone { get; set; }
    public bool IsInCheckOrCheckmateAndStalemateOrNone { get; set; }

    public void Move(ChessboardSquare newSquare);
    public bool CanMove(ChessboardSquare oldSquare, ChessboardSquare newSquare);
    public void Capture();
    public void Promote();
}