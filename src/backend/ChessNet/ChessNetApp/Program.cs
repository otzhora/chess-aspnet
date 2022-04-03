using ChessEngine.Board;
using ChessEngine.Configuration;
using ChessEngine.Models;

var config = new BoardConfig();
var board = new ChessBoard(config);

board.SetSquare("A1", new Piece(PieceType.Rook, Color.White));
board.SetSquare("A2", new Piece(PieceType.Bishop, Color.White));
board.SetSquare("A8", new Piece(PieceType.Rook, Color.Black));
board.SetSquare("H1", new Piece(PieceType.Rook, Color.White));
board.SetSquare("h8", new Piece(PieceType.Rook, Color.Black));
board.SetSquare("e4", new Piece(PieceType.Pawn, Color.White));

Console.WriteLine(board);