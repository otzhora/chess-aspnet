using System.Text;
using ChessEngine.Board;
using ChessEngine.Configuration;

namespace ChessEngine.Serializers;

public static class ChessBoardStringSerializer
{
    private static string Alphabet => "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private static int Dimensions => 8;

    public static string Serialize(BoardConfig config, ChessBoard board)
    {
        var sb = new StringBuilder();
        
        for (var row = 0; row < Dimensions; row++)
        {
            sb.Append($"{Dimensions - row} ");
            for (var col = 0; col < Dimensions; col++)
            {
                sb.Append($"{board[row, col]} ");
            }
            sb.Append("\n");
        }

        sb.Append($"  ");
        for (var row = 0; row < Dimensions; row++)
            sb.Append($"{Alphabet[row]} ");
        return sb.ToString();
    }
}