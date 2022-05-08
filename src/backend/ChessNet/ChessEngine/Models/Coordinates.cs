namespace ChessEngine.Models;

public class Coordinates : IEquatable<Coordinates>
{
    private static string Alphabet => "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private static int Dimensions => 8;
    public int Row { get; }
    public int Col { get; }

    public Coordinates(int row, int col)
    {
        Row = row;
        Col = col;
    }
    
    public Coordinates(Coordinates other)
    {
        Row = other.Row;
        Col = other.Col;
    }


    public Coordinates(string coordinates)
    {
        if (coordinates.Length != 2)
        {
            throw new ArgumentException("Coords format should be like A1");
        }

        coordinates = coordinates.ToUpper();
        Row = Dimensions - (coordinates[1] - '0');
        Col = Alphabet.IndexOf(coordinates[0]);
    }

    public override string ToString()
    {
        return $"{Alphabet[Col]}{Dimensions - Row}";
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Coordinates)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Row, Col);
    }

    public bool Equals(Coordinates? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Row == other.Row && Col == other.Col;
    }
}