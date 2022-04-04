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
    public Color ActiveColor { get; set; }
    public List<Castling> AvailableCastling { get; }
    public (int, int)? EnPassant { get; set; }
    public int HalfMoveClock { get; set; }
    public int FullMoveCount { get; set; }

    public ChessBoard(BoardConfig config)
    {
        Config = config;
        Board = CreateBoard();
        Alphabet = CreateAlphabet();
        AvailableCastling = new List<Castling>();
        ActiveColor = Color.White;
        EnPassant = null;
        HalfMoveClock = 0;
        FullMoveCount = 0;
    }
    
    public ChessBoard(BoardConfig config, string fen)
    {
        Config = config;
        Alphabet = CreateAlphabet();
        Board = CreateBoard();
        AvailableCastling = new List<Castling>();
        EnPassant = null;
        HalfMoveClock = 0;
        FullMoveCount = 0;
        InitFromFen(fen);
    }

    private void InitFromFen(string fen)
    {
        var fenItems = fen.Split(" ");
        if (fenItems.Length != 6)
        {
            throw new ArgumentException($"Invalid fen: {fen}");
        }

        SetBoardFromFen(fenItems[0]);
        SetActiveColorFromFen(fenItems[1]);
        SetCastlingsFromFen(fenItems[2]);
        SetEnPassantFromFen(fenItems[3]);
        SetHalfMoveClockFromFen(fenItems[4]);
        SetFullMoveCountFromFen(fenItems[5]);
    }

    private void SetFullMoveCountFromFen(string fullMoveCount)
    {
        FullMoveCount = Int32.Parse(fullMoveCount);
    }

    private void SetHalfMoveClockFromFen(string halfMoveCount)
    {
        HalfMoveClock = Int32.Parse(halfMoveCount);
    }

    private void SetEnPassantFromFen(string enPassant)
    {
        if (enPassant == "-") return;
        EnPassant = ParseCoords(enPassant);
    }

    private void SetActiveColorFromFen(string activeColor)
    {
        ActiveColor = activeColor == "w" ? Color.White : Color.Black;
    }

    private void SetCastlingsFromFen(string castlings)
    {
        if (castlings == "-") return;
        if (castlings.Contains("K")) AvailableCastling.Add(Castling.WhiteShort);
        if (castlings.Contains("Q")) AvailableCastling.Add(Castling.WhiteLong);
        if (castlings.Contains("k")) AvailableCastling.Add(Castling.BlackShort);
        if (castlings.Contains("q")) AvailableCastling.Add(Castling.BlackLong);
    }

    private void SetBoardFromFen(string fenLines)
    {
        var lines = fenLines.Split("/");
        if (lines.Length != Config.Dimensions)
        {
            throw new ArgumentException($"Invalid fen lines: {fenLines}");
        }

        var (row, col) = (0, 0);
        
        foreach (var line in lines)
        {
            foreach (var item in line)
            {
                if (Char.IsDigit(item))
                {
                    col += item - '0';
                }
                else
                {
                    Board[row, col].Piece = new Piece(item);
                    col++;
                }
            }

            row++;
            col = 0;
        }
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