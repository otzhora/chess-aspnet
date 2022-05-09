using ChessEngine.Models;
using Color = ChessEngine.Models.Color;

namespace ChessEngine.Board;

public class BoardSquare
{
    public Piece? Piece { get; set; }
    public Color SquareColor { get; }

    public BoardSquare(Color squareColor)
    {
        SquareColor = squareColor;
        Piece = null;
    }

    public BoardSquare(Color squareColor, Piece piece)
    {
        SquareColor = squareColor;
        Piece = piece;
    }

    public override string ToString()
    {
        if (Piece != null) return Piece.ToString();
        return SquareColor switch
        {
            Color.Black => "#",
            Color.White => "*",
            _ => "_"
        };
    }
} 