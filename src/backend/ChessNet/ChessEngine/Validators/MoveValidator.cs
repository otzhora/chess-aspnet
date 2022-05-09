using ChessEngine.Board;
using ChessEngine.Models;

namespace ChessEngine.Validators;

public static class MoveValidator
{
    private static int Dimensions => 8;
    
    public static bool ValidateMove(ChessBoard board, Move move)
    {
        if (!ApplyGeneralRules(board, move)) return false;
        if (!ApplyPieceRules(board, move)) return false;
        return true;
    }

    private static bool ApplyGeneralRules(ChessBoard board, Move move)
    {
        if (!ApplyMoveTurnRule(board, move)) return false;
        if (!ApplyPieceColorRule(board, move)) return false;
        if (!ApplyPiecePresentRule(board, move)) return false;
        if (!ApplyCoordinatesRule(board, move)) return false;
        if (!ApplyEnemyPieceAtTargetRule(board, move)) return false;
        return true;
    }

    private static bool ApplyEnemyPieceAtTargetRule(ChessBoard board, Move move)
    {
        var pieceAtTarget = board[move.To].Piece;
        if (pieceAtTarget == null) return true;
        return pieceAtTarget.Color != move.Piece.Color;
    }

    private static bool ApplyPiecePresentRule(ChessBoard board, Move move)
    {
        if (board[move.From].Piece == null) return false;
        return board[move.From].Piece!.Equals(move.Piece);
    }

    private static bool ApplyPieceColorRule(ChessBoard board, Move move)
    {
        return move.Piece.Color == board.ActiveColor;
    }

    private static bool ApplyCoordinatesRule(ChessBoard board, Move move)
    {
        if (move.From.Col < 0 || move.From.Col >= Dimensions) return false;
        if (move.From.Row < 0 || move.From.Row >= Dimensions) return false;
        if (move.To.Col < 0 || move.To.Col >= Dimensions) return false;
        if (move.To.Row < 0 || move.To.Row >= Dimensions) return false;
        return true;
    }

    private static bool ApplyMoveTurnRule(ChessBoard board, Move move)
    {
        return board.ActiveColor == move.Piece.Color;
    }

    private static bool ApplyPieceRules(ChessBoard board, Move move)
    {
        if (board[move.From].Piece == null || move.Piece != board[move.From].Piece)
            return false;

        switch (move.Piece.Type)
        {
            case PieceType.Pawn:
                return PawnMoveValidator.ValidateMove(board, move);
            case PieceType.Bishop:
                return BishopMoveValidator.ValidateMove(board, move);
            case PieceType.Knight:
                return KnightMoveValidator.ValidateMove(board, move);
            case PieceType.Rook:
                return RookMoveValidator.ValidateMove(board, move);
            case PieceType.Queen:
                return QueenMoveValidator.ValidateMove(board, move);
            case PieceType.King:
                return KingMoveValidator.ValidateMove(board, move);
        }

        return false;
    }
}