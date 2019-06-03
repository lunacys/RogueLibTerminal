using System;
using System.Threading;
using RogueLibTerminal.Logging;
using static SDL2.SDL;

namespace RogueLibTerminal
{
    public class Window : IDisposable
    {
        private IntPtr _windowPtr;
        internal bool IsInitialized { get; private set; }

        private int _width;
        public int Width {
            get => !IsInitialized ? _width : WindowSize.X;
            set
            {
                if (IsInitialized)
                    WindowSize = new Point(value, Height);
                else
                    _width = value;
            }
        }
        private int _height;
        public int Height
        {
            get => !IsInitialized ? _height : WindowSize.Y;
            set
            {
                if (IsInitialized)
                    WindowSize = new Point(Width, value);
                else
                    _height = value;
            }
        }

        private string _title;
        public string Title
        {
            get => !IsInitialized ? _title : SDL_GetWindowTitle(_windowPtr);
            set
            {
                if (IsInitialized)
                    SDL_SetWindowTitle(_windowPtr, value);
                else
                    _title = value;
            }
        }

        private Point? _position;
        public Point? Position
        {
            get => GetWindowPosition();
            set => Move(value);
        }
        public Point WindowSize
        {
            get => GetWindowSize();
            set => SetWindowSize(value);
        }
        // TODO: Call SDL2 methods on this
        public bool IsResizable { get; set; }
        // TODO: Call SDL2 methods on this
        public bool IsMaximized { get; set; }

        public Window(string title = "Window", int width = 800, int height = 600)
        {
            Title = title;
            Width = width;
            Height = height;
            IsResizable = true;
            IsMaximized = false;
        }

        internal void Initialize()
        {
            LogHelper.Target = LogTarget.Console;

            LogHelper.Log("[RogueLibTerminal.Window]: Initializing SDL2..");

            IsInitialized = false;

            if (SDL_Init(SDL_INIT_EVERYTHING) < 0)
                throw new Exception($"Init exception: {SDL_GetError()}");

            LogHelper.Log("[RogueLibTerminal.Window]: Done initializing SDL");

            LogHelper.Log("[RogueLibTerminal.Window]: Creating a new window..");

            _windowPtr = SDL_CreateWindow(Title,
                Position?.X ?? SDL_WINDOWPOS_CENTERED,
                Position?.Y ?? SDL_WINDOWPOS_CENTERED,
                Width,
                Height,
                IsResizable
                    ? SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL_WindowFlags.SDL_WINDOW_RESIZABLE
                    : SDL_WindowFlags.SDL_WINDOW_SHOWN
            );

            if (_windowPtr == null)
                throw new Exception($"Window creation exception: {SDL_GetError()}");

            LogHelper.Log("[RogueLibTerminal.Window]: Done creating window");

            IsInitialized = true;
        }

        private Point GetWindowPosition()
        {
            if (IsInitialized)
            {
                SDL_GetWindowPosition(_windowPtr, out var x, out var y);
                return new Point(x, y);
            }

            return new Point();
        }

        private void Move(Point? position)
        {
            if (IsInitialized)
            {
                if (position.HasValue)
                    SDL_SetWindowPosition(_windowPtr, position.Value.X, position.Value.Y);
            }
            else
            {
                _position = position;
            }
        }

        private Point GetWindowSize()
        {
            int w, h;

            if (IsInitialized)
            {
                SDL_GetWindowSize(_windowPtr, out w, out h);
            }
            else
            {
                w = Width;
                h = Height;
            }

            return new Point(w, h);
        }

        private void SetWindowSize(Point size)
        {
            if (IsInitialized)
            {
                SDL_SetWindowSize(_windowPtr, size.X, size.Y);
            }
            else
            {
                Width = size.X;
                Height = size.Y;
            }
        }

        private void ReleaseUnmanagedResources()
        {
            // TODO: release unmanaged resources here
            LogHelper.Log("[RogueLibTerminal.Window]: Destroying SDL2 Window..");
            SDL_DestroyWindow(_windowPtr);
            LogHelper.Log("[RogueLibTerminal.Window]: Quitting..");
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

        ~Window()
        {
            Dispose(false);
        }

        internal IntPtr AsPointer()
        {
            return _windowPtr;
        }
    }
}