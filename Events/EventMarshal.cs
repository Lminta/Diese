using System.Runtime.InteropServices;
using Diese.SdlWrap;

namespace Diese.Events;

public static class EventMarshall
{
    public static TEvent MapEventData<TEvent>(SdlEvent rawEvent) where TEvent : IEvent
    {
        int size = Marshal.SizeOf<TEvent>();

        if (rawEvent.Data.Length < size)
        {
            throw new ArgumentException(
                $"Data size ({rawEvent.Data.Length}) is smaller than the size of {typeof(TEvent).Name} ({size}).");
        }

        IntPtr buffer = Marshal.AllocHGlobal(size);

        try
        {
            Marshal.Copy(rawEvent.Data, 0, buffer, size);
            var parsedEvent = Marshal.PtrToStructure<TEvent>(buffer);
            if (parsedEvent == null)
            {
                throw new Exception($"Failed to unmarshal event data from {typeof(TEvent).Name}");
            }

            return parsedEvent;
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
    }
}
