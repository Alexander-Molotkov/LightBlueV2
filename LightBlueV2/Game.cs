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

		Piece[,] Pieces = new Piece[8, 8];
		private bool EndGame { get; set; }
		private int TurnNum { get; set; }


        public Game()
		{

			PopulateBoard(); 
			TurnNum = 0;

		}
		public bool ValidateMove(Move Mv)
        {
			if (Pieces[Mv.fromRow, Mv.fromCol].ValidMove(Mv, Pieces)){
				MakeMove(Mv);
				return true;
			}
			return false;
        }

		private void MakeMove(Move mv)
        {
			Pieces[mv.toRow, mv.toCol] = Pieces[mv.fromRow, mv.fromCol];
			Pieces[mv.fromRow, mv.fromCol] = null;
			return;
        }

		public int PopulateBoard()
		{
			for(int i = 0; i < 8; i++)
            {
				for(int j = 0; j < 8; j++)
                {
					Pieces[i, j] = null;
                }
            }

			Pieces[0,0] = new Rook('B', 0, 0);
			Pieces[0,7] = new Rook('B', 0, 7);
			Pieces[7,0] = new Rook('W', 7, 0);
			Pieces[7,7] = new Rook('W', 7, 7);
			Pieces[0,1] = new Knight('B', 0, 1);
			Pieces[0,6] = new Knight('B', 0, 6);
			Pieces[7,1] = new Knight('W', 7, 1);
			Pieces[7,6] = new Knight('W', 7, 6);
			Pieces[0,2] = new Bishop('B', 0 , 2);
			Pieces[0,5] = new Bishop('B', 0 , 5);
			Pieces[7,2] = new Bishop('W', 7 , 2);
			Pieces[7,5] = new Bishop('W', 7, 5);
			Pieces[0,3] = new Queen('B', 0 , 3);
			Pieces[7,3] = new Queen('W', 7, 3);
			Pieces[0,4] = new King('B', 0, 4);
			Pieces[7,4] = new King('W', 7, 4);

			for (int i = 0; i < 8; i++)
			{
				Pieces[1,i] = new Pawn('B', 1, i);
			}
			for (int i = 0; i < 8; i++)
			{
				Pieces[6,i] = new Pawn('W', 1, i);
			}
			return 0;
		}
	}
}