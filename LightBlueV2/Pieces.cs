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
        public bool HasMoved { get; set; }

		public Piece()
        {

        }

		public Piece(char color, int row, int col)
        {
			this.Color = color;
            this.Row = row;
            this.Col = col;
        }

        public abstract bool ValidMove(Move m, Piece[] White, Piece[] Black);
	}
	public class Bishop : Piece
	{
        public Bishop(char color, int row, int col) : base(color, row, col)
        {
            HasMoved = false;
            Name = 'B';
            if (color == 'W') {
                Img = "../../Images/white_bishop.png";
            }
            else
            {
                Img = "../../Images/black_bishop.png";
            }
        }

        public override bool ValidMove(Move m, Piece[] white, Piece[] black)
        {
            int rowDiff = m.toRow - m.fromRow;
            int colDiff = m.toCol - m.fromCol;

            if(Math.Abs(rowDiff) == Math.Abs(colDiff))
            {
                this.HasMoved = true;
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
            Name = 'N';
            if (color == 'W')
            {
                Img = "../../Images/white_knight.png";
            }
            else
            {
                Img = "../../Images/black_knight.png";
            }
        }

        public override bool ValidMove(Move m, Piece[] white, Piece[] black)
        {
            int rowDiff = m.toRow - m.fromRow;
            int colDiff = m.toCol - m.fromCol;

            if(Math.Abs(rowDiff) == 2 && Math.Abs(colDiff) == 1)
            {
                this.HasMoved = true;
                return true;
            }
            if (Math.Abs(rowDiff) == 1 && Math.Abs(colDiff) == 2)
            {
                this.HasMoved = true;
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
            Name = 'R';
            if (color == 'W')
            {
                Img = "../../Images/white_rook.png";
            }
            else
            {
                Img = "../../Images/black_rook.png";
            }
        }

        public override bool ValidMove(Move m, Piece[] white, Piece[] black)
        {
            int rowDiff = m.toRow - m.fromRow;
            int colDiff = m.toCol - m.fromCol;

            if(rowDiff == 0 || colDiff == 0)
            {
                this.HasMoved = true;
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
            Name = 'B';
            if (color == 'W')
            {
                Img = "../../Images/white_king.png";
            }
            else
            {
                Img = "../../Images/black_king.png";
            }
        }

        public override bool ValidMove(Move m, Piece[] white, Piece[] black)
        {
            int rowDiff = m.toRow - m.fromRow;
            int colDiff = m.toCol - m.fromCol;

            if(Math.Abs(rowDiff) < 2 && Math.Abs(colDiff)  < 2)
            {
                this.HasMoved = true;
                return true;
            }

            // Castling
            if (Color == 'W' && HasMoved == false && m.toCol == 2)
            {
                for (int i = 0; i < white.Length; i++)
                {
                    if (white[i].Name == 'R' && white[i].HasMoved == false && white[i].Col == 0)
                    {
                        white[i].Col = 3;
                        white[i].HasMoved = true;
                        this.HasMoved = true;
                        return true;
                    }
                }
            }

            if(Color == 'W' && HasMoved == false && m.toCol == 6)
            { 
                for(int i = 0; i < white.Length; i++)
                {
                    if(white[i].Name == 'R' && white[i].HasMoved == false && white[i].Col == 7)
                    {
                        white[i].Col = 5;
                        white[i].HasMoved = true;
                        this.HasMoved = true;
                        return true;
                    }
                }
                return false;
            }

            if(Color == 'B' && HasMoved == false && m.toCol == 2)
            {
                for(int i = 0; i < white.Length; i++)
                {
                    if(black[i].Name == 'R' && black[i].HasMoved == false && black[i].Col == 0)
                    {
                        black[i].Col = 3;
                        black[i].HasMoved = true;
                        this.HasMoved = true;
                        return true;
                    }
                }
            }

            if(Color == 'B' && HasMoved == false && m.toCol == 6)
            {
                for(int i = 0; i < white.Length; i++)
                {
                    if(black[i].Name == 'R' && black[i].HasMoved == false && black[i].Col == 7)
                    {
                        black[i].Col = 5;
                        black[i].HasMoved = true;
                        this.HasMoved = true;
                        return true;
                    }
                }
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
            Name = 'P';
            if (color == 'W')
            {
                Img = "../../Images/white_pawn.png";
            }
            else
            {
                Img = "../../Images/black_pawn.png";
            }
        }

        public override bool ValidMove(Move m, Piece[] white, Piece[] black)
        {
            int rowDiff = m.fromRow - m.toRow;
            int colDiff = m.fromCol - m.toCol;

            //Regular pawn movement
            //TODO: Implement pawn capturing
            //TODO: Implement in peasant
            if(Color == 'B' && colDiff == 0 && m.toRow == (m.fromRow + 1))
            {
                this.HasMoved = true;
                return true;
            }
            if(Color == 'W' && colDiff == 0 && m.toRow == (m.fromRow - 1))
            {
                this.HasMoved = true;
                return true;
            }

            //Pawn double movement on the first move 
            if(Color == 'B' && this.HasMoved == false && colDiff == 0 && rowDiff == -2)
            {
                this.HasMoved = true;
                return true;
            }
            if(Color == 'W' && this.HasMoved == false && colDiff == 0 && rowDiff == 2)
            {
                this.HasMoved = true;
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
            Name = 'Q';
            if (color == 'W')
            {
                Img = "../../Images/white_queen.png";
            }
            else
            {
                Img = "../../Images/black_queen.png";
            }
        }
        public override bool ValidMove(Move m, Piece[] white, Piece[] black)
        {
            int rowDiff = m.toRow - m.fromRow;
            int colDiff = m.toCol - m.fromCol;

            if (rowDiff == 0 || colDiff == 0 || Math.Abs(rowDiff) == Math.Abs(colDiff))
            {
                this.HasMoved = true;
                return true;
            }
            System.Console.WriteLine("BAD QUEEN MOVE");
            return false;
        }
	}
}
