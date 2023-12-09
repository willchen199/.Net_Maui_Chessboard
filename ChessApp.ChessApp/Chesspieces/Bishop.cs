using System.Collections.ObjectModel;

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

    public bool CanMove(ChessboardSquare oldSquare, ChessboardSquare newSquare, ObservableCollection<ChessboardSquare> chessboardSquares)
    {
        int rowDifference = Math.Abs(newSquare.Row - oldSquare.Row);
        int columnDifference = Math.Abs(newSquare.Column - oldSquare.Column);

        return rowDifference == columnDifference && AvailableSquares(oldSquare, chessboardSquares).Contains(newSquare);
    }

    public List<ChessboardSquare> AvailableSquares(ChessboardSquare currentSquare, 
        ObservableCollection<ChessboardSquare> chessboardSquares)
    {
        List<ChessboardSquare> availableSquares = new List<ChessboardSquare>();
        int[][] directions = { new[] { 1, 1 }, new[] { 1, -1 }, new[] { -1, -1 }, new[] { -1, 1 } };
        
        foreach (var dir in directions)
        {
            int newRow = currentSquare.Row; // 7
            int newCol = currentSquare.Column; // 2

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

    public void Capture()
    {
        throw new NotImplementedException();
    }

    public void Promote()
    {
        throw new NotImplementedException();
    }
}