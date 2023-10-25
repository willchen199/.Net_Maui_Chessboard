using System;
namespace ChessApp
{
	/// <summary>
	/// Chess board square model
	/// </summary>
	public class ChessboardSquareModel
	{
		public ChessboardSquareModel(int row, int col, string backgroundPieceImage)
		{
			Row = row;
			Col = col;
			BackgroundPieceImage = backgroundPieceImage;
		}
		
		public string BackgroundPieceImage { get; set; }
		/// <summary>
		/// The row it's currently in.
		/// </summary>
		public int Row { get; }
		
		/// <summary>
		/// The column it's currently in.
		/// </summary>
		public int Col { get; }
		
		/// <summary>
		/// The chess piece object.
		/// </summary>
		public ChessPieceModel Piece { get; set; }
	}
}

