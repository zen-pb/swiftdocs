using System;
using System.Windows.Forms;
using SwiftDocs.Forms;
using SwiftDocs.Services;

namespace SwiftDocs
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var userManager = new UserManager();
            Application.Run(new MenuForm(userManager));
        }
    }
}