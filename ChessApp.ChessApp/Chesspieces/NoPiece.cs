using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ChessApp.Chesspieces
{
    // Class representing a placeholder for no chess piece, implementing the IChesspiece interface
    public class NoPiece : IChesspiece
    {
        // Constructor for initializing a NoPiece with specific parameters
        public NoPiece(string imageSource, int currentRow, int currentColumn)
        {
            // Set the properties of the NoPiece
            Name = ChesspieceName.None;
            ImageSource = imageSource;
            CurrentRow = currentRow;
            CurrentColumn = currentColumn;
            IsCaptured = false;
            IsInCheck = false;
            IsInCheckmate = false;
            IsInStalemate = false;
        }

        // Default constructor for a NoPiece with no parameters
        public NoPiece()
        {
        }

        // Method to get the available squares for the NoPiece (null since it doesn't move)
        public List<ChessboardSquare> AvailableSquares(ChessboardSquare currentSquare,
            ObservableCollection<ChessboardSquare> chessboardSquares)
        {
            // No available squares for NoPiece (null)
            return null;
        }

        // Property to get or set the name of the NoPiece
        public ChesspieceName Name { get; set; }

        // Property to get or set the image source of the NoPiece
        public string ImageSource { get; set; }

        // Property to get the color of the NoPiece based on its image source
        public Color Color => ImageSource.EndsWith("w.png") ? Colors.White : Colors.Black;

        // Property to get or set the current row of the NoPiece on the chessboard
        public int CurrentRow { get; set; }

        // Property to get or set the current column of the NoPiece on the chessboard
        public int CurrentColumn { get; set; }

        // Property to get or set the captured status of the NoPiece
        public bool IsCaptured { get; set; }

        // Property to get or set whether the NoPiece is in check
        public bool IsInCheck { get; set; }

        // Property to get or set whether the NoPiece is in checkmate
        public bool IsInCheckmate { get; set; }

        // Property to get or set whether the NoPiece is in stalemate
        public bool IsInStalemate { get; set; }

        // Method to move the NoPiece to a new chessboard square (not implemented)
        public void Move(ChessboardSquare newSquare)
        {
            throw new NotImplementedException("NoPiece cannot be moved.");
        }

        // Method to check if the NoPiece can move from the old square to the new square (always false)
        public bool CanMove(ChessboardSquare oldSquare, ChessboardSquare newSquare, ObservableCollection<ChessboardSquare> chessboardSquares)
        {
            return false;
        }

        // Method representing the capture action (not implemented)
        public void Capture()
        {
            throw new NotImplementedException("NoPiece cannot be captured.");
        }

        // Method representing the promotion action (not implemented)
        public void Promote()
        {
            throw new NotImplementedException("NoPiece cannot be promoted.");
        }
    }
}
