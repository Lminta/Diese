using System.Runtime.InteropServices;

namespace Diese.Events;

public enum SdlWindowEventType : byte
{
    SHOWN = 0x01,
    HIDDEN = 0x02,
    EXPOSED = 0x03,
    RESIZED = 0x05,
    FOCUS_GAINED = 0x08,
    FOCUS_LOST = 0x09,
    CLOSE = 0x0A
}

[StructLayout(LayoutKind.Sequential)]
public struct WindowEvent : IEvent
{
    public uint WindowId;
    public SdlWindowEventType EventId;
    public byte Padding1;
    public byte Padding2;
    public byte Padding3;
    public int Data1;
    public int Data2;
}