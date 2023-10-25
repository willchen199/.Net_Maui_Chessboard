namespace ChessApp
{
    /// <summary>
    /// Represents a chessboard in a chess game.
    /// </summary>
    public class Chessboard
    {
        /// <summary>
        /// Stores the Frame objects that represent the squares on the chessboard.
        /// </summary>
        private readonly Dictionary<(int Row, int Col), Frame> _squares = new();

        /// <summary>
        /// Initializes a new instance of the Chessboard class and populates it with squares and pieces.
        /// </summary>
        /// <param name="chessboardGrid">The Grid layout that will contain the chessboard.</param>
        public Chessboard(Grid chessboardGrid)
        {
            // Initialize the chessboard by filling it with squares
            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                {
                    // Determine if the square is white or brown based on its row and column
                    bool isWhiteSquare = (row + col) % 2 == 0;
                    string squareImageSource = isWhiteSquare ? "white_chess_tile.png" : "brown_chess_tile.png";

                    // Create an image for the square
                    Image squareImage = new()
                    {
                        Source = ImageSource.FromFile(squareImageSource),
                        Aspect = Aspect.AspectFill
                    };

                    // Create a Grid for the square to allow overlapping with chess pieces
                    Grid squareGrid = new()
                    {
                        Children = { squareImage }
                    };

                    // Create a Frame for the square and bind its row and column context
                    Frame square = new()
                    {
                        Content = squareGrid,
                        Padding = new Thickness(0),
                        BindingContext = new { Row = row, Col = col }
                    };

                    // Add the square to the Grid layout and store it in the dictionary
                    chessboardGrid.Add(square, col, row);
                    _squares.Add((row, col), square);

                    // Add a DropGestureRecognizer to the square to handle piece drops
                    DropGestureRecognizer dropGesture = new();
                    dropGesture.Drop += OnDrop;
                    square.GestureRecognizers.Add(dropGesture);

                    // Initialize the chess piece on the square, if any
                    if (InitialPiecePosition(row, col, out string pieceImage))
                    {
                        // Create an image for the chess piece
                        Image piece = new Image
                        {
                            Source = pieceImage,
                            Aspect = Aspect.AspectFit
                        };

                        // Add the piece to the square grid
                        squareGrid.Children.Add(piece);

                        // Add a DragGestureRecognizer to the piece to enable dragging
                        DragGestureRecognizer dragGesture = new DragGestureRecognizer();
                        dragGesture.DragStarting += OnDragStarting;
                        piece.GestureRecognizers.Add(dragGesture);
                    }
                }
        }

        /// <summary>
        /// Determines the initial position for a chess piece based on its row and column.
        /// </summary>
        private bool InitialPiecePosition(int row, int col, out string pieceImage)
        {
            pieceImage = InitialBishopPosition(row, col);
            return pieceImage != null;
        }

        /// <summary>
        /// Example method to place a bishop piece on a specific square.
        /// </summary>
        private string InitialBishopPosition(int row, int col)
        {
            if (row == 1 && col == 1)
            {
                return "bishop_b.png";
            }

            return null;
        }

        /// <summary>
        /// Handles the drop event for a chess piece.
        /// </summary>
        private void OnDrop(object sender, DropEventArgs e)
        {
            // Check that the sender is a Frame with a Grid content that contains a piece
            if (sender is Frame frame && frame.Content is Grid squareGrid &&
                e.Data.Properties.TryGetValue("Piece", out var piece) && piece is Image image)
            {
                dynamic bindingContext = frame.BindingContext;
                int targetRow = bindingContext.Row;
                int targetCol = bindingContext.Col;

                // Add game logic here to validate the move
                squareGrid.Children.Add(image);
            }
        }

        /// <summary>
        /// Handles the drag starting event for a chess piece.
        /// </summary>
        private void OnDragStarting(object sender, DragStartingEventArgs e)
        {
            // Check that the sender is an Image (chess piece)
            if (sender is Image piece)
            {
                var initialSquare = GetInitialSquare(piece);
                e.Data.Properties.Add("Piece", piece);
                e.Data.Properties.Add("InitialSquare", initialSquare);

                // Remove the piece from its original square
                if (piece.Parent is Grid originalGrid)
                {
                    originalGrid.Children.Remove(piece);
                }

                // Set the data to indicate a drag operation for a chess piece
                e.Data.Text = "Chess Piece";
            }
        }

        /// <summary>
        /// Gets the initial square (row and column) of a chess piece.
        /// </summary>
        private (int row, int col) GetInitialSquare(Image piece)
        {
            // Implement logic to determine the initial square of the piece
            return (0, 0);  // Placeholder
        }
    }
}
