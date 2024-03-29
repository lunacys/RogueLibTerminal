﻿using System;
using SDL2;

namespace RogueLibTerminal
{
    public struct TerminalColor : IEquatable<TerminalColor>
    {
        public static TerminalColor Black => new TerminalColor(0, 0, 0);
        public static TerminalColor White => new TerminalColor(255, 255, 255);
        public static TerminalColor Red => new TerminalColor(255, 0, 0);
        public static TerminalColor Green => new TerminalColor(0, 255, 0);
        public static TerminalColor Blue => new TerminalColor(0, 0, 255);
        public static TerminalColor DarkGray => new TerminalColor(100, 100, 100);
        public static TerminalColor Gray => new TerminalColor(200, 200, 200);
        public static TerminalColor Cyan => new TerminalColor(0, 255, 255);
        public static TerminalColor Magenta => new TerminalColor(255, 0, 255);
        public static TerminalColor Yellow => new TerminalColor(255, 255, 0);
        public static TerminalColor DarkCyan => new TerminalColor(0, 139, 139);
        public static TerminalColor DarkMagenta => new TerminalColor(139, 0, 139);
        public static TerminalColor DarkYellow => new TerminalColor(139, 139, 0);
        public static TerminalColor DarkRed => new TerminalColor(139, 0, 0);
        public static TerminalColor DarkGreen => new TerminalColor(0, 139, 0);
        public static TerminalColor DarkBlue => new TerminalColor(0, 0, 139);


        public byte R;
        public byte G;
        public byte B;
        public byte A;

        public TerminalColor(byte r, byte g, byte b, byte a = 255)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public TerminalColor(int r, int g, int b, int a = 255)
        {
            R = (byte) r;
            G = (byte) g;
            B = (byte) b;
            A = (byte) a;
        }

        public static implicit operator TerminalColor(SDL.SDL_Color value)
        {
            return new TerminalColor(value.r, value.g, value.b, value.a);
        }

        public static implicit operator SDL.SDL_Color(TerminalColor value)
        {
            return new SDL.SDL_Color
            {
                r = value.R,
                g = value.G,
                b = value.B,
                a = value.A
            };
        }

        public static implicit operator TerminalColor(ConsoleColor color)
        {
	        switch (color)
	        {
		        case ConsoleColor.Black:
			        return Black;
		        case ConsoleColor.Blue:
			        return Blue;
		        case ConsoleColor.Cyan:
			        return Cyan;
		        case ConsoleColor.DarkBlue:
			        return DarkBlue;
		        case ConsoleColor.DarkCyan:
			        return DarkCyan;
		        case ConsoleColor.DarkGray:
			        return DarkGray;
		        case ConsoleColor.DarkGreen:
			        return DarkGreen;
		        case ConsoleColor.DarkMagenta:
			        return DarkMagenta;
		        case ConsoleColor.DarkRed:
			        return DarkRed;
		        case ConsoleColor.DarkYellow:
			        return DarkYellow;
		        case ConsoleColor.Gray:
			        return Gray;
		        case ConsoleColor.Green:
			        return Green;
		        case ConsoleColor.Magenta:
			        return Magenta;
		        case ConsoleColor.Red:
			        return Red;
		        case ConsoleColor.White:
			        return White;
		        case ConsoleColor.Yellow:
			        return Yellow;
		        default:
			        throw new ArgumentOutOfRangeException(nameof(color), color, null);
	        }
        }

        public static implicit operator ConsoleColor(TerminalColor color)
        {
	        if (color.Equals(Black))
		        return ConsoleColor.Black;
	        if (color.Equals(White))
		        return ConsoleColor.White;
	        if (color.Equals(Red))
		        return ConsoleColor.Red;
	        if (color.Equals(Green))
		        return ConsoleColor.Green;
	        if (color.Equals(Blue))
		        return ConsoleColor.Blue;
	        if (color.Equals(DarkGray))
		        return ConsoleColor.DarkGray;
	        if (color.Equals(Gray))
		        return ConsoleColor.Gray;
	        if (color.Equals(Cyan))
		        return ConsoleColor.Cyan;
	        if (color.Equals(Magenta))
		        return ConsoleColor.Magenta;
	        if (color.Equals(Yellow))
		        return ConsoleColor.Yellow;
	        if (color.Equals(DarkCyan))
		        return ConsoleColor.DarkCyan;
	        if (color.Equals(DarkMagenta))
		        return ConsoleColor.DarkMagenta;
	        if (color.Equals(DarkYellow))
		        return ConsoleColor.DarkYellow;
	        if (color.Equals(DarkRed))
		        return ConsoleColor.DarkRed;
	        if (color.Equals(DarkGreen))
		        return ConsoleColor.DarkGreen;
	        if (color.Equals(DarkBlue))
		        return ConsoleColor.DarkBlue;

	        return ConsoleColor.White;
        }

        public bool Equals(TerminalColor other)
        {
            return R == other.R && G == other.G && B == other.B && A == other.A;
        }

        public override bool Equals(object obj)
        {
            return obj is TerminalColor other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = R.GetHashCode();
                hashCode = (hashCode * 397) ^ G.GetHashCode();
                hashCode = (hashCode * 397) ^ B.GetHashCode();
                hashCode = (hashCode * 397) ^ A.GetHashCode();
                return hashCode;
            }
        }
    }
}