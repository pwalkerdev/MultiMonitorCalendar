namespace MultiMonitorCalendar.Windows.Input;

internal static class Input
{
    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-input
    [StructLayout(LayoutKind.Sequential)]
    public struct InputWrapper
    {
        public uint type;
        public Input U;
        public static int Size => Marshal.SizeOf(typeof(InputWrapper));

        /// <summary>
        /// A hacky implementation of a discriminated union (not currently supported in the C# language) to store either a Mouse, Keyboard or Hardware input struct
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        public struct Input
        {
            [FieldOffset(0)]
            internal MouseInput mi;
            [FieldOffset(0)]
            internal KeyboardInput ki;
            [FieldOffset(0)]
            internal HardwareInput hi;
        }
    }

    public static uint Send(params InputWrapper[] inputs)
    {
        var count = (uint) inputs.Length;
        return SendInput(count, inputs, InputWrapper.Size);
    }
}