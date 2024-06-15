namespace MultiMonitorCalendar.Windows.Input;

// https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-keybdinput
[StructLayout(LayoutKind.Sequential)]
internal struct KeyboardInput
{
    internal VirtualKeyShort wVk;
    internal ScanCodeShort wScan;
    internal KeyEventFlags dwFlags;
    internal int time;
    internal UIntPtr dwExtraInfo;
}

[Flags]
internal enum KeyEventFlags : uint
{
    EXTENDEDKEY = 0x0001,
    KEYUP = 0x0002,
    SCANCODE = 0x0008,
    UNICODE = 0x0004
}