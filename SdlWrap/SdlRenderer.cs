namespace Diese.SdlWrap;

internal class SdlRenderer(SdlWindow window, IntPtr renderer, Action onDispose) : IDisposable
{
    public SdlTexture CreateTexture()
    {
        var texture = SdlInternal.SDL_CreateTexture(
            renderer,
            SdlConstHelper.SDL_PIXELFORMAT_ARGB8888,
            SdlConstHelper.SDL_TEXTUREACCESS_STATIC,
            window.Width, window.Height);
        
        if (texture == IntPtr.Zero)
        {
            throw new Exception($"Texture could not be created! Error: {SdlInternal.GetError()}");
        }

        return new SdlTexture(texture, window.Width, window.Height);
    }
    
    public void RenderTexture(SdlTexture texture)
    {
        var res = SdlInternal.SDL_RenderCopy(renderer, texture.Texture, IntPtr.Zero, IntPtr.Zero);
        if (res < 0)
        {
            throw new Exception($"Texture could not be rendered! Error: {SdlInternal.GetError()}");
        }
    }
    
    public void RenderOnScreen()
    {
        SdlInternal.SDL_RenderPresent(renderer);
    }
    
    public void Dispose()
    {
        SdlInternal.SDL_DestroyRenderer(renderer);
        onDispose?.Invoke();
    }
}