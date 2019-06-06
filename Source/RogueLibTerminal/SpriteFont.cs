using System;
using static SDL2.SDL;
using static SDL2.SDL_ttf;

namespace RogueLibTerminal
{
	public sealed class SpriteFont : IDisposable
	{
		private IntPtr _fontPtr;
		public int PtSize { get; }

		internal SpriteFont(IntPtr fontPtr, int ptSize)
		{
			_fontPtr = fontPtr;
			PtSize = ptSize;
		}

		public static SpriteFont LoadFromFile(string filename, int ptSize)
		{
			return new SpriteFont(TTF_OpenFont(filename, ptSize), ptSize);
		}

		private void ReleaseUnmanagedResources()
		{
			// TODO release unmanaged resources here
			TTF_CloseFont(_fontPtr);
		}

		public void Dispose()
		{
			ReleaseUnmanagedResources();
			GC.SuppressFinalize(this);
		}

		internal IntPtr GetAsPointer()
		{
			return _fontPtr;
		}

		~SpriteFont()
		{
			ReleaseUnmanagedResources();
		}
	}
}
