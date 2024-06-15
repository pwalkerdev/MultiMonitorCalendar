namespace MultiMonitorCalendar;

public partial class Form1 : Form
{
    private static readonly string _notificationCenterText = "Notification Center";

    private Screen Screen { get; }
    private nint TaskBarHandle => GetWindowFromPoint(new Point(Screen.Bounds.Right - 1, Screen.Bounds.Bottom - 1)); // TODO: Support taskbar locations other than fixed screen bottom
    private Rect TaskBarLocation => TaskBarHandle is not 0 && GetWindowRect(TaskBarHandle, out var location) ? location : throw new Exception("Unable to locate taskbar");

#pragma warning disable CS8618
    // ReSharper disable once MemberCanBePrivate.Global
    public Form1() => InitializeComponent();
#pragma warning restore CS8618

    public Form1(Screen screen) : this()
    {
        Screen = screen;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        var taskBarLocation = TaskBarLocation;
        Location = new Point(taskBarLocation.Right - 148, taskBarLocation.Top);
        Size = new Size(148, taskBarLocation.Bottom - taskBarLocation.Top);

        SetWindowPos(Handle, -1, 0, 0, 0, 0, 0x0001 /* SWP_NOSIZE */ | 0x0002 /* SWP_NOMOVE */ | 0x0010 /* SWP_NOACTIVATE */);
        SetWindowLong(Handle, -20, GetWindowLong(Handle, -20) | 0x80 /* WS_EX_TOOLWINDOW */);
    }

    private void Form1_Click(object sender, EventArgs e)
    {
        // Simulate the keyboard event "Win+N" which shows the calendar
        Input.Send(
            new()
            {
                type = 1,
                U = new Input.InputUnion
                {
                    ki = new Input.KEYBDINPUT
                    {
                        wVk = Input.VirtualKeyShort.LWIN
                    }
                }
            },
            new()
            {
                type = 1,
                U = new Input.InputUnion
                {
                    ki = new Input.KEYBDINPUT
                    {
                        wScan = Input.ScanCodeShort.KEY_N,
                        dwFlags = Input.KEYEVENTF.SCANCODE
                    }
                }
            },
            new()
            {
                type = 1,
                U = new Input.InputUnion
                {
                    ki = new Input.KEYBDINPUT
                    {
                        wVk = Input.VirtualKeyShort.LWIN,
                        dwFlags = Input.KEYEVENTF.KEYUP
                    }
                }
            },
            new()
            {
                type = 1,
                U = new Input.InputUnion
                {
                    ki = new Input.KEYBDINPUT
                    {
                        wScan = Input.ScanCodeShort.KEY_N,
                        dwFlags = Input.KEYEVENTF.SCANCODE | Input.KEYEVENTF.KEYUP
                    }
                }
            });
    }

    private void Form1_MouseEnter(object sender, EventArgs e)
    {
        if ((FindWindow(null, _notificationCenterText) is var calendarHandle && calendarHandle == nint.Zero) || !GetWindowRect(calendarHandle, out var location))
            return;

        var taskBarLocation = TaskBarLocation;
        SetWindowPos(calendarHandle, -1, taskBarLocation.Right - (location.Right - location.Left), taskBarLocation.Top + (location.Top - location.Bottom), 0, 0, 0x0001 | 0x0004 | 0x0040 | 0x0010);
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr FindWindow(string? lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);
    [DllImport("user32.dll", EntryPoint = "WindowFromPoint")]
    private static extern IntPtr GetWindowFromPoint(Point pt);
    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);
    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
}