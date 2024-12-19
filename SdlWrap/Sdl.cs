namespace Diese.SdlWrap;

internal static class Sdl
{
    static bool _initialized = false;
    static int _windowCount = 0;
    public static bool Initialized => _initialized;
    public static int WindowCount => _windowCount;
    
    public static void Initialize()
    {
        if (_initialized)
        {
            throw new Exception("SDL is already initialized!");
        }
        
        if (SdlInternal.SDL_Init(SdlConstHelper.SDL_INIT_VIDEO) < 0)
        {
            throw new Exception($"SDL could not initialize! Error: {SdlInternal.GetError()}");
        }
        
        _initialized = true;
    }

    public static SdlWindow CreateWindow(string title, int posX, int posY, int width, int height)
    {
        if (!_initialized)
        {
            throw new Exception("SDL must be initialized before creating a window!");
        }
        
        var window = SdlInternal.SDL_CreateWindow(
            title,
            posX,
            posY,
            width,
            height,
            SdlConstHelper.SDL_WINDOW_SHOWN);
        
        if (window == IntPtr.Zero)
        {
            throw new Exception($"Window could not be created! Error: {SdlInternal.GetError()}");
        }
        
        _windowCount++;
        return new SdlWindow(window, width, height, OnWindowClose);
    }
    
    public static Dictionary<uint, List<SdlEvent>> PollEvents()
    {
        var sdlEvents = new Dictionary<uint, List<SdlEvent>>();
        while (SdlInternal.SDL_PollEvent(out var sdlEvent) > 0)
        {
            if (!sdlEvents.TryGetValue(sdlEvent.Type, out var value))
            {
                value = new List<SdlEvent>();
                sdlEvents.Add(sdlEvent.Type, value);
            }

            value.Add(sdlEvent);
        }
        return sdlEvents;
    }

    
    static void OnWindowClose()
    {
        _windowCount--;
    }
    
    public static void Quit()
    {
        if (!_initialized)
        {
            throw new Exception("SDL must be initialized before quitting!");
        }
        
        SdlInternal.SDL_Quit();
        _initialized = false;
    }
}