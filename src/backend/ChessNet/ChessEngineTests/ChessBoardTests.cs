using System.Collections.Generic;
using ChessEngine.Board;
using ChessEngine.Configuration;
using ChessEngine.Models;
using ChessEngine.Serializers;
using NUnit.Framework;

namespace ChessEngineTests;

public class Tests
{
    [Test]
    public void BoardToStringTest()
    {
        var config = new BoardConfig();
        var board = new ChessBoard(config);

        board.SetSquare("A1", new Piece(PieceType.Rook, Color.Black));
        board.SetSquare("A2", new Piece(PieceType.Bishop, Color.Black));
        board.SetSquare("A8", new Piece(PieceType.Rook, Color.White));
        board.SetSquare("H1", new Piece(PieceType.Rook, Color.Black));
        board.SetSquare("h8", new Piece(PieceType.Rook, Color.White));
        board.SetSquare("e4", new Piece(PieceType.Pawn, Color.Black));
        var expected =
            "8 R # * # * # * R \n7 # * # * # * # * \n6 * # * # * # * # \n5 # * # * # * # * \n4 * # * # p # * # \n3 # * # * # * # * \n2 b # * # * # * # \n1 r * # * # * # r \n  A B C D E F G H ";
        Assert.AreEqual(expected, board.ToString());
    }
    
    [Test]
    public void CreateBoardFromFenTest()
    {
        var config = new BoardConfig();
        var board = ChessBoardFenSerializer.Desirialize(config, "rnbqkb1r/p4ppp/1pp2n2/3pp3/2B1P3/2N2N2/PPPP1PPP/R1BQ1RK1 w kq d6 0 6");
        var expected =
            "8 r n b q k b * r \n7 p * # * # p p p \n6 * p p # * n * # \n5 # * # p p * # * \n4 * # B # P # * # \n3 # * N * # N # * \n2 P P P P * P P P \n1 R * B Q # R K * \n  A B C D E F G H ";
        Assert.AreEqual(expected, board.ToString());
        Assert.AreEqual(Color.White, board.ActiveColor);
        var expectedCastling = new List<Castling>() { Castling.BlackShort, Castling.BlackLong };
        Assert.AreEqual(expectedCastling, board.AvailableCastling);
        var expectedEnPassant = new Coordinates(2, 3);
        Assert.AreEqual(expectedEnPassant, board.EnPassant);
        Assert.AreEqual(0, board.HalfMoveClock);
        Assert.AreEqual(6, board.FullMoveCount);
    }
}