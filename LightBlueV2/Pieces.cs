using System;
using System.Drawing;
using System.Windows.Forms;

namespace LightBlueV2
{
	public class Bishop : Piece
	{
	}
	public class Knight : Piece
	{
	}
	public class Rook : Piece
	{
	}
	public class King : Piece
	{
	}
	public class Pawn : Piece
	{
	}
	public class Piece
	{
		public string color { get; set; }
	}
	public class Queen: Piece
	{
	}

	public class Setup
	{
		public int PopulateBoard(Piece[][] board)
		{
			Rook piece = new Rook();
			piece.color = "black";
			board[0][0] = piece;
			board[0][7] = piece;
			piece.color = "white";
			board[7][0] = piece;
			board[7][7] = piece;
			Knight piecek = new Knight();
			piecek.color = "black";
			board[0][1] = piecek;
			board[0][6] = piecek;
			piecek.color = "white";
			board[7][1] = piecek;
			board[7][6] = piecek;
			Bishop pieceb = new Bishop();
			piece.color = "black";
			board[0][2] = pieceb;
			board[0][5] = pieceb;
			pieceb.color = "white";
			board[7][2] = pieceb;
			board[7][5] = pieceb;
			Queen pieceq = new Queen();
			pieceq.color = "black";
			board[0][3] = pieceq;
			pieceq.color = "white";
			board[7][3] = pieceq;
			King piecei = new King();
			piecei.color = "black";
			board[0][4] = piecei;
			piecei.color = "white";
			board[7][4] = piecei;

			Pawn piecep = new Pawn();
			piecep.color = "black";
			for (int i = 0; i < 8; i++)
			{
				board[1][i] = piecep;
			}
			piecep.color = "white";
			for (int i = 0; i < 8; i++)
			{
				board[6][i] = piecep;
			}
			return 0;
		}
	}
}
