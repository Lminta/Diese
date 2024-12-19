namespace Diese.SdlWrap;

internal class SdlWindow(IntPtr window, int width, int height, Action onClose) : IDisposable
{
    public int Width => width;
    public int Height => height;
    
    SdlRenderer? _renderer = null;

    public SdlRenderer GetOrCreateRenderer()
    {
        if (_renderer != null)
        {
            return _renderer;
        }
        
        var renderer = SdlInternal.SDL_CreateRenderer(window, -1, 0);
        if (renderer == IntPtr.Zero)
        {
            throw new Exception($"Renderer could not be created! Error: {SdlInternal.GetError()}");
        }

        _renderer = new SdlRenderer(this, renderer, OnRendererDispose);
        return _renderer;
    }

    void OnRendererDispose()
    {
        _renderer = null;
    }
    
    public void Dispose()
    {
        SdlInternal.SDL_DestroyWindow(window);
        onClose?.Invoke();
    }
}