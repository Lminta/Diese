using Diese.Events;
using Diese.Rendering;
using Diese.SdlWrap;
using Diese.Utils;

namespace Diese;

public class Controller : Singleton<Controller>
{
    private static class EventActionContainer<TEvent> where TEvent : IEvent
    {
        public static Action<TEvent>? OnEvent; 
    }

    HashSet<Window> _activeWindows = new();
    HashSet<Window> _notActiveWindows = new();
    
    public void Initialize(ILogger logger)
    {
        Logger.SetupLogger(logger);
        if (Sdl.Initialized)
        {
            Logger.LogWarning("SDL already initialized");
            return;
        }

        Sdl.Initialize();
    }
    
    public IWindow CreateWindow(string name, int posX, int posY, int width, int height)
    {
        if (!Sdl.Initialized)
        {
            throw new Exception("Sdl not initialized");
        }

        var window = new Window(name, posX, posY, width, height, OnWindowClose);
        _activeWindows.Add(window);
        Logger.Log($"Opened window {name}");
        return window;
    }

    public void Update()
    {
        if (!Sdl.Initialized)
        {
            throw new Exception("Sdl not initialized");
        }
        
        foreach (var window in _activeWindows)
        {
            window.Update();
        }

        HandleEvents(Sdl.PollEvents());
    }

    void HandleEvents(Dictionary<uint, List<SdlEvent>> events)
    {
        foreach (var (eventCode, eventList) in events)
        {
            var eventType = (EventType)eventCode;
            Logger.Log($"Event {eventType}");
            
            switch (eventType)
            {
                case EventType.QUIT:
                    HandleEventList<QuitEvent>(eventList);
                    break;
                case EventType.WINDOW_EVENT:
                    HandleEventList<WindowEvent>(eventList);
                    break;
                case EventType.KEY_DOWN:
                case EventType.KEY_UP:
                    HandleEventList<KeyboardEvent>(eventList);
                    break;
                case EventType.MOUSE_MOTION:
                    HandleEventList<MouseMotionEvent>(eventList);
                    break;
                case EventType.MOUSE_BUTTON_DOWN:
                case EventType.MOUSE_BUTTON_UP:
                case EventType.MOUSE_WHEEL:
                    HandleEventList<MouseButtonEvent>(eventList);
                    break;
                default:
                    Logger.LogWarning($"Unknown event type: {eventType}");
                    break;
            }
        }
    }

    void HandleEventList<TEvent>(List<SdlEvent> events) where TEvent : IEvent
    {
        foreach (var sdlEvent in events)
        {
            var parsedEvent = EventMarshall.MapEventData<TEvent>(sdlEvent);
            EventActionContainer<TEvent>.OnEvent?.Invoke(parsedEvent);
        }
    }

    public void AddEventHandler<TEvent>(Action<TEvent> handler) where TEvent : IEvent
    {
        ArgumentNullException.ThrowIfNull(handler);

        EventActionContainer<TEvent>.OnEvent += handler;
    }

    public void RemoveEventHandler<TEvent>(Action<TEvent> handler) where TEvent : IEvent
    {
        ArgumentNullException.ThrowIfNull(handler);
        
        EventActionContainer<TEvent>.OnEvent -= handler;
    }

    void OnWindowClose(Window window)
    {
        _activeWindows.Remove(window);
        _notActiveWindows.Remove(window);
    }
    
    public void Quit()
    {
        if (!Sdl.Initialized)
        {
            throw new Exception("Sdl not initialized");
        }
        
        var windows = new List<Window>(_activeWindows);
        foreach (var window in windows)
        {
            window.Dispose();
        }
        Sdl.Quit();
        Logger.Log("SDL Quit");
    }
}