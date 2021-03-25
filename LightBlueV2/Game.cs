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
		private bool HasMoved { get; set; }
		private Move PlayerMove { get; set; }
		private bool EndGame { get; set; }
		private int TurnNum { get; set; }


        public void GameLoop()
		{

			PopulateBoard(this.Board); 
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

			if (ValidateMove(PlayerMove, this.Board))
			{
				TurnNum++;
				//TODO:
				//RecordMove(PlayerMove)
				MakeMove(PlayerMove, this.Board);
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

		public int PopulateBoard(Piece[,] board)
		{
			for(int i = 0; i < 8; i++)
            {
				for(int j = 0; j < 8; j++)
                {
					Board[i, j] = null;
                }
            }

			board[0,0] = new Rook('B');
			board[0,7] = new Rook('B');
			board[7,0] = new Rook('W');
			board[7,7] = new Rook('W');
			board[0,1] = new Knight('B');
			board[0,6] = new Knight('B');
			board[7,1] = new Knight('W');
			board[7,6] = new Knight('W');
			board[0,2] = new Bishop('B');
			board[0,5] = new Bishop('B');
			board[7,2] = new Bishop('W');
            board[7,5] = new Bishop('W');
			board[0,3] = new Queen('B');
			board[7,3] = new Queen('W');
			board[0,4] = new King('B');
			board[7,4] = new King('W');

			for (int i = 0; i < 8; i++)
			{
				board[1,i] = new Pawn('B');
			}
			for (int i = 0; i < 8; i++)
			{
				board[6,i] = new Pawn('W');
			}
			return 0;
		}
	}
}