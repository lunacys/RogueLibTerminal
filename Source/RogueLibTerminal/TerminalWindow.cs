using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using RogueLibTerminal.Input;
using RogueLibTerminal.Logging;
using SDL2;

using static SDL2.SDL;
using static SDL2.SDL_ttf;

namespace RogueLibTerminal
{
    public sealed class TerminalWindow : IDisposable
    {
        /*private IntPtr _windowPtr;
        private IntPtr _screenSurface;
        private IntPtr _renderer;*/
        //private IntPtr _terminalFont;

        private SDL_Rect _viewport;
        private SDL_Event _lastEvent;

        private Renderer _renderer;
        private Window _window;

        private bool _isRunning;

        public bool CapsLock { get; private set; }

        public int BufferWidth { get; set; }
        public int BufferHeight { get; set; }

        public int WindowWidth
        {
            get => _window.Width;
            set => _window.Width = value;
        }

        public int WindowHeight
        {
            get => _window.Height;
            set => _window.Height = value;
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
            get => _window.WindowSize;
            set => _window.WindowSize = value;
        }
        public bool CursorVisible { get; set; }
        public bool KeyAvailable { get; set; }
        
        public string Title
        {
            get => _window.Title;
            set => _window.Title = value;
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

        private SpriteFont _font;

        public TerminalWindow()
        {
            //RestoreDefaults();
            CloseOnEscape = true;
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
            _window = new Window("Window!", 800, 600);
            _window.Initialize();
            _renderer = new Renderer(_window);
            _renderer.Initialize();

            _font = SpriteFont.LoadFromFile(Path.Combine("Content", "FiraCode-Regular.ttf"), 16);

            _isRunning = true;

            while (_isRunning)
            {
                Update();
                Render();
            }
        }

        public void WriteLine(string value)
        {
            
        }

        /*private unsafe SDL_Surface GetSurface()
        {
            SDL_Surface surf = *(SDL.SDL_Surface*)SDL.SDL_GetWindowSurface(_windowPtr);
            return surf;
        }*/

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
            _renderer.Clear(TerminalColor.Black);

            _renderer.BeginDraw();
            _renderer.DrawString(_font, "Test String!\nNew line!\nAnother line!", new Point(16, 16), TerminalColor.Cyan);
            _renderer.EndDraw();
        }

        /*private void DrawGrid()
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
        }*/

        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
			/*TTF_CloseFont(_terminalFont);
			TTF_Quit();
            Console.WriteLine("Destroying renderer");
            SDL_DestroyRenderer(_renderer);
            Console.WriteLine("Destroying window");
            SDL.SDL_DestroyWindow(_windowPtr);
            Console.WriteLine("Quit");
            SDL.SDL_Quit();*/
        }

        public void ResetColors()
        {
            ForegroundColor = DefaultForegroundColor;
            BackgroundColor = DefaultBackgroundColor;
        }

        public void Dispose()
        {
            _font.Dispose();
            _renderer.Dispose();
            _window.Dispose();
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