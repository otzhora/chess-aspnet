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

    public Piece(char piece)
    {
        Type = piece switch
        {
            'p' or 'P' => PieceType.Pawn,
            'n' or 'N' => PieceType.Knight,
            'b' or 'B' => PieceType.Bishop,
            'r' or 'R' => PieceType.Rook,
            'q' or 'Q' => PieceType.Queen,
            'k' or 'K' => PieceType.King,
        };
        Color = Char.IsUpper(piece) ? Color.White : Color.Black;
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
        return Color == Color.Black ? letter.ToLower() : letter.ToUpper();
    }
}