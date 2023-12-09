using System.Collections.ObjectModel;

namespace ChessApp.Chesspieces;

public class Rook : IChesspiece
{
    public Rook(string imageSource, int currentRow, int currentColumn)
    {
        Name = ChesspieceName.Rook;
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
        int[][] directions = { new[] { 0, 1 }, new[] { 1, 0 }, new[] { 0, -1 }, new[] { -1, 0 } };

        foreach (var dir in directions)
        {
            int newRow = currentSquare.Row;
            int newCol = currentSquare.Column;

            while (true)
            {
                newRow += dir[0];
                newCol += dir[1];

                if (newRow < 0 || newRow > 7 || newCol < 0 || newCol > 7)
                    break;

                ChessboardSquare potentialSquare = chessboardSquares.FirstOrDefault(s => s.Row == newRow && s.Column == newCol);
                if (potentialSquare.Chesspiece.Name == ChesspieceName.None)
                {
                    availableSquares.Add(potentialSquare);
                }
                else
                {
                    if (potentialSquare.Chesspiece.Color != currentSquare.Chesspiece.Color)
                        availableSquares.Add(potentialSquare);
                
                    break;
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
        return (newSquare.Row == oldSquare.Row || newSquare.Column == oldSquare.Column) && AvailableSquares(oldSquare, chessboardSquares).Contains(newSquare);
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