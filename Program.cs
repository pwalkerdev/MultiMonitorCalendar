global using MultiMonitorCalendar.Windows;
global using MultiMonitorCalendar.Windows.Input;
global using System.Runtime.InteropServices;
global using static MultiMonitorCalendar.Windows.User32.User32;
global using static MultiMonitorCalendar.Windows.Input.Input;

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

        if (Screen.AllScreens.OrderByDescending(s => s.Primary).Select(s => new TaskbarClockOverlay(s)).ToArray() is not { Length: > 0 } forms)
            return;

        foreach (var form in forms)
            form.Show();
            
        Application.Run(new ApplicationContext(forms[0]));
    }
}