namespace MultiMonitorCalendar.Windows.Input;

// https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-hardwareinput
[StructLayout(LayoutKind.Sequential)]
internal struct HardwareInput
{
    internal int uMsg;
    internal short wParamL;
    internal short wParamH;
}