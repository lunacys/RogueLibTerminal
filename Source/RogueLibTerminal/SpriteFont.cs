using System;
using System.IO;
using RogueLibTerminal.Logging;
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
            if (!File.Exists(filename))
                throw new FileNotFoundException();

            LogHelper.Log($"[RogueLibTerminal.SpriteFont]: Loading font '{filename}' with size {ptSize}");

            IntPtr fontPtr = TTF_OpenFont(filename, ptSize);

            if (fontPtr == IntPtr.Zero)
                throw new NullReferenceException($"Failed loading font '{filename}'");

            return new SpriteFont(fontPtr, ptSize);
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
