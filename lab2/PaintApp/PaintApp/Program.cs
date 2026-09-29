using System;
using System.Windows.Forms;

namespace PaintApp
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            var view = new PaintView();
            var model = new PaintModel();
            var presenter = new PaintPresenter( view, model );
            Application.Run( view );
        }
    }
}
