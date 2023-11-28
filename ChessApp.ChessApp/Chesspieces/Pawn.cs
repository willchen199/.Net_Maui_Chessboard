namespace ChessApp.Chesspieces;

public class Pawn : IChesspiece
{
    public Pawn(string imageSource, int currentRow, int currentColumn)
    {
        Name = ChesspieceName.Pawn;
        ImageSource = imageSource;

        CurrentRow = currentRow;
        CurrentColumn = currentColumn;
        IsFirstMove = true;
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
    private bool IsFirstMove { get; set; }
    public bool IsInCheck { get; set; }
    public bool IsInCheckmate { get; set; }
    public bool IsInStalemate { get; set; }


    public void Move(ChessboardSquare newSquare)
    {
        CurrentRow = newSquare.Row;
        CurrentColumn = newSquare.Column;
        if (IsFirstMove)
            IsFirstMove = false;
    }

    public bool CanMove(ChessboardSquare oldSquare, ChessboardSquare newSquare)
    {
        int rowDifference = Equals(Color, Colors.White) ? oldSquare.Row - newSquare.Row : newSquare.Row - oldSquare.Row;
        int columnDifference = Math.Abs(newSquare.Column - oldSquare.Column);

        // Standard move
        if (columnDifference == 0 &&
            (((CurrentRow == 6 || CurrentRow == 1) &&
              rowDifference == 2) || rowDifference == 1) &&
            newSquare.Chesspiece.Name == ChesspieceName.None)
            return true;

        // Capture move 
        if (columnDifference == 1 && rowDifference == 1 &&
            newSquare.Chesspiece.Name != ChesspieceName.None &&
            !Equals(newSquare.Chesspiece.Color, Color))
            return true;

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