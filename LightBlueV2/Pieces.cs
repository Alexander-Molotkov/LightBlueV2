using System;
using System.Drawing;
using System.Windows.Forms;

namespace LightBlueV2
{
	public class Piece
	{
		public char color { get; set; }

		public Piece()
        {

        }

		public Piece(char color)
        {
			this.color = color;
        }
	}
	public class Bishop : Piece
	{
        public Bishop(char color) : base(color)
        {
        }
	}
	public class Knight : Piece
	{
        public Knight(char color) : base(color)
        {
        }
	}
    public class Rook : Piece
    {
        public Rook(char color) : base(color)
        {
        }
    }
    public class King : Piece
	{
        public King(char color) : base(color)
        {
        }
	}
	public class Pawn : Piece
	{
        public Pawn(char color) : base(color)
        {
        }
	}
	public class Queen: Piece
	{
        public Queen(char color) : base(color)
        {
        }
	}
}
