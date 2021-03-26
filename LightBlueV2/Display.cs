using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LightBlueV2
{
    public partial class Display : Form
    {
        public Display()
        {
            InitializeComponent();
        }


        private PictureBox PB = new PictureBox();


        private void Board_Load(object sender, EventArgs e)
        {
            PB.Dock = DockStyle.Fill;
            PB.BackColor = Color.PapayaWhip;
            PB.Paint += new System.Windows.Forms.PaintEventHandler(this.Board_Draw);
            
            Display.Board board = new Display.Board(PB);
            board.DrawBoxesInit();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board.pbs[i, j].Parent = PB;
                }
            }
            this.Controls.Add(PB);
        }

        private void Board_Draw(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Brush b = new SolidBrush(Color.Tan);

            int SquareWidth = (PB.Height / 8);

            int Rx;
            int Ry;

            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if((j+i) % 2 == 1)
                    {
                        Rx = i * SquareWidth;
                        Ry = j * SquareWidth;
                   
                        e.Graphics.FillRectangle(b, new Rectangle(Rx, Ry, SquareWidth+1, SquareWidth+1));
                    }
                }
            }
        }
        // Start a drag of a piece.
        public class Board : Form
        {
            public Move CurrentMove;
            public PictureBox[,] pbs;

            // 695 by 720
            public float SquareWidth = 695 / 8;
            private Cursor dragCursor;

            private Game G = new Game();

            public Board(PictureBox PB)
            {
                pbs = new PictureBox[8, 8];
                int x_coord = 0;
                int y_coord = 0;
                for (int i = 0; i < pbs.GetLength(0); i++)
                {
                    for (int j = 0; j < pbs.GetLength(1); j++)
                    {
                        pbs[i, j] = new PictureBox();
                        pbs[i, j].Width = (int)Math.Floor(SquareWidth - 10);
                        pbs[i,j].Height = (int)Math.Floor(SquareWidth - 10);
                        pbs[i,j].SizeMode = PictureBoxSizeMode.StretchImage;
                        pbs[i,j].Dock = DockStyle.None;
                        pbs[i,j].MouseDown += pieceDragSource_MouseDown;
                        pbs[i,j].DragEnter += pieceDropTarget_DragEnter;
                        pbs[i,j].DragDrop += pieceDropTarget_DragDrop;
                        pbs[i, j].GiveFeedback += pieceDragSource_GiveFeedback;
                        pbs[i,j].Location = new Point(x_coord, y_coord);
                        pbs[i,j].BackColor = Color.Transparent;
                        pbs[i, j].AllowDrop = true;
                        this.Controls.Add(pbs[i,j]);
                        x_coord += (int)Math.Floor(SquareWidth);
                    }
                    x_coord = 0;
                    y_coord += (int)Math.Floor(SquareWidth);
                }
            }
            public void DrawBoxesInit()
            {
                pbs[0, 0].Image = Image.FromFile("../../Images/white_rook.png");
                pbs[0, 1].Image = Image.FromFile("../../Images/white_knight.png");
                pbs[0, 2].Image = Image.FromFile("../../Images/white_bishop.png");
                pbs[0, 3].Image = Image.FromFile("../../Images/white_king.png");
                pbs[0, 4].Image = Image.FromFile("../../Images/white_queen.png");
                pbs[0, 5].Image = Image.FromFile("../../Images/white_bishop.png");
                pbs[0, 6].Image = Image.FromFile("../../Images/white_knight.png");
                pbs[0, 7].Image = Image.FromFile("../../Images/white_rook.png");

                for (int i = 0; i < 8; i++)
                {
                    pbs[1, i].Image = Image.FromFile("../../Images/white_pawn.png");
                    pbs[6, i].Image = Image.FromFile("../../Images/black_pawn.png");
                }

                pbs[7, 0].Image = Image.FromFile("../../Images/black_rook.png");
                pbs[7, 1].Image = Image.FromFile("../../Images/black_knight.png");
                pbs[7, 2].Image = Image.FromFile("../../Images/black_bishop.png");
                pbs[7, 3].Image = Image.FromFile("../../Images/black_king.png");
                pbs[7, 4].Image = Image.FromFile("../../Images/black_queen.png");
                pbs[7, 5].Image = Image.FromFile("../../Images/black_bishop.png");
                pbs[7, 6].Image = Image.FromFile("../../Images/black_knight.png");
                pbs[7, 7].Image = Image.FromFile("../../Images/black_rook.png");
            }
            public void DrawBoxesFromDisplay(Piece[,] pieces)
            {
                for (int i = 0; i < pieces.GetLength(0); i++)
                {
                    for (int j = 0; j < pieces.GetLength(1); j++)
                    {
                        string img = pieces[i, j].Img;
                        int row = pieces[i, j].Row;
                        int col = pieces[i, j].Col;
                        pbs[row, col].Image = Image.FromFile(img);
                    }
                }
            }
            public void pieceDragSource_MouseDown(object sender,
                MouseEventArgs e)
            {
                // Start the drag if it's the left mouse button.
                if (e.Button == MouseButtons.Left)
                {
                    PictureBox pb = (PictureBox)sender;

                    float SquareWidth = 695 / 8;
                    CurrentMove.fromCol = (int) pb.Location.X / (int) SquareWidth;
                    CurrentMove.fromRow = (int) pb.Location.Y / (int) SquareWidth;
                    

                    if (pb.Image != null)
                    {
                        Image tempImage = pb.Image;
                        pb.Image = null;
                        Bitmap bm = new Bitmap(tempImage);
                        IntPtr Hicon = bm.GetHicon();
                        dragCursor = new Cursor(Hicon);
                        Cursor.Current = dragCursor;
                        DragDropEffects drop = pb.DoDragDrop(tempImage,
                            DragDropEffects.Move);
                        if (drop == DragDropEffects.None)
                        {
                            pb.Image = tempImage;
                        }
                    }
                }
            }
            private void pieceDragSource_GiveFeedback(object sender, GiveFeedbackEventArgs e)
            {
                // Use custom cursor.
                // Sets the custom cursor based upon the effect.
                e.UseDefaultCursors = false;
                Cursor.Current = dragCursor;
            }
            // Allow a move of an image.
            private void pieceDropTarget_DragEnter(object sender,
                DragEventArgs e)
            {
                // See if this is a move and the data includes an image.
                if (e.Data.GetDataPresent(DataFormats.Bitmap) &&
                    (e.AllowedEffect & DragDropEffects.Move) != 0)
                {
                    // Allow this.
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    // Don't allow any other drop.
                    e.Effect = DragDropEffects.None;
                }
            }
            // Accept the drop.
            public void pieceDropTarget_DragDrop(object sender,
                DragEventArgs e)
            {
                PictureBox pb = (PictureBox)sender;

                float SquareWidth = 695 / 8;
                CurrentMove.toCol = (int) pb.Location.X / (int) SquareWidth;
                CurrentMove.toRow = (int) pb.Location.Y / (int) SquareWidth;

                if (G.ValidateMove(CurrentMove))
                {
                    pb.Image =
                        (Bitmap)e.Data.GetData(DataFormats.Bitmap, true);
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }
    }
}
