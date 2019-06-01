using System;
using System.Diagnostics;
using SDL2;

using static SDL2.SDL;

namespace RogueLibTerminal
{
    public sealed class TerminalWindow : IDisposable
    {
        private IntPtr _windowPtr;
        private IntPtr _screenSurface;
        private IntPtr _renderer;

        private SDL_Rect _viewport;
        private SDL_Event _lastEvent;
        public SDL_Rect Viewport
        {
            get => _viewport;
            set => _viewport = value;
        }

        private bool _isRunning;

        public bool CapsLock { get; private set; }

        public int BufferWidth { get; set; }
        public int BufferHeight { get; set; }
        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }
        public TerminalColor ForegroundColor { get; set; }
        public TerminalColor BackgroundColor { get; set; }
        public TerminalColor DefaultForegroundColor { get; set; }
        public TerminalColor DefaultBackgroundColor { get; set; }
        public bool RestoreColorsAfterWrite { get; set; }
        public int CursorLeft { get; set; }
        public int CursorTop { get; set; }
        public Point CursorSize { get; set; }
        public bool CursorVisible { get; set; }
        public bool KeyAvailable { get; set; }
        public string Title { get; set; }
        public Point BufferPosition { get; set; }
        public Point BufferSize { get; set; }
        public Point? WindowPosition { get; set; }
        public bool IsResizable { get; set; }

        public event EventHandler KeyPress;
        public event EventHandler KeyDown;
        public event EventHandler KeyUp;
        public event EventHandler MouseButtonPress;
        public event EventHandler MouseButtonDown;
        public event EventHandler MouseButtonUp;

        public TerminalWindow()
        {
            RestoreDefaults();
        }

        public void RestoreDefaults()
        {
            BufferWidth = 60;
            BufferHeight = 20;
            CursorLeft = 0;
            CursorTop = 0;
            CursorVisible = false;
            CursorSize = Point.Zero;
            Title = Process.GetCurrentProcess().MainModule?.FileName;
            BufferPosition = Point.Zero;
            RestoreColorsAfterWrite = false;
            WindowWidth = 800;
            WindowHeight = 300;
            IsResizable = true;

            ForegroundColor = TerminalColor.White;
            BackgroundColor = TerminalColor.Black;
            DefaultForegroundColor = TerminalColor.White;
            DefaultBackgroundColor = TerminalColor.Black;
        }

        public void Init()
        {
            if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) < 0)
                throw new Exception($"Init exception: {SDL.SDL_GetError()}");

            _windowPtr = SDL.SDL_CreateWindow(Title,
                WindowPosition?.X ?? SDL_WINDOWPOS_CENTERED,
                WindowPosition?.Y ?? SDL_WINDOWPOS_CENTERED,
                WindowWidth,
                WindowHeight,
                IsResizable 
                    ? SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL_WindowFlags.SDL_WINDOW_RESIZABLE
                    : SDL_WindowFlags.SDL_WINDOW_SHOWN
                    );

            if (_windowPtr == null)
                throw new Exception($"Window creation exception: {SDL.SDL_GetError()}");

            _renderer = SDL_CreateRenderer(_windowPtr, -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            if (_renderer == null)
                throw new Exception($"Create renderer exception: {SDL_GetError()}");

            SDL_SetRenderDrawColor(_renderer, BackgroundColor.R, BackgroundColor.G, BackgroundColor.B, BackgroundColor.A);

            Viewport = new SDL_Rect
            {
                x = 0,
                y = 0,
                w = WindowWidth, // TODO: viewport must be buffer
                h = WindowHeight 
            };

            _screenSurface = SDL.SDL_GetWindowSurface(_windowPtr);

            _isRunning = true;

            while (_isRunning)
            {
                Update();
                Render();
            }
        }

        private unsafe SDL_Surface GetSurface()
        {
            SDL_Surface surf = *(SDL.SDL_Surface*)SDL.SDL_GetWindowSurface(_windowPtr);
            return surf;
        }

        private void Update()
        {
            SDL_PollEvent(out _lastEvent);

            if (_lastEvent.type == SDL_EventType.SDL_QUIT)
            {
                _isRunning = false;
            }
            else if (_lastEvent.type == SDL_EventType.SDL_KEYDOWN)
            {
                SDL_Keycode keycode = _lastEvent.key.keysym.sym;

                if (keycode == SDL_Keycode.SDLK_ESCAPE)
                {
                    _isRunning = false;
                }
            }
        }

        private void Render()
        {
            SDL_RenderClear(_renderer);
            SDL_RenderSetViewport(_renderer, ref _viewport);

            DrawGrid();
            //SDL_SetRenderDrawColor(_renderer, 255, 0, 0, 255);
            //SDL_RenderFillRect(_renderer, ref rect);
            //SDL_SetRenderDrawColor(_renderer, 127, 127, 127, 255);
            //SDL_FillRect(_renderer, ref rect, SDL.SDL_MapRGB(GetSurface().format, 255, 0, 0));

            SDL_RenderPresent(_renderer);
        }

        private void DrawGrid()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    SDL_Rect rect = new SDL_Rect()
                    {
                        x = i * 32,
                        y = j * 32,
                        w = 32,
                        h = 32
                    };
                    //Console.WriteLine($"{i * 10 + j}");
                    if ((i * 10 + j) % 2 == 0)
                        SDL_SetRenderDrawColor(_renderer, 255, 0, 0, 255);
                    else
                        SDL_SetRenderDrawColor(_renderer, 0, 0, 255, 255);

                    SDL_RenderFillRect(_renderer, ref rect);
                }
            }

            SDL_SetRenderDrawColor(_renderer, BackgroundColor.R, BackgroundColor.G, BackgroundColor.B, BackgroundColor.A);
        }

        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
            Console.WriteLine("Destroying renderer");
            SDL_DestroyRenderer(_renderer);
            Console.WriteLine("Destroying window");
            SDL.SDL_DestroyWindow(_windowPtr);
            Console.WriteLine("Quit");
            SDL.SDL_Quit();
        }

        public void ResetColors()
        {
            ForegroundColor = DefaultForegroundColor;
            BackgroundColor = DefaultBackgroundColor;
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose()");
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~TerminalWindow()
        {
            Console.WriteLine("Destructor");
            ReleaseUnmanagedResources();
        }
    }
}