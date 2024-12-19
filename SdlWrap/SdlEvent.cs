using System.Runtime.InteropServices;
using Diese.Events;

namespace Diese.SdlWrap;

[StructLayout(LayoutKind.Sequential)]
public struct SdlEvent
{
    public uint Type;
    public uint Timestamp;
    
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
    public byte[] Data;
}