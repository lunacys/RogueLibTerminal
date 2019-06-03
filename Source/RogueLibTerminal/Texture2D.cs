namespace RogueLibTerminal
{
    public class Texture2D
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Texture2D(int width, int height)
        {
            Width = width;
            Height = height;

            //SDL2.SDL_image.IMG_LoadTexture()
        }


    }
}