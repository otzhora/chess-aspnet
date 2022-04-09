using ChessEngine.Configuration;
using ChessEngine.Models;
using ChessEngine.Serializers;

namespace ChessEngine.Board;

public class ChessBoard : IChessBoard
{
    private static int Dimensions => 8;
    public Dictionary<Coordinates, BoardSquare> Board { get; set; }
    public BoardConfig Config { get; }
    public Color ActiveColor { get; set; }
    public List<Castling> AvailableCastling { get; set; }
    public Coordinates? EnPassant { get; set; }
    public int HalfMoveClock { get; set; }
    public int FullMoveCount { get; set; }

    public ChessBoard(BoardConfig config)
    {
        Config = config;
        Board = CreateBoard();
        AvailableCastling = new List<Castling>();
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