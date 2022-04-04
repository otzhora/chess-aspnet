using ChessEngine.Board;
using ChessEngine.Configuration;
using ChessEngine.Models;

var config = new BoardConfig();
var board = new ChessBoard(config, "rnbqkb1r/p4ppp/1pp2n2/3pp3/2B1P3/2N2N2/PPPP1PPP/R1BQ1RK1 w kq d6 0 6");

/*
board.SetSquare("A1", new Piece(PieceType.Rook, Color.White));
board.SetSquare("A2", new Piece(PieceType.Bishop, Color.White));
board.SetSquare("A8", new Piece(PieceType.Rook, Color.Black));
board.SetSquare("H1", new Piece(PieceType.Rook, Color.White));
board.SetSquare("h8", new Piece(PieceType.Rook, Color.Black));
board.SetSquare("e4", new Piece(PieceType.Pawn, Color.White));
*/

Console.WriteLine(board);
Console.WriteLine();
