using ChessEngine.Board;
using ChessEngine.Models;

namespace ChessEngine.MoveProviders;

public static class KingMoveProvider
{
    public static bool AttacksSquare(ChessBoard board, Coordinates from, Coordinates to)
    {
        throw new NotImplementedException();
    }
    
    public static IEnumerable<Move> GetValidMoves(ChessBoard board, Coordinates from)
    {
        throw new NotImplementedException();
    }
}