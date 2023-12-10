using System.Collections.ObjectModel;

namespace ChessApp.Chesspieces
{
    // Interface representing a generic chess piece
    public interface IChesspiece
    {
        // Method to get the available squares for the chess piece
        List<ChessboardSquare> AvailableSquares(ChessboardSquare currentSquare,
            ObservableCollection<ChessboardSquare> chessboardSquares);
        
        /// <summary>
        /// I wrote this just had GPT comment it. 
        /// Checks if the current chess piece can capture the opponent's king.
        /// </summary>
        /// <param name="currentSquare">The current square occupied by the chess piece.</param>
        /// <param name="chessboardSquares">The collection of all chessboard squares.</param>
        /// <returns>True if the piece can capture the opponent's king, otherwise false.</returns>
        public virtual bool CanCaptureOpponentKing(ChessboardSquare currentSquare, ObservableCollection<ChessboardSquare> chessboardSquares)
        {
            // Get the available squares to which the chess piece can move.
            return AvailableSquares(currentSquare, chessboardSquares).Any(move =>
            {
                // Retrieve the chess piece on the destination square of the potential move.
                IChesspiece piece = chessboardSquares[move.Row * 8 + move.Column].Chesspiece;

                // Check if the destination square is occupied by a chess piece and it is a king.
                return piece != null && piece.Name == ChesspieceName.King;
            });
        }

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
