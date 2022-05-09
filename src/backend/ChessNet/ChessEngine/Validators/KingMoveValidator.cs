using ChessEngine.Board;
using ChessEngine.Models;

namespace ChessEngine.Validators;

public static class KingMoveValidator
{
    public static bool ValidateMove(ChessBoard board, Move move)
    {
        if (!ApplyGeometricRule(board, move)) return false;
        if (!ApplyTargetNotProtectedRule(board, move)) return false;
        if (!ApplyCastlingRules(board, move)) return false;
        return true;
    }

    private static bool ApplyGeometricRule(ChessBoard board, Move move)
    {
        if (Math.Abs(move.From.Col - move.To.Col) > 1) return false;
        if (Math.Abs(move.From.Row - move.To.Row) > 1) return false;
        return true;
    }
    
    private static bool ApplyTargetNotProtectedRule(ChessBoard board, Move move)
    {
        var opponentColor = move.Piece.Color == Color.White ? Color.Black : Color.White;
        return !board.GetAttackers(move.To, opponentColor).Any();
    }
    
    private static bool ApplyCastlingRules(ChessBoard board, Move move)
    {
        if (move.SpecialMove != SpecialMove.Castling) return true;
        
        var castlingType = GetCastlingType(move);
        if (!board.AvailableCastling.Contains(castlingType)) return false;
        if (CastlingRookHaveMoved(board, castlingType)) return false;
        
        var opponentColor = move.Piece.Color == Color.White ? Color.Black : Color.White;
        if (GetRookSquareAttacked(board, castlingType, opponentColor)) return false;
        return true;
    }

    private static bool CastlingRookHaveMoved(ChessBoard board, Castling castlingType)
    {
        switch (castlingType)
        {
            case Castling.WhiteShort:
                return board[new Coordinates(7, 5)].Piece?.Moved ?? true;
            case Castling.WhiteLong:
                return board[new Coordinates(7, 3)].Piece?.Moved ?? true;
            case Castling.BlackShort:
                return board[new Coordinates(0, 5)].Piece?.Moved ?? true;
            case Castling.BlackLong:
                return board[new Coordinates(0, 3)].Piece?.Moved ?? true;
        }
        return true;
    }

    private static bool GetRookSquareAttacked(ChessBoard board, Castling castlingType, Color opponentColor)
    {
        switch (castlingType)
        {
            case Castling.WhiteShort:
                return board.GetAttackers(new Coordinates(7, 5), opponentColor).Any();
            case Castling.WhiteLong:
                return board.GetAttackers(new Coordinates(7, 3), opponentColor).Any();
            case Castling.BlackShort:
                return board.GetAttackers(new Coordinates(0, 5), opponentColor).Any();
            case Castling.BlackLong:
                return board.GetAttackers(new Coordinates(0, 3), opponentColor).Any();
        }
        return true;
    }

    private static Castling GetCastlingType(Move move)
    {
        Coordinates shortDest, longDest;
        if (move.Piece.Color == Color.White)
        {
            shortDest = new Coordinates(7, 6);
            if (move.To.Equals(shortDest)) return Castling.WhiteShort;
            longDest = new Coordinates(7, 2);
            if (move.To.Equals(longDest)) return Castling.WhiteLong;
        }
        shortDest = new Coordinates(0, 6);
        if (move.To.Equals(shortDest)) return Castling.BlackShort;
        longDest = new Coordinates(0, 2);
        if (move.To.Equals(longDest)) return Castling.BlackLong;
        throw new ArgumentException($"Unknown castling {move}");
    }
}