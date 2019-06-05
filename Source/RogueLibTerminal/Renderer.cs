using System;
using System.Collections.Generic;
using RogueLibTerminal.Logging;
using static SDL2.SDL;
using static SDL2.SDL_ttf;

namespace RogueLibTerminal
{
    public sealed class Renderer : IDisposable
    {
        private IntPtr _renderer;
        internal Window Window { get; }
        public bool IsInitialized { get; private set; }

		private Dictionary<string, Texture2D> _cachedTextures = new Dictionary<string, Texture2D>();

        private bool _isBegunToDraw;

        internal Renderer(Window window)
        {
            Window = window;
        }

        internal void Initialize()
        {
            if (!Window.IsInitialized)
                throw new Exception("Please, initialize Window first");

            LogHelper.Target = LogTarget.Console;
            LogHelper.Log("[RogueLibTerminal.Renderer]: Initializing renderer..");

            IsInitialized = false;

            _renderer = SDL_CreateRenderer(Window.AsPointer(), -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            if (_renderer == null)
                throw new Exception($"Create renderer exception: {SDL_GetError()}");

            LogHelper.Log("[RogueLibTerminal.Renderer]: Renderer initialization done");

            IsInitialized = true;
        }

        public void BeginDraw()
        {
	        _isBegunToDraw = true;
        }

        public void DrawString(SpriteFont font, string str, Point position, TerminalColor color)
        {
			if (!_isBegunToDraw)
				throw new Exception("Please call BeginDraw() first");

			if (str.Contains("\r\n") || str.Contains("\n"))
			{
				DrawMultiLineString(font, str, position, color);
			}
			else
			{
				if (_cachedTextures.ContainsKey(str))
				{
					var t = _cachedTextures[str];

                    Draw(t.GetAsPointer(), position, t.Width, t.Height);
				}
				else
				{
					var surface = TTF_RenderText_Blended(font.GetAsPointer(), str, color);
					var texture = SurfaceToTexture(surface, out var w, out var h);

					_cachedTextures[str] = new Texture2D(w, h, texture);

					Draw(texture, position, w, h);
                }
			}
        }

        private void DrawMultiLineString(SpriteFont font, string lines, Point basePosition, TerminalColor color)
        {
	        var linesArray = lines.Split('\r', '\n');

	        for (int i = 0; i < linesArray.Length; i++)
	        {
				DrawString(font, linesArray[i], new Point(basePosition.X, basePosition.Y + i * font.PtSize), color);
		        /*if (_cachedTextures.ContainsKey(linesArray[i]))
		        {
			        var t = _cachedTextures[linesArray[i]];

			        Draw(t.GetAsPointer(), new Point(basePosition.X, basePosition.Y + i * t.Height), t.Width, t.Height);
                }
				else
                { }
		        var surface = TTF_RenderText_Blended(font.GetAsPointer(), linesArray[i], color);
		        var texture = SurfaceToTexture(surface, out var w, out var h);

				Draw(texture, new Point(basePosition.X, basePosition.Y + i * h), w, h);*/
            }
        }

        internal IntPtr SurfaceToTexture(IntPtr surface, out int w, out int h)
        {
	        var texture = SDL_CreateTextureFromSurface(_renderer, surface);
			SDL_FreeSurface(surface);
	        SDL_QueryTexture(texture, out _, out _, out w, out h);
	        return texture;
        }

        internal void Draw(IntPtr texture, Point position, int w, int h)
        {
			if (!_isBegunToDraw)
				throw new Exception("Please call BeginDraw() first");

			SDL_Rect dstRect = new SDL_Rect
			{
				x = position.X,
				y = position.Y,
				w = w,
				h = h
			};

			SDL_RenderCopy(_renderer, texture, IntPtr.Zero, ref dstRect);
        }

        public void EndDraw()
        {
			if (!_isBegunToDraw)
				throw new Exception("Tried to end drawing without calling BeginDraw()");

	        _isBegunToDraw = false;

			SDL_RenderPresent(_renderer);
        }

        private void ReleaseUnmanagedResources()
        {
	        // TODO release unmanaged resources here
	        foreach (var cachedTexture in _cachedTextures)
	        {
		        cachedTexture.Value.Dispose();
	        }

			_cachedTextures.Clear();

			SDL_DestroyRenderer(_renderer);
        }

        private void Dispose(bool disposing)
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

        ~Renderer()
        {
	        Dispose(false);
        }
    }
}