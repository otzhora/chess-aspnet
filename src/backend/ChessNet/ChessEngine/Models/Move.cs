namespace ChessEngine.Models;

public class Move : IEquatable<Move>
{
    public Coordinates From { get; }
    public Coordinates To { get; }
    public Piece Piece { get; }
    public SpecialMove? SpecialMove { get; }

    public Move(Coordinates from, Coordinates to, Piece piece, SpecialMove? specialMove)
    {
        From = from;
        To = to;
        Piece = piece;
        SpecialMove = specialMove;
    }

    public bool Equals(Move? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return From.Equals(other.From) 
            && To.Equals(other.To) 
            && Piece.Equals(other.Piece) 
            && SpecialMove == other.SpecialMove;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Move)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(From, To, Piece);
    }
}