using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessApp.Chesspieces
{
    // Class representing a Queen chess piece implementing the IChesspiece interface
    public class Queen : IChesspiece
    {
        // Constructor for initializing a Queen with specific parameters
        public Queen(string imageSource, int currentRow, int currentColumn)
        {
            // Set the properties of the Queen
            Name = ChesspieceName.Queen;
            ImageSource = imageSource;
            CurrentRow = currentRow;
            CurrentColumn = currentColumn;
            IsCaptured = false;
            IsInCheck = false;
            IsInCheckmate = false;
            IsInStalemate = false;
        }

        // Default constructor for a Queen with no parameters
        public Queen()
        {
        }

        // Method to get the available squares for the Queen
        public List<ChessboardSquare> AvailableSquares(ChessboardSquare currentSquare,
            ObservableCollection<ChessboardSquare> chessboardSquares)
        {
            // List to store the available squares for the Queen
            List<ChessboardSquare> availableSquares = new List<ChessboardSquare>();

            // Diagonal moves
            int[] directionsRow = { 1, 1, -1, -1 };
            int[] directionsCol = { 1, -1, 1, -1 };

            for (int i = 0; i < directionsRow.Length; i++)
            {
                int newRow = currentSquare.Row;
                int newCol = currentSquare.Column;

                // Check each diagonal direction until the edge of the chessboard or a piece is encountered
                while (true)
                {
                    newRow += directionsRow[i];
                    newCol += directionsCol[i];

                    // Break if outside the chessboard boundaries
                    if (newRow < 0 || newRow > 7 || newCol < 0 || newCol > 7)
                        break;

                    ChessboardSquare potentialNewSquare = chessboardSquares.FirstOrDefault(s => s.Row == newRow && s.Column == newCol);

                    // If the square is empty, add it to available squares; if it has an opponent's piece, add it and break
                    if (potentialNewSquare.Chesspiece.Name == ChesspieceName.None)
                    {
                        availableSquares.Add(potentialNewSquare);
                    }
                    else
                    {
                        if (!Equals(potentialNewSquare.Chesspiece.Color, currentSquare.Chesspiece.Color))
                            availableSquares.Add(potentialNewSquare);

                        break;
                    }
                }
            }

            // Horizontal and vertical moves
            int[][] directions = { new[] { 0, 1 }, new[] { 1, 0 }, new[] { 0, -1 }, new[] { -1, 0 } };

            foreach (var dir in directions)
            {
                int newRow = currentSquare.Row;
                int newCol = currentSquare.Column;

                // Check each horizontal and vertical direction until the edge of the chessboard or a piece is encountered
                while (true)
                {
                    newRow += dir[0];
                    newCol += dir[1];

                    // Break if outside the chessboard boundaries
                    if (newRow < 0 || newRow > 7 || newCol < 0 || newCol > 7)
                        break;

                    ChessboardSquare potentialSquare = chessboardSquares.FirstOrDefault(s => s.Row == newRow && s.Column == newCol);
                    if (potentialSquare.Chesspiece.Name == ChesspieceName.None)
                    {
                        availableSquares.Add(potentialSquare);
                    }
                    else
                    {
                        // If the square has an opponent's piece, add it to available squares and break
                        if (potentialSquare.Chesspiece.Color != currentSquare.Chesspiece.Color)
                            availableSquares.Add(potentialSquare);

                        break;
                    }
                }
            }

            return availableSquares;
        }

        // Property to get or set the name of the Queen
        public ChesspieceName Name { get; set; }

        // Property to get or set the image source of the Queen
        public string ImageSource { get; set; }

        // Property to get the color of the Queen based on its image source
        public Color Color => ImageSource.EndsWith("w.png") ? Colors.White : Colors.Black;

        // Property to get or set the current row of the Queen on the chessboard
        public int CurrentRow { get; set; }

        // Property to get or set the current column of the Queen on the chessboard
        public int CurrentColumn { get; set; }

        // Property to get or set the captured status of the Queen
        public bool IsCaptured { get; set; }

        // Property to get or set whether the Queen is in check
        public bool IsInCheck { get; set; }

        // Property to get or set whether the Queen is in checkmate
        public bool IsInCheckmate { get; set; }

        // Property to get or set whether the Queen is in stalemate
        public bool IsInStalemate { get; set; }

        // Method to move the Queen to a new chessboard square
        public void Move(ChessboardSquare newSquare)
        {
            // Update the current row and column of the Queen
            CurrentRow = newSquare.Row;
            CurrentColumn = newSquare.Column;
        }

        // Method to check if the Queen can move from the old square to the new square
        public bool CanMove(ChessboardSquare oldSquare, ChessboardSquare newSquare, ObservableCollection<ChessboardSquare> chessboardSquares)
        {
            // Calculate the row and column differences
            int rowDifference = Math.Abs(newSquare.Row - oldSquare.Row);
            int columnDifference = Math.Abs(newSquare.Column - oldSquare.Column);

            // Check if the move is diagonal, horizontal, or vertical, and if the squares in between are empty
            return rowDifference == columnDifference || newSquare.Row == oldSquare.Row ||
                   newSquare.Column == oldSquare.Column && AvailableSquares(oldSquare, chessboardSquares).Contains(newSquare);
        }

        // Method representing the capture action (currently not implemented)
        public void Capture()
        {
            throw new NotImplementedException();
        }

        // Method representing the promotion action (currently not implemented)
        public void Promote()
        {
            throw new NotImplementedException();
        }
    }
}
