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
    }
}
