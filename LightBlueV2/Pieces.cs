using System;
using System.Drawing;
using System.Windows.Forms;

namespace LightBlueV2
{
	public abstract class Piece
	{
		public char Color { get; set; }
        protected bool HasMoved { get; set; }

		public Piece()
        {
        }

		public Piece(char color)
        {
			this.Color = color;
        }

        public abstract bool ValidMove(Move m, Piece[,] Board);
	}
	public class Bishop : Piece
	{
        public Bishop(char color) : base(color)
        {
            HasMoved = false;
        }

        public override bool ValidMove(Move m, Piece[,] Board)
        {
            int rowDiff = m.toRow - m.fromRow;
            int colDiff = m.toCol - m.fromCol;

            if(rowDiff == colDiff)
            {
                return true;
            }
            return false;
        }
	}
	public class Knight : Piece
	{
        public Knight(char color) : base(color)
        {
            HasMoved = false;
        }

        public override bool ValidMove(Move m, Piece[,] Board)
        {
            int rowDiff = m.toRow - m.fromRow;
            int colDiff = m.toCol - m.fromCol;

            if(Math.Abs(rowDiff) == 2 && Math.Abs(colDiff) == 1)
            {
                return true;
            }
            return false;
        }
	}
    public class Rook : Piece
    {
        public Rook(char color) : base(color)
        {
            HasMoved = false;
        }

        public override bool ValidMove(Move m, Piece[,] Board)
        {
            int rowDiff = m.toRow - m.fromRow;
            int colDiff = m.toCol - m.fromCol;

            if(rowDiff == 0 || colDiff == 0)
            {
                return true;
            }
            return false;
        }
    }
    public class King : Piece
	{
        public King(char color) : base(color)
        {
            HasMoved = false;
        }

        public override bool ValidMove(Move m, Piece[,] Board)
        {
            //TODO: Castling
            int rowDiff = m.toRow - m.fromRow;
            int colDiff = m.toCol - m.fromCol;

            if(Math.Abs(rowDiff) == 1 && Math.Abs(colDiff)  == 1)
            {
                return true;
            }
            return false;
        }
	}
	public class Pawn : Piece
	{
        public Pawn(char color) : base(color)
        {
            HasMoved = false;
        }

        public override bool ValidMove(Move m, Piece[,] Board)
        {
            int rowDiff = m.toRow - m.fromRow;
            int colDiff = m.toCol - m.fromCol;

            //Regular pawn movement
            //TODO: Implement pawn capturing
            //TODO: Implement in peasant
            if(colDiff > 0 && rowDiff == 1 && this.Color == 'W')
            {
                return true;
            }
            if(colDiff > 0 && rowDiff == -1 && this.Color == 'B')
            {
                return true;
            }

            //Pawn double movement on the first move 
            if(colDiff > 0 && rowDiff == 2 && this.Color == 'W')
            {
                return true;
            }
            if(colDiff > 0 && rowDiff == -2 && this.Color == 'B')
            {
                return true;
            }
            return false;
        }
	}
	public class Queen: Piece
	{
        public Queen(char color) : base(color)
        {
            HasMoved = false;
        }
        public override bool ValidMove(Move m, Piece[,] Board)
        {
            int rowDiff = m.toRow - m.fromRow;
            int colDiff = m.toCol - m.fromCol;

            if (rowDiff == 0 || colDiff == 0 || rowDiff == colDiff)
            {
                return true;
            }
            return false;
        }
	}
}
