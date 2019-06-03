using System;
using RogueLibTerminal.Logging;
using static SDL2.SDL;

namespace RogueLibTerminal
{
    public class Renderer
    {
        private IntPtr _renderer;
        internal Window Window { get; }
        public bool IsInitialized { get; private set; }

        internal Renderer(Window window)
        {
            Window = window;
        }

        internal void Initialize()
        {
            if (!Window.IsInitialized)
                throw new Exception("Please, initialize Window first");

            LogHelper.Target = LogTarget.Console;
            LogHelper.Log("[RogueLibTerminal.RendererInternal]: Initializing renderer..");

            IsInitialized = false;

            _renderer = SDL_CreateRenderer(Window.AsPointer(), -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            if (_renderer == null)
                throw new Exception($"Create renderer exception: {SDL_GetError()}");

            LogHelper.Log("[RogueLibTerminal.RendererInternal]: Renderer initialization done");

            IsInitialized = true;
        }
    }
}