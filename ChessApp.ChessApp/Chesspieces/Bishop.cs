namespace ChessApp.Chesspieces;

public class Bishop : IChesspiece
{
    public Bishop(string imageSource, int currentRow, int currentColumn)
    {
        Name = ChesspieceName.Bishop;
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

        return rowDifference == columnDifference;
    }

    public bool HasClearPath(ChessboardSquare oldSquare, ChessboardSquare newSquare,
        List<ChessboardSquare> currentSquares)
    {
        int rowDifference = newSquare.Row - oldSquare.Row;
        int columnDifference = newSquare.Column - oldSquare.Column;

        int rowDirection = rowDifference > 0 ? 1 : -1;
        int columnDirection = columnDifference > 0 ? 1 : -1;

        int potentialNewRow = oldSquare.Row + rowDirection;
        int potentialNewColumn = oldSquare.Column + columnDirection;

        while (potentialNewRow != newSquare.Row && potentialNewColumn != newSquare.Column)
        {
            if (currentSquares.Any(s => s.Row == potentialNewRow &&
                                        s.Column == potentialNewColumn &&
                                        s.Chesspiece.Name != ChesspieceName.None))
                return false;

            potentialNewRow += rowDirection;
            potentialNewColumn += columnDirection;
        }

        return true;
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