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
		private bool HasMoved { get; set; }
		private Move PlayerMove { get; set; }
		private bool EndGame { get; set; }
		private int TurnNum { get; set; }


        public void GameLoop()
		{

			PopulateBoard(this.Pieces); 
			TurnNum = 0;

			while (!EndGame)
			{
				//Turn();
			}
			System.Console.WriteLine("Game Over!");
		}
		private void Turn()
		{
			while (HasMoved == false)
            {
				//Wait for a move
			}

			if (ValidateMove(PlayerMove, this.Pieces))
			{
				TurnNum++;
				//TODO:
				//RecordMove(PlayerMove)
				MakeMove(PlayerMove, this.Pieces);
			}
			HasMoved = false;
		}

		private bool ValidateMove(Move Mv, Piece[,] Board)
        {
			//TODO
			return true;
        }

		private void MakeMove(Move mv, Piece [,] Board)
        {
			Board[mv.toRow, mv.toCol] = Board[mv.fromRow, mv.fromCol];
			Board[mv.fromRow, mv.fromCol] = null;
			return;
        }

		public int PopulateBoard(Piece[,] pieces)
		{
			for(int i = 0; i < 8; i++)
            {
				for(int j = 0; j < 8; j++)
                {
					pieces[i, j] = null;
                }
            }

			pieces[0,0] = new Rook('B', 0, 0);
			pieces[0,7] = new Rook('B', 0, 7);
			pieces[7,0] = new Rook('W', 7, 0);
			pieces[7,7] = new Rook('W', 7, 7);
			pieces[0,1] = new Knight('B', 0, 1);
			pieces[0,6] = new Knight('B', 0, 6);
			pieces[7,1] = new Knight('W', 7, 1);
			pieces[7,6] = new Knight('W', 7, 6);
			pieces[0,2] = new Bishop('B', 0 , 2);
			pieces[0,5] = new Bishop('B', 0 , 5);
			pieces[7,2] = new Bishop('W', 7 , 2);
			pieces[7,5] = new Bishop('W', 7, 5);
			pieces[0,3] = new Queen('B', 0 , 3);
			pieces[7,3] = new Queen('W', 7, 3);
			pieces[0,4] = new King('B', 0, 4);
			pieces[7,4] = new King('W', 7, 4);

			for (int i = 0; i < 8; i++)
			{
				pieces[1,i] = new Pawn('B', 1, i);
			}
			for (int i = 0; i < 8; i++)
			{
				pieces[6,i] = new Pawn('W', 1, i);
			}
			return 0;
		}
	}
}