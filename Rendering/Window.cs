using System.Diagnostics;
using Diese.SdlWrap;
using Diese.Utils;

namespace Diese.Rendering;

internal class Window : IWindow
{
    SdlWindow _window;
    SdlRenderer _renderer;
    SdlTexture _renderTexture;
    Action<Window> _onDispose;
    
    internal Window(string name, int posX, int posY, int width, int height, Action<Window> onDispose)
    {
        _window = Sdl.CreateWindow(name, posX, posY, width, height);
        _renderer = _window.GetOrCreateRenderer();
        _renderTexture = _renderer.CreateTexture();
        _onDispose = onDispose;
        Logger.Log($"Created window: {name}");
    }

    public void Update()
    {
        _renderer.RenderOnScreen();
    }
    
    public void RenderTexture(ITexture texture)
    {
        _renderTexture.UpdateTexture(texture.Pixels);
        _renderer.RenderTexture(_renderTexture);
    }
    
    public void Dispose()
    {
        _renderer.Dispose();
        _window.Dispose();
        _onDispose?.Invoke(this);
        Logger.Log("Window disposed");
    }
}