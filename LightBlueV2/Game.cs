using System;
using System.Numerics;
using System.Drawing;
using System.Windows.Forms;

namespace LightBlueV2
{

	public struct Move
    {
		public int fromRow;
		public int fromCol;
		public int toRow;
		public int toCol;
    }

	public class Game
	{

		public Piece[] blackPieces = new Piece[16];
		public Piece[] whitePieces = new Piece[16];
		public char allowedColor = 'W';
		private bool EndGame { get; set; }
		public int TurnNum { get; set; }

        public Game()
		{
			PopulateBoard(); 
			TurnNum = 0;
		}

		public bool ValidateMove(Move Mv)
        {
			Piece p = null;
			for(int i = 0; i < blackPieces.GetLength(0); i++)
            {
				if (blackPieces[i].Row == Mv.fromRow && blackPieces[i].Col == Mv.fromCol)
                {
					p = blackPieces[i];
                }
            }
			if (p == null)
			{
				for (int i = 0; i < whitePieces.GetLength(0); i++)
				{
					if (whitePieces[i].Row == Mv.fromRow && whitePieces[i].Col == Mv.fromCol)
					{
						p = whitePieces[i];
					}
				}
			}
			if (p.Color != allowedColor)
            {
				return false;
            }
			if (p.ValidMove(Mv, whitePieces, blackPieces, p.Color)){
				MakeMove(Mv, p);
				return true;
			}
			return false;
        }

		public void MakeMove(Move mv, Piece p)
        {
			p.Row = mv.toRow;
			p.Col = mv.toCol;
			if (p.Color == 'B') {
				allowedColor = 'W';
            }
			else
            {
				allowedColor = 'B';
            }
			TurnNum++;
			return;
        }

		public int PopulateBoard()
		{
			for(int i = 0; i < 8; i++)
            {
				blackPieces[i] = null;
				whitePieces[i] = null;
            }

			blackPieces[0] = new Rook('B', 0, 0);
			blackPieces[1] = new Rook('B', 0, 7);
			blackPieces[2] = new Knight('B', 0, 1);
			blackPieces[3] = new Knight('B', 0, 6);
			blackPieces[4] = new Bishop('B', 0, 2);
			blackPieces[5] = new Bishop('B', 0, 5);
			blackPieces[6] = new Queen('B', 0, 3);
			blackPieces[7] = new King('B', 0, 4);

			whitePieces[0] = new Rook('W', 7, 0);
			whitePieces[1] = new Rook('W', 7, 7);			
			whitePieces[2] = new Knight('W', 7, 1);
			whitePieces[3] = new Knight('W', 7, 6);
			whitePieces[4] = new Bishop('W', 7 , 2);
			whitePieces[5] = new Bishop('W', 7, 5);
			whitePieces[6] = new Queen('W', 7, 3);
			whitePieces[7] = new King('W', 7, 4);

			int j = 8;
			for (int i = 0; i < 8; i++)
			{
				blackPieces[j++] = new Pawn('B', 1, i);
			}
			j = 8;
			for (int i = 0; i < 8; i++)
			{
				whitePieces[j++] = new Pawn('W', 6, i);
			}
			return 0;
		}
		public void Turn(Engine e, Display.Board board)
        {
			if (TurnNum % 2 == 0)
			{
				// White's turn to move.
				allowedColor = 'W';
			}
			else
			{
				allowedColor = 'B';
				e.EngineMakeMove(board);
			}
        }
	}
}