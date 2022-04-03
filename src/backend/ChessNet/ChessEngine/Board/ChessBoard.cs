using System.Text;
using ChessEngine.Configuration;
using ChessEngine.Models;

namespace ChessEngine.Board;

public class ChessBoard : IChessBoard
{
    // Bottom left square is A1
    public BoardSquare[,] Board { get; set; }
    public BoardConfig Config { get; }
    public char[] Alphabet { get; }

    public ChessBoard(BoardConfig config)
    {
        Config = config;
        Board = CreateBoard();
        Alphabet = CreateAlphabet();
    }

    private BoardSquare[,] CreateBoard()
    {
        var board = new BoardSquare[Config.Dimensions, Config.Dimensions];
        for (var row = 0; row < Config.Dimensions; row++)
        {
            for (var col = 0; col < Config.Dimensions; col++)
            {
                var color = row % 2 == col % 2 ? Color.White : Color.Black;
                board[row, col] = new BoardSquare(color);
            }
        }

        return board;
    }

    public void SetSquare(string coords, Piece? piece)
    {
        var (row, col) = ParseCoords(coords);
        Board[row, col].Piece = piece;
    }

    private (int, int) ParseCoords(string coords)
    {
        if (coords.Length != 2)
        {
            throw new ArgumentException("Coords format should be like A1");
        }

        coords = coords.ToUpper();
        var row = Config.Dimensions - coords[1] + '0';
        var col = Array.IndexOf(Alphabet, coords[0]);
        return (row, col);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        
        for (var row = 0; row < Config.Dimensions; row++)
        {
            sb.Append($"{Config.Dimensions - row} ");
            for (var col = 0; col < Config.Dimensions; col++)
            {
                sb.Append($"{Board[row, col]} ");
            }
            sb.Append("\n");
        }

        sb.Append($"  ");
        for (var row = 0; row < Config.Dimensions; row++)
            sb.Append($"{Alphabet[row]} ");
        return sb.ToString();
    }

    private char[] CreateAlphabet()
    {
        var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var boardDimensions = Config.Dimensions;
        if (boardDimensions > letters.Length)
        {
            throw new ArgumentException("Board size should not exceed 26x26");
        }

        return letters[..boardDimensions].ToCharArray();
    }
}