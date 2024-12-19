namespace Diese.SdlWrap;

using System;
using System.Runtime.InteropServices;

internal static class SdlInternal
{
    private const string SDL2_LIB = "ThirdParty/SDL2.dll";
    
    // Get the last SDL error
    [DllImport(SDL2_LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr SDL_GetError();

    public static string GetError()
    {
        IntPtr errorPtr = SDL_GetError();
        return Marshal.PtrToStringAnsi(errorPtr);
    }

    // Initialize SDL
    [DllImport(SDL2_LIB, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_Init(uint flags);

    // Create a window
    [DllImport(SDL2_LIB, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateWindow(
        string title,
        int x, int y,
        int w, int h,
        uint flags
    );
    
    // Destroy a window
    [DllImport(SDL2_LIB, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DestroyWindow(IntPtr window);

    // Create a renderer
    [DllImport(SDL2_LIB, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateRenderer(IntPtr window, int index, uint flags);
    
    // Destroy renderer
    [DllImport(SDL2_LIB, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DestroyRenderer(IntPtr renderer);

    // Render the screen
    [DllImport(SDL2_LIB, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_RenderPresent(IntPtr renderer);
    
    // Create an SDL Texture
    [DllImport(SDL2_LIB, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDL_CreateTexture(
        IntPtr renderer,              // Renderer pointer
        uint format,                  // Pixel format
        int access,                   // Texture access
        int w, int h                  // Width and height
    );

    // Update the texture with pixel data
    [DllImport(SDL2_LIB, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_UpdateTexture(
        IntPtr texture,               // Texture pointer
        IntPtr rect,                  // Rectangle to update (null = entire texture)
        IntPtr pixels,                // Pointer to pixel data
        int pitch                     // Length of a row in bytes
    );

    // Copy the texture to the renderer
    [DllImport(SDL2_LIB, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_RenderCopy(
        IntPtr renderer,              // Renderer pointer
        IntPtr texture,               // Texture pointer
        IntPtr srcrect,               // Source rectangle (null = entire texture)
        IntPtr dstrect                // Destination rectangle (null = entire window)
    );

    // Free a texture
    [DllImport(SDL2_LIB, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_DestroyTexture(IntPtr texture);
    
    // Poll SDL events
    [DllImport(SDL2_LIB, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_PollEvent(out SdlEvent e);

    // Quit SDL
    [DllImport(SDL2_LIB, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_Quit();
}
