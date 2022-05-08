using ChessEngine.Board;
using ChessEngine.Models;

namespace ChessEngine.MoveProviders;

public static class MoveProvider
{
    public static bool AttacksSquare(ChessBoard board, Coordinates from, Coordinates to, Piece piece)
    {
        throw new NotImplementedException();
    }
    
    public static IEnumerable<Move> GetValidMoves(ChessBoard board, Coordinates from, Piece piece)
    {
        throw new NotImplementedException();
    }
    
}