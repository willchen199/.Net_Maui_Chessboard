namespace ChessApp.Chesspieces;

public class King : IChesspiece
{
    public King(string imageSource, int currentRow, int currentColumn)
    {
        Name = ChesspieceName.King;
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
        CurrentRow = newSquare.Row;
        CurrentColumn = newSquare.Column;
    }

    public bool CanMove(ChessboardSquare oldSquare, ChessboardSquare newSquare)
    {
        int rowDifference = Math.Abs(newSquare.Row - oldSquare.Row);
        int columnDifference = Math.Abs(newSquare.Column - oldSquare.Column);

        return rowDifference <= 1 && columnDifference <= 1;
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