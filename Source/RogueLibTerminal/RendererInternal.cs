using System;
using RogueLibTerminal.Logging;
using static SDL2.SDL;

namespace RogueLibTerminal
{
    internal class RendererInternal : IDisposable
    {
        private IntPtr _renderer;
        public WindowInternal Window { get; }
        internal bool IsInitialized { get; private set; }

        public RendererInternal(WindowInternal window)
        {
            Window = window;
        }

        public void Initialize()
        {
            if (!Window.IsInitialized)
                throw new Exception("Please, initialize WindowInternal first");

            LogHelper.Target = LogTarget.Console;
            LogHelper.Log("[RogueLibTerminal.RendererInternal]: Initializing renderer..");

            IsInitialized = false;

            _renderer = SDL_CreateRenderer(Window.AsPointer(), -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            if (_renderer == null)
                throw new Exception($"Create renderer exception: {SDL_GetError()}");

            LogHelper.Log("[RogueLibTerminal.RendererInternal]: Renderer initialization done");

            IsInitialized = true;
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