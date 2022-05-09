using NUnit.Framework;

namespace ChessEngineTests;

public class QueenMoveValidatorTests
{
    [TestCase("", "", "", false)]
    public void MovesShouldBeValid(string boardFen,
        string moveFrom,
        string moveTo,
        bool isEnPassant)
    {

    }

    [TestCase("", "", "", false)]
    public void MovesShouldBeInValid(string boardFen,
        string moveFrom,
        string moveTo,
        bool isEnPassant)
    {

    }
}