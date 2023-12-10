// Namespace declaration for the Position class in the Moving namespace
namespace ChessApp.Moving
{
    // Class representing a position on a two-dimensional grid
    public class Position
    {
        // Constructor to initialize a Position with specified row and column values
        public Position(int row, int column)
        {
            // Set the Row and Column properties
            Row = row;
            Column = column;
        }

        // Property to get or set the row value of the position
        public int Row { get; set; }

        // Property to get or set the column value of the position
        public int Column { get; set; }
    }
}
