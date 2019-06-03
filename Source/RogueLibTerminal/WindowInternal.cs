using System;
using System.Threading;
using RogueLibTerminal.Logging;
using static SDL2.SDL;

namespace RogueLibTerminal
{
    internal class WindowInternal : IDisposable
    {
        private IntPtr _windowPtr;
        public bool IsInitialized { get; private set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public string Title { get; set; }
        public bool IsResizable { get; set; }

        public WindowInternal(string title = "WindowInternal", int width = 800, int height = 600)
        {
            Title = title;
            Width = width;
            Height = height;
            IsResizable = true;
        }

        public void Open()
        {
            Thread thread = new Thread(InitializeSdl);
            thread.Start();
            thread.Join();
        }

        private void InitializeSdl()
        {
            LogHelper.Target = LogTarget.Console;

            LogHelper.Log("[RogueLibTerminal.WindowInternal]: Initializing SDL2..");

            IsInitialized = false;

            if (SDL_Init(SDL_INIT_EVERYTHING) < 0)
                throw new Exception($"Init exception: {SDL_GetError()}");

            LogHelper.Log("[RogueLibTerminal.WindowInternal]: Done initializing SDL");

            LogHelper.Log("[RogueLibTerminal.WindowInternal]: Creating a new window..");

            _windowPtr = SDL_CreateWindow(Title,
                SDL_WINDOWPOS_CENTERED,
                SDL_WINDOWPOS_CENTERED,
                Width,
                Height,
                IsResizable
                    ? SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL_WindowFlags.SDL_WINDOW_RESIZABLE
                    : SDL_WindowFlags.SDL_WINDOW_SHOWN
            );

            if (_windowPtr == null)
                throw new Exception($"WindowInternal creation exception: {SDL_GetError()}");

            LogHelper.Log("[RogueLibTerminal.WindowInternal]: Done creating window");

            IsInitialized = true;
        }

        private void ReleaseUnmanagedResources()
        {
            // TODO: release unmanaged resources here
            LogHelper.Log("[RogueLibTerminal.WindowInternal]: Destroying SDL2 Window..");
            SDL_DestroyWindow(_windowPtr);
            LogHelper.Log("[RogueLibTerminal.WindowInternal]: Quitting..");
            SDL_Quit();
        }

        protected virtual void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                // TODO: Dispose IDisposable here
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~WindowInternal()
        {
            Dispose(false);
        }

        public IntPtr AsPointer()
        {
            return _windowPtr;
        }
    }
}