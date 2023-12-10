using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace ChessApp.Chesspieces
{
    // Class representing a Rook chess piece implementing the IChesspiece interface
    public class Rook : IChesspiece
    {
        // Constructor for initializing a Rook with specific parameters
        public Rook(string imageSource, int currentRow, int currentColumn)
        {
            // Set the properties of the Rook
            Name = ChesspieceName.Rook;
            ImageSource = imageSource;
            CurrentRow = currentRow;
            CurrentColumn = currentColumn;
            IsCaptured = false;
            IsInCheck = false;
            IsInCheckmate = false;
            IsInStalemate = false;
        }

        // Default constructor for a Rook with no parameters
        public Rook()
        {
        }

        // Method to get the available squares for the Rook
        public List<ChessboardSquare> AvailableSquares(ChessboardSquare currentSquare,
            ObservableCollection<ChessboardSquare> chessboardSquares)
        {
            // List to store the available squares for the Rook
            List<ChessboardSquare> availableSquares = new List<ChessboardSquare>();

            // Offsets for all possible moves of a Rook
            int[][] directions = { new[] { 0, 1 }, new[] { 1, 0 }, new[] { 0, -1 }, new[] { -1, 0 } };

            // Check each potential move of the Rook
            foreach (var dir in directions)
            {
                int newRow = currentSquare.Row;
                int newCol = currentSquare.Column;

                // Move in the specified direction until reaching the edge or an occupied square
                while (true)
                {
                    newRow += dir[0];
                    newCol += dir[1];

                    // Break if the potential move is outside the chessboard boundaries
                    if (newRow < 0 || newRow > 7 || newCol < 0 || newCol > 7)
                        break;

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

        // Property to get or set the name of the Rook
        public ChesspieceName Name { get; set; }

        // Property to get or set the image source of the Rook
        public string ImageSource { get; set; }

        // Property to get the color of the Rook based on its image source
        public Color Color => ImageSource.EndsWith("w.png") ? Colors.White : Colors.Black;

        // Property to get or set the current row of the Rook on the chessboard
        public int CurrentRow { get; set; }

        // Property to get or set the current column of the Rook on the chessboard
        public int CurrentColumn { get; set; }

        // Property to get or set the captured status of the Rook
        public bool IsCaptured { get; set; }

        // Property to get or set whether the Rook is in check
        public bool IsInCheck { get; set; }

        // Property to get or set whether the Rook is in checkmate
        public bool IsInCheckmate { get; set; }

        // Property to get or set whether the Rook is in stalemate
        public bool IsInStalemate { get; set; }

        // Method to move the Rook to a new chessboard square
        public void Move(ChessboardSquare newSquare)
        {
            // Update the current row and column of the Rook
            CurrentRow = newSquare.Row;
            CurrentColumn = newSquare.Column;
        }

        // Method to check if the Rook can move from the old square to the new square
        public bool CanMove(ChessboardSquare oldSquare, ChessboardSquare newSquare, ObservableCollection<ChessboardSquare> chessboardSquares)
        {
            // Check if the move is either along the same row or the same column and if the destination square is in the available squares
            return (newSquare.Row == oldSquare.Row || newSquare.Column == oldSquare.Column) && AvailableSquares(oldSquare, chessboardSquares).Contains(newSquare);
        }

        // Method representing the capture action (not implemented)
        public void Capture()
        {
            throw new NotImplementedException("Rook capture not implemented.");
        }

        // Method representing the promotion action (not implemented)
        public void Promote()
        {
            throw new NotImplementedException("Rook promotion not implemented.");
        }
    }
}
