namespace ChessEngine.Models;

public class Piece : IEquatable<Piece>
{
    public PieceType Type { get; }
    public Color Color { get; }
    public bool Moved { get; set; }

    public Piece(PieceType type, Color color)
    {
        Type = type;
        Color = color;
        Moved = false;
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

    public bool Equals(Piece? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Type == other.Type && Color == other.Color && Moved == other.Moved;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Piece)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Type, (int)Color, Moved);
    }
}