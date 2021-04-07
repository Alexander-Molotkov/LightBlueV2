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

using System.Reflection;
using System.Runtime.InteropServices;

// https://stackoverflow.com/questions/24695976/resize-system-icon-in-c-sharp
public class IconEx : IDisposable
{
    public enum SystemIcons
    {
        Application = 100,
        Asterisk = 104,
        Error = 103,
        Exclamation = 101,
        Hand = 103,
        Information = 104,
        Question = 102,
        Shield = 106,
        Warning = 101,
        WinLogo = 100
    }

    // Edited to pass in hicon instead of path
    public IconEx(IntPtr hicon, Size size)
    {
        if (hicon == IntPtr.Zero) throw new System.ComponentModel.Win32Exception();
        attach(hicon);

    }
    public IconEx(SystemIcons sysicon, Size size)
    {
        IntPtr hUser = GetModuleHandle("user32");
        IntPtr hIcon = LoadImage(hUser, (IntPtr)sysicon, IMAGE_ICON, size.Width, size.Height, 0);
        if (hIcon == IntPtr.Zero) throw new System.ComponentModel.Win32Exception();
        attach(hIcon);
    }


    public Icon Icon
    {
        get { return this.icon; }
    }

    public void Dispose()
    {
        if (icon != null) icon.Dispose();
    }

    private Icon icon;

    private void attach(IntPtr hIcon)
    {
        // Invoke the private constructor so we can get the Icon object to own the handle
        var ci = typeof(Icon).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance,
            null, new Type[] { typeof(IntPtr), typeof(bool) }, null);
        this.icon = (Icon)ci.Invoke(new object[] { hIcon, true });
    }

    private const int IMAGE_ICON = 1;
    private const int LR_LOADFROMFILE = 0x10;
    private const int LR_SHARED = 0x8000;

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern IntPtr GetModuleHandle(string name);
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern IntPtr LoadImage(IntPtr hinst, string lpszName, int uType,
                                 int cxDesired, int cyDesired, int fuLoad);
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern IntPtr LoadImage(IntPtr hinst, IntPtr resId, int uType,
                                 int cxDesired, int cyDesired, int fuLoad);
}

namespace LightBlueV2
{
    public partial class Display : Form
    {
        public Display()
        {
            InitializeComponent();
        }


        private PictureBox PB = new PictureBox();
        public Display.Board board;

        private void Board_Load(object sender, EventArgs e)
        {
            PB.Dock = DockStyle.Fill;
            PB.BackColor = Color.PapayaWhip;
            PB.Paint += new System.Windows.Forms.PaintEventHandler(this.Board_Draw);
            
            board = new Display.Board(PB);

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

            public Game G;

            public Engine E;

            public char allowedColor;

            public Board(PictureBox PB)
            {
                E = new Engine();
                G = new Game();
                pbs = new PictureBox[8, 8];
                int x_coord = 0;
                int y_coord = 0;
                for (int i = 0; i < pbs.GetLength(0); i++)
                {
                    for (int j = 0; j < pbs.GetLength(1); j++)
                    {
                        pbs[i, j] = new PictureBox();
                        pbs[i, j].Width = (int)Math.Floor(SquareWidth - 10);
                        pbs[i, j].Height = (int)Math.Floor(SquareWidth - 10);
                        pbs[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                        pbs[i, j].Dock = DockStyle.None;
                        pbs[i, j].MouseDown += pieceDragSource_MouseDown;
                        pbs[i, j].DragEnter += pieceDropTarget_DragEnter;
                        pbs[i, j].DragDrop += pieceDropTarget_DragDrop;
                        pbs[i, j].GiveFeedback += pieceDragSource_GiveFeedback;
                        pbs[i, j].Location = new System.Drawing.Point(x_coord, y_coord);
                        pbs[i, j].BackColor = Color.Transparent;
                        pbs[i, j].AllowDrop = true;
                        this.Controls.Add(pbs[i, j]);
                        x_coord += (int)Math.Floor(SquareWidth);
                    }
                    x_coord = 0;
                    y_coord += (int)Math.Floor(SquareWidth);
                }
                DrawBoxesFromDisplay(G.whitePieces, G.blackPieces);
                G.Turn(E, this);
                G.Turn(E, this);
            }

            public void DrawBoxesFromDisplay(Piece[] whitePieces, Piece[] blackPieces)
            {

                foreach(PictureBox p in pbs)
                {
                    p.Image = null;
                }
                for (int i = 0; i < whitePieces.GetLength(0); i++)
                {
                    pbs[whitePieces[i].Row, whitePieces[i].Col].Image = Image.FromFile(whitePieces[i].Img);
                }
                for (int i = 0; i < blackPieces.GetLength(0); i++)
                {
                    pbs[blackPieces[i].Row, blackPieces[i].Col].Image = Image.FromFile(blackPieces[i].Img);
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

                        // This chunk is non-functional, but let's see if it does something eventually.
                        Graphics graphics = this.CreateGraphics();
                        Rectangle rectangle = new Rectangle(
                          new Point(10, 10), new Size(Cursor.Size.Width/2,
                          (int)Math.Floor(SquareWidth)));
                        dragCursor.DrawStretched(graphics, rectangle);
                        DragDropEffects drop = pb.DoDragDrop(tempImage,
                            DragDropEffects.Move);
                        var icon = new IconEx(Hicon, new Size(10, 10));

                        Cursor.Current = dragCursor;
                        if (drop == DragDropEffects.None)
                        {
                            pb.Image = tempImage;
                        }
                        dragCursor.Dispose();
                        Cursor.Current = Cursors.Default;
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
                    DrawBoxesFromDisplay(G.whitePieces, G.blackPieces);
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }
    }
}
