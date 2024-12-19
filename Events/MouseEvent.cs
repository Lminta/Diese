using System.Runtime.InteropServices;

namespace Diese.Events;

public enum InputDevice
{
    MOUSE = 0,
    TOUCH = 1,
    JOYSTICK = 2
}

public enum ButtonState : byte
{
    RELEASED = 0,
    PRESSED = 1
}

[StructLayout(LayoutKind.Sequential)]
public struct MouseMotionEvent : IEvent
{
    public uint WindowId;
    public InputDevice Which;
    public int X;
    public int Y;
    public int XRel;
    public int YRel;
}

[StructLayout(LayoutKind.Sequential)]
public struct MouseButtonEvent : IEvent
{
    public uint WindowId;
    public InputDevice Which;
    public byte Button;
    public ButtonState State;
    public byte Clicks;
    public byte Padding1;
    public int X;
    public int Y;
}
