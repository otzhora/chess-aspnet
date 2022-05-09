using ChessEngine.Configuration;
using ChessEngine.Models;
using ChessEngine.Serializers;
using ChessEngine.Validators;
using NUnit.Framework;

namespace ChessEngineTests;

public class BishopMoveValidatorTests
{

    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N2/PPPB1PPP/R2QKB1R w KQkq - 0 7", "d2", "c3")]
    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N2/PPPB1PPP/R2QKB1R w KQkq - 0 7", "d2", "b4")]
    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N2/PPPB1PPP/R2QKB1R w KQkq - 0 7", "f1", "e2")]
    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N1P/PPPB1PP1/R2QKB1R b KQkq - 0 7", "b4", "a5", Color.Black)]
    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N1P/PPPB1PP1/R2QKB1R b KQkq - 0 7", "b4", "a3", Color.Black)]
    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N1P/PPPB1PP1/R2QKB1R b KQkq - 0 7", "b4", "d2", Color.Black)]
    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N1P/PPPB1PP1/R2QKB1R b KQkq - 0 7", "b4", "e7", Color.Black)]
    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N1P/PPPB1PP1/R2QKB1R b KQkq - 0 7", "b4", "f8", Color.Black)]
    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N1P/PPPB1PP1/R2QKB1R b KQkq - 0 7", "d7", "c8", Color.Black)]
    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N1P/PPPB1PP1/R2QKB1R b KQkq - 0 7", "d7", "c6", Color.Black)]
    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N1P/PPPB1PP1/R2QKB1R b KQkq - 0 7", "d7", "b5", Color.Black)]
    public void MovesShouldBeValid(string boardFen,
        string moveFrom,
        string moveTo,
        Color? bishopColor = null)
    {
        var config = new BoardConfig();
        var board = ChessBoardFenSerializer.Desirialize(config, boardFen);
        var fromCoordinates = new Coordinates(moveFrom);
        var toCoordinates = new Coordinates(moveTo);
        var bishop = new Piece(PieceType.Bishop, bishopColor ?? Color.White);
        var move = new Move(fromCoordinates, toCoordinates, bishop, null);


        Assert.True(MoveValidator.ValidateMove(board, move));
    }


    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N2/PPPB1PPP/R2QKB1R w KQkq - 0 7", "d2", "c1")]
    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N2/PPPB1PPP/R2QKB1R w KQkq - 0 7", "d2", "e1")]
    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N2/PPPB1PPP/R2QKB1R w KQkq - 0 7", "d2", "a5")]
    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N2/PPPB1PPP/R2QKB1R w KQkq - 0 7", "f1", "e4")]
    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N2/PPPB1PPP/R2QKB1R w KQkq - 0 7", "f1", "d3")]
    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N2/PPPB1PPP/R2QKB1R w KQkq - 0 7", "f1", "g2")]
    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N2/PPPB1PPP/R2QKB1R w KQkq - 0 7", "f4", "g3")]
    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N1P/PPPB1PP1/R2QKB1R b KQkq - 0 7", "b4", "b6", Color.Black)]
    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N1P/PPPB1PP1/R2QKB1R b KQkq - 0 7", "b4", "a4", Color.Black)]
    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N1P/PPPB1PP1/R2QKB1R b KQkq - 0 7", "d7", "e8", Color.Black)]
    [TestCase("rn1qk2r/pppb1pp1/4pn1p/1N1p4/1b2P3/3P1N1P/PPPB1PP1/R2QKB1R b KQkq - 0 7", "d7", "e6", Color.Black)]
    public void MovesShouldBeInValid(string boardFen,
        string moveFrom,
        string moveTo,
        Color? bishopColor = null)
    {
        var config = new BoardConfig();
        var board = ChessBoardFenSerializer.Desirialize(config, boardFen);
        var fromCoordinates = new Coordinates(moveFrom);
        var toCoordinates = new Coordinates(moveTo);
        var bishop = new Piece(PieceType.Bishop, bishopColor ?? Color.White);
        var move = new Move(fromCoordinates, toCoordinates, bishop, null);


        Assert.False(MoveValidator.ValidateMove(board, move));
    }
}