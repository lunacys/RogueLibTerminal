using SDL2;

namespace RogueLibTerminal
{
    internal static class MouseButtonMapper
    {
        public static MouseButton MapSdlMouseButton(SDL.SDL_MouseButtonEvent mouseButtonEvent)
        {
            switch (mouseButtonEvent.button)
            {
                case (byte)SDL.SDL_BUTTON_LEFT:
                    return MouseButton.Left;
                case (byte)SDL.SDL_BUTTON_MIDDLE:
                    return MouseButton.Middle;
                case (byte)SDL.SDL_BUTTON_RIGHT:
                    return MouseButton.Right;
                case (byte)SDL.SDL_BUTTON_X1:
                    return MouseButton.X1;
                case (byte)SDL.SDL_BUTTON_X2:
                    return MouseButton.X2;
                default:
                    return MouseButton.Unknown;
            }
        }
    }
}