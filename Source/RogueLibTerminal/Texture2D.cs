using System;

namespace RogueLibTerminal
{
    public class Texture2D: IDisposable
    {
        public int Width { get; set; }
        public int Height { get; set; }

        private IntPtr _dataPtr;

        public Texture2D(int width, int height)
        {
            Width = width;
            Height = height;
        }

        internal Texture2D(int width, int height, IntPtr dataPtr)
        {
	        Width = width;
	        Height = height;
	        _dataPtr = dataPtr;
        }

        internal IntPtr GetAsPointer()
        {
	        return _dataPtr;
        }

        private void ReleaseUnmanagedResources()
        {
	        // TODO release unmanaged resources here
			SDL2.SDL.SDL_DestroyTexture(_dataPtr);
        }

        public void Dispose()
        {
	        ReleaseUnmanagedResources();
	        GC.SuppressFinalize(this);
        }

        ~Texture2D()
        {
	        ReleaseUnmanagedResources();
        }
    }
}