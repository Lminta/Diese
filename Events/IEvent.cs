namespace Diese.Events;

public enum EventType : uint
{
    QUIT = 0x100,
    WINDOW_EVENT = 0x200,
    KEY_DOWN = 0x300,
    KEY_UP = 0x301,
    MOUSE_MOTION = 0x400,
    MOUSE_BUTTON_DOWN = 0x401,
    MOUSE_BUTTON_UP = 0x402,
    MOUSE_WHEEL = 0x403
}

public interface IEvent
{
}