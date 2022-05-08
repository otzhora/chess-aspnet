using ChessEngine.Configuration;
using ChessEngine.Models;
using ChessEngine.MoveProviders;
using ChessEngine.Serializers;
using ChessEngine.Validators;

namespace ChessEngine.Board;

public class ChessBoard : IChessBoard
{
    private static int Dimensions => 8;
    public Dictionary<Coordinates, BoardSquare> Board { get; set; }
    public BoardConfig Config { get; }
    public Color ActiveColor { get; set; }
    public HashSet<Castling> AvailableCastling { get; set; }
    public Coordinates? EnPassant { get; set; }
    public int HalfMoveClock { get; set; }
    public int FullMoveCount { get; set; }

    public ChessBoard(BoardConfig config)
    {
        Config = config;
        Board = CreateBoard();
        AvailableCastling = new HashSet<Castling>();
        ActiveColor = Color.White;
        EnPassant = null;
        HalfMoveClock = 0;
        FullMoveCount = 0;
    }

    public void SetSquare(string coordinates, Piece? piece)
    {
        var boardCoordinates = new Coordinates(coordinates);
        if (Board.TryGetValue(boardCoordinates, out var square))
            square.Piece = piece;
    }
    
    public void SetSquare(Coordinates coordinates, Piece? piece)
    {
        Board[coordinates].Piece = piece;
    }
    
    public void SetSquare(int row, int col, Piece? piece)
    {
        var boardCoordinates = new Coordinates(row, col);
        Board[boardCoordinates].Piece = piece;
    }

    public bool MakeMove(Move move)
    {
        if (MoveValidator.ValidateMove(this, move))
            return false;
        
        SetSquare(move.From, null);
        SetSquare(move.To, move.Piece);
        move.Piece.Moved = true;
        ActiveColor = ActiveColor == Color.White ? Color.Black : Color.White;
        return true;
    }

    public IEnumerable<Coordinates> GetAttackers(Coordinates targetCoordinates)
    {
        foreach (var (from, square) in Board)
        {
            if (square.Piece == null) continue;
            var targetSquareAttacked = MoveProvider.AttacksSquare(this, from, targetCoordinates, square.Piece);
            if (targetSquareAttacked)
                yield return from;
        }
    }

    public IEnumerable<Coordinates> GetAttackers(Coordinates targetCoordinates, Color targetColor)
    {
        foreach (var (from, square) in Board)
        {
            if (square.Piece == null || square.Piece.Color != targetColor) continue;
            var targetSquareAttacked = MoveProvider.AttacksSquare(this, from, targetCoordinates, square.Piece);
            if (targetSquareAttacked)
                yield return from;
        }
    }
    
    public override string ToString()
    {
        return ChessBoardStringSerializer.Serialize(Config, this);
    }

    public BoardSquare this[Coordinates coordinates]
    {
        get => Board[coordinates];
    }
    
    public BoardSquare this[int row, int col]
    {
        get => Board[new Coordinates(row, col)];
    }
    

    private Dictionary<Coordinates, BoardSquare> CreateBoard()
    {
        var board = new Dictionary<Coordinates, BoardSquare>();
        for (var row = 0; row < Dimensions; row++)
        {
            for (var col = 0; col < Dimensions; col++)
            {
                var coords = new Coordinates(row, col);
                var color = row % 2 == col % 2 ? Color.White : Color.Black;
                board[coords] = new BoardSquare(color);
            }
        }

        return board;
    }

}