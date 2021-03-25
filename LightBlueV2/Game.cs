using System;
using System.Drawing;
using System.Windows.Forms;


namespace LightBlueV2
{ 

	// TODO: PUT THIS IN BOARD
	// Start a drag of a piece.
	public class MoveForm : Form {
		private PictureBox pieceDragSource;
		private PictureBox pieceDropTarget;
		private void MoveForm_Load(object sender, EventArgs e)
		{
			pieceDropTarget.AllowDrop = true;
		}
		private void Piece_Load(object sender, EventArgs e)
		{
			pieceDropTarget.AllowDrop = true;
		}
		private void pieceDragSource_MouseDown(object sender,
			MouseEventArgs e)
		{
			// Start the drag if it's the left mouse button.
			if (e.Button == MouseButtons.Left)
			{
				pieceDragSource.DoDragDrop(pieceDragSource.Image,
					DragDropEffects.Move);
			}
		}
		// Accept the drop.
		private void pieceDropTarget_DragDrop(object sender,
			DragEventArgs e)
		{
			pieceDropTarget.Image =
				(Bitmap)e.Data.GetData(DataFormats.Bitmap, true);
		}
	}

	public class Game
	{
		public string status;
		public string player;
		public void GameLoop()
		{
			while (status != "finished")
			{
				Turn(player);
			}
		}
		public void Turn()
		{
			if (player == "white")
			{

			}
		}
	}
}