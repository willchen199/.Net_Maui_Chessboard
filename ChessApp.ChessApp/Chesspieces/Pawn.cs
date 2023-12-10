using System.Collections.ObjectModel;

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

    public Pawn()
    {
    }

    public List<ChessboardSquare> AvailableSquares(ChessboardSquare currentSquare,
        ObservableCollection<ChessboardSquare> chessboardSquares)
    {
        List<ChessboardSquare> availableSquares = new();
        int direction = !Equals(Color, Colors.White) ? 1 : -1;
        int startRow = !Equals(Color, Colors.White) ? 1 : 6;

        // Forward move
        int newRow = currentSquare.Row + direction;
        if (newRow >= 0 && newRow <= 7 && chessboardSquares.FirstOrDefault(s => s.Row == newRow && s.Column == currentSquare.Column).Chesspiece.Name == ChesspieceName.None)
        {
            availableSquares.Add(chessboardSquares.FirstOrDefault(s => s.Row == newRow && s.Column == currentSquare.Column));

            // Double move from start position
            if (IsFirstMove && currentSquare.Row == startRow)
            {
                newRow += direction;
                if (chessboardSquares.FirstOrDefault(s => s.Row == newRow && s.Column == currentSquare.Column).Chesspiece.Name == ChesspieceName.None)
                {
                    availableSquares.Add(chessboardSquares.FirstOrDefault(s => s.Row == newRow && s.Column == currentSquare.Column));
                }
            }
        }

        // Diagonal captures
        int[] captureColumns = new int[] { currentSquare.Column - 1, currentSquare.Column + 1 };
        foreach (int col in captureColumns)
        {
            if (col >= 0 && col <= 7)
            {
                ChessboardSquare captureSquare = chessboardSquares.FirstOrDefault(s => s.Row == newRow && s.Column == col);
                if (captureSquare.Chesspiece.Name != ChesspieceName.None && !Equals(captureSquare.Chesspiece.Color, currentSquare.Chesspiece.Color))
                {
                    availableSquares.Add(captureSquare);
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

    public bool CanMove(ChessboardSquare oldSquare, ChessboardSquare newSquare, ObservableCollection<ChessboardSquare> chessboardSquares)
    {
        int rowDifference = Equals(Color, Colors.White) ? oldSquare.Row - newSquare.Row : newSquare.Row - oldSquare.Row;
        int columnDifference = Math.Abs(newSquare.Column - oldSquare.Column);

        // Standard move
        if (columnDifference == 0 &&
            (((CurrentRow == 6 || CurrentRow == 1) &&
              rowDifference == 2) || rowDifference == 1) &&
            newSquare.Chesspiece.Name == ChesspieceName.None && newSquare.Chesspiece.Name == ChesspieceName.None && AvailableSquares(oldSquare, chessboardSquares).Contains(newSquare))
            return true;

        // Capture move 
        if (columnDifference == 1 && rowDifference == 1 &&
            newSquare.Chesspiece.Name != ChesspieceName.None &&
            !Equals(newSquare.Chesspiece.Color, Color) && AvailableSquares(oldSquare, chessboardSquares).Contains(newSquare))
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