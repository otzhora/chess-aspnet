using ChessEngine.Board;
using ChessEngine.Configuration;
using ChessEngine.Models;

namespace ChessEngine.Serializers;

public static class ChessBoardFenSerializer
{
    private static int Dimensions => 8;

    public static ChessBoard Desirialize(BoardConfig config, string fen)
    {
        var fenItems = fen.Split(" ");
        if (fenItems.Length != 6)
        {
            throw new ArgumentException($"Invalid fen: {fen}");
        }

        var board = new ChessBoard(config);
        
        board = GetBoard(config, fenItems[0], board);
        board.ActiveColor = GetActiveColor(fenItems[1]);
        board.AvailableCastling = GetCastlings(fenItems[2]);
        board.EnPassant = GetEnPassant(config, fenItems[3]);
        board.HalfMoveClock = GetHalfMoveClock(fenItems[4]);
        board.FullMoveCount = GetFullMoveCount(fenItems[5]);
        return board;
    }

    private static int GetFullMoveCount(string fullMoveCount)
    {
        return Int32.Parse(fullMoveCount);
    }

    private static int GetHalfMoveClock(string halfMoveCount)
    {
        return Int32.Parse(halfMoveCount);
    }

    private static Coordinates? GetEnPassant(BoardConfig config, string enPassant)
    {
        if (enPassant == "-") return null;
        return new Coordinates(enPassant);
    }

    private static Color GetActiveColor(string activeColor)
    {
        return activeColor == "w" ? Color.White : Color.Black;
    }

    private static HashSet<Castling> GetCastlings(string castlings)
    {
        var availableCastling = new HashSet<Castling>();
        if (castlings == "-") return availableCastling;
        if (castlings.Contains("K")) availableCastling.Add(Castling.WhiteShort);
        if (castlings.Contains("Q")) availableCastling.Add(Castling.WhiteLong);
        if (castlings.Contains("k")) availableCastling.Add(Castling.BlackShort);
        if (castlings.Contains("q")) availableCastling.Add(Castling.BlackLong);
        return availableCastling;
    }

    private static ChessBoard GetBoard(BoardConfig config, string fenLines,
        ChessBoard board)
    {
        var lines = fenLines.Split("/");
        if (lines.Length != Dimensions)
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
                    var piece = new Piece(item);
                    var coordinates = new Coordinates(row, col);
                    piece.Moved = HavePawnMoved(coordinates, piece);
                    board.SetSquare(coordinates, new Piece(item));
                    col++;
                }
            }

            row++;
            col = 0;
        }

        return board;
    }

    private static bool HavePawnMoved(Coordinates coordinates, Piece piece)
    {
        if (piece.Type != PieceType.Pawn) return false;
        if (piece.Color == Color.White && coordinates.Row == 6) return false;
        if (piece.Color == Color.Black && coordinates.Row == 1) return false;
        return true;
    }
}