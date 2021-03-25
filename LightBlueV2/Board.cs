using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightBlueV2
{
    public partial class Board : Form
    {
        public Board()
        {
            InitializeComponent();
        }

        private PictureBox PB = new PictureBox();

        private void Board_Load(object sender, EventArgs e)
        {
            PB.Dock = DockStyle.Fill;
            PB.BackColor = Color.PapayaWhip;
            PB.Paint += new System.Windows.Forms.PaintEventHandler(this.Board_Draw);

            
            Board.MoveForm moveForm = new Board.MoveForm();
            moveForm.pieceDropTarget.Parent = PB;
            moveForm.pieceDropTarget.Location = new Point(200,200);
            moveForm.pieceDragSource.Parent = PB;
            moveForm.pieceDragSource.BackColor = Color.Transparent;
            moveForm.pieceDropTarget.BackColor = Color.Transparent;
            moveForm.pieceDragSource.Location = new Point(500, 500);
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
        public class MoveForm : Form
        {
            public PictureBox pieceDragSource;
            public PictureBox pieceDropTarget;
            public MoveForm()
            {
                this.SuspendLayout();
                pieceDragSource = new PictureBox();
                pieceDragSource.Image = Image.FromFile("../../Images/black_bishop.png");
                pieceDropTarget = new PictureBox();
                pieceDropTarget.Image = Image.FromFile("../../Images/white_bishop.png");
                pieceDragSource.Width = 100;
                pieceDragSource.Height = 100;
                pieceDropTarget.Width = 100;
                pieceDropTarget.Height = 100;
                pieceDropTarget.SizeMode = PictureBoxSizeMode.StretchImage;
                pieceDragSource.SizeMode = PictureBoxSizeMode.StretchImage;
                pieceDropTarget.Dock = DockStyle.None;
                pieceDragSource.Dock = DockStyle.None;
                pieceDragSource.MouseDown += pieceDragSource_MouseDown;
                pieceDropTarget.DragEnter += pieceDropTarget_DragEnter;
                pieceDropTarget.DragDrop += pieceDropTarget_DragDrop;
                pieceDropTarget.AllowDrop = true;
                this.Controls.Add(pieceDragSource);
                this.Controls.Add(pieceDropTarget);
                this.ResumeLayout(false);
            }
            public void pieceDragSource_MouseDown(object sender,
                MouseEventArgs e)
            {
                // Start the drag if it's the left mouse button.
                if (e.Button == MouseButtons.Left)
                {
                    pieceDragSource.DoDragDrop(pieceDragSource.Image,
                        DragDropEffects.Move);
                }
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
                pieceDropTarget.Image =
                    (Bitmap)e.Data.GetData(DataFormats.Bitmap, true);
            }
        }
    }
}
