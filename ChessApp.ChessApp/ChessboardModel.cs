// namespace ChessApp
// {
//     /// <summary>
//     /// Represents a chessboardModel in a chess game.
//     /// </summary>
//     public class ChessboardModel
//     {
//         /// <summary>
//         /// Stores the Frame objects that represent the squares on the chessboardModel.
//         /// </summary>
//         private readonly Dictionary<(int Row, int Col), Frame> _squares = new();
//
//         /// <summary>
//         /// Initializes a new instance of the ChessboardModel class and populates it with squares and pieces.
//         /// </summary>
//         /// <param name="chessboardGrid">The Grid layout that will contain the chessboardModel.</param>
//         public ChessboardModel(Grid chessboardGrid)
//         {
//             // Initialize the chessboardModel by filling it with squares
//             for (int row = 0; row < 8; row++)
//                 for (int col = 0; col < 8; col++)
//                 {
//                     // Determine if the square is white or brown based on its row and column
//                     bool isWhiteSquare = (row + col) % 2 == 0;
//                     string squareImageSource = isWhiteSquare ? "white_chess_tile.png" : "brown_chess_tile.png";
//
//                     // Create an image for the square
//                     Image squareImage = new()
//                     {
//                         Source = ImageSource.FromFile(squareImageSource),
//                         Aspect = Aspect.AspectFill
//                     };
//
//                     // Create a Grid for the square to allow overlapping with chess pieces
//                     Grid squareGrid = new()
//                     {
//                         Children = { squareImage }
//                     };
//
//                     // Create a Frame for the square and bind its row and column context
//                     Frame square = new()
//                     {
//                         Content = squareGrid,
//                         Padding = new Thickness(0),
//                         BindingContext = new { Row = row, Col = col }
//                     };
//
//                     // Add the square to the Grid layout and store it in the dictionary
//                     chessboardGrid.Add(square, col, row);
//                     _squares.Add((row, col), square);
//
//                     // Add a DropGestureRecognizer to the square to handle piece drops
//                     DropGestureRecognizer dropGesture = new();
//                     dropGesture.Drop += OnDrop;
//                     square.GestureRecognizers.Add(dropGesture);
//
//                     // Initialize the chess piece on the square, if any
//                     if (InitialPiecePosition(row, col, out string pieceImage))
//                     {
//                         // Create an image for the chess piece
//                         Image piece = new Image
//                         {
//                             Source = pieceImage,
//                             Aspect = Aspect.AspectFit
//                         };
//
//                         // Add the piece to the square grid
//                         squareGrid.Children.Add(piece);
//
//                         // Add a DragGestureRecognizer to the piece to enable dragging
//                         DragGestureRecognizer dragGesture = new DragGestureRecognizer();
//                         dragGesture.DragStarting += OnDragStarting;
//                         Console.WriteLine("drag gesture thing");
//                         piece.GestureRecognizers.Add(dragGesture);
//                     }
//                 }
//         }
//
//         /// <summary>
//         /// Determines the initial position for a chess piece based on its row and column.
//         /// </summary>
//         private bool InitialPiecePosition(int row, int col, out string pieceImage)
//         {
//             pieceImage = InitialBishopPosition(row, col);
//             return pieceImage != null;
//         }
//
//         /// <summary>
//         /// Example method to place a bishop piece on a specific square.
//         /// </summary>
//         private string InitialBishopPosition(int row, int col)
//         {
//             if (row == 1 && col == 1)
//             {
//                 return "bishop_b.png";
//             }
//
//             return null;
//         }
//
//         /// <summary>
//         /// Handles the drop event for a chess piece.
//         /// </summary>
//         private void OnDrop(object sender, DropEventArgs e)
//         {
//             // Safely try to get the piece from the properties
//             if (e.Data.Properties.TryGetValue("Piece", out var pieceObj) && pieceObj is Image piece)
//             {
//                 // Try to get the parent Grid of the sender Element
//                 if (sender is Element element && element.Parent is Grid gridSquare)
//                 {
//                     if (originGridSquare != null)
//                     {
//                         originGridSquare.Children.Remove(piece);
//                         gridSquare.Children.Add(piece);
//                     }
//                 }
//             }
//             else
//             {
//                 // Log or handle the case where "Piece" is not available
//                 Console.WriteLine("No Piece key found in Properties.");
//             }
//         }
//
//
//         /// <summary>
//         /// Handles the drag starting event for a chess piece.
//         /// </summary>
//         private void OnDragStarting(object sender, DragStartingEventArgs e)
//         {
//             if (sender is Image piece)
//             {
//                 (int row, int col) initialSquare = GetInitialSquare(piece);
//                 originGridSquare = piece.Parent as Grid;
//
//                 Console.WriteLine($"OnDragStarting triggered. Sender: {sender}");
//                 e.Data.Properties["Piece"] = piece;
//                 Console.WriteLine($"Data Properties after adding Piece: {string.Join(", ", e.Data.Properties.Keys)}"); e.Data.Properties["InitialSquare"] = initialSquare;
//
//                 // Debugging: make sure this line prints when you begin dragging
//                 Console.WriteLine("Drag started for a piece.");
//             }
//         }
//
//         /// <summary>
//         /// Gets the initial square (row and column) of a chess piece.
//         /// </summary>
//         private (int row, int col) GetInitialSquare(Image piece)
//         {
//             // Implement logic to determine the initial square of the piece
//             return (0, 0);  // Placeholder
//         }
//         private Grid originGridSquare;
//     }
// }
//
