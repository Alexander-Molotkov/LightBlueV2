using System;
using System.Drawing;
using System.Windows.Forms;

namespace LightBlueV2
{
	public abstract class Piece
	{
        public char Name;
        public int Row;
        public int Col;
        public string Img;
        public char Color { get; set; }
        protected bool HasMoved { get; set; }

		public Piece()
        {

        }

		public Piece(char color, int row, int col)
        {
			this.Color = color;
            this.Row = row;
            this.Col = col;
        }

        public abstract bool ValidMove(Move m, Piece[,] Board);
	}
	public class Bishop : Piece
	{
        public Bishop(char color, int row, int col) : base(color, row, col)
        {
            HasMoved = false;
            if (color == 'W') {
                Img = "../../Images/white_bishop.png";
            }
            else
            {
                Img = "../../Images/black_bishop.png";
            }
        }

        public override bool ValidMove(Move m, Piece[,] Board)
        {
            int rowDiff = m.toRow - m.fromRow;
            int colDiff = m.toCol - m.fromCol;

            if(Math.Abs(rowDiff) == Math.Abs(colDiff))
            {
                return true;
            }
            System.Console.WriteLine("BAD BISHOP MOVE");
            System.Console.WriteLine("Row Diff: " + rowDiff);
            System.Console.WriteLine("Col Diff: " + colDiff);
            return false;
        }
	}
	public class Knight : Piece
	{
        public Knight(char color, int row, int col) : base(color, row, col)
        {
            HasMoved = false;
            if (color == 'W')
            {
                Img = "../../Images/white_knight.png";
            }
            else
            {
                Img = "../../Images/black_knight.png";
            }
        }

        public override bool ValidMove(Move m, Piece[,] Board)
        {
            int rowDiff = m.toRow - m.fromRow;
            int colDiff = m.toCol - m.fromCol;

            if(Math.Abs(rowDiff) == 2 && Math.Abs(colDiff) == 1)
            {
                return true;
            }
            if (Math.Abs(rowDiff) == 1 && Math.Abs(colDiff) == 2)
            {
                return true;
            }

            System.Console.WriteLine("BAD KNIGHT MOVE");
            return false;
        }
	}
    public class Rook : Piece
    {
        public Rook(char color, int row, int col) : base(color, row, col)
        {
            HasMoved = false;
            if (color == 'W')
            {
                Img = "../../Images/white_rook.png";
            }
            else
            {
                Img = "../../Images/black_rook.png";
            }
        }

        public override bool ValidMove(Move m, Piece[,] Board)
        {
            int rowDiff = m.toRow - m.fromRow;
            int colDiff = m.toCol - m.fromCol;

            if(rowDiff == 0 || colDiff == 0)
            {
                return true;
            }
            System.Console.WriteLine("BAD ROOK MOVE");
            return false;
        }
    }
    public class King : Piece
	{
        public King(char color, int row, int col) : base(color, row, col)
        {
            HasMoved = false;
            if (color == 'W')
            {
                Img = "../../Images/white_king.png";
            }
            else
            {
                Img = "../../Images/black_king.png";
            }
        }

        public override bool ValidMove(Move m, Piece[,] Board)
        {
            //TODO: Castling
            int rowDiff = m.toRow - m.fromRow;
            int colDiff = m.toCol - m.fromCol;

            if(Math.Abs(rowDiff) < 2 && Math.Abs(colDiff)  < 2)
            {
                return true;
            }
            System.Console.WriteLine("Row Diff: ", Math.Abs(rowDiff));
            System.Console.WriteLine("Col Diff: ", Math.Abs(colDiff));
            System.Console.WriteLine("BAD KING MOVE");
            return false;
        }
	}
	public class Pawn : Piece
	{
        public Pawn(char color, int row, int col) : base(color, row, col)
        {
            HasMoved = false;
            if (color == 'W')
            {
                Img = "../../Images/white_pawn.png";
            }
            else
            {
                Img = "../../Images/black_pawn.png";
            }
        }

        public override bool ValidMove(Move m, Piece[,] Board)
        {
            int rowDiff = m.toRow - m.fromRow;
            int colDiff = m.toCol - m.fromCol;

            //Regular pawn movement
            //TODO: Implement pawn capturing
            //TODO: Implement in peasant
            if(colDiff == 0 && rowDiff == 1 && Color == 'B')
            {
                return true;
            }
            if(colDiff== 0 && rowDiff == -1 && Color == 'W')
            {
                return true;
            }

            //Pawn double movement on the first move 
            if(colDiff == 0 && rowDiff == 2 && Color == 'B')
            {
                return true;
            }
            if(colDiff == 0 && rowDiff == -2 && Color == 'W')
            {
                return true;
            }
            System.Console.WriteLine("BAD PAWN MOVE");
            return false;
        }
	}
	public class Queen: Piece
	{
        public Queen(char color, int row, int col) : base(color, row, col)
        {
            HasMoved = false;
            if (color == 'W')
            {
                Img = "../../Images/white_queen.png";
            }
            else
            {
                Img = "../../Images/black_queen.png";
            }
        }
        public override bool ValidMove(Move m, Piece[,] Board)
        {
            int rowDiff = m.toRow - m.fromRow;
            int colDiff = m.toCol - m.fromCol;

            if (rowDiff == 0 || colDiff == 0 || Math.Abs(rowDiff) == Math.Abs(colDiff))
            {
                return true;
            }
            System.Console.WriteLine("BAD QUEEN MOVE");
            return false;
        }
	}
}
