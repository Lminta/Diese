using System.Runtime.InteropServices;

namespace Diese.Events;

[StructLayout(LayoutKind.Sequential)]
public struct KeyboardEvent : IEvent
{
    public uint WindowId;
    public ButtonState State;
    public byte Repeat;
    public byte Padding2;
    public byte Padding3;
    public Keysym Keysym;
}

public enum Scancode : int
{
    UNKNOWN = 0,
    A = 4,
    B = 5,
    C = 6,
    D = 7,
    E = 8,
    F = 9,
    G = 10,
    H = 11,
    I = 12,
    // Add more scancodes as needed
}

public enum Keycode : int
{
    UNKNOWN = 0,
    RETURN = '\r',
    ESCAPE = 27,
    BACKSPACE = 8,
    TAB = 9,
    SPACE = 32,
    A = 'a',
    B = 'b',
    C = 'c',
    // Add more keycodes as needed
}

[Flags]
public enum Keymod : ushort
{
    NONE = 0x0000,
    LSHIFT = 0x0001,
    RSHIFT = 0x0002,
    LCTRL = 0x0040,
    RCTRL = 0x0080,
    LALT = 0x0100,
    RALT = 0x0200,
    LGUI = 0x0400,
    RGUI = 0x0800,
    NUM = 0x1000,
    CAPS = 0x2000,
    MODE = 0x4000,
    RESERVED = 0x8000
}

[StructLayout(LayoutKind.Sequential)]
public struct Keysym
{
    public Scancode Scancode; // Physical key code
    public Keycode Sym;       // Virtual key code
    public Keymod Mod;           // Current key modifiers
    public uint Unused;          // Reserved
}
