using System.Collections.ObjectModel;

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

    public List<ChessboardSquare> AvailableSquares(ChessboardSquare currentSquare,
        ObservableCollection<ChessboardSquare> chessboardSquares)
    {
        List<ChessboardSquare> availableSquares = new();
        int[] rowOffsets = new int[] { -1, -1, -1, 0, 1, 1, 1, 0 };
        int[] colOffsets = new int[] { -1, 0, 1, 1, 1, 0, -1, -1 };

        for (int i = 0; i < 8; i++)
        {
            int newRow = currentSquare.Row + rowOffsets[i];
            int newCol = currentSquare.Column + colOffsets[i];

            if (newRow >= 0 && newRow <= 7 && newCol >= 0 && newCol <= 7)
            {
                ChessboardSquare potentialSquare = chessboardSquares.FirstOrDefault(s => s.Row == newRow && s.Column == newCol);
                if (potentialSquare.Chesspiece.Name == ChesspieceName.None || potentialSquare.Chesspiece.Color != currentSquare.Chesspiece.Color)
                {
                    availableSquares.Add(potentialSquare);
                }
            }
        }

        return availableSquares;
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

    public bool CanMove(ChessboardSquare oldSquare, ChessboardSquare newSquare, ObservableCollection<ChessboardSquare> chessboardSquares)
    {
        int rowDifference = Math.Abs(newSquare.Row - oldSquare.Row);
        int columnDifference = Math.Abs(newSquare.Column - oldSquare.Column);

        return rowDifference <= 1 && columnDifference <= 1 && AvailableSquares(oldSquare, chessboardSquares).Contains(newSquare);
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