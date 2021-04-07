using System;
using System.Windows.Forms;

namespace LightBlueV2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Display d = new Display();
            Application.Run(d);
        }
    }
}