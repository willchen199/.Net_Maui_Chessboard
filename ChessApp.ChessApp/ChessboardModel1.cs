using ChessApp.ChessPieces;

namespace ChessApp;

public class ChessboardModel1
{
    public ChessboardModel1()
    {
        // Initialize the chessboard squares
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                bool isWhiteSquare = (row + col) % 2 == 0;
                ChessboardSquareModel chessboardSquare = new(row, col, isWhiteSquare ? "white_chess_tile.png" : "brown_chess_tile.png");

                // Set starting pieces if they exist on this square
                if (IsStartingSquareForPiece(row, col, out ChessPieceModel piece))
                    chessboardSquare.Piece = piece;

                Squares[(row, col)] = chessboardSquare;
            }
        }
    }

    public Dictionary<(int Row, int Col), ChessboardSquareModel> Squares { get; } = new();

    private bool IsStartingSquareForPiece(int row, int col, out ChessPieceModel piece)
    {
        piece = null;

        // For example, let's place a black rook on (0, 0) and a white rook on (7, 7):
        if (row == 0 && col == 0)
        {
            piece = new Bishop("bishop_b.png", "black");
            return true;
        }

        return piece != null;
    }
}