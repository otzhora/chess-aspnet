using ChessEngine.Board;
using ChessEngine.Models;

namespace ChessEngine.Validators;

public static class KnightMoveValidator
{
    public static bool ValidateMove(ChessBoard board, Move move)
    {
        if (!ApplyGeometricRule(board, move)) return false;
        return true;
    }

    private static bool ApplyGeometricRule(ChessBoard board, Move move)
    {
        var rowDifference = Math.Abs(move.To.Row - move.From.Row);
        var colDifference = Math.Abs(move.To.Col - move.From.Col);
        return rowDifference == 2 && colDifference == 1 
            || rowDifference == 1 && colDifference == 2;
    }
}