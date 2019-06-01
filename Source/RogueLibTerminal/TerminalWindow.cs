using System;
using SDL2;

namespace RogueLibTerminal
{
    public class TerminalWindow : IDisposable
    {
        private IntPtr _windowPtr;
        private IntPtr _screenSurface;

        public TerminalWindow()
        {
            
        }

        public void Init()
        {
            if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) < 0)
                throw new Exception($"Init exception: {SDL.SDL_GetError()}");

            _windowPtr = SDL.SDL_CreateWindow("Title", 
                SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, 
                800, 600,
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            if (_windowPtr == null)
                throw new Exception($"Window creation exception: {SDL.SDL_GetError()}");

            _screenSurface = SDL.SDL_GetWindowSurface(_windowPtr);
            
            SDL.SDL_Rect rect = new SDL.SDL_Rect()
            {
                x = 100,
                y = 100,
                w = 100,
                h = 100
            };
            SDL.SDL_Surface v = new SDL.SDL_Surface();

            //SDL.SDL_FillRect(_screenSurface, ref rect, SDL.SDL_MapRGB(SDL.SDL_PIXELFORMAT_RGB888, 255, 255, 255));

            SDL.SDL_UpdateWindowSurface(_windowPtr);

            SDL.SDL_Delay(2000);
        }

        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
            SDL.SDL_DestroyWindow(_windowPtr);
            SDL.SDL_Quit();
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~TerminalWindow()
        {
            ReleaseUnmanagedResources();
        }
    }
}