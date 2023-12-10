using System.Collections.ObjectModel;
using ChessApp.Moving;

namespace ChessApp.Chesspieces
{
    // Interface representing a generic chess piece
    public interface IChesspiece
    {
        // Method to get the available squares for the chess piece
        List<ChessboardSquare> AvailableSquares(ChessboardSquare currentSquare,
            ObservableCollection<ChessboardSquare> chessboardSquares);

        // Property to get or set the name of the chess piece
        public ChesspieceName Name { get; set; }

        // Property to get or set the image source of the chess piece
        public string ImageSource { get; set; }

        // Property to get the color of the chess piece based on its image source
        public Color Color { get; }

        // Property to get or set the current row of the chess piece on the chessboard
        public int CurrentRow { get; set; }

        // Property to get or set the current column of the chess piece on the chessboard
        public int CurrentColumn { get; set; }

        // Property to get or set the captured status of the chess piece
        public bool IsCaptured { get; set; }

        // Property to get or set whether the chess piece is in check
        public bool IsInCheck { get; set; }

        // Property to get or set whether the chess piece is in checkmate
        public bool IsInCheckmate { get; set; }

        // Property to get or set whether the chess piece is in stalemate
        public bool IsInStalemate { get; set; }

        // Method to move the chess piece to a new chessboard square
        public void Move(ChessboardSquare newSquare);

        // Method to check if the chess piece can move from the old square to the new square
        public bool CanMove(ChessboardSquare oldSquare, ChessboardSquare newSquare, ObservableCollection<ChessboardSquare> chessboardSquares);

        // Method representing the capture action
        public void Capture();

        // Method representing the promotion action
        public void Promote();
    }
}
