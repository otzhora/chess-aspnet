using ChessEngine.Board;
using ChessEngine.Models;

namespace ChessEngine.Validators;

public static class RookMoveValidator
{
    public static bool ValidateMove(ChessBoard board, Move move)
    {
        if (!ApplyGeometricRule(board, move)) return false;
        if (!ApplyNoPiecesBeforeTargetRule(board, move)) return false;
        return true;
    }

    private static bool ApplyNoPiecesBeforeTargetRule(ChessBoard board, Move move)
    {
        var distance = Math.Abs(move.From.Col - move.To.Col);
        var rowInc = (move.From.Row - move.To.Row) / distance;
        var colInc = (move.From.Col - move.To.Col) / distance;

        var currentCoordinate = new Coordinates(move.From);
        for (var i = 0; i < distance - 1; i++)
        {
            currentCoordinate = new Coordinates(currentCoordinate.Row + rowInc,
                currentCoordinate.Col + colInc);
            if (board[currentCoordinate].Piece != null) return false;
        }

        return true;
    }

    private static bool ApplyGeometricRule(ChessBoard board, Move move)
    {
        return Math.Abs(move.From.Col - move.To.Col) == Math.Abs(move.From.Row - move.To.Row);
    }
}