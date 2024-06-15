namespace MultiMonitorCalendar;

public partial class TaskbarClockOverlay : Form
{
    private static readonly string _notificationCenterText = "Notification Center";

    private Screen Screen { get; }
    private nint TaskbarHandle => GetWindowFromPoint(new Point(Screen.Bounds.Right - 1, Screen.Bounds.Bottom - 1)); // TODO: Support taskbar locations other than fixed screen bottom
    private Rect TaskbarLocation => TaskbarHandle is not 0 && GetWindowRect(TaskbarHandle, out var location) ? location : throw new Exception("Unable to locate taskbar");

#pragma warning disable CS8618
    // ReSharper disable once MemberCanBePrivate.Global
    public TaskbarClockOverlay() => InitializeComponent();
#pragma warning restore CS8618

    public TaskbarClockOverlay(Screen screen) : this()
    {
        Screen = screen;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        var taskbarLocation = TaskbarLocation;
        Location = new Point(taskbarLocation.Right - 142, taskbarLocation.Top);
        Size = new Size(142, taskbarLocation.Bottom - taskbarLocation.Top);

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
                U = new InputWrapper.Input
                {
                    ki = new KeyboardInput
                    {
                        wVk = VirtualKeyShort.LWIN
                    }
                }
            },
            new()
            {
                type = 1,
                U = new InputWrapper.Input
                {
                    ki = new KeyboardInput
                    {
                        wScan = ScanCodeShort.KEY_N,
                        dwFlags = KeyEventFlags.SCANCODE
                    }
                }
            },
            new()
            {
                type = 1,
                U = new InputWrapper.Input
                {
                    ki = new KeyboardInput
                    {
                        wVk = VirtualKeyShort.LWIN,
                        dwFlags = KeyEventFlags.KEYUP
                    }
                }
            },
            new()
            {
                type = 1,
                U = new InputWrapper.Input
                {
                    ki = new KeyboardInput
                    {
                        wScan = ScanCodeShort.KEY_N,
                        dwFlags = KeyEventFlags.SCANCODE | KeyEventFlags.KEYUP
                    }
                }
            });
    }

    private void Form1_MouseEnter(object sender, EventArgs e)
    {
        if ((FindWindow(null, _notificationCenterText) is var calendarHandle && calendarHandle == nint.Zero) || !GetWindowRect(calendarHandle, out var location))
            return;

        var taskbarLocation = TaskbarLocation;
        SetWindowPos(calendarHandle, -1, taskbarLocation.Right - (location.Right - location.Left), taskbarLocation.Top + (location.Top - location.Bottom), 0, 0, 0x0001 | 0x0004 | 0x0040 | 0x0010);
    }
}