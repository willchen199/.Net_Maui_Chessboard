namespace ChessApp.Chesspieces;

public class NoPiece : IChesspiece
{
    public NoPiece(string imageSource, int currentRow, int currentColumn)
    {
        Name = ChesspieceName.None;
        ImageSource = imageSource;

        CurrentRow = currentRow;
        CurrentColumn = currentColumn;
        IsCaptured = false;
        IsInCheck = false;
        IsInCheckmate = false;
        IsInStalemate = false;
    }

    public ChesspieceName Name { get; set; }
    public string ImageSource { get; set; }
    public Color Color => ImageSource.EndsWith("w.png") ? Colors.White : Colors.Black;
    public int CurrentRow { get; set; }
    public int CurrentColumn { get; set; }

    public bool HasClearPath(ChessboardSquare oldSquare, ChessboardSquare newSquare,
        List<ChessboardSquare> currentSquares)
    {
        return true;
    }

    public bool IsCaptured { get; set; }
    public bool IsInCheck { get; set; }
    public bool IsInCheckmate { get; set; }
    public bool IsInStalemate { get; set; }

    public void Move(ChessboardSquare newSquare)
    {
        throw new NotImplementedException();
    }

    public bool CanMove(ChessboardSquare oldSquare, ChessboardSquare newSquare)
    {
        return false;
    }

    public void Capture()
    {
        throw new NotImplementedException();
    }

    public void Promote()
    {
        throw new NotImplementedException();
    }
}