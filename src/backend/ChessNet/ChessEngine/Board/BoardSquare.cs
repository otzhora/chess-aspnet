using ChessEngine.Models;
using Color = ChessEngine.Models.Color;

namespace ChessEngine.Board;

public class BoardSquare
{
    public Piece? Piece { get; set; }
    public Color Color { get; }

    public BoardSquare(Color color)
    {
        Color = color;
        Piece = null;
    }

    public BoardSquare(Color color, Piece piece)
    {
        Color = color;
        Piece = piece;
    }

    public override string ToString()
    {
        if (Piece != null) return Piece.ToString();
        return Color switch
        {
            Color.Black => "#",
            Color.White => "*"
        };
    }
} 