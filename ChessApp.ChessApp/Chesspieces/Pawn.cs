using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessApp.Chesspieces
{
    // Class representing a Pawn chess piece implementing the IChesspiece interface
    public class Pawn : IChesspiece
    {
        // Constructor for initializing a Pawn with specific parameters
        public Pawn(string imageSource, int currentRow, int currentColumn)
        {
            // Set the properties of the Pawn
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

        // Default constructor for a Pawn with no parameters
        public Pawn()
        {
        }

        // Method to get the available squares for the Pawn
        public List<ChessboardSquare> AvailableSquares(ChessboardSquare currentSquare,
            ObservableCollection<ChessboardSquare> chessboardSquares)
        {
            // List to store the available squares for the Pawn
            List<ChessboardSquare> availableSquares = new List<ChessboardSquare>();

            // Determine the direction based on the Pawn's color
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

        // Property to get or set the name of the Pawn
        public ChesspieceName Name { get; set; }

        // Property to get or set the image source of the Pawn
        public string ImageSource { get; set; }

        // Property to get the color of the Pawn based on its image source
        public Color Color => ImageSource.EndsWith("w.png") ? Colors.White : Colors.Black;

        // Property to get or set the current row of the Pawn on the chessboard
        public int CurrentRow { get; set; }

        // Property to get or set the current column of the Pawn on the chessboard
        public int CurrentColumn { get; set; }

        // Property to get or set the captured status of the Pawn
        public bool IsCaptured { get; set; }

        // Private property to track whether the Pawn has made its first move
        private bool IsFirstMove { get; set; }

        // Property to get or set whether the Pawn is in check
        public bool IsInCheck { get; set; }

        // Property to get or set whether the Pawn is in checkmate
        public bool IsInCheckmate { get; set; }

        // Property to get or set whether the Pawn is in stalemate
        public bool IsInStalemate { get; set; }

        // Method to move the Pawn to a new chessboard square
        public void Move(ChessboardSquare newSquare)
        {
            // Update the current row and column of the Pawn
            CurrentRow = newSquare.Row;
            CurrentColumn = newSquare.Column;

            // If it's the first move, update the first move status
            if (IsFirstMove)
                IsFirstMove = false;
        }

        // Method to check if the Pawn can move from the old square to the new square
        public bool CanMove(ChessboardSquare oldSquare, ChessboardSquare newSquare, ObservableCollection<ChessboardSquare> chessboardSquares)
        {
            // Calculate the row and column differences
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
