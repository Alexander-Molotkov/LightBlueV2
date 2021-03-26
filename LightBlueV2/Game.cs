using System;
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

		Piece[,] Board = new Piece[8, 8];
		private bool EndGame { get; set; }
		private int TurnNum { get; set; }


        public Game()
		{
			PopulateBoard(); 
			TurnNum = 0;
		}
		public bool ValidateMove(Move Mv)
        {
			//TODO

			if (Board[Mv.fromRow, Mv.fromCol].ValidMove(Mv, Board)){
				MakeMove(Mv);
				return true;
			}
			return false;
        }

		private void MakeMove(Move mv)
        {
			Board[mv.toRow, mv.toCol] = Board[mv.fromRow, mv.fromCol];
			Board[mv.fromRow, mv.fromCol] = null;
			return;
        }

		public int PopulateBoard()
		{
			for(int i = 0; i < 8; i++)
            {
				for(int j = 0; j < 8; j++)
                {
					Board[i, j] = null;
                }
            }

			Board[0,0] = new Rook('B');
			Board[0,7] = new Rook('B');
			Board[7,0] = new Rook('W');
			Board[7,7] = new Rook('W');
			Board[0,1] = new Knight('B');
			Board[0,6] = new Knight('B');
			Board[7,1] = new Knight('W');
			Board[7,6] = new Knight('W');
			Board[0,2] = new Bishop('B');
			Board[0,5] = new Bishop('B');
			Board[7,2] = new Bishop('W');
            Board[7,5] = new Bishop('W');
			Board[0,3] = new Queen('B');
			Board[7,3] = new Queen('W');
			Board[0,4] = new King('B');
			Board[7,4] = new King('W');

			for (int i = 0; i < 8; i++)
			{
				Board[1,i] = new Pawn('B');
			}
			for (int i = 0; i < 8; i++)
			{
				Board[6,i] = new Pawn('W');
			}
			return 0;
		}
	}
}