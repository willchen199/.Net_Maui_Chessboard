namespace ChessApp;

public class Chessboard
{
    private readonly Dictionary<(int Row, int Col), Frame> _squares = new();

    public Chessboard(Grid chessboardGrid)
    {
        for (int row = 0; row < 8; row++)
        for (int col = 0; col < 8; col++)
        {
            bool isWhiteSquare = (row + col) % 2 == 0;
            string squareImageSource = isWhiteSquare ? "white_chess_tile.png" : "brown_chess_tile.png";
            Image squareImage = new()
            {
                Source = ImageSource.FromFile(squareImageSource),
                Aspect = Aspect.AspectFill
            };

            // Creates each square grid (allows us to overlap a chess piece image on top of the square)
            Grid squareGrid = new()
            {
                Children = { squareImage }
            };

            Frame square = new()
            {
                Content = squareGrid, Padding 
                    = new Thickness(0)
            };

            chessboardGrid.Add(square, col, row);
            _squares.Add((row, col), square);

            DropGestureRecognizer dropGesture = new();
            dropGesture.Drop += OnDrop;
            square.GestureRecognizers.Add(dropGesture);

            // Actually creates the image
            if (InitialPiecePosition(row, col, out string pieceImage))
            {
                Image piece = new Image
                {
                    Source = pieceImage,
                    Aspect = Aspect.AspectFit
                };
                squareGrid.Children.Add(piece); 

                DragGestureRecognizer dragGesture = new DragGestureRecognizer();
                dragGesture.DragStarting += OnDragStarting;
                piece.GestureRecognizers.Add(dragGesture);
            }
        }
    }

    private bool InitialPiecePosition(int row, int col, out string pieceImage)
    {
        pieceImage = InitialBishopPosition(row, col);
        return pieceImage != null;
    }

    private string InitialBishopPosition(int row, int col)
    {
        if (row == 1 && col == 1)
        {
            return "bishop_b.png";
        }

        return null;
    }

    private void OnDrop(object sender, DropEventArgs e)
    {
        if (sender is Frame frame && frame.Content is Grid squareGrid && 
            e.Data.Properties.TryGetValue("Piece", out var piece) && piece is Image image)
        {
            squareGrid.Children.Add(image);
        }    
    }

    private void OnDragStarting(object sender, DragStartingEventArgs e)
    {
        if (sender is Image piece)
        {
            e.Data.Properties.Add("Piece", piece);
            if (piece.Parent is Grid originalGrid)
            {
                originalGrid.Children.Remove(piece);
            }
            e.Data.Text = "Chess Piece";  // Optionally, set the DataPackage text to describe the item being dragged
        }
    }

    private Image chessPiece;
    
}