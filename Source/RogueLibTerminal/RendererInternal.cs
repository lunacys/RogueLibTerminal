using System;
using RogueLibTerminal.Logging;
using static SDL2.SDL;

namespace RogueLibTerminal
{
    internal class RendererInternal : IDisposable
    {
        private IntPtr _renderer;
        public Window Window { get; }
        internal bool IsInitialized { get; private set; }

        public RendererInternal(Window window)
        {
            Window = window;
        }

        

        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
            SDL_DestroyRenderer(_renderer);
        }

        protected virtual void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~RendererInternal()
        {
            Dispose(false);
        }
    }
}