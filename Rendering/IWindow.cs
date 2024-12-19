namespace Diese.Rendering;

public interface IWindow : IDisposable
{
    void RenderTexture(ITexture texture);
}