namespace ChessApp.Chesspieces;

public class NoPiece : IChesspiece
{
    public NoPiece( string imageSource, int currentRow, int currentColumn)
    {
        Name = ChesspieceName.None;
        ImageSource = imageSource;
        
        CurrentRow = currentRow;
        CurrentColumn = currentColumn;
        IsCaptured = false;
        IsInCheck = false;
        IsInCheckmate = false;
        IsInStalemate = false;
        IsInCheckmateAndStalemate = false;
        IsInCheckAndCheckmateAndStalemate = false;
        IsInCheckOrCheckmateOrStalemate = false;
        IsInCheckAndCheckmateOrStalemate = false;
        IsInCheckOrCheckmateAndStalemate = false;
        IsInCheckOrCheckmateOrStalemateOrNone = false;
        IsInCheckAndCheckmateOrStalemateOrNone = false;
        IsInCheckOrCheckmateAndStalemateOrNone = false;
    }
    public ChesspieceName Name { get; set; }
    public string ImageSource { get; set; }
    public Color Color { get; set; }
    public int CurrentRow { get; set; }
    public int CurrentColumn { get; set; }
    public bool IsCaptured { get; set; }
    public bool IsInCheck { get; set; }
    public bool IsInCheckmate { get; set; }
    public bool IsInStalemate { get; set; }
    public bool IsInCheckmateAndStalemate { get; set; }
    public bool IsInCheckAndCheckmateAndStalemate { get; set; }
    public bool IsInCheckOrCheckmateOrStalemate { get; set; }
    public bool IsInCheckAndCheckmateOrStalemate { get; set; }
    public bool IsInCheckOrCheckmateAndStalemate { get; set; }
    public bool IsInCheckOrCheckmateOrStalemateOrNone { get; set; }
    public bool IsInCheckAndCheckmateOrStalemateOrNone { get; set; }
    public bool IsInCheckOrCheckmateAndStalemateOrNone { get; set; }
    public void Move()
    {
        throw new NotImplementedException();
    }

    public bool CanMove(int newRow, int newColumn)
    {
        throw new NotImplementedException();
    }

    public void Capture()
    {
        throw new NotImplementedException();
    }

    public void Promote()
    {
        throw new NotImplementedException();
    }
}