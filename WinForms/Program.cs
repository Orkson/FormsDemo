using WinForms.Data;

namespace WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var dbContext = new NewContext();
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(dbContext));
        }
    }
}