namespace ChessEngine.Models;

public class Piece
{
    public PieceType Type { get; }
    public Color Color { get; }

    public Piece(PieceType type, Color color)
    {
        Type = type;
        Color = color;
    }

    public override string ToString()
    {
        var letter = Type switch
        {
            PieceType.Pawn => "p",
            PieceType.Knight => "n",
            PieceType.Bishop => "b",
            PieceType.Rook => "r",
            PieceType.Queen => "q",
            PieceType.King => "k"
        };
        return Color == Color.White ? letter.ToLower() : letter.ToUpper();
    }
}