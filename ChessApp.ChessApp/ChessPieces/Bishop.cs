namespace ChessApp.ChessPieces;

public class Bishop : ChessPieceModel
{
    public Bishop(string image, string color)
    {
        Image = image;
        Color = color;
    }

    public string Color { get; set; }
}