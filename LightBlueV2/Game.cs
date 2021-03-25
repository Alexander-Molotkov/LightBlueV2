using System;
using System.Drawing;
using System.Windows.Forms;


namespace LightBlueV2
{ 

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
		public void Turn(string player)
		{
			if (player == "white")
			{

			}
		}
	}
}