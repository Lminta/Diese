using System.Runtime.InteropServices;

namespace Diese.Events;

[StructLayout(LayoutKind.Sequential)]
public struct QuitEvent : IEvent
{ }