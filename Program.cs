global using System.Runtime.InteropServices;

namespace MultiMonitorCalendar;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        if (Screen.AllScreens.OrderByDescending(s => s.Primary).Select(s => new Form1(s)).ToArray() is not { Length: > 0 } forms)
            return;

        foreach (var form in forms)
            form.Show();
            
        Application.Run(new ApplicationContext(forms[0]));
    }
}