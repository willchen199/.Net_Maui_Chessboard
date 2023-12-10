using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

// Namespace declaration for the Chesspieces in the ChessApp
namespace ChessApp.Chesspieces
{
    // Class representing a Bishop chess piece implementing the IChesspiece interface
    public class Bishop : IChesspiece
    {
        // Constructor for initializing a Bishop with specific parameters
        public Bishop(string imageSource, int currentRow, int currentColumn)
        {
            // Set the properties of the Bishop
            Name = ChesspieceName.Bishop;
            ImageSource = imageSource;
            CurrentRow = currentRow;
            CurrentColumn = currentColumn;
            IsCaptured = false;
            IsInCheck = false;
            IsInCheckmate = false;
            IsInStalemate = false;
        }

        // Default constructor for a Bishop with no parameters
        public Bishop()
        {
        }

        // Property to get or set the name of the Bishop
        public ChesspieceName Name { get; set; }

        // Property to get or set the image source of the Bishop
        public string ImageSource { get; set; }

        // Property to get the color of the Bishop based on its image source
        public Color Color => ImageSource.EndsWith("w.png") ? Colors.White : Colors.Black;

        // Property to get or set the current row of the Bishop on the chessboard
        public int CurrentRow { get; set; }

        // Property to get or set the current column of the Bishop on the chessboard
        public int CurrentColumn { get; set; }

        // Property to get or set the captured status of the Bishop
        public bool IsCaptured { get; set; }

        // Property to get or set whether the Bishop is in check
        public bool IsInCheck { get; set; }

        // Property to get or set whether the Bishop is in checkmate
        public bool IsInCheckmate { get; set; }

        // Property to get or set whether the Bishop is in stalemate
        public bool IsInStalemate { get; set; }

        // Method to move the Bishop to a new chessboard square
        public void Move(ChessboardSquare newSquare)
        {
            // Update the current row and column of the Bishop
            CurrentRow = newSquare.Row;
            CurrentColumn = newSquare.Column;
        }

        // Method to check if the Bishop can move from the old square to the new square
        public bool CanMove(ChessboardSquare oldSquare, ChessboardSquare newSquare, ObservableCollection<ChessboardSquare> chessboardSquares)
        {
            // Calculate the absolute differences in row and column
            int rowDifference = Math.Abs(newSquare.Row - oldSquare.Row);
            int columnDifference = Math.Abs(newSquare.Column - oldSquare.Column);

            // Check if the move is diagonal and if the destination square is in the available squares
            return rowDifference == columnDifference && AvailableSquares(oldSquare, chessboardSquares).Contains(newSquare);
        }

        // Method to get the available squares for the Bishop
        public List<ChessboardSquare> AvailableSquares(ChessboardSquare currentSquare,
            ObservableCollection<ChessboardSquare> chessboardSquares)
        {
            // List to store the available squares for the Bishop
            List<ChessboardSquare> availableSquares = new List<ChessboardSquare>();

            // Offsets for all possible diagonal moves of a Bishop
            int[][] directions = { new[] { 1, 1 }, new[] { 1, -1 }, new[] { -1, -1 }, new[] { -1, 1 } };

            // Iterate over each diagonal direction
            foreach (var dir in directions)
            {
                int newRow = currentSquare.Row; // Starting row for the current diagonal
                int newCol = currentSquare.Column; // Starting column for the current diagonal

                // Move in the specified diagonal direction until reaching the edge or an occupied square
                while (true)
                {
                    newRow += dir[0]; // Increment row in the current diagonal direction
                    newCol += dir[1]; // Increment column in the current diagonal direction

                    // Break if the potential move is outside the chessboard boundaries
                    if (newRow < 0 || newRow > 7 || newCol < 0 || newCol > 7)
                        break;

                    // Find the potential square on the chessboard
                    ChessboardSquare potentialSquare = chessboardSquares.FirstOrDefault(s => s.Row == newRow && s.Column == newCol);

                    // If the square is empty, add it to available squares; if occupied, add if it has an opponent's piece
                    if (potentialSquare.Chesspiece.Name == ChesspieceName.None)
                    {
                        availableSquares.Add(potentialSquare);
                    }
                    else
                    {
                        // Add the square if it contains an opponent's piece, then exit the loop
                        if (potentialSquare.Chesspiece.Color != currentSquare.Chesspiece.Color)
                            availableSquares.Add(potentialSquare);

                        break;
                    }
                }
            }

            return availableSquares;
        }

        // Method representing the capture action (not implemented)
        public void Capture()
        {
            throw new NotImplementedException("Bishop capture not implemented.");
        }

        // Method representing the promotion action (not implemented)
        public void Promote()
        {
            throw new NotImplementedException("Bishop promotion not implemented.");
        }
    }
}
