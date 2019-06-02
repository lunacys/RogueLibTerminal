using System;
using System.Net;
using SDL2;

namespace RogueLibTerminal
{
    public static class Terminal
    {
        internal static TerminalWindow TerminalInstance;

        public static int WindowWidth
        {
            get => TerminalInstance.WindowWidth;
            set => TerminalInstance.WindowWidth = value;
        }
        public static int WindowHeight
        {
            get => TerminalInstance.WindowHeight;
            set => TerminalInstance.WindowHeight = value;
        }
        public static int BufferWidth
        {
            get => TerminalInstance.BufferWidth;
            set => TerminalInstance.BufferWidth = value;
        }
        public static int BufferHeight
        {
            get => TerminalInstance.BufferHeight;
            set => TerminalInstance.BufferHeight = value;
        }
        public static TerminalColor ForegroundColor
        {
            get => TerminalInstance.ForegroundColor;
            set => TerminalInstance.ForegroundColor = value;
        }
        public static TerminalColor BackgroundColor
        {
            get => TerminalInstance.BackgroundColor;
            set => TerminalInstance.BackgroundColor = value;
        }
        public static TerminalColor DefaultForegroundColor
        {
            get => TerminalInstance.DefaultForegroundColor;
            set => TerminalInstance.DefaultForegroundColor = value;
        }
        public static TerminalColor DefaultBackgroundColor
        {
            get => TerminalInstance.DefaultBackgroundColor;
            set => TerminalInstance.DefaultBackgroundColor = value;
        }
        public static bool CapsLock => TerminalInstance.CapsLock;
        public static int CursorLeft
        {
            get => TerminalInstance.CursorLeft;
            set => TerminalInstance.CursorLeft = value;
        }
        public static int CursorTop
        {
            get => TerminalInstance.CursorTop;
            set => TerminalInstance.CursorTop = value;
        }
        public static Point CursorSize
        {
            get => TerminalInstance.CursorSize;
            set => TerminalInstance.CursorSize = value;
        }
        public static bool KeyAvailable => TerminalInstance.KeyAvailable;
        public static string Title
        {
            get => TerminalInstance.Title;
            set => TerminalInstance.Title = value;
        }
        public static Point BufferPosition
        {
            get => TerminalInstance.BufferPosition;
            set => TerminalInstance.BufferPosition = value;
        }
        public static Point BufferSize
        {
            get => TerminalInstance.BufferSize;
            set => TerminalInstance.BufferSize = value;
        }
        public static Point? WindowPosition
        {
            get => TerminalInstance.WindowPosition;
            set => TerminalInstance.WindowPosition = value;
        }

        static Terminal()
        {
            InitTerminalWindow();
        }

        private static void InitTerminalWindow()
        {
            TerminalInstance = new TerminalWindow();
            TerminalInstance.Init();
        }

        public static void Write(string value)
        {
            throw new NotImplementedException();
        }

        public static void WriteLine(string value)
        {
            throw new NotImplementedException();
        }

        public static void WriteAt(string value, Point position)
        {
            throw new NotImplementedException();
        }

        public static void WriteLineAt(string value, Point position)
        {
            throw new NotImplementedException();
        }

        public static void WriteEx(string value, Point offset)
        {
            throw new NotImplementedException();
        }

        public static void WriteLineEx(string value, Point offset)
        {
            throw new NotImplementedException();
        }

        public static void WriteAtEx(string value, Point position, Point offset)
        {
            throw new NotImplementedException();
        }

        public static void WriteLineAtEx(string value, Point position, Point offset)
        {
            throw new NotImplementedException();
        }

        public static void Refresh()
        {
            throw new NotImplementedException();
        }

        public static void RefreshAt(Point position)
        {
            RefreshAt(position, Point.Unit);
        }

        public static void RefreshAt(Point position, Point size)
        {
            throw new NotImplementedException();
        }

        public static string ReadLine()
        {
            throw new NotImplementedException();
        }

        public static int Read()
        {
            throw new NotImplementedException();
        }

        public static SDL.SDL_Keycode ReadKey()
        {
            throw new NotImplementedException();
        }

        public static void RestoreDefaultColors()
        {
            throw new NotImplementedException();
        }

        public static void Beep()
        {
            throw new NotImplementedException();
        }

        public static void PlaySound()
        {
            throw new NotImplementedException();
        }

        public static void Clear()
        {
            throw new NotImplementedException();
        }

        public static void ClearAt(Point position)
        {
            ClearAt(position, Point.Unit);
        }

        public static void ClearAt(Point position, Point size)
        {
            throw new NotImplementedException();
        }

        public static void Destroy()
        {
            TerminalInstance.Dispose();
        }
    }
}