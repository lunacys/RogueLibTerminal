using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SDL2;

using static SDL2.SDL;
using static SDL2.SDL_ttf;

namespace RogueLibTerminal
{
    public sealed class TerminalWindow : IDisposable
    {
        private IntPtr _windowPtr;
        private IntPtr _screenSurface;
        private IntPtr _renderer;
        private IntPtr _terminalFont;

        private SDL_Rect _viewport;
        private SDL_Event _lastEvent;

        private bool _isRunning;

        public bool CapsLock { get; private set; }

        public int BufferWidth { get; set; }
        public int BufferHeight { get; set; }
        private int _windowWidth;
        public int WindowWidth
        {
            get => !_isInitialized ? _windowWidth : WindowSize.X;
            set
            {
                if (_isInitialized)
                    WindowSize = new Point(value, WindowHeight);
                else
                    _windowWidth = value;
            }
        }

        private int _windowHeight;
        private bool _isInitialized;
        public int WindowHeight
        {
            get => !_isInitialized ? _windowHeight : WindowSize.Y;
            set
            {
                if (_isInitialized)
                    WindowSize = new Point(WindowWidth, value);
                else
                    _windowHeight = value;
            }
        }
        public TerminalColor ForegroundColor { get; set; }
        public TerminalColor BackgroundColor { get; set; }
        public TerminalColor DefaultForegroundColor { get; set; }
        public TerminalColor DefaultBackgroundColor { get; set; }
        public bool RestoreColorsAfterWrite { get; set; }
        public int CursorLeft { get; set; }
        public int CursorTop { get; set; }
        public Point CursorSize { get; set; }
        public Point WindowSize
        {
            get => GetWindowSize();
            set => SDL_SetWindowSize(_windowPtr, value.X, value.Y);
        }
        public bool CursorVisible { get; set; }
        public bool KeyAvailable { get; set; }
        private string _title;
        public string Title
        {
            get => !_isInitialized ? _title : SDL_GetWindowTitle(_windowPtr);
            set
            {
                if (_isInitialized)
                    SDL_SetWindowTitle(_windowPtr, value);
                else
                    _title = value;
            }
        }

        public Point BufferPosition { get; set; }
        public Point BufferSize { get; set; }
        public Point? WindowPosition { get; set; }
        public bool IsResizable { get; set; }
        public bool CloseOnEscape { get; set; }

        public event EventHandler<OnKeyEventEventHandler> KeyPress;
        public event EventHandler<OnKeyEventEventHandler> KeyDown;
        public event EventHandler<OnKeyEventEventHandler> KeyUp;
        public event EventHandler<OnMouseEventEventHandler> MouseButtonPress;
        public event EventHandler<OnMouseEventEventHandler> MouseButtonDown;
        public event EventHandler<OnMouseEventEventHandler> MouseButtonUp;

		

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
            WindowWidth = 976;
            WindowHeight = 418;
            IsResizable = true;
            CloseOnEscape = true;

            ForegroundColor = Console.ForegroundColor;
            BackgroundColor = Console.BackgroundColor;
            DefaultForegroundColor = Console.ForegroundColor;
            DefaultBackgroundColor = Console.BackgroundColor;
        }

        public void Init()
        {
            Console.WriteLine("Initializing SDL");
            _isInitialized = false;

            if (SDL_Init(SDL.SDL_INIT_EVERYTHING) < 0)
                throw new Exception($"Init exception: {SDL.SDL_GetError()}");

            Console.WriteLine("Done initializing SDL");

			if (TTF_Init() < 0)
				throw new Exception($"TTF Init Exception: {SDL_GetError()}");

            Console.WriteLine("Creating a new SDL window");

            _windowPtr = SDL_CreateWindow(Title,
                WindowPosition?.X ?? SDL_WINDOWPOS_CENTERED,
                WindowPosition?.Y ?? SDL_WINDOWPOS_CENTERED,
                WindowWidth,
                WindowHeight,
                IsResizable 
                    ? SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL_WindowFlags.SDL_WINDOW_RESIZABLE
                    : SDL_WindowFlags.SDL_WINDOW_SHOWN
                    );
            
            if (_windowPtr == null)
                throw new Exception($"Window creation exception: {SDL.SDL_GetError()}");

            Console.WriteLine("Successfully created a new SDL window");

            Console.WriteLine("Creating global renderer");

            _renderer = SDL_CreateRenderer(_windowPtr, -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            if (_renderer == null)
                throw new Exception($"Create renderer exception: {SDL_GetError()}");

            Console.WriteLine("Done creating global renderer");

            SDL_SetRenderDrawColor(_renderer, BackgroundColor.R, BackgroundColor.G, BackgroundColor.B, BackgroundColor.A);

            _viewport = new SDL_Rect
            {
                x = 0,
                y = 0,
                w = WindowWidth, // TODO: viewport must be buffer
                h = WindowHeight 
            };

            _screenSurface = SDL_GetWindowSurface(_windowPtr);
			
            _terminalFont = TTF_OpenFont(Path.Combine("Content", "FiraCode-Regular.ttf"), 14);
			
            _isRunning = true;

            _isInitialized = true;
            Console.WriteLine("Initialization done, running main cycle");

            while (_isRunning)
            {
                Update();
                Render();
            }
        }

        private Point GetWindowSize()
        {
	        int w, h;

			if (_isInitialized)
				SDL_GetWindowSize(_windowPtr, out w, out h);
			else
			{
				w = WindowWidth;
				h = WindowHeight;
			}

            return new Point(w, h);
        }

        private unsafe SDL_Surface GetSurface()
        {
            SDL_Surface surf = *(SDL.SDL_Surface*)SDL.SDL_GetWindowSurface(_windowPtr);
            return surf;
        }

        private void Update()
        {
            SDL_PollEvent(out _lastEvent);
            //int numkeys;
            //var state = SDL_GetKeyboardState(out numkeys);
            
            if (_lastEvent.type == SDL_EventType.SDL_QUIT)
            {
                _isRunning = false;
            }
            else if (_lastEvent.type == SDL_EventType.SDL_KEYDOWN)
            {
                SDL_Keycode keycode = _lastEvent.key.keysym.sym;

                if (keycode == SDL_Keycode.SDLK_ESCAPE)
                {
                    if (CloseOnEscape)
                        _isRunning = false;
                }
				
                KeyDown?.Invoke(this, new OnKeyEventEventHandler(KeyMapper.MapSdlKey(keycode), KeyModifierMapper.MapSdlMods(_lastEvent.key.keysym.mod)));
            }
            else if (_lastEvent.type == SDL_EventType.SDL_KEYUP)
            {
                SDL_Keycode keycode = _lastEvent.key.keysym.sym;

                KeyUp?.Invoke(this, new OnKeyEventEventHandler(KeyMapper.MapSdlKey(keycode), KeyModifierMapper.MapSdlMods(_lastEvent.key.keysym.mod)));
            }
            else if (_lastEvent.type == SDL_EventType.SDL_MOUSEBUTTONDOWN)
            {
                MouseButtonDown?.Invoke(this, new OnMouseEventEventHandler());
            }
            else if (_lastEvent.type == SDL_EventType.SDL_MOUSEBUTTONUP)
            {
                MouseButtonUp?.Invoke(this, new OnMouseEventEventHandler());
            }
            else if (_lastEvent.type == SDL_EventType.SDL_WINDOWEVENT)
            {
                if (_lastEvent.window.windowEvent == SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED) {
                    Console.WriteLine("Resize window");
                    ResizeWindow(_lastEvent.window.data1, _lastEvent.window.data2);
                }
            }
        }

        private void ResizeWindow(int w, int h)
        {
            _viewport.w = w;
            _viewport.h = h;

            Console.WriteLine($"New size: {w}, {h}");
        }

        private void Render()
        {
            SDL_RenderClear(_renderer);
            SDL_RenderSetViewport(_renderer, ref _viewport);

            //DrawGrid();
            //SDL_SetRenderDrawColor(_renderer, 255, 0, 0, 255);
            //SDL_RenderFillRect(_renderer, ref rect);
            //SDL_SetRenderDrawColor(_renderer, 127, 127, 127, 255);
            //SDL_FillRect(_renderer, ref rect, SDL.SDL_MapRGB(GetSurface().format, 255, 0, 0));
            IntPtr surface = TTF_RenderText_Shaded(_terminalFont, "Test!!!!!! => <= !=\nNew line\nAnother line\nWew", ForegroundColor, BackgroundColor);
			
            IntPtr texture = SDL_CreateTextureFromSurface(_renderer, surface);
            int texW = 0;
            int texH = 0;
            uint format;
	        int access;
            SDL_QueryTexture(texture, out format, out access, out texW, out texH);
            SDL_Rect srcRect = new SDL_Rect
            {
	            x = 0,
	            y = 0,
	            w = 128,
	            h = 64
            };
            SDL_Rect dstRect = new SDL_Rect
            {
	            x = 2,
	            y = 2,
	            w = texW,
	            h = texH
            };
            SDL_RenderCopy(_renderer, texture, IntPtr.Zero, ref dstRect);
			SDL_DestroyTexture(texture);
			SDL_FreeSurface(surface);

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
			TTF_CloseFont(_terminalFont);
			TTF_Quit();
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