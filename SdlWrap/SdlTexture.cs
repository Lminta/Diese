using System.Runtime.InteropServices;

namespace Diese.SdlWrap;

internal class SdlTexture(IntPtr texture, int width, int height) : IDisposable
{
    public IntPtr Texture => texture;
    
    public void UpdateTexture(UInt32[] pixels)
    {
        if (pixels == null)
            throw new ArgumentNullException(nameof(pixels), "Pixel array cannot be null.");
        
        var handle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
        try
        {
            SdlInternal.SDL_UpdateTexture(
                texture,
                IntPtr.Zero,               
                handle.AddrOfPinnedObject(), 
                4 * width
            );
        }
        finally
        {
            handle.Free();
        }
    }
    
    public void Dispose()
    {
        SdlInternal.SDL_DestroyTexture(texture);
    }
}