using System;
using System.Windows.Forms;
using Dovidnik_Abiturienta;
using Dovidnik_Abiturienta.Forms;

namespace Dovidnik_Abiturienta
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
