using ChessEngine.Board;
using ChessEngine.Models;

namespace ChessEngine.Validators;

public static class PawnMoveValidator
{
    public static bool ValidateMove(ChessBoard board, Move move)
    {
        if (!ApplyGeometricRule(board, move))
            return false;
        if (!ApplyNoPiecesBeforeTargetRule(board, move))
            return false;
        return true;
    }

    private static bool ApplyNoPiecesBeforeTargetRule(ChessBoard board, Move move)
    {
        var distance = Math.Abs(move.From.Col - move.To.Col);
        var currentCoordinate = new Coordinates(move.From);
        for (var i = 0; i < distance - 1; i++)
        {
            currentCoordinate = new Coordinates(currentCoordinate.Row,
                currentCoordinate.Col + 1);
            if (board[currentCoordinate].Piece != null)
                return false;
        }

        return true;
    }

    private static bool ApplyGeometricRule(ChessBoard board, Move move)
    {
        var isEnPassant = Math.Abs(move.To.Col - move.From.Col) == 2;
        return isEnPassant ? ApplyEnPassantRule(board, move) : ApplyNormalRule(board, move);
    }

    private static bool ApplyEnPassantRule(ChessBoard board, Move move)
    {
        if (board[move.From].Piece?.Moved == true)
            return false;
        return true;
    }

    private static bool ApplyNormalRule(ChessBoard board, Move move)
    {
        if (move.From.Row == move.To.Row) 
            return true;
        if (board.EnPassant == null)
            return false;
        
        return board.EnPassant == move.To;
    }
}