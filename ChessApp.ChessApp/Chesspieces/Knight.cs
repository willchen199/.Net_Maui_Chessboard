using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace ChessApp.Chesspieces
{
    // Class representing a Knight chess piece implementing the IChesspiece interface
    public class Knight : IChesspiece
    {
        // Constructor for initializing a Knight with specific parameters
        public Knight(string imageSource, int currentRow, int currentColumn)
        {
            // Set the properties of the Knight
            Name = ChesspieceName.Knight;
            ImageSource = imageSource;
            CurrentRow = currentRow;
            CurrentColumn = currentColumn;
            IsCaptured = false;
            IsInCheck = false;
            IsInCheckmate = false;
            IsInStalemate = false;
        }

        // Default constructor for a Knight with no parameters
        public Knight()
        {
        }

        // Method to get the available squares for the Knight
        public List<ChessboardSquare> AvailableSquares(ChessboardSquare currentSquare,
            ObservableCollection<ChessboardSquare> chessboardSquares)
        {
            // List to store the available squares for the Knight
            List<ChessboardSquare> availableSquares = new List<ChessboardSquare>();

            // Offsets for all possible moves of a Knight
            int[] rowOffsets = { -2, -1, 1, 2, 2, 1, -1, -2 };
            int[] colOffsets = { 1, 2, 2, 1, -1, -2, -2, -1 };

            // Check each potential move of the Knight
            for (int i = 0; i < 8; i++)
            {
                int newRow = currentSquare.Row + rowOffsets[i];
                int newCol = currentSquare.Column + colOffsets[i];

                // Ensure the potential move is within the chessboard boundaries
                if (newRow >= 0 && newRow <= 7 && newCol >= 0 && newCol <= 7)
                {
                    ChessboardSquare potentialSquare = chessboardSquares.FirstOrDefault(s => s.Row == newRow && s.Column == newCol);

                    // If the square is empty or has an opponent's piece, add it to available squares
                    if (potentialSquare.Chesspiece.Name == ChesspieceName.None || potentialSquare.Chesspiece.Color != currentSquare.Chesspiece.Color)
                    {
                        availableSquares.Add(potentialSquare);
                    }
                }
            }

            return availableSquares;
        }

        // Property to get or set the name of the Knight
        public ChesspieceName Name { get; set; }

        // Property to get or set the image source of the Knight
        public string ImageSource { get; set; }

        // Property to get the color of the Knight based on its image source
        public Color Color => ImageSource.EndsWith("w.png") ? Colors.White : Colors.Black;

        // Property to get or set the current row of the Knight on the chessboard
        public int CurrentRow { get; set; }

        // Property to get or set the current column of the Knight on the chessboard
        public int CurrentColumn { get; set; }

        // Property to get or set the captured status of the Knight
        public bool IsCaptured { get; set; }

        // Property to get or set whether the Knight is in check
        public bool IsInCheck { get; set; }

        // Property to get or set whether the Knight is in checkmate
        public bool IsInCheckmate { get; set; }

        // Property to get or set whether the Knight is in stalemate
        public bool IsInStalemate { get; set; }

        // Method to move the Knight to a new chessboard square
        public void Move(ChessboardSquare newSquare)
        {
            // Update the current row and column of the Knight
            CurrentRow = newSquare.Row;
            CurrentColumn = newSquare.Column;
        }

        // Method to check if the Knight can move from the old square to the new square
        public bool CanMove(ChessboardSquare oldSquare, ChessboardSquare newSquare, ObservableCollection<ChessboardSquare> chessboardSquares)
        {
            // Calculate the row and column differences
            int rowDifference = Math.Abs(newSquare.Row - oldSquare.Row);
            int columnDifference = Math.Abs(newSquare.Column - oldSquare.Column);

            // Check if the move is a valid L-shaped Knight move and if the destination square is in the available squares
            return (rowDifference == 2 && columnDifference == 1) || (rowDifference == 1 && columnDifference == 2) && AvailableSquares(oldSquare, chessboardSquares).Contains(newSquare);
        }

        // Method representing the capture action (not implemented)
        public void Capture()
        {
            throw new NotImplementedException("Knight capture not implemented.");
        }

        // Method representing the promotion action (not implemented)
        public void Promote()
        {
            throw new NotImplementedException("Knight promotion not implemented.");
        }
    }
}
