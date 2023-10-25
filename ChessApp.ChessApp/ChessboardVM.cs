using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ChessApp;

public class ChessboardVM : INotifyPropertyChanged
{
    private readonly ChessboardModel1 chessboard;


    public ChessboardVM()
    {
        chessboard = new ChessboardModel1();
        // Convert Chessboard's Squares to ObservableCollection for data-binding
        Squares = new ObservableCollection<ChessboardSquareVM>(
            chessboard.Squares.Select(s => new ChessboardSquareVM(s.Value)));
    }

    public (int Row, int Col)? SelectedSquare { get; set; }
    public ICommand PieceDragCommand { get; }
    public ICommand SquareDropCommand { get; }
    public string GameStatus { get; }
    public List<(int Row, int Col)> HighlightedSquares { get; }


    public ObservableCollection<ChessboardSquareVM> Squares { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    public void HighlightMovesForSelectedPiece()
    {
        // Logic to highlight squares
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}