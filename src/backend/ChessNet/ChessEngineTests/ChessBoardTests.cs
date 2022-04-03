using System;
using ChessEngine.Board;
using ChessEngine.Configuration;
using ChessEngine.Models;
using NUnit.Framework;

namespace ChessEngineTests;

public class Tests
{
    [Test]
    public void BoardToStringTest()
    {
        var config = new BoardConfig();
        var board = new ChessBoard(config);

        board.SetSquare("A1", new Piece(PieceType.Rook, Color.White));
        board.SetSquare("A2", new Piece(PieceType.Bishop, Color.White));
        board.SetSquare("A8", new Piece(PieceType.Rook, Color.Black));
        board.SetSquare("H1", new Piece(PieceType.Rook, Color.White));
        board.SetSquare("h8", new Piece(PieceType.Rook, Color.Black));
        board.SetSquare("e4", new Piece(PieceType.Pawn, Color.White));
        var expected =
            "8 R # * # * # * R \n7 # * # * # * # * \n6 * # * # * # * # \n5 # * # * # * # * \n4 * # * # p # * # \n3 # * # * # * # * \n2 b # * # * # * # \n1 r * # * # * # r \n  A B C D E F G H ";
        Console.Write(expected);
        Console.WriteLine();
        Console.Write(board);
        Assert.AreEqual(expected, board.ToString());
    }
}